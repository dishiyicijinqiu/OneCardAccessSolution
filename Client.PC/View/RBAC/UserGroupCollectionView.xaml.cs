using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC;
using FengSharp.OneCardAccess.Core;
using System;
using System.Globalization;
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
