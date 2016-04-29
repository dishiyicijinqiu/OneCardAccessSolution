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
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : BaseUserControl
    {
        public RegisterCollectionView()
        {
            InitializeComponent();
            var vm = new RegisterCollectionViewModel();
            this.DataContext = vm;
            vm.Init();
        }
        protected override void Init()
        {
            base.Init();
            DefaultEventAggregator.Current.GetEvent<CreateRegisterViewEvent<object>>().Subscribe(OnCreateRegisterView);
        }
        protected override void UnInit()
        {
            base.UnInit();
            DefaultEventAggregator.Current.GetEvent<CreateRegisterViewEvent<object>>().Unsubscribe(OnCreateRegisterView);
        }

        private void OnCreateRegisterView(object sender, CreateRegisterViewEventArgs args)
        {
            if (sender == this.DataContext)
            {
                this.CreateRegisterView(args.RegisterEditMessage);
            }
        }
        private void CreateRegisterView(RegisterEditMessage editmsg)
        {
            var window = new UI.BaseRibbonWindow();
            window.Title = Properties.Resources.View_RegisterView_Title;
            window.Style = FindResource("DialogWindowStyle") as Style;
            window.Content = new RegisterView(this.DataContext, editmsg);
            window.Owner = Window.GetWindow(this);
            window.ShowDialog();
        }

        private void BarAddButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                CreateRegisterView(new RegisterEditMessage());
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
                var SelectedRow = this.grid.CurrentItem as FirstRegisterEntity;
                if (SelectedRow == null)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                    return;
                }
                CreateRegisterView(new RegisterEditMessage(_CopyKey: SelectedRow.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd));
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
            var SelectedRow = this.grid.CurrentItem as FirstRegisterEntity;
            if (SelectedRow == null)
            {
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                return;
            }
            CreateRegisterView(new RegisterEditMessage(SelectedRow.RegisterId, EntityEditMode.Edit));
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
