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
        private static readonly DependencyProperty GridControlMouseDoubleClickCommandBehaviorProperty = DependencyProperty.RegisterAttached("GridControlMouseDoubleClickCommandBehavior", typeof(GridControlMouseDoubleClickCommandBehavior), typeof(MouseDoubleClick), null);
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseDoubleClick), new PropertyMetadata(OnSetCommandCallback));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(MouseDoubleClick), new PropertyMetadata(OnSetCommandParameterCallback));
        public static readonly DependencyProperty HitPositionProperty = DependencyProperty.RegisterAttached("HitPosition", typeof(HitPosition), typeof(MouseDoubleClick), new PropertyMetadata(OnSetClickPositionCallback));
        #region GetSet
        public static void SetCommand(GridControl dg, ICommand command)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(GridControl dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            return dg.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommandParameter(GridControl dg, object parameter)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(CommandParameterProperty, parameter);
        }
        public static object GetCommandParameter(GridControl dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            return dg.GetValue(CommandParameterProperty);
        }
        public static void SetHitPosition(GridControl dg, HitPosition parameter)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            dg.SetValue(HitPositionProperty, parameter);
        }
        public static HitPosition GetHitPosition(GridControl dg)
        {
            if (dg == null) throw new System.ArgumentNullException("dg");
            //return (HitPosition)Enum.Parse(typeof(HitPosition), (string)dg.GetValue(HitPositionProperty));
            return (HitPosition)dg.GetValue(HitPositionProperty);
        }
        #endregion
        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl dg = dependencyObject as GridControl;
            if (dg != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.Command = e.NewValue as ICommand;
            }
        }
        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl dg = dependencyObject as GridControl;
            if (dg != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static void OnSetClickPositionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl dg = dependencyObject as GridControl;
            if (dg != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(dg);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static GridControlMouseDoubleClickCommandBehavior GetOrCreateBehavior(GridControl dg)
        {
            GridControlMouseDoubleClickCommandBehavior behavior = dg.GetValue(GridControlMouseDoubleClickCommandBehaviorProperty) as GridControlMouseDoubleClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new GridControlMouseDoubleClickCommandBehavior(dg);
                dg.SetValue(GridControlMouseDoubleClickCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
    }

    public class GridControlMouseDoubleClickCommandBehavior : Microsoft.Practices.Prism.Commands.CommandBehaviorBase<GridControl>
    {
        public HitPosition HitPosition { get; set; }
        public GridControlMouseDoubleClickCommandBehavior(GridControl targetObject) : base(targetObject)
        {
            //this.HitPosition = HitPosition;
            if (targetObject == null) throw new System.ArgumentNullException("targetObject can't be null");
            targetObject.MouseDoubleClick += TargetObject_MouseDoubleClick;
        }
        private void TargetObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as GridControl;
            ITableViewHitInfo hitInfo = ((ITableView)grid.View).CalcHitInfo(e.OriginalSource as DependencyObject);
            if (hitInfo == null)
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
                    if (hitInfo.InRow)
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
