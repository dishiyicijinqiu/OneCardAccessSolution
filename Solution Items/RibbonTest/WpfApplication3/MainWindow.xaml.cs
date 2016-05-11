using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UserInfoEntity> list = new List<UserInfoEntity>();
        public MainWindow()
        {
            InitializeComponent();
            this.grid.AutoGeneratingColumn += Grid_AutoGeneratingColumn;
            for (int i = 0; i < 20; i++)
            {
                var entity = new UserInfoEntity()
                {
                    UserId = 999 + i,
                    UserId1 = i,
                    UserId2 = i,
                    UserId3 = i,
                    UserId4 = i,
                    UserId5 = i,
                };
                list.Add(entity);
            }
            this.grid.ItemsSource = list;
        }

        private void Grid_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Column.FieldName)) return;
            var itemsourcetype = this.grid.ItemsSource.GetType();
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
