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
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ApplicationMenuPopup.xaml
    /// </summary>
    public partial class ApplicationMenuPopup : UserControl
    {
        private ListenableList<ApplicationMasterButton> masterApplicationButtons = new ListenableList<ApplicationMasterButton>();
        private ListenableList<ApplicationMenuButton> menuButtons = new ListenableList<ApplicationMenuButton>();
        private ListenableList<ApplicationDocument> documentButtons = new ListenableList<ApplicationDocument>();
        private Popup parentPopup;
        private bool keepShowing = true;
        private bool insideControl = false;

        private delegate void RefreshDelegate();

        public ApplicationMenuPopup()
        {
            InitializeComponent();

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);

            masterApplicationButtons.ElementAdded += new ListenableList<ApplicationMasterButton>.ElementAddedDelegate(masterApplicationButtons_ElementAdded);
            masterApplicationButtons.ElementRemoved += new ListenableList<ApplicationMasterButton>.ElementRemovedDelegate(masterApplicationButtons_ElementRemoved);
            menuButtons.ElementAdded += new ListenableList<ApplicationMenuButton>.ElementAddedDelegate(menuButtons_ElementAdded);
            menuButtons.ElementRemoved += new ListenableList<ApplicationMenuButton>.ElementRemovedDelegate(menuButtons_ElementRemoved);
            documentButtons.ElementAdded += new ListenableList<ApplicationDocument>.ElementAddedDelegate(documentButtons_ElementAdded);
            documentButtons.ElementRemoved += new ListenableList<ApplicationDocument>.ElementRemovedDelegate(documentButtons_ElementRemoved);

            ApplicationButton ab = new ApplicationButton();
            ab.HorizontalAlignment = HorizontalAlignment.Left;
            ab.VerticalAlignment = VerticalAlignment.Top;
            ab.Margin = new Thickness(3, 0, 0, 0);
            ab.Clicked += new MouseEventHandler(ab_Clicked);
            
            applicationButtonPlacement.Children.Add(ab);

            this.MouseEnter += new MouseEventHandler(ApplicationMenuPopup_MouseEnter);
            this.MouseLeave += new MouseEventHandler(ApplicationMenuPopup_MouseLeave);
        }

        #region add / remove from gui
        private void documentButtons_ElementRemoved(ListenableList<ApplicationDocument> sender, ListenableList<ApplicationDocument>.ElementRemovedEventArgs<ApplicationDocument> args)
        {
            documentsStack.Children.RemoveAt(args.Index + 1);
            args.Item.Clicked -= new MouseButtonEventHandler(Item_Clicked);
        }

        private void documentButtons_ElementAdded(ListenableList<ApplicationDocument> sender, ListenableList<ApplicationDocument>.ElementAddedEventArgs<ApplicationDocument> args)
        {
            documentsStack.Children.Insert(args.Index + 1, args.Item);
            args.Item.Clicked += new MouseButtonEventHandler(Item_Clicked);
        }

        private void menuButtons_ElementRemoved(ListenableList<ApplicationMenuButton> sender, ListenableList<ApplicationMenuButton>.ElementRemovedEventArgs<ApplicationMenuButton> args)
        {
            buttonsStack.Children.RemoveAt(args.Index);
            args.Item.Clicked -= new MouseButtonEventHandler(Item_Clicked);

            if (args.Item.PopupMenu != null)
            {
                args.Item.PopupMenu.Clicked -= new MouseButtonEventHandler(Item_Clicked);
            }
        }

        private void menuButtons_ElementAdded(ListenableList<ApplicationMenuButton> sender, ListenableList<ApplicationMenuButton>.ElementAddedEventArgs<ApplicationMenuButton> args)
        {
            buttonsStack.Children.Insert(args.Index, args.Item);
            args.Item.Clicked += new MouseButtonEventHandler(Item_Clicked);

            if (args.Item.PopupMenu != null)
            {
                args.Item.PopupMenu.Clicked += new MouseButtonEventHandler(Item_Clicked);
            }
        }

        private void masterApplicationButtons_ElementRemoved(ListenableList<ApplicationMasterButton> sender, ListenableList<ApplicationMasterButton>.ElementRemovedEventArgs<ApplicationMasterButton> args)
        {
            masterButtonsStack.Children.RemoveAt(args.Index);
            args.Item.Clicked -= new MouseButtonEventHandler(Item_Clicked);
        }

        private void masterApplicationButtons_ElementAdded(ListenableList<ApplicationMasterButton> sender, ListenableList<ApplicationMasterButton>.ElementAddedEventArgs<ApplicationMasterButton> args)
        {
            masterButtonsStack.Children.Insert(args.Index, args.Item);
            args.Item.Clicked += new MouseButtonEventHandler(Item_Clicked);
        }
        #endregion

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            masterBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
            masterGrid.Background = RibbonStyleHandler.RibbonBarBackground;
            contentBorderLeft.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
            contentBorderRight.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
        }

        #region popup stuff
        #region Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(ApplicationMenuPopup));

        public PlacementMode Placement
        {
            get
            {
                return (PlacementMode)GetValue(PlacementProperty);
            }
            set
            {
                SetValue(PlacementProperty, value);
            }
        }
        #endregion

        #region PlacementTarget
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(ApplicationMenuPopup));

        public UIElement PlacementTarget
        {
            get
            {
                return (UIElement)GetValue(PlacementTargetProperty);
            }
            set
            {
                SetValue(PlacementTargetProperty, value);
            }
        }
        #endregion

        #region PlacementRectangle
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(ApplicationMenuPopup));

        public Rect PlacementRectangle
        {
            get
            {
                return (Rect)GetValue(PlacementRectangleProperty);
            }

            set
            {
                SetValue(PlacementRectangleProperty, value);
            }
        }
        #endregion

        #region HorizontalOffset
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(ApplicationMenuPopup));

        public double HorizontalOffset
        {
            get
            {
                return (double)GetValue(HorizontalOffsetProperty);
            }
            set
            {
                SetValue(HorizontalOffsetProperty, value);
            }
        }
        #endregion

        #region VerticalOffset
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(ApplicationMenuPopup));

        public double VerticalOffset
        {
            get
            {
                return (double)GetValue(VerticalOffsetProperty);
            }
            set
            {
                SetValue(VerticalOffsetProperty, value);
            }
        }
        #endregion

        #region IsOpen
        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(typeof(ApplicationMenuPopup), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ApplicationMenuPopup ctrl = (ApplicationMenuPopup)d;

            if ((bool)e.NewValue)
            {
                if (ctrl.parentPopup == null)
                {
                    ctrl.HookupParentPopup();
                }

                ctrl.keepShowing = true;
                ctrl.hideMenuLoop();
            }
        }
        #endregion

        public void HookupParentPopup()
        {
            parentPopup = new Popup();
            parentPopup.AllowsTransparency = true;
            Popup.CreateRootPopup(parentPopup, this);
        }
        #endregion

        #region assesors
        public ListenableList<ApplicationMasterButton> MasterApplicationButtons
        {
            get
            {
                return masterApplicationButtons;
            }
        }

        public ListenableList<ApplicationMenuButton> MenuButtons
        {
            get
            {
                return menuButtons;
            }
        }

        public ListenableList<ApplicationDocument> DocumentButtons
        {
            get
            {
                return documentButtons;
            }
        }
        #endregion

        #region hide menu
        private void Item_Clicked(object sender, MouseButtonEventArgs e)
        {
            this.IsOpen = false;
        }

        private void ab_Clicked(object sender, MouseEventArgs e)
        {
            this.IsOpen = false;
        }

        private void ApplicationMenuPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            insideControl = false;
        }

        private void ApplicationMenuPopup_MouseEnter(object sender, MouseEventArgs e)
        {
            insideControl = true;
        }

        private void hideMenuLoop()
        {
            Thread t = new Thread(delegate()
            {
                while (keepShowing)
                {
                    this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        if (!(Mouse.LeftButton == MouseButtonState.Released &&
                    Mouse.MiddleButton == MouseButtonState.Released &&
                    Mouse.RightButton == MouseButtonState.Released &&
                    Mouse.XButton1 == MouseButtonState.Released &&
                    Mouse.XButton2 == MouseButtonState.Released))
                        {
                            if (!insideControl)
                            {
                                for (int i = 0; i < this.MenuButtons.Count; i++)
                                {
                                    if (this.MenuButtons[i].PopupMenu != null && this.MenuButtons[i].PopupMenu.IsMouseInside)
                                    {
                                        return;
                                    }
                                }
                                keepShowing = false;
                            }
                        }
                    }));
                    Thread.Sleep(10);
                }

                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                {
                    this.IsOpen = false;
                }));
            });
            t.Name = "Capture Click Events Thread";
            t.Priority = ThreadPriority.AboveNormal;
            t.Start();
        }
        #endregion

        public void updateApplicationButton(ApplicationButton button)
        {
            ((ApplicationButton)(applicationButtonPlacement.Children[0])).Clicked -= new MouseEventHandler(ab_Clicked);
            applicationButtonPlacement.Children.Clear();

            ApplicationButton ab = button.getPressedClone();
            ab.HorizontalAlignment = HorizontalAlignment.Left;
            ab.VerticalAlignment = VerticalAlignment.Top;
            ab.Margin = new Thickness(3, 0, 0, 0);
            ab.Clicked += new MouseEventHandler(ab_Clicked);

            applicationButtonPlacement.Children.Add(ab);
        }
    }
}
