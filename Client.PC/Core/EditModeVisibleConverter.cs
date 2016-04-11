using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace FengSharp.OneCardAccess.Client.PC.Core
{
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
