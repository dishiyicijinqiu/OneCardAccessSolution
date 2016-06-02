using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.Core.EventToCommand
{
    //public class TreeListControlMouseDoubleClick
    //{
    //    private static readonly DependencyProperty TreeListControlMouseDoubleClickCommandBehaviorProperty =
    //        DependencyProperty.RegisterAttached("TreeListControlMouseDoubleClickCommandBehavior", typeof(TreeListControlMouseDoubleClickCommandBehavior), typeof(TreeListControlMouseDoubleClick), null);
    //    public static readonly DependencyProperty CommandProperty =
    //        DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetCommandCallback));
    //    public static readonly DependencyProperty CommandTargetProperty =
    //        DependencyProperty.RegisterAttached("CommandTarget", typeof(UIElement), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetCommandTargetCallback));

    //    public static readonly DependencyProperty CommandParameterProperty =
    //        DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetCommandParameterCallback));
    //    public static readonly DependencyProperty HitPositionProperty =
    //        DependencyProperty.RegisterAttached("HitPosition", typeof(TreeListControlHitPosition), typeof(TreeListControlMouseDoubleClick), new PropertyMetadata(OnSetHitPositionCallback));
    //    const string controlname = "TreeListControl";
    //    #region GetSet
    //    public static void SetCommand(TreeListControl ctl, ICommand command)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        ctl.SetValue(CommandProperty, command);
    //    }
    //    public static ICommand GetCommand(TreeListControl ctl)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        return ctl.GetValue(CommandProperty) as ICommand;
    //    }
    //    public static void SetCommandParameter(TreeListControl ctl, object parameter)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        ctl.SetValue(CommandParameterProperty, parameter);
    //    }
    //    public static object GetCommandParameter(TreeListControl ctl)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        return ctl.GetValue(CommandParameterProperty);
    //    }
    //    public static void SetCommandTarget(TreeListControl ctl, object parameter)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        ctl.SetValue(CommandTargetProperty, parameter);
    //    }
    //    public static object GetCommandTarget(TreeListControl ctl)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        return ctl.GetValue(CommandTargetProperty);
    //    }
    //    public static void SetHitPosition(TreeListControl ctl, string parameter)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        TreeListControlHitPosition hitposition = TreeListControlHitPosition.None;
    //        if (!string.IsNullOrWhiteSpace(parameter))
    //        {
    //            string[] hitpositions = parameter.Split('|');
    //            foreach (var item in hitpositions)
    //            {
    //                var temphitposition = Enum.Parse(typeof(TreeListControlHitPosition), item);
    //                if (temphitposition == null)
    //                    throw new System.ArgumentNullException(controlname);
    //                hitposition = hitposition | ((TreeListControlHitPosition)temphitposition);
    //            }
    //        }
    //        ctl.SetValue(HitPositionProperty, hitposition);
    //    }
    //    public static TreeListControlHitPosition GetHitPosition(TreeListControl ctl)
    //    {
    //        if (ctl == null) throw new System.ArgumentNullException(controlname);
    //        return (TreeListControlHitPosition)ctl.GetValue(HitPositionProperty);
    //    }
    //    #endregion
    //    private static TreeListControlMouseDoubleClickCommandBehavior GetOrCreateBehavior(TreeListControl ctl)
    //    {
    //        TreeListControlMouseDoubleClickCommandBehavior behavior = ctl.GetValue(TreeListControlMouseDoubleClickCommandBehaviorProperty) as TreeListControlMouseDoubleClickCommandBehavior;
    //        if (behavior == null)
    //        {
    //            behavior = new TreeListControlMouseDoubleClickCommandBehavior(ctl);
    //            ctl.SetValue(TreeListControlMouseDoubleClickCommandBehaviorProperty, behavior);
    //        }
    //        return behavior;
    //    }

    //    private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    //    {
    //        TreeListControl ctl = dependencyObject as TreeListControl;
    //        if (ctl != null)
    //        {
    //            TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
    //            behavior.Command = e.NewValue as ICommand;
    //        }
    //    }
    //    private static void OnSetCommandTargetCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    //    {
    //        TreeListControl ctl = dependencyObject as TreeListControl;
    //        if (ctl != null)
    //        {
    //            TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
    //            behavior.CommandTarget = e.NewValue as UIElement;
    //        }
    //    }
    //    private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    //    {
    //        TreeListControl ctl = dependencyObject as TreeListControl;
    //        if (ctl != null)
    //        {
    //            TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
    //            behavior.CommandParameter = e.NewValue;
    //        }
    //    }
    //    private static void OnSetHitPositionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    //    {
    //        TreeListControl ctl = dependencyObject as TreeListControl;
    //        if (ctl != null)
    //        {
    //            TreeListControlMouseDoubleClickCommandBehavior behavior = GetOrCreateBehavior(ctl);
    //            behavior.CommandParameter = e.NewValue;
    //        }
    //    }
    //}

    //public class TreeListControlMouseDoubleClickCommandBehavior : Microsoft.Practices.Prism.Commands.CommandBehaviorBase<TreeListControl>
    //{
    //    public TreeListControlHitPosition HitPosition { get; set; }
    //    public TreeListControlMouseDoubleClickCommandBehavior(TreeListControl targetObject) : base(targetObject)
    //    {
    //        if (targetObject == null) throw new System.ArgumentNullException("targetObject can't be null");
    //        targetObject.MouseDoubleClick += TargetObject_MouseDoubleClick;
    //    }
    //    protected override void UpdateEnabledState()
    //    {
    //        if (this.Command != null && CommandTarget != null)
    //        {
    //            CommandTarget.IsEnabled = this.Command.CanExecute(this.CommandParameter);
    //        }
    //    }
    //    public UIElement CommandTarget { get; set; }

    //    private void TargetObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    //    {
    //        var treelist = sender as TreeListControl;
    //        var hitInfo = treelist.View.CalcHitInfo(e.OriginalSource as DependencyObject);
    //        if (hitInfo == null)
    //            return;
    //        if (e.ChangedButton != MouseButton.Left)
    //            return;
    //        TreeListControlHitPosition nowHitPosition = TreeListControlHitPosition.None;
    //        if (hitInfo.InBandPanel)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InBandPanel;
    //        if (hitInfo.InColumnHeader)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InColumnHeader;
    //        if (hitInfo.InColumnPanel)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InColumnPanel;
    //        if (hitInfo.InGroupColumn)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InGroupColumn;
    //        if (hitInfo.InGroupPanel)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InGroupPanel;
    //        if (hitInfo.InNodeCheckbox)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InNodeCheckbox;
    //        if (hitInfo.InNodeExpandButton)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InNodeExpandButton;
    //        if (hitInfo.InNodeImage)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InNodeImage;
    //        if (hitInfo.InNodeIndent)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InNodeIndent;
    //        if (hitInfo.InRow)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InRow;
    //        if (hitInfo.InRowCell)
    //            nowHitPosition = nowHitPosition | TreeListControlHitPosition.InRowCell;
    //        if ((HitPosition & nowHitPosition) != TreeListControlHitPosition.None)
    //        {
    //            ExecuteCommand();
    //        }
    //    }
    //}
    //public enum TreeListControlHitPosition
    //{
    //    None = 0,
    //    InBandPanel = 2,
    //    InColumnHeader = 4,
    //    InColumnPanel = 8,
    //    InFilterPanel = 16,
    //    InGroupColumn = 32,
    //    InGroupPanel = 64,
    //    InNodeCheckbox = 128,
    //    InNodeExpandButton = 256,
    //    InNodeImage = 512,
    //    InNodeIndent = 1024,
    //    InRow = 2048,
    //    InRowCell = 4096,
    //}
}
