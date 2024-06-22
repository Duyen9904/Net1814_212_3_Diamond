using System.Windows;
using Diamond.Business;
using Diamond.Data.Models;

namespace Diamond.WpfApp.UI.CustomerUI
{
	/// <summary>
	/// Interaction logic for wProductCategoryReport.xaml
	/// </summary>
	public partial class wCustomerReport : Window
	{
		private CustomerBusiness _business;
		public wCustomerReport(string categoryId)
		{
			InitializeComponent();
            _business = new CustomerBusiness();
			this.LoadGrdCategoryReport(categoryId);

		}

		private async void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private async void LoadGrdCategoryReport(string customerId)
		{
			var result = await _business.GetById(customerId);
			if (result.Data != null)
			{
				var item = result.Data as Customer;
				CustomerId.Text = item.CustomerId.ToString();
				Email.Text = item.Email;
				FirstName.Text = item.FirstName;
				LastName.Text = item.LastName;
				Address.Text = item.Address;
				PhoneNumber.Text = item.PhoneNumber;
				DateOfBirth.Text = item.DateOfBirth
					.ToString();
				Gender.Text = item.Gender;
				IsActive.Text = (bool)item.IsActive ? "Yes" : "No";
				Country.Text = item.Country;
			}
		}
	}
}