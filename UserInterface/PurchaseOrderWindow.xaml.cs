using System;
using System.Linq;
using System.Windows;
using Business;

namespace UserInterface
{

    public partial class PurchaseOrderWindow : Window
    {
        private PurchaseOrderService _service;

        public PurchaseOrderWindow()
        {
            InitializeComponent();
            _service = new PurchaseOrderService();

            // Set current date on DatePicker
            dpAuthorizationDate.SelectedDate = DateTime.Now;

            LoadInvoices();
            RefreshList();
        }

        private void LoadInvoices()
        {
            var invoices = _service.GetAllInvoices();
            cmbInvoices.ItemsSource = invoices;

            if (invoices.Any())
                cmbInvoices.SelectedIndex = 0;
        }

        private void BtnAddPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string number = txtOrderNumber.Text.Trim();
                DateTime authorizationDate = dpAuthorizationDate.SelectedDate ?? DateTime.Now;
                decimal value = decimal.Parse(txtValue.Text);
                string description = txtDescription.Text.Trim();
                int invoiceId = (int)cmbInvoices.SelectedValue;

                _service.AddPurchaseOrder(number, authorizationDate, value, description, invoiceId);

                txtOrderNumber.Clear();
                txtValue.Clear();
                txtDescription.Clear();
                dpAuthorizationDate.SelectedDate = DateTime.Now;
                txtOrderNumber.Focus();

                RefreshList();
                LoadInvoices();
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
            var pOrders = _service.GetAllPurchaseOrders();

            lstPurchaseOrders.Items.Clear();
            foreach (var pOrder in pOrders)
            {
                //Add full object
                lstPurchaseOrders.Items.Add(pOrder);
            }

            txtCounter.Text = $"Total: {_service.CountPurchaseOrders()} order(s)";
        }
    }
}
