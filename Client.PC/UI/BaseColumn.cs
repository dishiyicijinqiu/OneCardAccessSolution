using DevExpress.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.OneCardAccess.Client.PC.UI
{
    public class ComboBoxColumn : BaseColumn
    {
        public IList Source { get; set; }
    }
    public class BaseColumn
    {
        public string FieldName { get; set; }
        public string Header { get; set; }
        public string HeaderToolTip { get; set; }
        bool _Visible = true;
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }
        int _VisibleIndex = 0;
        public int VisibleIndex
        {
            get { return _VisibleIndex; }
            set { _VisibleIndex = value; }
        }
        double _Width = 100;
        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public string DisplayFormat { get; set; }
        public SettingsType Settings { get; set; }
    }
    public enum SettingsType { Default, CheckEdit, Date, Combobox, Image }

    public class BaseSummary
    {
        public SummaryItemType Type { get; set; }
        public string FieldName { get; set; }
    }

    public class ColumnTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            BaseColumn column = (BaseColumn)item;
            if (column == null) return base.SelectTemplate(item, container);
            return (DataTemplate)((Control)container).FindResource(column.Settings + "ColumnTemplate");
        }
    }
}
