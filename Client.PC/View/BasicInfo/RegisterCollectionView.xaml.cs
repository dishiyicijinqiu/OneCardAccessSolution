using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : BaseUserControl
    {
        public RegisterCollectionView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                DefaultEventAggregator.Current.GetEvent<ViewRegisterEvent<RegisterCollectionViewModel>>().
                    Subscribe(OnViewRegister);
                this.DataContext = new RegisterCollectionViewModel();
            }
        }

        private void OnViewRegister(RegisterCollectionViewModel sender, ViewRegisterEventArgs args)
        {
            if (sender == this.DataContext)
            {
                var window = new UI.BaseRibbonWindow();
                window.Title = Properties.Resources.View_RegisterView_Title;
                window.Style = FindResource("DialogWindowStyle") as Style;
                window.Content = new RegisterView(args.RegisterEditMessage);
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();

            }
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
