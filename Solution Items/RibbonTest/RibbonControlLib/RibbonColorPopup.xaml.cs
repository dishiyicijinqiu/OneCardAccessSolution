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
using System.Windows.Forms;

namespace DNBSoft.WPF.RibbonControl
{
    /// <summary>
    /// Interaction logic for RibbonColorPopup.xaml
    /// </summary>
    public partial class RibbonColorPopup : System.Windows.Controls.UserControl
    {
        private RibbonBorder selectedBorder = null;

        public RibbonColorPopup()
        {
            InitializeComponent();

            #region styling
            List<GradientStop> transparentStops = new List<GradientStop>();
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.647));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.6471));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 1));

            RibbonStyleHandler.styleButtonBorder(automaticBorder, this, this, Color.FromRgb(0, 0, 0), transparentStops);
            RibbonStyleHandler.styleButtonBorder(moreBorder, this, this, Color.FromRgb(0, 0, 0), transparentStops);

            #region theme colours
            RibbonStyleHandler.styleButtonBorder(t0, this, this, ((SolidColorBrush)t0.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t1, this, this, ((SolidColorBrush)t1.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t2, this, this, ((SolidColorBrush)t2.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t3, this, this, ((SolidColorBrush)t3.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t4, this, this, ((SolidColorBrush)t4.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t5, this, this, ((SolidColorBrush)t5.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t6, this, this, ((SolidColorBrush)t6.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t7, this, this, ((SolidColorBrush)t7.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t8, this, this, ((SolidColorBrush)t8.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t9, this, this, ((SolidColorBrush)t9.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t10, this, this, ((SolidColorBrush)t10.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t11, this, this, ((SolidColorBrush)t11.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t12, this, this, ((SolidColorBrush)t12.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t13, this, this, ((SolidColorBrush)t13.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t14, this, this, ((SolidColorBrush)t14.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t15, this, this, ((SolidColorBrush)t15.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t16, this, this, ((SolidColorBrush)t16.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t17, this, this, ((SolidColorBrush)t17.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t18, this, this, ((SolidColorBrush)t18.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t19, this, this, ((SolidColorBrush)t19.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t20, this, this, ((SolidColorBrush)t20.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t21, this, this, ((SolidColorBrush)t21.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t22, this, this, ((SolidColorBrush)t22.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t23, this, this, ((SolidColorBrush)t23.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t24, this, this, ((SolidColorBrush)t24.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t25, this, this, ((SolidColorBrush)t25.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t26, this, this, ((SolidColorBrush)t26.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t27, this, this, ((SolidColorBrush)t27.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t28, this, this, ((SolidColorBrush)t28.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t29, this, this, ((SolidColorBrush)t29.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t30, this, this, ((SolidColorBrush)t30.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t31, this, this, ((SolidColorBrush)t31.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t32, this, this, ((SolidColorBrush)t32.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t33, this, this, ((SolidColorBrush)t33.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t34, this, this, ((SolidColorBrush)t34.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t35, this, this, ((SolidColorBrush)t35.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t36, this, this, ((SolidColorBrush)t36.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t37, this, this, ((SolidColorBrush)t37.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t38, this, this, ((SolidColorBrush)t38.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t39, this, this, ((SolidColorBrush)t39.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t40, this, this, ((SolidColorBrush)t40.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t41, this, this, ((SolidColorBrush)t41.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t42, this, this, ((SolidColorBrush)t42.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t43, this, this, ((SolidColorBrush)t43.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t44, this, this, ((SolidColorBrush)t44.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t45, this, this, ((SolidColorBrush)t45.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t46, this, this, ((SolidColorBrush)t46.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t47, this, this, ((SolidColorBrush)t47.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t48, this, this, ((SolidColorBrush)t48.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t49, this, this, ((SolidColorBrush)t49.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t50, this, this, ((SolidColorBrush)t50.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t51, this, this, ((SolidColorBrush)t51.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t52, this, this, ((SolidColorBrush)t52.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t53, this, this, ((SolidColorBrush)t53.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t54, this, this, ((SolidColorBrush)t54.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t55, this, this, ((SolidColorBrush)t55.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t56, this, this, ((SolidColorBrush)t56.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t57, this, this, ((SolidColorBrush)t57.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t58, this, this, ((SolidColorBrush)t58.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(t59, this, this, ((SolidColorBrush)t59.Background).Color, transparentStops);
            #endregion

            #region standard colours
            RibbonStyleHandler.styleButtonBorder(s0, this, this, ((SolidColorBrush)s0.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s1, this, this, ((SolidColorBrush)s1.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s2, this, this, ((SolidColorBrush)s2.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s3, this, this, ((SolidColorBrush)s3.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s4, this, this, ((SolidColorBrush)s4.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s5, this, this, ((SolidColorBrush)s5.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s6, this, this, ((SolidColorBrush)s6.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s7, this, this, ((SolidColorBrush)s7.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s8, this, this, ((SolidColorBrush)s8.Background).Color, transparentStops);
            RibbonStyleHandler.styleButtonBorder(s9, this, this, ((SolidColorBrush)s9.Background).Color, transparentStops);
            #endregion

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);
            #endregion
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            automaticLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            themeLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            standardLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            moreLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            themeHeaderBackground.Background = new SolidColorBrush(RibbonStyleHandler.GroupBackgroundHover[0].Color);
            standardHeaderBackground.Background = new SolidColorBrush(RibbonStyleHandler.GroupBackgroundHover[0].Color);
        }

        private void colorGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (selectedBorder != null)
            {
                selectedBorder.IsSelected = false;
                //selectedBorder.IsEnabled = true;
            }

            selectedBorder = (RibbonBorder)((Grid)sender).Parent;
            selectedBorder.IsSelected = true;
            //selectedBorder.IsEnabled = false;
        }

        private void moreLabel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
