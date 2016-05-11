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
using System.Threading;

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
    /// Interaction logic for ApplicationMenuButton.xaml
    /// </summary>
    public partial class ApplicationMenuButton : UserControl
    {
        public event MouseButtonEventHandler Clicked;
        private ApplicationMenuButtonPopup popupMenu = null;
        private bool mouseInside = false;
        //private bool mouseInsideArrow = false;
        private delegate void RefreshDelegate();

        public ApplicationMenuButton()
        {
            InitializeComponent();

            List<GradientStop> transparentStops = new List<GradientStop>();
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 255, 255, 255), 0));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.647));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 255, 255, 255), 0.6471));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 255, 255, 255), 1));
            RibbonStyleHandler.styleButtonBorder(masterBorder, this, this, Color.FromArgb(0, 255, 255,255), transparentStops);
            RibbonStyleHandler.styleButtonBorder(arrowBorder, this, this, Color.FromArgb(0, 255, 255, 255), transparentStops);
            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);

            this.ShowDivider = false;
            this.ShowPopupArrow = false;
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            theLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            arrowLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
        }

        public String Text
        {
            get
            {
                return theLabel.Content.ToString();
            }
            set
            {
                if (value != null)
                {
                    theLabel.Content = value;
                }
                else
                {
                    theLabel.Content = "";
                }
            }
        }

        public ImageSource Image
        {
            get
            {
                return theImage.Source;
            }
            set
            {
                theImage.Source = value;
            }
        }

        public bool ShowDivider
        {
            get
            {
                return dividerBorder.BorderThickness.Top == 1;
            }
            set
            {
                if (value)
                {
                    dividerBorder.BorderThickness = new Thickness(1);
                }
                else
                {
                    dividerBorder.BorderThickness = new Thickness(0);
                }
            }
        }

        public bool ShowPopupArrow
        {
            get
            {
                return arrowBorder.Height == 42;
            }
            set
            {
                if (value)
                {
                    arrowBorder.Height = 42;
                }
                else
                {
                    arrowBorder.Height = 0;
                }
            }
        }

        public ApplicationMenuButtonPopup PopupMenu
        {
            get
            {
                return popupMenu;
            }
            set
            {
                if (popupMenu != null)
                {
                    popupMenu.MouseLeave -= new MouseEventHandler(popupMenu_MouseLeave);
                }
                popupMenu = value;
                if (popupMenu != null)
                {
                    popupMenu.MouseLeave += new MouseEventHandler(popupMenu_MouseLeave);
                }
            }
        }

        private void masterBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(this, e);
            }
        }

        private void arrowBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;

            if (popupMenu != null && this.Parent != null)
            {
                popupMenu.PlacementTarget = (UIElement)this.Parent;
                popupMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                popupMenu.Height = ((StackPanel)this.Parent).ActualHeight - 5;
                popupMenu.HorizontalOffset = 115;
                popupMenu.VerticalOffset = 2;
                popupMenu.IsOpen = true;
            }
        }

        #region handle auto show/hide of popup
        private void masterBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseInside = false;
            if (popupMenu != null && popupMenu.IsOpen == true && popupMenu.IsMouseInside != true)
            {
                //Console.WriteLine("Test Close: " + mouseInside.ToString() + ", " + popupMenu.IsOpen.ToString());
                popupMenu.IsOpen = false;
            }
        }

        private void popupMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            Thread t = new Thread(delegate()
            {
                try
                {
                    Thread.Sleep(100);
                }
                catch (Exception)
                {
                }

                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new RefreshDelegate(delegate()
                {
                    if (mouseInside != true && popupMenu != null && popupMenu.IsOpen == true)
                    {
                        popupMenu.IsOpen = false;
                    }
                }));
            });
            t.Name = "Auto Close Popup Thread";
            t.Priority = ThreadPriority.Normal;
            t.Start();
        }

        private void masterBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseInside = true;
        }

        private void arrowBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            //mouseInsideArrow = true;

            if (popupMenu != null && popupMenu.IsOpen == false)
            {
                Thread t = new Thread(delegate()
                {
                    try
                    {
                        Thread.Sleep(200);
                    }
                    catch (Exception)
                    {
                    }

                    this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new RefreshDelegate(delegate()
                    {
                        if (arrowBorder.IsMouseOver && popupMenu.IsOpen == false && this.Parent != null)
                        {
                            popupMenu.PlacementTarget = (UIElement)this.Parent;
                            popupMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                            popupMenu.Height = ((StackPanel)this.Parent).ActualHeight - 5;
                            popupMenu.HorizontalOffset = 115;
                            popupMenu.VerticalOffset = 2;
                            popupMenu.IsOpen = true;
                        }
                    }));
                });
                t.Name = "Auto Open PopupMenu Thread";
                t.Start();
            }
        }

        private void arrowBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            //mouseInsideArrow = false;
        }
        #endregion
    }
}
