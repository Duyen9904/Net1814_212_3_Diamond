using System.Windows;
using System.Windows.Controls;
using Diamond.Business;
using Diamond.Data.Models;

namespace Diamond.WpfApp.UI.CustomerUI
{
    /// <summary>
    /// Interaction logic for wProductCategorySearch.xaml
    /// </summary>
    public partial class wCustomerSearch : Window
	{
		private readonly CustomerBusiness _business;
		public wCustomerSearch()
		{
			InitializeComponent();
            _business = new CustomerBusiness();
			this.LoadGrdCustomer();
		}

		private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				var customer = new Customer()
				{
					CustomerId = CustomerId.Text,
					Email = Email.Text,
					FirstName = FirstName.Text,
					LastName = LastName.Text,
					Address = Address.Text,
					PhoneNumber = PhoneNumber.Text,
					DateOfBirth = DateTime.Parse(DateOfBirth.Text),
                    IsActive = IsActive.IsChecked == true,
                    Country = Country.Text,
					Gender = Gender.Text
				};


				//var result = await _business.SearchByFields(categoryId, name, description, iconUrl, promotionImageUrl, promotionalTagline, careInstructions, maximumPrice, minimumPrice);
				var result = await _business.SearchByFields(customer);
				MessageBox.Show(result.Message, "Save");

				this.LoadGrdCustomer(result.Data as List<Customer>);

				// Clear all the text boxes
				CustomerId.Text = string.Empty;
				Email.Text = string.Empty;
				FirstName.Text = string.Empty;
				LastName.Text = string.Empty;
				Address.Text = string.Empty;
				PhoneNumber.Text = string.Empty;
				DateOfBirth.Text = string.Empty;
				IsActive.IsChecked = false;
				Country.Text = string.Empty;
				Gender.Text = string.Empty;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
			}
		}

		private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var categoryId = button.CommandParameter.ToString();

			if (!string.IsNullOrEmpty(categoryId))
			{
				var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					try
					{
						var deleteResult = await _business.DeleteById(categoryId);
						MessageBox.Show(deleteResult.Message, "Delete");

						// Refresh the DataGrid
						this.LoadGrdCustomer();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "Error");
					}
				}
			}
		}

		private async void grdCustomer_ButtonView_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var customerId = button.CommandParameter.ToString();
			if (string.IsNullOrEmpty(customerId))
			{
				var item = await _business.GetById(customerId);
			}
		}


		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
		{
			// Lấy thông tin từ các TextBox
			Customer updatedCustomer = _business.GetById(CustomerId.Text).Result.Data as Customer;
			updatedCustomer.Address = Address.Text;
			try
			{
				// Gọi phương thức Update trong lớp business để cập nhật dữ liệu
				var result = await _business.Update(updatedCustomer);
				MessageBox.Show(result.Message, "Update");

				// Làm mới DataGrid để hiển thị dữ liệu mới
				LoadGrdCustomer();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
			}
		}

		private void grdCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Lấy dòng được chọn trong DataGrid
			if (grdCustomer.SelectedItem != null)
			{
				Customer selectedCustomer = (Customer)grdCustomer.SelectedItem;

				// Điền thông tin của dòng được chọn vào các TextBox
				CustomerId.Text = selectedCustomer.CustomerId;
				Email.Text = selectedCustomer.Email;
				FirstName.Text = selectedCustomer.FirstName;
				LastName.Text = selectedCustomer.LastName;
				Address.Text = selectedCustomer.Address;
				PhoneNumber.Text = selectedCustomer.PhoneNumber;
				DateOfBirth.Text = selectedCustomer.DateOfBirth.ToString();
				IsActive.IsChecked = selectedCustomer.IsActive;
				Country.Text = selectedCustomer.Country;
				Gender.Text = selectedCustomer.Gender;
			}
		}

		private async void grdCustomer_ButtonReport_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var customerId = button.CommandParameter.ToString();

			if (!string.IsNullOrEmpty(customerId))
			{
				var report = new wCustomerReport(customerId);
				report.Owner = this;
				report.Show();
			}
		}

		private async void grdCustomer_MouseDouble_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Double Click on Grid");
			DataGrid grd = sender as DataGrid;
			if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
			{
				var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
				if (row != null)
				{
					var item = row.Item as Customer;
					if (item != null)
					{
						var customerResult = await _business.GetById(item.CustomerId);

						if (customerResult.Status > 0 && customerResult.Data != null)
						{
							item = customerResult.Data as Customer;
							CustomerId.Text = item.CustomerId;
							Email.Text = item.Email;
							FirstName.Text = item.FirstName;
							LastName.Text = item.LastName;
							Address.Text = item.Address;
							PhoneNumber.Text = item.PhoneNumber;
							DateOfBirth.Text = item.DateOfBirth.ToString();
							Gender.Text = item.Gender;
							Country.Text = item.Country;
							IsActive.IsChecked = item.IsActive;
						}
					}
				}
			}
		}

		private async void LoadGrdCustomer()
		{
			var result = await _business.GetAll();

			if (result.Status > 0 && result.Data != null)
			{
				grdCustomer.ItemsSource = result.Data as List<Customer>;
			}
			else
			{
                grdCustomer.ItemsSource = new List<Customer>();
			}
		}

		private async void LoadGrdCustomer(List<Customer> list)
		{
			if (list.Count > 0)
			{
                grdCustomer.ItemsSource = list;
			}
			else
			{
                grdCustomer.ItemsSource = new List<Customer>();
			}
		}
	}
}
