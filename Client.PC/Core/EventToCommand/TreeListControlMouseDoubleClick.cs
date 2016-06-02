using DevExpress.Xpf.Grid;
using System;
using System.Windows;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Core
{
    public class TreeListControlMouseDoubleClick
    {
        private static readonly DependencyProperty TreeListControlMouseDoubleClickCommandBehaviorProperty =
            DependencyProperty.RegisterAttached("TreeListControlMouseDoubleClickCommandBehavior", typeof(TreeListControlMouseDoubleClickCommandBehavior), typeof(TreeListControlMouseDoubleClick), null);
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetCommandCallback));
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.RegisterAttached("CommandTarget", typeof(UIElement), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetCommandTargetCallback));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetCommandParameterCallback));
        public static readonly DependencyProperty HitPositionProperty =
            DependencyProperty.RegisterAttached("HitPosition", typeof(TreeListControlHitPosition), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetHitPositionCallback));
        const string controlname = "TreeListControl";
        #region GetSet
        public static void SetCommand(TreeListControl ctl, ICommand command)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(CommandProperty, command);
        }
        public static ICommand GetCommand(TreeListControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return ctl.GetValue(CommandProperty) as ICommand;
        }
        public static void SetCommandParameter(TreeListControl ctl, object parameter)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(CommandParameterProperty, parameter);
        }
        public static object GetCommandParameter(TreeListControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return ctl.GetValue(CommandParameterProperty);
        }
        public static void SetCommandTarget(TreeListControl ctl, object parameter)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(CommandTargetProperty, parameter);
        }
        public static object GetCommandTarget(TreeListControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            return ctl.GetValue(CommandTargetProperty);
        }
        public static void SetHitPosition(TreeListControl ctl, string parameter)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            ctl.SetValue(HitPositionProperty, parameter);
        }
        public static TreeListControlHitPosition GetHitPosition(TreeListControl ctl)
        {
            if (ctl == null) throw new System.ArgumentNullException(controlname);
            TreeListControlHitPosition hitposition = TreeListControlHitPosition.None;
            var pvalue = ctl.GetValue(HitPositionProperty);
            if (pvalue != null)
            {
                string parameter = pvalue.ToString();
                if (!string.IsNullOrWhiteSpace(parameter))
                {
                    string[] hitpositions = parameter.Split('|');
                    foreach (var item in hitpositions)
                    {
                        var temphitposition = Enum.Parse(typeof(TreeListControlHitPosition), item);
                        if (temphitposition == null)
                            throw new System.ArgumentNullException(controlname);
                        hitposition = hitposition | ((TreeListControlHitPosition)temphitposition);
                    }
                }
            }
            return hitposition;
        }
        #endregion
        private static TreeListControlMouseDoubleClickCommandBehavior GetOrCreateBehavior(TreeListControl ctl)
        {
            TreeListControlMouseDoubleClickCommandBehavior behavior = ctl.GetValue(TreeListControlMouseDoubleClickCommandBehaviorProperty) as TreeListControlMouseDoubleClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new TreeListControlMouseDoubleClickCommandBehavior(ctl);
                ctl.SetValue(TreeListControlMouseDoubleClickCommandBehaviorProperty, behavior);
            }
            return behavior;
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TreeListControl ctl = dependencyObject as TreeListControl;
            if (ctl != null)
            {
                TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.Command = e.NewValue as ICommand;
            }
        }
        private static void OnSetCommandTargetCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TreeListControl ctl = dependencyObject as TreeListControl;
            if (ctl != null)
            {
                TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.CommandTarget = e.NewValue as UIElement;
            }
        }
        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TreeListControl ctl = dependencyObject as TreeListControl;
            if (ctl != null)
            {
                TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                behavior.CommandParameter = e.NewValue;
            }
        }
        private static void OnSetHitPositionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TreeListControl ctl = dependencyObject as TreeListControl;
            if (ctl != null)
            {
                TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
                TreeListControlHitPosition hitposition = TreeListControlHitPosition.None;
                if (e.NewValue != null)
                {
                    string parameter = e.NewValue.ToString();
                    if (!string.IsNullOrWhiteSpace(parameter))
                    {
                        string[] hitpositions = parameter.Split('|');
                        foreach (var item in hitpositions)
                        {
                            var temphitposition = Enum.Parse(typeof(TreeListControlHitPosition), item);
                            if (temphitposition == null)
                                throw new System.ArgumentNullException(controlname);
                            hitposition = hitposition | ((TreeListControlHitPosition)temphitposition);
                        }
                    }
                }
                behavior.HitPosition = hitposition;
            }
        }
    }

    public class TreeListControlMouseDoubleClickCommandBehavior : Microsoft.Practices.Prism.Commands.CommandBehaviorBase<TreeListControl>
    {
        public TreeListControlHitPosition HitPosition { get; set; }
        public TreeListControlMouseDoubleClickCommandBehavior(TreeListControl targetObject) : base(targetObject)
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
            var treelist = sender as TreeListControl;
            var hitInfo = treelist.View.CalcHitInfo(e.OriginalSource as DependencyObject);

            if (hitInfo == null)
                return;
            if (e.ChangedButton != MouseButton.Left)
                return;
            TreeListControlHitPosition nowHitPosition = (TreeListControlHitPosition)Enum.Parse(typeof(TreeListControlHitPosition), hitInfo.HitTest.ToString());
            if ((HitPosition & nowHitPosition) != TreeListControlHitPosition.None)
            {
                ExecuteCommand();
            }
        }
    }
    [Flags]
    public enum TreeListControlHitPosition
    {
        None = 0,
        RowCell = 2,
        Row = 4,
        ColumnHeaderPanel = 8,
        ColumnHeader = 16,
        ColumnHeaderFilterButton = 32,
        BandHeaderPanel = 64,
        BandHeader = 128,
        VerticalScrollBar = 256,
        HorizontalScrollBar = 512,
        FilterPanel = 1024,
        FilterPanelCloseButton = 2048,
        FilterPanelCustomizeButton = 4096,
        FilterPanelActiveButton = 8192,
        FilterPanelText = 16384,
        MRUFilterListComboBox = 32768,
        TotalSummaryPanel = 65536,
        TotalSummary = 131072,
        FixedTotalSummary = 262144,
        DataArea = 524288,
        ColumnButton = 1048576,
        BandButton = 2097152,
        ColumnEdge = 4194304,
        BandEdge = 8388608,
        FixedLeftDiv = 16777216,
        FixedRightDiv = 33554432,
        RowIndicator = 67108864,
        NodeIndent = 134217728,
        ExpandButton = 268435456,
        NodeImage = 536870912,
        NodeCheckbox = 1073741824
    }
}
