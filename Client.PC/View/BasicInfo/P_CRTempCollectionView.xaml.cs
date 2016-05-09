using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using System.Windows;
using System;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;

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
            this.Loaded += P_CRTempCollectionView_Loaded;
        }

        private void P_CRTempCollectionView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = new P_CRTempCollectionViewModel();
            }
            catch (Exception ex)
            {
                ex.HandleException(this);
                InterCloseDocument();
            }
        }

        protected override void Init()
        {

            base.Init();
            DefaultEventAggregator.Current.GetEvent<CreateViewEvent<object, CreateViewEventArgs<P_CRTempEditMessage, string>, P_CRTempEditMessage, string>>().Subscribe(OnCreateP_CRTempView);
        }

        private void OnCreateP_CRTempView(object sender, CreateViewEventArgs<P_CRTempEditMessage, string> args)
        {
            if (sender == this.DataContext)
            {
                this.CreateP_CRTempView(args.EditMessage);
            }
        }

        protected override void UnInit()
        {
            base.UnInit();
            DefaultEventAggregator.Current.GetEvent<CreateViewEvent<object, CreateViewEventArgs<P_CRTempEditMessage, string>, P_CRTempEditMessage, string>>().Unsubscribe(OnCreateP_CRTempView);
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
                EditP_CRTempView();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        private void EditP_CRTempView()
        {
            var SelectedRow = this.grid.CurrentItem as P_CRTempEntity;
            if (SelectedRow == null)
            {
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                return;
            }
            CreateP_CRTempView(new P_CRTempEditMessage(SelectedRow.P_CRTempId, EntityEditMode.Edit));
        }
        private void GridControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton != System.Windows.Input.MouseButton.Left)
                    return;
                var grid = sender as GridControl;
                #region 第一种写法
                int rowHandle = grid.View.GetRowHandleByMouseEventArgs(e);
                if (rowHandle < 0)
                    return;
                #endregion
                #region 第二种写法
                //ITableViewHitInfo hitInfo = ((ITableView)grid.View).CalcHitInfo(e.OriginalSource as DependencyObject);
                //if (hitInfo == null || !hitInfo.InRow)
                //    return;
                #endregion
                EditP_CRTempView();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
    }
}
