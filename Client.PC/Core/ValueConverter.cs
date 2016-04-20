using DevExpress.Xpf.Grid;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

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


    [ValueConversion(typeof(EntityEditMode), typeof(bool))]
    public class EditModeVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EntityEditMode re = (EntityEditMode)value;
            switch (re)
            {
                case EntityEditMode.Add:
                case EntityEditMode.CopyAdd:
                    return true;
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
