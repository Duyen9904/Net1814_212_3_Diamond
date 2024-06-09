using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Diamond.Business;
using Diamond.Common;
using Diamond.Data.Models;

namespace Diamond.WpfApp.UI.ProductCategoryUI
{
	/// <summary>
	/// Interaction logic for wProductCategorySearch.xaml
	/// </summary>
	public partial class wProductCategorySearch : Window
	{
		private ProductCategoryBusiness _business;
		public wProductCategorySearch()
		{
			InitializeComponent();
			_business = new ProductCategoryBusiness();
			this.LoadGrdCategory();
		}

		private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			try
			{

				var category = new Productcategory()
				{
					CategoryId = CategoryId.Text,
					Name = Name.Text,
					Description = Description.Text,
					IconUrl = IconUrl.Text,
					PromotionImageUrl = PromotionImageUrl.Text,
					IsFeatured = FeatureTrue.IsChecked == true ? true : FeatureFalse.IsChecked==true?false:null,
					PromotionalTagline = PromotionalTagline.Text,
					ProductAmount = string.IsNullOrEmpty(ProductAmount.Text.Trim())?-1:int.Parse(ProductAmount.Text),
					CareInstructions = CareInstruction.Text,
					MaximumPrice = string.IsNullOrEmpty(MaximumPrice.Text.Trim()) ? 0 : decimal.Parse(MaximumPrice.Text),
					MinimumPrice = string.IsNullOrEmpty(MinimumPrice.Text.Trim()) ? 0 : decimal.Parse(MinimumPrice.Text),
				};


				//var result = await _business.SearchByFields(categoryId, name, description, iconUrl, promotionImageUrl, promotionalTagline, careInstructions, maximumPrice, minimumPrice);
				var result = await _business.SearchByFields(category);
				MessageBox.Show(result.Message, "Save");

				this.LoadGrdCategory(result.Data as List<Productcategory>);

				// Clear all the text boxes
				CategoryId.Text = string.Empty;
				Name.Text = string.Empty;
				Description.Text = string.Empty;
				IconUrl.Text = string.Empty;
				PromotionImageUrl.Text = string.Empty;
				PromotionalTagline.Text = string.Empty;
				ProductAmount.Text = string.Empty;
				FeatureTrue.IsChecked = false;
				CareInstruction.Text = string.Empty;
				MaximumPrice.Text = string.Empty;
				MinimumPrice.Text = string.Empty;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
			}
		}

		private async void grdProductCategory_ButtonDelete_Click(object sender, RoutedEventArgs e)
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
						this.LoadGrdCategory();
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
			// Lấy thông tin từ các TextBox
			Productcategory updatedCategory = _business.GetById(CategoryId.Text).Result.Data as Productcategory;
			updatedCategory.Name = Name.Text;
			try
			{
				// Gọi phương thức Update trong lớp business để cập nhật dữ liệu
				var result = await _business.Update(updatedCategory);
				MessageBox.Show(result.Message, "Update");

				// Làm mới DataGrid để hiển thị dữ liệu mới
				LoadGrdCategory();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error");
			}
		}

		private void grdProductCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Lấy dòng được chọn trong DataGrid
			if (grdProductCategory.SelectedItem != null)
			{
				Productcategory selectedCategory = (Productcategory)grdProductCategory.SelectedItem;

				// Điền thông tin của dòng được chọn vào các TextBox
				CategoryId.Text = selectedCategory.CategoryId;
				Name.Text = selectedCategory.Name;
				Description.Text = selectedCategory.Description;
				IconUrl.Text = selectedCategory.IconUrl;
				PromotionImageUrl.Text = selectedCategory.PromotionImageUrl;
				PromotionalTagline.Text = selectedCategory.PromotionalTagline;
				ProductAmount.Text = selectedCategory.ProductAmount.ToString();
				FeatureTrue.IsChecked = (bool)selectedCategory.IsFeatured ? true : false;
				CareInstruction.Text = selectedCategory.CareInstructions;
				MaximumPrice.Text = selectedCategory.MaximumPrice.ToString();
				MinimumPrice.Text = selectedCategory.MinimumPrice.ToString();
			}
		}

		private async void grdProductCategory_ButtonReport_Click(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var categoryId = button.CommandParameter.ToString();

			if (!string.IsNullOrEmpty(categoryId))
			{
				var report = new wProductCategoryReport(categoryId);
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
					var item = row.Item as Productcategory;
					if (item != null)
					{
						var categoryResult = await _business.GetById(item.CategoryId);

						if (categoryResult.Status > 0 && categoryResult.Data != null)
						{
							item = categoryResult.Data as Productcategory;
							CategoryId.Text = item.CategoryId;
							Name.Text = item.Name;
							Description.Text = item.Description;
							IconUrl.Text = item.IconUrl;
							PromotionImageUrl.Text = item.PromotionImageUrl;
							PromotionalTagline.Text = item.PromotionalTagline;
							ProductAmount.Text = item.ProductAmount.ToString();
							FeatureTrue.IsChecked = (bool)item.IsFeatured ? true : false;
							CareInstruction.Text = item.CareInstructions;
							MaximumPrice.Text = item.MaximumPrice.ToString();
							MinimumPrice.Text = item.MinimumPrice.ToString();
						}
					}
				}
			}
		}

		private async void LoadGrdCategory()
		{
			var result = await _business.GetAll();

			if (result.Status > 0 && result.Data != null)
			{
				grdProductCategory.ItemsSource = result.Data as List<Productcategory>;
			}
			else
			{
				grdProductCategory.ItemsSource = new List<Productcategory>();
			}
		}

		private async void LoadGrdCategory(List<Productcategory> list)
		{
			if (list.Count > 0)
			{
				grdProductCategory.ItemsSource = list;
			}
			else
			{
				grdProductCategory.ItemsSource = new List<Productcategory>();
			}
		}
	}
}
