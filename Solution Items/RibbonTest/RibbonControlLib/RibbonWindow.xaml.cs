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

//------------------------------------------------------------------------------//
//                                                                              //
// Author:  Derek Bartram                                                       //
// Date:    23/01/2008                                                          //
// Version: 1.000                                                               //
// Website: http://www.derek-bartram.co.uk                                      //
// Email:   webmaster@derek-bartram.co.uk                                       //
//                                                                              //
// This code is provided on a free to use and/or modify basis for personal work //
// provided that this banner remains in each of the source code files that is   //
// found in the original source. For any publicically available work (source    //
// and/or binaries 'Derek Bartram' and 'http://www.derek-bartram.co.uk' must be //
// credited in both the user documentation, source code (where applicable), and //
// in the user interface (typically Help > About would be appropiate). Please   //
// also contact myself via the provided email address to let me know where and  //
// what my code is being used for; this helps me provide better solutions for   //
// all.                                                                         //
//                                                                              //
// THIS SOURCE AND/OR COMPILED LIBRARY MUST NOT BE USED FOR COMMERCIAL WORK,    //
// including not-for-profit work, without prior consent.                        //
//                                                                              //
// This agreement overrides any other agreements made by any other parties. By  //
// using, viewing, linking, or compiling the included source or binaries you    //
// agree to the terms and conditions as set out here and in any included (if    //
// applicable) license.txt. For commercial licensing please see the web address //
// above or contact myself via email. Thank you.                                //
//                                                                              //
// Please contact me at the above email for further help, information,          //
// comments, suggestions, licensing, or feature requests. Thank you.            //
//                                                                              //
//                                                                              //
//------------------------------------------------------------------------------//

namespace DNBSoft.WPF.RibbonControl
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RibbonWindow : Window
    {
        private RibbonController ribbonController = null;
        private RibbonKeyboardAccessController keyboardController = null;
        private QuickAccessToolbar quickAccessToolbar = null;
        private ApplicationButton applicationButton = null;
        private ApplicationMenuPopup applicationPopup = null;

        #region constructor / initialisation
        public RibbonWindow()
        {
            InitializeComponent();

            ribbonController = new RibbonController(ribbonGrid, titleHook, this);

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
            applicationButton.Clicked += new MouseEventHandler(applicationButton_Clicked);
            applicationButtonHook.Children.Add(applicationButton);

            applicationPopup = new ApplicationMenuPopup();
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

            
            quickAccessToolbar.resetToDefault(ribbonController);
            quickAccessToolbar.CustomiseQuickAccessToolbarEvent += new CustomiseQuickAccessToolbarDelegate(rc_CustomiseQuickAccessToolbarEvent);

            #region link ribbon to qatoolbar
            ribbonController.AddToQuickAccessToolbarEvent += new AddToQuickAccessToolbarDelegate(rc_AddToQuickAccessToolbarEvent);
            ribbonController.CustomiseQuickAccessToolbarEvent += new CustomiseQuickAccessToolbarDelegate(rc_CustomiseQuickAccessToolbarEvent);
            #endregion
            #endregion

            #region setup keyboard access
            keyboardController = new RibbonKeyboardAccessController((Window)this, ribbonController, quickAccessToolbar);
            //this.KeyDown += new KeyEventHandler(Window1_KeyDown);
            #endregion
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
            w.ConfigIconSource = new BitmapImage(new Uri(RibbonFileLocations.RibbonBasePath + @"\Graphics\config.png", UriKind.RelativeOrAbsolute));
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
                margins.cxLeftWidth = Convert.ToInt32(0 * (DesktopDpiX / 96)); //700
                margins.cxRightWidth = Convert.ToInt32(0 * (DesktopDpiX / 96));
                margins.cyTopHeight = Convert.ToInt32(28 * (DesktopDpiX / 96)); //500
                margins.cyBottomHeight = Convert.ToInt32(0 * (DesktopDpiX / 96));

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
        #endregion

        #region standard title bar button handlers
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void maximiseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;

                //fix task bar problem
                //this.WindowState = WindowState.Normal;
                //this.Height = SystemParameters.MaximizedPrimaryScreenHeight;
                //this.Width = SystemParameters.MaximizedPrimaryScreenWidth;
                //this.Top = 0;
                //this.Left = 0;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void minimiseButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public new WindowState WindowState
        {
            get
            {
                return base.WindowState;
            }
            set
            {
                base.WindowState = value;

                if (this.WindowState == WindowState.Maximized)
                {
                    titleLabel.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    titleLabelEffect.GlowSize = 0;
                }
                else
                {
                    titleLabel.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    titleLabelEffect.GlowSize = 5;
                }
            }
        }
        #endregion

        #region title bar click handers
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
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

        public ApplicationMenuPopup ApplicationMenu
        {
            get
            {
                return applicationPopup;
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

        #region application menu handlers


        private void applicationButton_Clicked(object sender, MouseEventArgs e)
        {
            applicationPopup.IsOpen = true;
            applicationPopup.PlacementTarget = this;
            applicationPopup.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
            applicationPopup.VerticalOffset = 3;
            applicationPopup.HorizontalOffset = 0;
            applicationPopup.updateApplicationButton(this.ApplicationButton);
        }
        #endregion
    }
}
