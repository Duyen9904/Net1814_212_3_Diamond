using System.Windows;
using System.Windows.Controls;
using Diamond.Business;
using Diamond.Data.Models;

namespace Diamond.WpfApp.UI.OrderDetailUI
{
    /// <summary>
    /// Interaction logic for wProductCategorySearch.xaml
    /// </summary>
    public partial class wOrderDetailSearch : Window
	{
		private OrderDetailBusiness _business;
		public wOrderDetailSearch()
		{
			InitializeComponent();
			_business = new OrderDetailBusiness();
			this.LoadGrdOrderDetail();
		}

		private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				var orderdetail = new Orderdetail()
				{
                    OrderDetailId = txtOrderDetailId.Text,
                    OrderId = txtOrderId.Text,
                    ShellId = txtShellId.Text,
                    SubDiamondId = txtSubDiamondId.Text,
                    MainDiamondId = txtMainDiamondId.Text,
                    LineTotal = decimal.Parse(txtLineTotal.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    UnitWeight = decimal.Parse(txtUnitWeight.Text),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    DiscountPercentage = decimal.Parse(txtDiscountPercentage.Text),
                    Note = txtNote.Text
                };


				//var result = await _business.SearchByFields(categoryId, name, description, iconUrl, promotionImageUrl, promotionalTagline, careInstructions, maximumPrice, minimumPrice);
				var result = await _business.SearchByFields(orderdetail);
				MessageBox.Show(result.Message, "Save");

				this.LoadGrdOrderDetail(result.Data as List<Orderdetail>);

                // Clear all the text boxes
                txtOrderDetailId.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtShellId.Text = string.Empty;
                txtSubDiamondId.Text = string.Empty;
                txtMainDiamondId.Text = string.Empty;
                txtLineTotal.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtUnitWeight.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtDiscountPercentage.Text = string.Empty;
                txtNote.Text = string.Empty;

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
			}
		}

		private async void grdOrderDetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
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
						this.LoadGrdOrderDetail();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.ToString(), "Error");
					}
				}
			}
		}

		private async void grdProductCategory_ButtonView_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var categoryId = button.CommandParameter.ToString();
			if (string.IsNullOrEmpty(categoryId))
			{
				var item = await _business.GetById(categoryId);
			}
		}


		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderDetailResult = await _business.GetById(txtOrderDetailId.Text);
                if (orderDetailResult.Data is Orderdetail updatedOrderDetail)
                {
                    if (int.TryParse(txtQuantity.Text, out int quantity))
                    {
                        updatedOrderDetail.Quantity = quantity;
                    }
                    else
                    {
                        MessageBox.Show("Invalid quantity format.", "Error");
                        return;
                    }
                    var result = await _business.Update(updatedOrderDetail);
                    MessageBox.Show(result.Message, "Update");
                    LoadGrdOrderDetail();
                }
                else
                {
                    MessageBox.Show("Order detail not found.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }


        private void grdProductCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Lấy dòng được chọn trong DataGrid
			if (grdOrderDetail.SelectedItem != null)
			{

                Orderdetail selectedOrderDetail = (Orderdetail)grdOrderDetail.SelectedItem;
                txtOrderDetailId.Text = selectedOrderDetail.OrderDetailId;
                txtOrderId.Text = selectedOrderDetail.OrderId;
                txtShellId.Text = selectedOrderDetail.ShellId;
                txtSubDiamondId.Text = selectedOrderDetail.SubDiamondId;
                txtMainDiamondId.Text = selectedOrderDetail.MainDiamondId;
                txtLineTotal.Text = selectedOrderDetail.LineTotal.ToString();
                txtQuantity.Text = selectedOrderDetail.Quantity.ToString();
                txtUnitWeight.Text = selectedOrderDetail.UnitWeight.ToString();
                txtUnitPrice.Text = selectedOrderDetail.UnitPrice.ToString();
                txtDiscountPercentage.Text = selectedOrderDetail.DiscountPercentage.ToString();
                txtNote.Text = selectedOrderDetail.Note;
            }
		}

		private async void grdProductCategory_ButtonReport_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var categoryId = button.CommandParameter.ToString();

			if (!string.IsNullOrEmpty(categoryId))
			{
				var report = new wOrderDetailReport(categoryId);
				report.Owner = this;
				report.Show();
			}
		}

		private async void grdProductCategory_MouseDouble_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Double Click on Grid");
			DataGrid grd = sender as DataGrid;
			if (grd != null && grd.SelectedItems != null && grd.SelectedItems.Count == 1)
			{
				var row = grd.ItemContainerGenerator.ContainerFromItem(grd.SelectedItem) as DataGridRow;
				if (row != null)
				{
					var item = row.Item as Orderdetail;
					if (item != null)
					{
						var OrderdetailResult = await _business.GetById(item.OrderDetailId);

						if (OrderdetailResult.Status > 0 && OrderdetailResult.Data != null)
						{
                            item = OrderdetailResult.Data as Orderdetail;
                            txtOrderDetailId.Text = item.OrderDetailId;
                            txtOrderId.Text = item.OrderId;
                            txtShellId.Text = item.ShellId;
                            txtSubDiamondId.Text = item.SubDiamondId;
                            txtMainDiamondId.Text = item.MainDiamondId;
                            txtLineTotal.Text = item.LineTotal.ToString();
                            txtQuantity.Text = item.Quantity.ToString();
                            txtUnitWeight.Text = item.UnitWeight.ToString();
                            txtUnitPrice.Text = item.UnitPrice.ToString();
                            txtDiscountPercentage.Text = item.DiscountPercentage.ToString();
                            txtNote.Text = item.Note;
                        }
					}
				}
			}
		}

		private async void LoadGrdOrderDetail()
		{
			var result = await _business.getAll();

			if (result.Status > 0 && result.Data != null)
			{
				grdOrderDetail.ItemsSource = result.Data as List<Orderdetail>;
			}
			else
			{
				grdOrderDetail.ItemsSource = new List<Orderdetail>();
			}
		}

		private async void LoadGrdOrderDetail(List<Orderdetail> list)
		{
			if (list.Count > 0)
			{
				grdOrderDetail.ItemsSource = list;
			}
			else
			{
				grdOrderDetail.ItemsSource = new List<Orderdetail>();
			}
		}
	}
}
