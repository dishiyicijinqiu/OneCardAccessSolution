using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.DragDrop;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.View.RBAC
{
    /// <summary>
    /// UserGroupCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class UserGroupCollectionView : BaseUserControl, IUserGroupCollectionView
    {
        public UserGroupCollectionView()
        {
            InitializeComponent();
        }
        public UserGroupCollectionView(UserGroupCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }

        private void TreeListDragDropManager_Drop(object sender, TreeListDropEventArgs e)
        {
            if (e.DraggedRows == null)
            {
                e.Handled = true;
                return;
            }
            if (e.TargetNode == null)
            {
                e.Handled = true;
                return;
            }
            var entity = e.TargetNode.Content as FirstUserGroupEntity;
            if (entity == null)
            {
                e.Handled = true;
                return;
            }
            foreach (var item in e.DraggedRows)
            {
                var node = item as TreeListNode;
                if (node == null)
                {
                    e.Handled = true;
                    break;
                }
                var dragEntity = node.Content as FirstUserGroupEntity;
                if (dragEntity == null)
                {
                    e.Handled = true;
                    break;
                }
                switch (e.DropTargetType)
                {
                    case DropTargetType.InsertRowsAfter:
                    case DropTargetType.InsertRowsBefore:
                        if (string.IsNullOrWhiteSpace(entity.TreeParentNo))
                        {
                            e.Handled = true;
                            break;
                        }
                        if (dragEntity.TreeParentNo == entity.TreeParentNo)
                        {
                            e.Handled = true;
                            break;
                        }
                        {
                            try
                            {
                                if (DevExpress.Xpf.Core.DXMessageBox.Show(Properties.Resources.Info_ConfirmToMove, Properties.Resources.Info_Title, System.Windows.MessageBoxButton.YesNo,
                                    System.Windows.MessageBoxImage.Information) == System.Windows.MessageBoxResult.Yes)
                                {
                                    var irbacservice = ServiceProxyFactory.Create<IRBACService>();
                                    if (irbacservice.MoveUserGroup(dragEntity.UserGroupId, entity.UserGroupId, BusinessEntity.MoveTree.After))
                                    {
                                        var rsourceEntity = irbacservice.GetFirstUserGroupEntityById(dragEntity.UserGroupId);
                                        dragEntity = rsourceEntity;
                                        e.Handled = true;
                                    }
                                    else
                                    {
                                        e.Handled = true;
                                    }
                                }
                                else
                                    e.Handled = true;
                            }
                            catch (Exception ex)
                            {
                                ex.HandleException(this);
                                e.Handled = true;
                            }
                        }
                        break;
                    case DropTargetType.InsertRowsIntoNode:
                        if (entity.IsSuper)
                        {
                            e.Handled = true;
                            break;
                        }
                        {
                            try
                            {
                                if (DevExpress.Xpf.Core.DXMessageBox.Show(Properties.Resources.Info_ConfirmToMove, Properties.Resources.Info_Title, System.Windows.MessageBoxButton.YesNo,
                                 System.Windows.MessageBoxImage.Information) == System.Windows.MessageBoxResult.Yes)
                                {
                                    var irbacservice = ServiceProxyFactory.Create<IRBACService>();
                                    if (irbacservice.MoveUserGroup(dragEntity.UserGroupId, entity.UserGroupId, BusinessEntity.MoveTree.IntoNode))
                                    {
                                        var rsourceEntity = irbacservice.GetFirstUserGroupEntityById(dragEntity.UserGroupId);
                                        dragEntity = rsourceEntity;
                                        e.Handled = true;
                                    }
                                    else
                                    {
                                        e.Handled = true;
                                    }
                                }
                                else
                                {
                                    e.Handled = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.HandleException(this);
                                e.Handled = true;
                            }
                        }
                        break;
                    default:
                        e.Handled = true;
                        break;
                }
            }
        }

        private void TreeListDragDropManager_DragOver(object sender, TreeListDragOverEventArgs e)
        {
            if (e.TargetNode == null)
            {
                e.AllowDrop = false;
                e.Handled = true;
                return;
            }
            var entity = e.TargetNode.Content as FirstUserGroupEntity;
            if (entity == null)
            {
                e.AllowDrop = false;
                e.Handled = true;
                return;
            }
            foreach (var item in e.DraggedRows)
            {
                var node = item as TreeListNode;
                if (node == null)
                {
                    e.AllowDrop = false;
                    e.Handled = true;
                    break;
                }
                var dragEntity = node.Content as FirstUserGroupEntity;
                if (dragEntity == null)
                {
                    e.AllowDrop = false;
                    e.Handled = true;
                    break;
                }
                switch (e.DropTargetType)
                {
                    case DropTargetType.InsertRowsAfter:
                    case DropTargetType.InsertRowsBefore:
                        if (string.IsNullOrWhiteSpace(entity.TreeParentNo))
                        {
                            e.AllowDrop = false;
                            e.Handled = true;
                            break;
                        }
                        if (dragEntity.TreeParentNo == entity.TreeParentNo)
                        {
                            e.AllowDrop = false;
                            e.Handled = true;
                            break;
                        }
                        break;
                    case DropTargetType.InsertRowsIntoNode:
                        if (entity.IsSuper)
                        {
                            e.AllowDrop = false;
                            e.Handled = true;
                            break;
                        }
                        break;
                    default:
                        e.AllowDrop = false;
                        e.Handled = true;
                        break;
                }
            }
        }
        private void TreeListDragDropManager_StartDrag(object sender, TreeListStartDragEventArgs e)
        {
            if (e.Node == null)
            {
                e.CanDrag = false;
                return;
            }
            var entity = e.Node.Content as FirstUserGroupEntity;
            if (entity == null)
            {
                e.CanDrag = false;
                return;
            }
            if (entity.IsSuper)
            {
                e.CanDrag = false;
                return;
            }
            if (string.IsNullOrWhiteSpace(entity.TreeParentNo))
            {
                e.CanDrag = false;
                return;
            }
        }

        private void TreeListView_CellValueChanging(object sender, DevExpress.Xpf.Grid.TreeList.TreeListCellValueChangedEventArgs e)
        {

        }
    }

    public class UserGroupCollectionViewSelectionModeConverter : MarkupExtension, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ViewStyle style = (ViewStyle)value;
            switch (style)
            {
                case ViewStyle.OneSelect:
                    return DevExpress.Xpf.Grid.MultiSelectMode.None;
                default:
                    return DevExpress.Xpf.Grid.MultiSelectMode.Cell;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    public class UserGroupCollectionViewViewStyleConverter : MarkupExtension, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string method = parameter.ToString();
            ViewStyle style = (ViewStyle)value;
            if (method == "ViewIsVisible")
            {
                return ViewStyle.View == style;
            }
            else if (method == "SelectIsVisible")
            {
                return (ViewStyle.View == ViewStyle.OneSelect) | (ViewStyle.View == ViewStyle.MulSelect);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
