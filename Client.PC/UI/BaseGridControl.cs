using DevExpress.Xpf.Grid;
using FengSharp.OneCardAccess.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    public class BaseGridControl : GridControl
    {
        public BaseGridControl()
        {
            this.AutoGeneratingColumn += BaseGridControl_AutoGeneratingColumn;
        }

        private void BaseGridControl_AutoGeneratingColumn(object sender, AutoGeneratingColumnEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Column.FieldName)) return;
            if (this.ItemsSource == null) return;
            var itemsourcetype = this.ItemsSource.GetType();
            var method = itemsourcetype.GetMethod("Add");
            if (method == null) return;
            var type = method.DeclaringType.GetGenericArguments()[0];
            var property = type.GetProperty(e.Column.FieldName);
            object[] objAttrs = property.GetCustomAttributes(typeof(WidthAttribute), true);
            foreach (var item in objAttrs)
            {
                WidthAttribute attr = item as WidthAttribute;
                if (attr != null)
                {
                    e.Column.Width = attr.Width;
                }
            }
        }
    }
}
