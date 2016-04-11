using DevExpress.Xpf.Ribbon;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.View
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
    }

    public enum GroupAlignment { Left, Right }
    public class MyRibbonPageGroupsItemsPanel : RibbonPageGroupsItemsPanel
    {
        public static readonly DependencyProperty GroupAlignmentProperty = DependencyProperty.RegisterAttached("GroupAlignment", typeof(GroupAlignment), typeof(MyRibbonPageGroupsItemsPanel), new UIPropertyMetadata(GroupAlignment.Left));

        public static GroupAlignment GetGroupAlignment(DependencyObject target)
        {
            return (GroupAlignment)target.GetValue(GroupAlignmentProperty);
        }
        public static void SetGroupAlignment(DependencyObject target, GroupAlignment value)
        {
            target.SetValue(GroupAlignmentProperty, value);
        }

        protected override Size ArrangeOrderedChildren(RibbonOrderedItemInfo[] items, Size finalSize)
        {
            Rect rect = new Rect();
            Rect rect2 = new Rect();
            rect2.X = finalSize.Width;
            for (int i = 0; i < items.Length; i++)
            {
                UIElement child = items[i].Item as UIElement;
                Size sz = new Size(child.DesiredSize.Width, Math.Max(child.DesiredSize.Height, finalSize.Height));
                rect.Size = sz;
                rect2.Size = sz;
                RibbonPageGroupControl content = (RibbonPageGroupControl)child;
                GroupAlignment align = GetGroupAlignment(content.PageGroup);
                if (align == GroupAlignment.Left)
                {
                    child.Arrange(rect);
                    rect.X += rect.Width;
                }
                else
                {
                    rect2.X -= child.DesiredSize.Width;
                    child.Arrange(rect2);
                }
            }
            return finalSize;
        }
    }
    public class MyRibbonPagesItemsPanel : RibbonPagesItemsPanel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in Children)
            {
                RibbonPageGroupsControl groupsControl = child as RibbonPageGroupsControl;
                if (groupsControl.Page.IsSelected)
                {
                    child.Opacity = 1;
                    child.IsHitTestVisible = true;
                }
                else
                {
                    child.Opacity = 0;
                    child.IsHitTestVisible = false;
                }
                child.Arrange(new Rect(0, 0, Math.Max(child.DesiredSize.Width, finalSize.Width), finalSize.Height));
            }
            return finalSize;
        }
    }
}
