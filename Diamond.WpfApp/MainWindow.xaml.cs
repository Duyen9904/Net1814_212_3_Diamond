using Diamond.WpfApp.UI.OrderDetailUI;
using DiamondShop.WpfApp.UI.CustomerUI;
using System.Windows;


namespace Diamond.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_wOrderDetail_Click(object sender, RoutedEventArgs e)
        {
            var p = new wOrderDetail();
            p.Owner = this;
            p.Show();
        }
        private void Open_wCustomer_Click(object sender, RoutedEventArgs e)
        {
            var p = new wCustomer();
            p.Owner = this;
            p.Show();
        }
    }
}