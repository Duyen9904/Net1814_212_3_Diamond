using System.Windows;
using Diamond.Business;
using Diamond.Data.Models;

namespace Diamond.WpfApp.UI.OrderDetailUI
{
    /// <summary>
    /// Interaction logic for wProductCategoryReport.xaml
    /// </summary>
    public partial class wOrderDetailReport : Window
    {
        private OrderDetailBusiness _business;
        public wOrderDetailReport(string orderDetailID)
        {
            InitializeComponent();
			_business = new OrderDetailBusiness();
			this.LoadGrdOrderDetailReport(orderDetailID);

		}

        private async void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
			this.Close();
		}

		private async void LoadGrdOrderDetailReport(string orderDetailID)
		{
			var result = await _business.GetById(orderDetailID);
            if (result.Data != null)
            {
                var item = result.Data as Orderdetail;
                OrderDetailId.Text = item.OrderDetailId.ToString();
                OrderId.Text = item.OrderId.ToString();
                MainDiamondId.Text = item.MainDiamondId.ToString();
                ShellId.Text = item.ShellId.ToString();
                SubDiamondId.Text = item.SubDiamondId.ToString();
                LineTotal.Text = item.LineTotal.ToString();
                Quantity.Text = item.Quantity.ToString();
                UnitWeight.Text = item.UnitWeight.ToString();
                UnitPrice.Text = item.UnitPrice.ToString();
                DiscountPercentage.Text = item.DiscountPercentage.ToString();
                Note.Text = item.Note?.ToString() ?? string.Empty;


            }
        }
	}
}
