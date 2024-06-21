using Diamond.Business;
using Diamond.Data.Models;
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
using System.Xml.Linq;

namespace Diamond.WpfApp.UI
{
    /// <summary>
    /// Interaction logic for wOrderDetail.xaml
    /// </summary>
    public partial class wOrderDetail : Window
    {
        private OrderDetailBusiness _business;
        
        public wOrderDetail()
        {
            InitializeComponent();
            _business = new OrderDetailBusiness();
            this.LoadGrdOrderdetail();
        }
        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = await _business.GetById(txtOrderDetailId.Text);

                if (item.Data == null)
                {
                    var orderdetail = new Orderdetail()
                    {
                        OrderDetailId = txtOrderDetailId.Text,
                        OrderId = txtOrderId.Text,
                        ShellId = txtShellId.Text,
                        SubDiamondId = txtSubDiamondId.Text,
                        MainDiamondId = txtMainDiamondId.Text,
                        LineTotal = decimal.Parse(txtLineTotal.Text),
                    };

                    var result = await _business.Save(orderdetail);
                    MessageBox.Show(result.Message, "Save");
                }
                else // cai tien: ton tai thi update
                {
                    //MessageBox.Show("Exist Diamond", "Warning");
                    var updateOrderdetail = item.Data as Orderdetail;
                    updateOrderdetail.OrderId = txtOrderId.Text;
                    updateOrderdetail.ShellId = txtShellId.Text;
                    updateOrderdetail.SubDiamondId = txtSubDiamondId.Text;
                    updateOrderdetail.MainDiamondId = txtMainDiamondId.Text;
                    updateOrderdetail.LineTotal = decimal.Parse(txtLineTotal.Text);
                    updateOrderdetail.OrderDetailId = txtOrderDetailId.Text;
                    var result = await _business.Update(updateOrderdetail);
                    MessageBox.Show(result.Message, "Update");
                }
                // Clear all the text boxes
                txtOrderDetailId.Text = string.Empty;
                txtOrderId.Text = string.Empty;
                txtShellId.Text = string.Empty;
                txtSubDiamondId.Text = string.Empty;
                txtMainDiamondId.Text = string.Empty;
                txtLineTotal.Text = string.Empty;

                LoadGrdOrderdetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private async void grdOrderdetail_ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string orderDetailID = button.CommandParameter.ToString();
            if (!string.IsNullOrEmpty(orderDetailID))
            {
                var result = MessageBox.Show("Are you sure you want to delete this line of order detail?", "Confirm Delete", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var deleteResult = await _business.DeleteById(orderDetailID);
                        MessageBox.Show(deleteResult.Message, "Delete");
                        LoadGrdOrderdetail();
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
            txtOrderDetailId.Text = string.Empty;
            txtOrderId.Text = string.Empty;
            txtShellId.Text = string.Empty;
            txtSubDiamondId.Text = string.Empty;
            txtMainDiamondId.Text = string.Empty;
            txtLineTotal.Text = string.Empty;

            LoadGrdOrderdetail();
        }

        //private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        //{
        //    // Lấy thông tin từ các TextBox
        //    Diamond updatedDiamond = _business.GetById(txtDiamondId.Text).Result.Data as Diamond;
        //    updatedDiamond.Name = txtName.Text;
        //    updatedDiamond.Color = txtColor.Text;
        //    updatedDiamond.Clarity = txtClarity.Text;
        //    updatedDiamond.Carat = decimal.Parse(txtCarat.Text);
        //    updatedDiamond.Cut = txtCut.Text;
        //    updatedDiamond.CertificateScan = txtCertificateScan.Text;
        //    updatedDiamond.Cost = decimal.Parse(txtCost.Text);
        //    updatedDiamond.AmountAvailable = int.Parse(txtAmountAvailable.Text);
        //    updatedDiamond.CategoryId = txtCategoryId.Text;
        //    try
        //    {
        //        // Gọi phương thức Update trong lớp business để cập nhật dữ liệu
        //        var result = await _business.Update(updatedDiamond);
        //        MessageBox.Show(result.Message, "Update");

        //        // Làm mới DataGrid để hiển thị dữ liệu mới
        //        LoadGrdDiamond();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "Error");
        //    }
        //}


        private void grdOrderdetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grdOrderdetail.SelectedItem != null)
            {
                Orderdetail selectedOrderDetail = (Orderdetail)grdOrderdetail.SelectedItem;
                txtOrderDetailId.Text = selectedOrderDetail.OrderDetailId;
                txtOrderId.Text = selectedOrderDetail.OrderId;
                txtShellId.Text = selectedOrderDetail.ShellId;
                txtSubDiamondId.Text = selectedOrderDetail.SubDiamondId;
                txtMainDiamondId.Text = selectedOrderDetail.MainDiamondId;
                txtLineTotal.Text = selectedOrderDetail.LineTotal.ToString();
            }
        }

        private async void grdOrderdetail_MouseDouble_Click(object sender, RoutedEventArgs e)
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
                        }
                    }
                }
            }
        }


        private async void LoadGrdOrderdetail()
        {
            var result = await _business.getAll();

            if (result.Status > 0 && result.Data != null)
            {
                grdOrderdetail.ItemsSource = result.Data as List<Orderdetail>;
            }
            else
            {
                grdOrderdetail.ItemsSource = new List<Orderdetail>();
            }
        }
        private void grdOrderdetail_ButtonView_Click(object sender, EventArgs e)
        {
            // Your code here
        }
    }
}
