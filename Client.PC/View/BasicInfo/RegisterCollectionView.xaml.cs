using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Ribbon;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : UserControl
    {
        public RegisterCollectionView()
        {
            InitializeComponent();
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            //WindowService windowService = new WindowService();
            //windowService.WindowStyle = Resources["DialogWindowStyle"] as Style;
            //windowService.Show(null, null);
            //this.grid.GetFocusedValue();
            //this.tableview.FocusedRowHandle;
            //this.tableview.SetCurrentValue();
            //this.tableview.FocusedRowData;
            //this.tableview.FocusedRow
        }
    }
}
