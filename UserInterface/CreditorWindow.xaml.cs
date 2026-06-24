using Business;
using System;
using System.Windows;

namespace UserInterface
{
    public partial class CreditorWindow : Window
    {
        private CreditorService _service;

        public CreditorWindow()
        {
            InitializeComponent();
            _service = new CreditorService();
            RefreshList();
        }

        private void BtnAddCred_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = txtCredName.Text;
                string document = txtCredDocument.Text;
                string email = txtCredEmail.Text;
                string phone = txtCredPhone.Text;
                string address = txtCredAddress.Text;

                _service.AddCreditor(name, document, email, phone, address);

                txtCredName.Clear();
                txtCredDocument.Clear();
                txtCredEmail.Clear();
                txtCredPhone.Clear();
                txtCredAddress.Clear();
                txtCredName.Focus();

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
            var creditors = _service.GetCreditors();

            lstCreditors.Items.Clear();
            foreach (var creditor in creditors)
            {
                lstCreditors.Items.Add(creditor.ToString());
            }

            txtCounter.Text = $"Total: {_service.CountCreditors()} creditors";
        }
    }
}
