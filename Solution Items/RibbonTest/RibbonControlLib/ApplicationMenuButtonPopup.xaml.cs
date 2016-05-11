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
    /// Interaction logic for ApplicationMenuButtonPopup.xaml
    /// </summary>
    public partial class ApplicationMenuButtonPopup : UserControl
    {
        public event MouseButtonEventHandler Clicked;

        private bool mouseInside = false;
        private Popup parentPopup;
        private ListenableList<ApplicationMenuButtonPopupItem> items = new ListenableList<ApplicationMenuButtonPopupItem>();

        public ApplicationMenuButtonPopup()
        {
            InitializeComponent();

            items.ElementAdded += new ListenableList<ApplicationMenuButtonPopupItem>.ElementAddedDelegate(items_ElementAdded);
            items.ElementRemoved += new ListenableList<ApplicationMenuButtonPopupItem>.ElementRemovedDelegate(items_ElementRemoved);
            
            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);
        }

        #region children handlers
        private void items_ElementRemoved(ListenableList<ApplicationMenuButtonPopupItem> sender, ListenableList<ApplicationMenuButtonPopupItem>.ElementRemovedEventArgs<ApplicationMenuButtonPopupItem> args)
        {
            theStack.Children.RemoveAt(args.Index + 1);
            args.Item.Clicked -= new MouseButtonEventHandler(Item_Clicked);
        }

        private void items_ElementAdded(ListenableList<ApplicationMenuButtonPopupItem> sender, ListenableList<ApplicationMenuButtonPopupItem>.ElementAddedEventArgs<ApplicationMenuButtonPopupItem> args)
        {
            theStack.Children.Insert(args.Index + 1, args.Item);
            args.Item.Clicked += new MouseButtonEventHandler(Item_Clicked);
        }

        private void Item_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(sender, e);
            }

            this.IsOpen = false;
        }
        #endregion

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            headerLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            headerBarGrid.Background = new SolidColorBrush(((LinearGradientBrush)RibbonStyleHandler.RibbonBarBackground).GradientStops[0].Color);
            masterBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
        }

        #region popup stuff
        #region Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(ApplicationMenuButtonPopup));

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
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(ApplicationMenuButtonPopup));

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
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(ApplicationMenuButtonPopup));

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
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(ApplicationMenuButtonPopup));

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
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(ApplicationMenuButtonPopup));

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
        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(typeof(ApplicationMenuButtonPopup), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

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
            ApplicationMenuButtonPopup ctrl = (ApplicationMenuButtonPopup)d;

            if ((bool)e.NewValue)
            {
                if (ctrl.parentPopup == null)
                {
                    ctrl.HookupParentPopup();
                }
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

        #region accessors
        public String Text
        {
            get
            {
                return headerLabel.Content.ToString();
            }
            set
            {
                headerLabel.Content = value;
            }
        }

        public ListenableList<ApplicationMenuButtonPopupItem> Items
        {
            get
            {
                return items;
            }
        }

        public bool IsMouseInside
        {
            get
            {
                return mouseInside;
            }
        }
        #endregion

        #region control event handlers
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseInside = true;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseInside = false;
        }
        #endregion
    }
}
