using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.IO;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for DXWindow1.xaml
    /// </summary>
    public partial class DXWindow1 : DXWindow
    {
        public DXWindow1()
        {
            InitializeComponent();
            var data = new UserInfoEntity()
            {
                UserId = 0,
                UserId1 = 1,
                UserId2 = 2,
                UserId3 = 3,
                UserId4 = 4,
                UserId5 = 5,
                Scores = new List<int>() { 1, 2, 3, 4, 5, 6 }
            };
            //this.datalayoutcontrol.CurrentItem = data;
            this.grid.ItemsSource = data.Scores;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //Save the layout
        //    XmlWriter writer = XmlWriter.Create(@"D:\1.xml");
        //    datalayoutcontrol.WriteToXML(writer);
        //    writer.Close();
        //}
    }


    public partial class UserInfoEntity
    {
        //DataLayoutControl:AutoGenerateField，Name，Order，GroupName,Description
        //DataGrid:AutoGenerateField，Description，Order，Name，ShortName
        //AutoGenerateField，Description，GroupName，Order，Name，ShortName

        [Display(GroupName = "[A]", Description = "你好世界")]
        public List<int> Scores { get; set; }
        [Display(GroupName = "[A]/<A>", Description = "你好世界")]
        public int UserId { get; set; }
        [Display(GroupName = "[A]/<A>", Description = "你好世界")]
        public int UserId1 { get; set; }
        [Display(GroupName = "[A]/<B>", Description = "你好世界")]
        public int UserId2 { get; set; }
        [Display(GroupName = "[A]/<B>", Description = "你好世界")]
        public int UserId3 { get; set; }
        [Display(GroupName = "[A]/<B>/[E]", Description = "你好世界")]
        public int UserId4 { get; set; }
        [Display(GroupName = "[A]", Description = "你好世界")]
        public int UserId5 { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class WidthAttribute : Attribute
    {
        public WidthAttribute(double Width = 100)
        {
            this.Width = Width;

        }
        public double Width { get; set; }
    }
}
