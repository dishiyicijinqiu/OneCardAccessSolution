using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// P_CRTempCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class P_CRTempCollectionView : BaseUserControl, IP_CRTempCollectionView
    {
        public P_CRTempCollectionView(P_CRTempCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public P_CRTempCollectionView()
        {
            InitializeComponent();
        }
    }
    public class P_CRTempCollectionViewViewOrEditConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var grid = (DevExpress.Xpf.Grid.GridControl)values[0];
            ViewStyle style = (ViewStyle)values[1];
            switch (style)
            {
                case ViewStyle.OneSelect:
                case ViewStyle.MulSelect:
                    return grid.FindResource("ConfirmCommand");
                default:
                    return grid.FindResource("EditCommand");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    public class P_CRTempCollectionViewViewStyleConverter : MarkupExtension, System.Windows.Data.IValueConverter
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
    public class P_CRTempCollectionViewSelectionModeConverter : MarkupExtension, System.Windows.Data.IValueConverter
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
