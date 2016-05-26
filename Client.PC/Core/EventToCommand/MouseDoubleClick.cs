using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Globalization;

namespace FengSharp.OneCardAccess.Core
{
    public class MouseDoubleClick
    {
        private static readonly DependencyProperty GridDataControlBaseMouseDoubleClickCommandBehaviorProperty = DependencyProperty.RegisterAttached("GridDataControlBaseMouseDoubleClickCommandBehavior", typeof(GridDataControlBaseMouseDoubleClickCommandBehavior), typeof(MouseDoubleClick), null);
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseDoubleClick), new PropertyMetadata(OnSetCommandCallback));
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.RegisterAttached("CommandTarget", typeof(UIElement), typeof(MouseDoubleClick), new PropertyMetadata(OnSetCommandTargetCallback));

        //System.Windows.IInputElement CommandTarget
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(MouseDoubleClick), new PropertyMetadata(OnSetCommandParameterCallback));
        public static readonly DependencyProperty HitPositionProperty = DependencyProperty.RegisterAttached("HitPosition", typeof(HitPosition), typeof(MouseDoubleClick), new PropertyMetadata(OnSetClickPositionCallback));
        #region GetSet
        public static void SetCommand(GridDataControlBase dg, ICommand command)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(GridDataControlBase dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            return dg.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommandParameter(GridDataControlBase dg, object parameter)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(CommandParameterProperty, parameter);
        }
        public static object GetCommandParameter(GridDataControlBase dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            return dg.GetValue(CommandParameterProperty);
        }

        public static void SetCommandTarget(GridDataControlBase dg, object parameter)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(CommandTargetProperty, parameter);
        }
        public static object GetCommandTarget(GridDataControlBase dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            return dg.GetValue(CommandTargetProperty);
        }
        public static void SetHitPosition(GridDataControlBase dg, HitPosition parameter)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(HitPositionProperty, parameter);
        }
        public static HitPosition GetHitPosition(GridDataControlBase dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            //return (HitPosition)Enum.Parse(typeof(HitPosition), (string)dg.GetValue(HitPositionProperty));
            return (HitPosition)dg.GetValue(HitPositionProperty);
        }
        #endregion
        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridDataControlBase dg = dependencyObject as GridDataControlBase;
            if (dg != null)
            {
                GridDataControlBaseMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.Command = e.NewValue as ICommand;
            }
        }
        private static void OnSetCommandTargetCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridDataControlBase dg = dependencyObject as GridDataControlBase;
            if (dg != null)
            {
                GridDataControlBaseMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.CommandTarget = e.NewValue as UIElement;
            }
        }



        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridDataControlBase dg = dependencyObject as GridDataControlBase;
            if (dg != null)
            {
                GridDataControlBaseMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static void OnSetClickPositionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridDataControlBase dg = dependencyObject as GridDataControlBase;
            if (dg != null)
            {
                GridDataControlBaseMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static GridDataControlBaseMouseDoubleClickCommandBehavior GetOrCreateBehavior(GridDataControlBase dg)
        {
            GridDataControlBaseMouseDoubleClickCommandBehavior behavior = dg.GetValue(GridDataControlBaseMouseDoubleClickCommandBehaviorProperty) as GridDataControlBaseMouseDoubleClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new GridDataControlBaseMouseDoubleClickCommandBehavior(dg);
                dg.SetValue(GridDataControlBaseMouseDoubleClickCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }

    public class GridDataControlBaseMouseDoubleClickCommandBehavior : Microsoft.Practices.Prism.Commands.CommandBehaviorBase<GridDataControlBase>
    {

        //behavior.TargetObject = e.NewValue as ICommand;
        public HitPosition HitPosition { get; set; }
        public GridDataControlBaseMouseDoubleClickCommandBehavior(GridDataControlBase targetObject) : base(targetObject)
        {
            //this.HitPosition = HitPosition;
            if (targetObject == null) throw new System.ArgumentNullException("targetObject can't be null");
            targetObject.MouseDoubleClick += TargetObject_MouseDoubleClick;
        }
        protected override void UpdateEnabledState()
        {
            if (this.Command != null && CommandTarget != null)
            {
                CommandTarget.IsEnabled = this.Command.CanExecute(this.CommandParameter);
            }
        }
        public UIElement CommandTarget { get; set; }

        private void TargetObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ITableView tableView = null;
            var treelist = sender as TreeListControl;
            if (treelist != null)
                tableView = (ITableView)treelist.View;
            else
            {
                var grid = sender as GridControl;
                if (grid != null)
                    tableView = (ITableView)grid.View;
            }
            if (tableView == null) return;
            ITableViewHitInfo hitInfo = tableView.CalcHitInfo(e.OriginalSource as DependencyObject);
            if (hitInfo == null)
                return;

            //((DevExpress.Xpf.Grid.TableViewHitInfo)hitInfo).HitTest GroupRow DevExpress.Xpf.Grid.TableViewHitTest
            //((DevExpress.Xpf.Grid.TableViewHitInfo)hitInfo).HitTest== TableViewHitTest.GroupRow

            //HitTest RowCell DevExpress.Xpf.Grid.TreeList.TreeListViewHitTest
            if (e.ChangedButton != MouseButton.Left)
                return;
            switch (HitPosition)
            {
                case HitPosition.InColumnHeader:
                    if (hitInfo.InColumnHeader)
                        ExecuteCommand();
                    break;
                case HitPosition.InGroupPanel:
                    if (hitInfo.InGroupPanel)
                        ExecuteCommand();
                    break;
                case HitPosition.InRow:
                    if (hitInfo.InRow && hitInfo.IsRowCell)
                        ExecuteCommand();
                    break;
                case HitPosition.InColumnHeaderOrGroupPanel:
                    if (hitInfo.InColumnHeader || hitInfo.InGroupPanel)
                        ExecuteCommand();
                    break;
                case HitPosition.InColumnHeaderOrRow:
                    if (hitInfo.InColumnHeader || hitInfo.InRow)
                        ExecuteCommand();
                    break;
                case HitPosition.InGroupPanelOrRow:
                    if (hitInfo.InGroupPanel || hitInfo.InRow)
                        ExecuteCommand();
                    break;
                case HitPosition.All:
                    ExecuteCommand();
                    break;
                default:
                    break;
            }
        }
    }
    public enum HitPosition
    {
        InColumnHeader = 1,
        InGroupPanel = 2,
        InRow = 0,
        InColumnHeaderOrGroupPanel = 3,
        InColumnHeaderOrRow = 4,
        InGroupPanelOrRow = 5,
        All = 6,
    }
}
