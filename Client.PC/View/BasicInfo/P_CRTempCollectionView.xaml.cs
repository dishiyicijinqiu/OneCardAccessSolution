using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using System.Windows;
using System;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// P_CRTempCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class P_CRTempCollectionView : BaseUserControl
    {
        public P_CRTempCollectionView()
        {
            InitializeComponent();
            var vm = new P_CRTempCollectionViewModel();
            this.DataContext = vm;
            vm.Init();
        }
        protected override void Init()
        {

            base.Init();
            DefaultEventAggregator.Current.GetEvent<CreateP_CRTempViewEvent<object>>().Subscribe(OnCreateP_CRTempView);
        }
        protected override void UnInit()
        {
            base.UnInit();
            DefaultEventAggregator.Current.GetEvent<CreateP_CRTempViewEvent<object>>().Unsubscribe(OnCreateP_CRTempView);
        }
        private void OnCreateP_CRTempView(object sender, CreateP_CRTempViewEventArgs args)
        {
            if (sender == this.DataContext)
            {
                this.CreateP_CRTempView(args.P_CRTempEditMessage);
            }
        }
        private void CreateP_CRTempView(P_CRTempEditMessage editmsg)
        {
            var window = new BaseRibbonWindow();
            window.Title = Properties.Resources.View_P_CRTempView_Title;
            window.Style = FindResource("DialogWindowStyle") as Style;
            window.Content = new P_CRTempView(this.DataContext, editmsg);
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
        }

        private void BarAddButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                CreateP_CRTempView(new P_CRTempEditMessage());
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this.DataContext, new ExceptionEventArgs(ex));
            }
        }

        private void BarCopyAddButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                var SelectedRow = this.grid.CurrentItem as P_CRTempEntity;
                if (SelectedRow == null)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                    return;
                }
                CreateP_CRTempView(new P_CRTempEditMessage(_CopyKey: SelectedRow.P_CRTempId, _EntityEditMode: EntityEditMode.CopyAdd));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this.DataContext, new ExceptionEventArgs(ex));
            }
        }

        private void BarEditButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                EditRegisterView();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        private void EditRegisterView()
        {
            var SelectedRow = this.grid.CurrentItem as P_CRTempEntity;
            if (SelectedRow == null)
            {
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                return;
            }
            CreateP_CRTempView(new P_CRTempEditMessage(SelectedRow.P_CRTempId, EntityEditMode.Edit));
        }
        private void tableview_RowDoubleClick(object sender, DevExpress.Xpf.Grid.RowDoubleClickEventArgs e)
        {
            try
            {
                if (e.HitInfo.RowHandle <= 0) return;
                EditRegisterView();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
    }
}
