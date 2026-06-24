using System;
using System.Windows;
using Business;

namespace UserInterface
{
    public partial class PersonWindow : Window
    {
        private PersonService _service;

        public PersonWindow()
        {
            InitializeComponent();
            _service = new PersonService();
            RefreshList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtName.Text;
                int age = int.Parse(txtAge.Text);

                _service.AddPerson(name, age);

                txtName.Clear();
                txtAge.Clear();
                txtName.Focus();

                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshList()
        {
            var people = _service.GetPeople();

            lstPeople.Items.Clear();
            foreach (var person in people)
            {
                lstPeople.Items.Add(person.ToString());
            }

            txtCounter.Text = $"Total: {_service.CountPeople()} people";
        }
    }
}