using System;
using System.Linq;
using System.Windows;
using Business;

namespace UserInterface
{

    public partial class InvoiceWindow : Window
    {
        private InvoiceService _service;

        public InvoiceWindow()
        {
            InitializeComponent();
            _service = new InvoiceService();

            // Set current date on DatePicker
            dpIssueDate.SelectedDate = DateTime.Now;

            LoadCreditors();
            RefreshList();
        }

        private void LoadCreditors()
        {
            var creditors = _service.GetAllCreditors();
            cmbCreditors.ItemsSource = creditors;

            if (creditors.Any())
                cmbCreditors.SelectedIndex = 0;
        }

        private void BtnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string number = txtInvoiceNumber.Text.Trim();
                DateTime issueDate = dpIssueDate.SelectedDate ?? DateTime.Now;
                decimal value = decimal.Parse(txtValue.Text);
                string description = txtDescription.Text.Trim();
                int creditorId = (int)cmbCreditors.SelectedValue;

                _service.AddInvoice(number, issueDate, value, description, creditorId);

                txtInvoiceNumber.Clear();
                txtValue.Clear();
                txtDescription.Clear();
                dpIssueDate.SelectedDate = DateTime.Now;
                txtInvoiceNumber.Focus();

                RefreshList();
                LoadCreditors(); //Refresh creditors list (case changed)
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid value (use comma for decimals).", "Format Error",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Attention",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RefreshList()
        {
            var invoices = _service.GetAllInvoices();

            lstInvoices.Items.Clear();
            foreach (var invoice in invoices)
            {
                // Add full object
                lstInvoices.Items.Add(invoice);
            }

            txtCounter.Text = $"Total: {_service.CountInvoices()} invoice(s)";
        }
    }
}
