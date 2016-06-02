using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.Core.EventToCommand
{
    public class GridControlMouseDoubleClick
    {
        private static readonly DependencyProperty GridControlMouseDoubleClickCommandBehaviorProperty =
            DependencyProperty.RegisterAttached("GridControlMouseDoubleClickCommandBehavior", typeof(GridControlMouseDoubleClickCommandBehavior), typeof(GridControlMouseDoubleClick), null);
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(GridControlMouseDoubleClick), new PropertyMetadata(OnSetCommandCallback));
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.RegisterAttached("CommandTarget", typeof(UIElement), typeof(GridControlMouseDoubleClick), new PropertyMetadata(OnSetCommandTargetCallback));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(GridControlMouseDoubleClick), new PropertyMetadata(OnSetCommandParameterCallback));
        public static readonly DependencyProperty HitPositionProperty =
            DependencyProperty.RegisterAttached("HitPosition", typeof(GridControlHitPosition), typeof(GridControlMouseDoubleClick), new PropertyMetadata(OnSetHitPositionCallback));
        const string controlname = "GridControl";
        #region GetSet
        public static void SetCommand(GridControl ctl, ICommand command)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(GridControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return ctl.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommandParameter(GridControl ctl, object parameter)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(CommandParameterProperty, parameter);
        }
        public static object GetCommandParameter(GridControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return ctl.GetValue(CommandParameterProperty);
        }
        public static void SetCommandTarget(GridControl ctl, object parameter)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(CommandTargetProperty, parameter);
        }
        public static object GetCommandTarget(GridControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return ctl.GetValue(CommandTargetProperty);
        }
        public static void SetHitPosition(GridControl ctl, string parameter)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            GridControlHitPosition hitposition = GridControlHitPosition.None;
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                string[] hitpositions = parameter.Split('|');
                foreach (var item in hitpositions)
                {
                    var temphitposition = Enum.Parse(typeof(GridControlHitPosition), item);
                    if (temphitposition == null)
                        throw new System.ArgumentNullException(controlname);
                    hitposition = hitposition | ((GridControlHitPosition)temphitposition);
                }
            }
            ctl.SetValue(HitPositionProperty, hitposition);
        }
        public static GridControlHitPosition GetHitPosition(GridControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return (GridControlHitPosition)ctl.GetValue(HitPositionProperty);
        }
        #endregion
        private static GridControlMouseDoubleClickCommandBehavior GetOrCreateBehavior(GridControl ctl)
        {
            GridControlMouseDoubleClickCommandBehavior behavior = ctl.GetValue(GridControlMouseDoubleClickCommandBehaviorProperty) as GridControlMouseDoubleClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new GridControlMouseDoubleClickCommandBehavior(ctl);
                ctl.SetValue(GridControlMouseDoubleClickCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl ctl = dependencyObject as GridControl;
            if (ctl != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.Command = e.NewValue as ICommand;
            }
        }
        private static void OnSetCommandTargetCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl ctl = dependencyObject as GridControl;
            if (ctl != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.CommandTarget = e.NewValue as UIElement;
            }
        }
        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl ctl = dependencyObject as GridControl;
            if (ctl != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static void OnSetHitPositionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            GridControl ctl = dependencyObject as GridControl;
            if (ctl != null)
            {
                GridControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.CommandParameter = e.NewValue;
            }
        }
    }


    public class GridControlMouseDoubleClickCommandBehavior : Microsoft.Practices.Prism.Commands.CommandBehaviorBase<GridControl>
    {
        public GridControlHitPosition HitPosition { get; set; }
        public GridControlMouseDoubleClickCommandBehavior(GridControl targetObject) : base(targetObject)
        {
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
            var grid = sender as GridControl;
            ITableViewHitInfo hitInfo = ((ITableView)grid.View).CalcHitInfo(e.OriginalSource as DependencyObject);
            if (hitInfo == null)
                return;
            if (e.ChangedButton != MouseButton.Left)
                return;
            GridControlHitPosition nowHitPosition = GridControlHitPosition.None;
            if (hitInfo.InColumnHeader)
                nowHitPosition = nowHitPosition | GridControlHitPosition.InColumnHeader;
            if (hitInfo.InGroupPanel)
                nowHitPosition = nowHitPosition | GridControlHitPosition.InGroupPanel;
            if (hitInfo.InRow)
                nowHitPosition = nowHitPosition | GridControlHitPosition.InRow;
            if ((HitPosition & nowHitPosition) != GridControlHitPosition.None)
            {
                ExecuteCommand();
            }
        }
    }
    public enum GridControlHitPosition
    {
        None = 0,
        InColumnHeader = 1,
        InGroupPanel = 2,
        InRow = 4
    }
}
