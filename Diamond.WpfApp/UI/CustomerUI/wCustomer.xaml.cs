using Diamond.Business;
using Diamond.Data.Models;
using Diamond.WpfApp.UI.CustomerUI;
using System.Diagnostics.Metrics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace DiamondShop.WpfApp.UI.CustomerUI
{
    /// <summary>
    /// Interaction logic for wCustomer.xaml
    /// </summary>
    public partial class wCustomer : Window
    {
        private readonly CustomerBusiness _business;

        public wCustomer()
        {
            InitializeComponent();
            _business = new CustomerBusiness();
            this.LoadGrdCustomer();
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CustomerId.Text.Trim()))
                {
                    MessageBox.Show("Can not save because customer ID is empty.");
                    return;
                };
                var item = await _business.GetById(CustomerId.Text);

                if (item.Data == null)
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
                        Gender = Gender.Text,
                        IsActive = IsActive.IsChecked == true ? true : false,
                        Country = Country.Text
                    };

                    var result = await _business.Save(customer);
                    MessageBox.Show(result.Message, "Save");
                    LoadGrdCustomer();


                }
                else // cai tien: ton tai thi update
                {
                    //ButtonUpdate_Click(sender, e);
                    // Lấy thông tin từ các TextBox
                    Customer updatedCustomer = item.Data as Customer;
                   updatedCustomer.Email = Email.Text;
                    updatedCustomer.FirstName = FirstName.Text;
                    updatedCustomer.LastName = LastName.Text;
                    updatedCustomer.Address = Address.Text;
                    updatedCustomer.PhoneNumber = PhoneNumber.Text;
                    updatedCustomer.DateOfBirth = DateTime.Parse(DateOfBirth.Text);
                    updatedCustomer.Gender = Gender.Text;
                    updatedCustomer.IsActive = IsActive.IsChecked == true ? true : false;
                    updatedCustomer.Country = Country.Text;

                    // Gọi phương thức Update trong lớp business để cập nhật dữ liệu
                    var result = await _business.Update(updatedCustomer);
                    MessageBox.Show(result.Message, "Update");



                    // Clear all the text boxes
                    CustomerId.Text = "";
                    Email.Text = "";
                    FirstName.Text = "";
                    LastName.Text = "";
                    Address.Text = "";
                    PhoneNumber.Text = "";
                    DateOfBirth.Text = "";
                    Gender.Text = Gender.Text;
                    IsActive.IsChecked = false;
                    Country.Text = "";

                    this.LoadGrdCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void grdCustomer_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var customerId = button.CommandParameter.ToString();
            if (customerId == null)
            {
                MessageBox.Show("Can not delete because customer ID is empty.");
                return;
            }
            var customer = await _business.GetById(customerId);

            if (!string.IsNullOrEmpty(customerId))
            {
                var result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        Customer updatedCustomer = customer.Data as Customer;
                        updatedCustomer.IsActive = false;
                        var deleteResult = await _business.Update(updatedCustomer);
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

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerId.Text) &&
                string.IsNullOrEmpty(Email.Text) &&
                string.IsNullOrEmpty(FirstName.Text) &&
                string.IsNullOrEmpty(LastName.Text) &&
                string.IsNullOrEmpty(Address.Text) &&
                string.IsNullOrEmpty(PhoneNumber.Text) &&
                string.IsNullOrEmpty(DateOfBirth.Text) &&
                string.IsNullOrEmpty(Gender.Text) &&
                IsActive.IsChecked == false &&
                string.IsNullOrEmpty(Country.Text))
            {
                MessageBox.Show("Nothing to clear", "Info");
                return;
            }

            CustomerId.Text = "";
            Email.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Address.Text = "";
            PhoneNumber.Text = "";
            DateOfBirth.Text = "";
            Gender.Text = "";
            IsActive.IsChecked = false;
            Country.Text = "";

            LoadGrdCustomer();
        }


        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            Customer updatedCustomer = _business.GetById(CustomerId.Text).Result.Data as Customer;
            updatedCustomer.Email = Email.Text;
            try
            {
                var result = await _business.Update(updatedCustomer);
                MessageBox.Show(result.Message, "Update");
                LoadGrdCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void grdCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdCustomer.SelectedItem != null)
            {
                Customer selectedCustomer = (Customer)grdCustomer.SelectedItem;
                CustomerId.Text = selectedCustomer.CustomerId;
                Email.Text = selectedCustomer.Email;
                FirstName.Text = selectedCustomer.FirstName;
                LastName.Text = selectedCustomer.LastName;
                Address.Text = selectedCustomer.Address;
                PhoneNumber.Text = selectedCustomer.PhoneNumber;
                DateOfBirth.Text = selectedCustomer.DateOfBirth
                    .ToString();
                Gender.Text = selectedCustomer.Gender;
                IsActive.IsChecked = selectedCustomer.IsActive;
                Country.Text = selectedCustomer.Country;
            }
        }

        private async void grdCustomer_ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var customerID = button.CommandParameter.ToString();

            if (!string.IsNullOrEmpty(customerID))
            {
                var report = new wCustomerReport(customerID);
                report.Owner = this;
                report.Show();
            }
        }

        private async void grdCustomer_ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            var customerSearchWindow = Application.Current.Windows
        .OfType<wCustomerSearch>()
        .FirstOrDefault();

    if (customerSearchWindow == null)
    {
        customerSearchWindow = new wCustomerSearch();
        customerSearchWindow.Show();
    }
    else
    {
        customerSearchWindow.Activate();
    }

    this.Close(); // Close the current window
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
                            IsActive.IsChecked = item.IsActive;
                            Country.Text = item.Country;
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
        private void grdCustomer_ButtonView_Click(object sender, EventArgs e)
        {
            // Your code here
        }
    }
}
