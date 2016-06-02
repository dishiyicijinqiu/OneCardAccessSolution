using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Core
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
            DependencyProperty.RegisterAttached("HitPosition", typeof(string), typeof(GridControlMouseDoubleClick), new PropertyMetadata(OnSetHitPositionCallback));
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
            ctl.SetValue(HitPositionProperty, parameter);
        }
        public static GridControlHitPosition GetHitPosition(GridControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            GridControlHitPosition hitposition = GridControlHitPosition.None;
            var pvalue = ctl.GetValue(HitPositionProperty);
            if (pvalue != null)
            {
                string parameter = pvalue.ToString();
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
            }
            return hitposition;
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
                GridControlHitPosition hitposition = GridControlHitPosition.None;
                if (e.NewValue != null)
                {
                    string parameter = e.NewValue.ToString();
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
                }
                behavior.HitPosition = hitposition;
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
            var hitInfo = ((ITableView)grid.View).CalcHitInfo(e.OriginalSource as DependencyObject) as TableViewHitInfo;
            if (hitInfo == null)
                return;
            if (e.ChangedButton != MouseButton.Left)
                return;
            GridControlHitPosition nowHitPosition = (GridControlHitPosition)Enum.Parse(typeof(GridControlHitPosition), hitInfo.HitTest.ToString());
            if ((HitPosition & nowHitPosition) != GridControlHitPosition.None)
            {
                ExecuteCommand();
            }
        }
    }
    [Flags]
    public enum GridControlHitPosition : long
    {
        None = 0,
        RowCell = 2,
        Row = 4,
        GroupRow = 8,
        GroupRowButton = 16,
        GroupRowCheckBox = 32,
        ColumnHeaderPanel = 64,
        ColumnHeader = 128,
        ColumnHeaderFilterButton = 256,
        BandHeaderPanel = 512,
        BandHeader = 1024,
        GroupPanel = 2048,
        GroupPanelColumnHeader = 4096,
        GroupPanelColumnHeaderFilterButton = 8192,
        VerticalScrollBar = 16384,
        HorizontalScrollBar = 32768,
        FilterPanel = 65536,
        FilterPanelCloseButton = 131072,
        FilterPanelCustomizeButton = 262144,
        FilterPanelActiveButton = 524288,
        FilterPanelText = 1048576,
        MRUFilterListComboBox = 2097152,
        TotalSummaryPanel = 4194304,
        TotalSummary = 8388608,
        FixedTotalSummary = 16777216,
        DataArea = 33554432,
        GroupValue = 67108864,
        GroupSummary = 134217728,
        ColumnButton = 268435456,
        BandButton = 536870912,
        ColumnEdge = 1073741824,
        BandEdge = 2147483648,
        FixedLeftDiv = 4294967296,
        FixedRightDiv = 8589934592,
        RowIndicator = 17179869184,
        GroupFooterRow = 34359738368,
        GroupFooterSummary = 68719476736,
        MasterRowButton = 137438953472
    }
}
