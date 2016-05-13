using DevExpress.Xpf.Grid;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : BaseUserControl
    {
        public RegisterView(object ParentViewModel, RegisterEditMessage EditMessage)
        {
            InitializeComponent();
            this.ParentDataContext = ParentViewModel;
            this.Loaded += (sender, e) =>
            {
                try
                {
                    this.DataContext = new RegisterViewModel(this.ParentDataContext, EditMessage);
                }
                catch (System.Exception ex)
                {
                    ex.HandleException(this);
                    InterCloseDocument();
                }
            };
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
                var fileEntity = grid.GetRow(rowHandle) as BusinessEntity.BasicInfo.Register_FileEntity;
                //ViewRegisterFileCommand
            }
            catch (Exception ex)
            {
                ex.HandleException(this);
            }
        }
    }
}
