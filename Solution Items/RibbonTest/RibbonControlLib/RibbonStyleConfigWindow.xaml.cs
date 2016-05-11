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
    /// Interaction logic for RibbonStyleConfigWindow.xaml
    /// </summary>
    public partial class RibbonStyleConfigWindow : Window
    {
        public RibbonStyleConfigWindow()
        {
            InitializeComponent();

            #region setup styling
            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);
            #endregion
        }

        #region accesors
        public ImageSource HelpIconSource
        {
            get
            {
                //return chooseCommandsFromHelp.Source.Clone();
                return null;
            }
            set
            {
                //chooseCommandsFromHelp.Source = value.Clone();
                //customizeCommandsFromHelp.Source = value.Clone();
            }
        }

        public ImageSource ConfigIconSource
        {
            get
            {
                return configIconImage.Source.Clone();
            }
            set
            {
                configIconImage.Source = value;
            }
        }
        #endregion

        #region bottom button handlers
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Color primaryColor = ((SolidColorBrush)previewGrid.Background).Color;

            if (IsDarkColor(primaryColor))
            {
                #region button
                RibbonStyleHandler.ButtonNormalText = new SolidColorBrush(primaryColor);
                RibbonStyleHandler.ButtonBorderNormal = ModifyColor(primaryColor, 50, 50, 50);

                List<GradientStop> buttonBackgroundNormal = new List<GradientStop>();
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, 205, 205, 205), 0));
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, 210, 210, 210), 0.647));
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, 215, 215, 215), 0.6471));
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, 205, 205, 205), 1));
                RibbonStyleHandler.ButtonBackgroundNormal = buttonBackgroundNormal;
                #endregion

                #region group
                RibbonStyleHandler.GroupText = new SolidColorBrush(ModifyColor(primaryColor, 100, 100, 100));

                RibbonStyleHandler.GroupBorderNormal = ModifyColor(primaryColor, 150, 150, 150);
                RibbonStyleHandler.GroupLabelBorderNormal = ModifyColor(primaryColor, 150, 150, 150);

                List<GradientStop> groupLabelNormalBackground = new List<GradientStop>();
                groupLabelNormalBackground.Add(new GradientStop(ModifyColor(primaryColor, 150, 150, 150), 0));
                groupLabelNormalBackground.Add(new GradientStop(ModifyColor(primaryColor, 200, 200, 200), 1));
                RibbonStyleHandler.GroupLabelBackgroundNormal = groupLabelNormalBackground;

                List<GradientStop> groupLabelHoverBackground = new List<GradientStop>();
                groupLabelHoverBackground.Add(new GradientStop(ModifyColor(primaryColor, 200, 200, 200), 0));
                groupLabelHoverBackground.Add(new GradientStop(ModifyColor(primaryColor, 200, 200, 200), 1));
                RibbonStyleHandler.GroupLabelBackgroundHover = groupLabelHoverBackground;

                List<GradientStop> groupBackgroundHover = new List<GradientStop>();
                groupBackgroundHover.Add(new GradientStop(ModifyColor(primaryColor, 238, 238, 238), 0));
                groupBackgroundHover.Add(new GradientStop(ModifyColor(primaryColor, 230, 230, 230), 1));
                RibbonStyleHandler.GroupBackgroundHover = groupBackgroundHover;
                #endregion

                #region ribbon background
                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop(ModifyColor(primaryColor, 225, 225, 225), 0)); //219, 230, 244), 0));
                gsc.Add(new GradientStop(ModifyColor(primaryColor, 170, 170, 170), 0.333));
                gsc.Add(new GradientStop(ModifyColor(primaryColor, 185, 185, 185), 0.666));
                gsc.Add(new GradientStop(ModifyColor(primaryColor, 215, 215, 215), 1));//231, 242, 255), 1));
                RibbonStyleHandler.RibbonBarBackground = new LinearGradientBrush(gsc, 90.0);
                #endregion

                #region header background
                RibbonStyleHandler.RibbonTitleBackground = new SolidColorBrush(ModifyColor(primaryColor, 45, 45, 45));
                #endregion
            }
            else
            {
                #region button
                RibbonStyleHandler.ButtonNormalText = new SolidColorBrush(primaryColor);
                RibbonStyleHandler.ButtonBorderNormal = ModifyColor(primaryColor, -50, -50, -50);

                List<GradientStop> buttonBackgroundNormal = new List<GradientStop>();
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, -205, -205, -205), 0));
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, -210, -210, -210), 0.647));
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, -215, -215, -215), 0.6471));
                buttonBackgroundNormal.Add(new GradientStop(ModifyColor(primaryColor, -205, -205, -205), 1));
                RibbonStyleHandler.ButtonBackgroundNormal = buttonBackgroundNormal;
                #endregion

                #region group
                RibbonStyleHandler.GroupText = new SolidColorBrush(ModifyColor(primaryColor, -100, -100, -100));

                RibbonStyleHandler.GroupBorderNormal = ModifyColor(primaryColor, -150, -150, -150);
                RibbonStyleHandler.GroupLabelBorderNormal = ModifyColor(primaryColor, -150, -150, -150);

                List<GradientStop> groupLabelNormalBackground = new List<GradientStop>();
                groupLabelNormalBackground.Add(new GradientStop(ModifyColor(primaryColor, -150, -150, -150), 0));
                groupLabelNormalBackground.Add(new GradientStop(ModifyColor(primaryColor, -200, -200, -200), 1));
                RibbonStyleHandler.GroupLabelBackgroundNormal = groupLabelNormalBackground;

                List<GradientStop> groupLabelHoverBackground = new List<GradientStop>();
                groupLabelHoverBackground.Add(new GradientStop(ModifyColor(primaryColor, -200, -200, -200), 0));
                groupLabelHoverBackground.Add(new GradientStop(ModifyColor(primaryColor, -200, -200, -200), 1));
                RibbonStyleHandler.GroupLabelBackgroundHover = groupLabelHoverBackground;

                List<GradientStop> groupBackgroundHover = new List<GradientStop>();
                groupBackgroundHover.Add(new GradientStop(ModifyColor(primaryColor, -238, -238, -238), 0));
                groupBackgroundHover.Add(new GradientStop(ModifyColor(primaryColor, -230, -230, -230), 1));
                RibbonStyleHandler.GroupBackgroundHover = groupBackgroundHover;
                #endregion

                #region ribbon background
                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop(ModifyColor(primaryColor, -225, -225, -225), 0)); //219, 230, 244), 0));
                gsc.Add(new GradientStop(ModifyColor(primaryColor, -170, -170, -170), 0.333));
                gsc.Add(new GradientStop(ModifyColor(primaryColor, -185, -185, -185), 0.666));
                gsc.Add(new GradientStop(ModifyColor(primaryColor, -215, -215, -215), 1));//231, 242, 255), 1));
                RibbonStyleHandler.RibbonBarBackground = new LinearGradientBrush(gsc, 90.0);
                #endregion

                #region header background
                RibbonStyleHandler.RibbonTitleBackground = new SolidColorBrush(ModifyColor(primaryColor, -45, -45, -45));
                #endregion
            }

            RibbonStyleHandler.reStyle();
        }
        #endregion

        #region styling
        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            titleLabel.Foreground = RibbonStyleHandler.GroupText;
            ((LinearGradientBrush)row0Grid.Background).GradientStops[0].Color = RibbonStyleHandler.GroupLabelBackgroundNormal[0].Color;
        }
        #endregion

        #region get primary color
        private void setPrimaryColorButton_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            Color currentColor = ((SolidColorBrush)(previewGrid.Background)).Color;
            cd.Color = System.Drawing.Color.FromArgb(currentColor.R, currentColor.G, currentColor.B);

            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                previewGrid.Background = new SolidColorBrush(Color.FromRgb(cd.Color.R, cd.Color.G, cd.Color.B));
            }
        }
        #endregion

        #region statics 
        public static Color ModifyColor(Color inColor, int r_dx, int g_dx, int b_dx)
        {
            #region get component color
            int r = inColor.R;
            int g = inColor.G;
            int b = inColor.B;
            #endregion

            #region modify
            r += r_dx;
            g += g_dx;
            b += b_dx;
            #endregion

            #region clamp
            r = Math.Min(r, 255);
            g = Math.Min(g, 255);
            b = Math.Min(b, 255);

            r = Math.Max(r, 0);
            g = Math.Max(g, 0);
            b = Math.Max(b, 0);
            #endregion

            #region return
            return Color.FromRgb((byte)r, (byte)g, (byte)b);
            #endregion
        }

        public static bool IsDarkColor(Color inColor)
        {
            #region get component color
            int r = inColor.R;
            int g = inColor.G;
            int b = inColor.B;
            #endregion

            return !(r > 128 || g > 128 || b > 128);
        }
        #endregion
    }
}
