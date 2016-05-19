using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Core;
using System;
using System.Globalization;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : BaseUserControl, IRegisterCollectionView
    {
        public RegisterCollectionView(RegisterCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public RegisterCollectionView()
        {
            InitializeComponent();
        }
    }

    public class RegisterCollectionViewViewStyleConverter : MarkupExtension, System.Windows.Data.IValueConverter
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
                return (ViewStyle.View == ViewStyle.OneSelect)|(ViewStyle.View == ViewStyle.MulSelect);
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
    public class RegisterCollectionViewSelectionModeConverter : MarkupExtension, System.Windows.Data.IValueConverter
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
}
