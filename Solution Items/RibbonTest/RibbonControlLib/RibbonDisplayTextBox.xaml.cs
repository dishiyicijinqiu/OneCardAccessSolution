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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DNBSoft.WPF.RibbonControl
{
    /// <summary>
    /// Interaction logic for RibbonDisplayTextBox.xaml
    /// </summary>
    public partial class RibbonDisplayTextBox : RibbonControlBase, IRibbonFullControl
    {
        public RibbonDisplayTextBox()
        {
            InitializeComponent();

            this.KeyboardAccessVerticalOffset = 58;
            contentTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        public String ContentText
        {
            get
            {
                return contentTextBox.Text.ToString();
            }
            set
            {
                contentTextBox.Text = value;
            }
        }
    }
}
