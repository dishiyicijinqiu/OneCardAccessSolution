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
    /// Interaction logic for RibbonDisplayImage.xaml
    /// </summary>
    public partial class RibbonDisplayImage : RibbonControlBase, IRibbonFullControl
    {
        public RibbonDisplayImage()
        {
            InitializeComponent();
            this.KeyboardAccessVerticalOffset = 58;

            hasQATbutton = false;
        }

        public override ImageSource NormalImage
        {
            get
            {
                return base.NormalImage;
            }
            set
            {
                base.NormalImage = value;
                theImage.Source = value;
            }
        }
    }
}
