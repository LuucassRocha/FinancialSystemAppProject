using System.Windows;

namespace UserInterface
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPeople_Click(object sender, RoutedEventArgs e)
        {
            var window = new PersonWindow();
            window.ShowDialog();
        }

        private void OpenCreditors_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreditorWindow();
            window.ShowDialog();
        }

        private void OpenInvoices_Click(object sender, RoutedEventArgs e)
        {
            var window = new InvoiceWindow();
            window.ShowDialog();
        }

        private void OpenPurchaseOrders_Click(object sender, RoutedEventArgs e)
        {
            var window = new PurchaseOrderWindow();
            window.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
