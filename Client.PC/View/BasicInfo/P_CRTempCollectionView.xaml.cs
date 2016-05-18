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
    public class ViewOrEditConverter : MarkupExtension, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var grid = (DevExpress.Xpf.Grid.GridControl)values[0];
            CollectionViewStyle style = (CollectionViewStyle)values[1];
            switch (style)
            {
                case CollectionViewStyle.CollectionOneSelect:
                case CollectionViewStyle.CollectionMulSelect:
                    return grid.FindResource("ConfirmCommand");
                default:
                    return grid.FindResource("EditCommand");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    var element = parameter as System.Windows.Controls.DataGrid;
        //    CollectionViewStyle style = (CollectionViewStyle)value;
        //    switch (style)
        //    {
        //        case CollectionViewStyle.CollectionOneSelect:
        //        case CollectionViewStyle.CollectionMulSelect:
        //            return element.FindResource("ConfirmCommand");
        //        default:
        //            return element.FindResource("EditCommand");
        //    }
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
