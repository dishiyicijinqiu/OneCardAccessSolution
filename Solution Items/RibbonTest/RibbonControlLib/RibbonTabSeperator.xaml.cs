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
    /// Interaction logic for RibbonTabSeperator.xaml
    /// </summary>
    public partial class RibbonTabSeperator : UserControl
    {
        private bool isVisible = false;

        public RibbonTabSeperator()
        {
            InitializeComponent();
            this.IsVisible = false;

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0));
            gsc.Add(new GradientStop(RibbonStyleHandler.ButtonBorderNormal, 0.8));
            gsc.Add(new GradientStop(RibbonStyleHandler.ButtonBorderNormal, 1.0));
            LinearGradientBrush lgb = new LinearGradientBrush(gsc, 90.0);
            theBorder.BorderBrush = lgb;
        }

        public new bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;

                if (isVisible)
                {
                    this.Width = 3;
                }
                else
                {
                    this.Width = 0;
                }
            }
        }
    }
}
