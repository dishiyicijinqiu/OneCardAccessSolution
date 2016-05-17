using DevExpress.Xpf.Grid;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Linq;
using System.Windows;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace FengSharp.OneCardAccess.Core
{

    public class RowHandleConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var grid = (GridControl)values[0];
            var rowHandle = (int)values[1];
            var result = grid.GetListIndexByRowHandle(rowHandle);
            return rowHandle >= 0 ? string.Format("{0}", result + 1) : string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CateRowHandleConverter : MarkupExtension, IMultiValueConverter
    {
        private string _CateField = "IsFather";
        public string CateField
        {
            get { return _CateField; }
            set
            {
                if (_CateField != value)
                    _CateField = value;
            }
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var grid = (GridControl)values[0];
            var rowHandle = (int)values[1];
            var t = grid.GetRow(rowHandle);
            var p = t.GetType().GetProperty(CateField);
            var result = grid.GetListIndexByRowHandle(rowHandle);
            if (p == null) return rowHandle >= 0 ? string.Format("{0}", result + 1) : string.Empty;
            var val = (bool)p.GetValue(t, null);
            return result >= 0 ? string.Format("{0}{1}", result + 1, val ? ".." : string.Empty) : string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ListCastConverter : MarkupExtension, IValueConverter//TODO:不明白为什么Command中接受参数的数量是0
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string CastType = (string)parameter;
            if (string.IsNullOrWhiteSpace(CastType))
                return null;
            Type type = System.Type.GetType(CastType);
            MethodInfo mi = typeof(Enumerable).GetMethod("Cast");
            MethodInfo mi2 = mi.MakeGenericMethod(new Type[] { type });
            var enumerables = mi2.Invoke(value, new object[] { value });
            MethodInfo mi3 = typeof(Enumerable).GetMethod("ToList");
            MethodInfo mi4 = mi3.MakeGenericMethod(new Type[] { type });
            return mi4.Invoke(enumerables, new object[] { enumerables });
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

    public class StringDateValueConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = DateTime.Now;
            DateTime.TryParse((string)value, out date);
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("yyyy-MM-dd");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class CollectionViewVisibleConverter : MarkupExtension, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CollectionViewStyle style = (CollectionViewStyle)value;
            return style == CollectionViewStyle.CollectionView;
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
    public class CollectionSelectVisibleConverter : MarkupExtension, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CollectionViewStyle style = (CollectionViewStyle)value;
            return style != CollectionViewStyle.CollectionView;
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
    public class CollectionSelectionModeConverter : MarkupExtension, System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CollectionViewStyle style = (CollectionViewStyle)value;
            switch (style)
            {
                case CollectionViewStyle.CollectionOneSelect:
                    return DevExpress.Xpf.Grid.MultiSelectMode.None;
                case CollectionViewStyle.CollectionView:
                    return DevExpress.Xpf.Grid.MultiSelectMode.Row;
                case CollectionViewStyle.CollectionMulSelect:
                    return DevExpress.Xpf.Grid.MultiSelectMode.MultipleRow;
                default:
                    return DevExpress.Xpf.Grid.MultiSelectMode.Row;
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
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
            {
                return DependencyProperty.UnsetValue;
            }
            if (Enum.IsDefined(value.GetType(), value) == false)
            {
                return DependencyProperty.UnsetValue;
            }
            object parameterValue = Enum.Parse(value.GetType(), parameterString, true);
            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;
            return Enum.Parse(targetType, parameterString, true);
        }
    }
}
