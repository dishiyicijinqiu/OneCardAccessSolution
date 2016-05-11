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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;
using System.Windows.Media.Effects;
using DNBSoft.WPF;
using DNBSoft.WPF.RibbonControl;

namespace DNBSoft.WPF.RibbonControl
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RibbonPage : Page
    {
        private RibbonController ribbonController = null;
        private RibbonKeyboardAccessController keyboardController = null;
        private QuickAccessToolbar quickAccessToolbar = null;
        private ApplicationButton applicationButton = null;

        #region constructor / initialisation
        public RibbonPage()
        {
            try
            {
                InitializeComponent();

                ribbonController = new RibbonController(ribbonGrid, titleHook, this);

                #region window background
                GradientStopCollection gsc = new GradientStopCollection();
                gsc.Add(new GradientStop(Color.FromArgb(150, 150, 150, 150), 0)); //50 good for hover
                gsc.Add(new GradientStop(Color.FromArgb(150, 90, 90, 90), 0.5));
                //gsc.Add(new GradientStop(Color.FromRgb(0, 255, 0), 0.7));
                gsc.Add(new GradientStop(Color.FromArgb(150, 158, 158, 158), 1));

                LinearGradientBrush lgb = new LinearGradientBrush(gsc, 48.0);
                backgroundBorder.Background = lgb;
                #endregion

                #region setup window resizing
                //WindowResizer wr = new WindowResizer(this);
                //wr.addResizerRight(rightSizeGrip);
                //wr.addResizerLeft(leftSizeGrip);
                //wr.addResizerUp(topSizeGrip);
                //wr.addResizerDown(bottomSizeGrip);
                //wr.addResizerLeftUp(topLeftSizeGrip);
                //wr.addResizerRightUp(topRightSizeGrip);
                //wr.addResizerLeftDown(bottomLeftSizeGrip);
                //wr.addResizerRightDown(bottomRightSizeGrip);
                #endregion

                #region styling
                RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
                RibbonStyleHandler_StyleChanged(null);
                #endregion

                #region setup application button
                applicationButton = new ApplicationButton();
                //b.Image = new BitmapImage(new Uri(RibbonFileLocations.RibbonBasePath + @"\\Standard Icons\open.png", UriKind.RelativeOrAbsolute));
                //b.BaseColor = Color.FromRgb(153, 171, 198);
                applicationButton.BaseColor = ((SolidColorBrush)RibbonStyleHandler.GroupText).Color;
                applicationButton.HighlightColor = ((LinearGradientBrush)RibbonStyleHandler.RibbonBarBackground).GradientStops[0].Color;
                applicationButtonHook.Children.Add(applicationButton);
                #endregion

                #region setup QAT
                //<!-- quick access toolbar -->
                //                    <!-- qat:QuickAccessToolbar Name="qatoolbar" Grid.Column="0" 
                //                             Margin="40,0,0,0" DockPanel.Dock="Left"/ -->
                quickAccessToolbar = new QuickAccessToolbar();
                quickAccessToolbar.Name = "qatoolbar";
                quickAccessToolbar.Margin = new Thickness(40, 0, 0, 0);
                //qatoolbar.SetValue(DockPanel.DockProperty, "Left");
                Grid.SetColumn(quickAccessToolbar, 0);
                DockPanel.SetDock(quickAccessToolbar, Dock.Left);
                //titleBarDock.Children.Add(quickAccessToolbar);
                titleBarDock.Children.Insert(0, quickAccessToolbar);

                //qatoolbar.addQuickAccessButton(qatB1);
                //qatoolbar.addQuickAccessButton(qatB2);
                //qatoolbar.addQuickAccessButton(qatB3);


                quickAccessToolbar.resetToDefault(ribbonController);
                quickAccessToolbar.CustomiseQuickAccessToolbarEvent += new CustomiseQuickAccessToolbarDelegate(rc_CustomiseQuickAccessToolbarEvent);

                #region link ribbon to qatoolbar
                ribbonController.AddToQuickAccessToolbarEvent += new AddToQuickAccessToolbarDelegate(rc_AddToQuickAccessToolbarEvent);
                ribbonController.CustomiseQuickAccessToolbarEvent += new CustomiseQuickAccessToolbarDelegate(rc_CustomiseQuickAccessToolbarEvent);
                #endregion
                #endregion

                #region setup keyboard access
                keyboardController = new RibbonKeyboardAccessController((Page)this, ribbonController, quickAccessToolbar);
                //this.KeyDown += new KeyEventHandler(Window1_KeyDown);
                #endregion
            }
            catch (Exception err)
            {
                Console.WriteLine("ERROR: " + err.ToString());
            }
        }

        private void Window1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt || e.SystemKey == Key.F10)
            {
                keyboardController.showPrimaryKeyboardAccessPopups();
            }
        }

        private void rc_CustomiseQuickAccessToolbarEvent(object sender, CustomiseQuickAccessToolbarEventArgs args)
        {
            QuickAccessToolBarConfigurationWindow w = new QuickAccessToolBarConfigurationWindow(quickAccessToolbar, ribbonController);
            w.HelpIconSource = new BitmapImage(new Uri(RibbonFileLocations.RibbonBasePath + @"\\Graphics\help_icon.png", UriKind.RelativeOrAbsolute));
            w.ConfigIconSource = new BitmapImage(new Uri(RibbonFileLocations.RibbonBasePath + @"\\Graphics\config.png", UriKind.RelativeOrAbsolute));
            w.ShowDialog();
        }

        private void rc_AddToQuickAccessToolbarEvent(object sender, AddToQuickAccessToolbarEventArgs args)
        {
            RibbonControlBase rcb = (RibbonControlBase)sender;
            quickAccessToolbar.Buttons.Add(rcb.getQAButton());
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            RibbonStyleHandler.styleRibbonTitleBackground(ribbonTitleBackground);
            ribbonBorder.Background = RibbonStyleHandler.RibbonBarBackground.Clone();
            ribbonBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
            ribbonBorderWrapperGrid.Background = RibbonStyleHandler.RibbonTitleBackground.Clone();
            
        }

        /**
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Obtain the window handle for WPF application
                IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
                HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
                mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                // Get System Dpi
                System.Drawing.Graphics desktop = System.Drawing.Graphics.FromHwnd(mainWindowPtr);
                float DesktopDpiX = desktop.DpiX;
                float DesktopDpiY = desktop.DpiY;

                // Set Margins
                NonClientRegionAPI.MARGINS margins = new NonClientRegionAPI.MARGINS();

                // Extend glass frame into client area
                // Note that the default desktop Dpi is 96dpi. The  margins are
                // adjusted for the system Dpi.
                margins.cxLeftWidth = Convert.ToInt32(700 * (DesktopDpiX / 96));
                margins.cxRightWidth = Convert.ToInt32(700 * (DesktopDpiX / 96));
                margins.cyTopHeight = Convert.ToInt32(500 * (DesktopDpiX / 96));
                margins.cyBottomHeight = Convert.ToInt32(500 * (DesktopDpiX / 96));

                int hr = NonClientRegionAPI.DwmExtendFrameIntoClientArea(mainWindowSrc.Handle, ref margins);
                //
                if (hr < 0)
                {
                    //DwmExtendFrameIntoClientArea Failed
                }

                TitleBar.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                //backgroundBorder.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                //titleBackground.Background = new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
                //headerGrid.Background = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
                //buttonBorder.Background = new SolidColorBrush(Color.FromArgb(128, 138, 215, 255));
                //buttonBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                //contentBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                //contentBorder.Background = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255));
                //keyGrid
                //keyBorder.Background = new LinearGradientBrush(Color.FromArgb(128, 255, 255, 255), Color.FromArgb(128, 138, 215, 255), 90.0);
                //keyBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            }
            // If not Vista, paint background white.
            catch (DllNotFoundException)
            {
                //error
            }
        }
        **/
        #endregion

        #region standard title bar button handlers
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        private void maximiseButton_Click(object sender, RoutedEventArgs e)
        {
            /**
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
             **/
        }

        private void minimiseButton_Click(object sender, RoutedEventArgs e)
        {
            //this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region title bar click handers
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //this.DragMove();
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                /**
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
                 **/
            }
        }
        #endregion

        #region standard title bar button glow effects
        private void closeButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OuterGlowBitmapEffect ogbe = new OuterGlowBitmapEffect();
            ogbe.GlowColor = (Color)(ColorConverter.ConvertFromString("Red"));
            closeButton.BitmapEffect = ogbe;
        }

        private void closeButton_MouseLeave(object sender, MouseEventArgs e)
        {
            closeButton.BitmapEffect = null;
        }

        private void maximiseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OuterGlowBitmapEffect ogbe = new OuterGlowBitmapEffect();
            ogbe.GlowColor = (Color)(ColorConverter.ConvertFromString("Cyan"));
            maximiseButton.BitmapEffect = ogbe;
        }

        private void maximiseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            maximiseButton.BitmapEffect = null;
        }

        private void minimiseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OuterGlowBitmapEffect ogbe = new OuterGlowBitmapEffect();
            ogbe.GlowColor = (Color)(ColorConverter.ConvertFromString("Cyan"));
            minimiseButton.BitmapEffect = ogbe;
        }

        private void minimiseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            minimiseButton.BitmapEffect = null;
        }
        #endregion

        #region restyle buttons
        private void blueButton_Click(object sender, RoutedEventArgs e)
        {
            RibbonStyleHandler.reStyle(RibbonStyleHandler.Style.Blue);
        }

        private void blackButton_Click(object sender, RoutedEventArgs e)
        {
            RibbonStyleHandler.reStyle(RibbonStyleHandler.Style.Black);
        }

        private void grayButton_Click(object sender, RoutedEventArgs e)
        {
            RibbonStyleHandler.reStyle(RibbonStyleHandler.Style.Gray);
        }

        private void reenButton_Click(object sender, RoutedEventArgs e)
        {
            RibbonStyleHandler.reStyle(RibbonStyleHandler.Style.Green);
        }
        #endregion

        private void toggleMinimise_Click(object sender, RoutedEventArgs e)
        {
            //font.Minimised = !font.Minimised;
        }

        #region assessors
        public ApplicationButton ApplicationButton
        {
            get
            {
                return applicationButton;
            }
        }

        public RibbonController RibbonController
        {
            get
            {
                return ribbonController;
            }
        }

        public QuickAccessToolbar QuickAccessToolbar
        {
            get
            {
                return quickAccessToolbar;
            }
        }

        public new UIElement Content
        {
            get
            {
                if (contentPanel.Children.Count == 1)
                {
                    return contentPanel.Children[0];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                contentPanel.Children.Clear();
                contentPanel.Children.Add(value);
            }
        }

        public Brush ContentBackground
        {
            get
            {
                return contentPanel.Background;
            }
            set
            {
                contentPanel.Background = value;
            }
        }

        public new String Title
        {
            get
            {
                return base.Title;
            }
            set
            {
                base.Title = value;
                this.titleLabel.Content = value;
            }
        }
        #endregion
    }
}
