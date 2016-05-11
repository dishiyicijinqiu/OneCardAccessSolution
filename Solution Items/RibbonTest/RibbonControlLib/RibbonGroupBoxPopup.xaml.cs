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
    /// Interaction logic for RibbonGroupBoxPopup.xaml
    /// </summary>
    public partial class RibbonGroupBoxPopup : UserControl
    {
        #region class variables
        private List<IRibbonFullControl> elements = new List<IRibbonFullControl>();
        private bool showLabel = false;
        private RibbonGroupBoxLabelButton gbl = null;
        public event MouseButtonEventHandler LabelButtonPressed;

        #region minimising vars
        private Popup parentPopup;
        public delegate void PopupClosed(RibbonPreviewBox clicked);
        #endregion
        #endregion

        #region initialisation
        public RibbonGroupBoxPopup()
        {
            initialise(null);
        }

        public RibbonGroupBoxPopup(String text)
        {
            initialise(text);
        }

        private void initialise(String text)
        {
            InitializeComponent();

            titleLabel.Foreground = RibbonStyleHandler.GroupText;
            RibbonStyleHandler.styleGroupBorder(mainBorder, labelBorder, this, this);
            mainBorder.Background = RibbonStyleHandler.toLGradientBrush(RibbonStyleHandler.GroupBackgroundHover, 90.0);

            this.Text = text;

            gbl = new RibbonGroupBoxLabelButton();
            gbl.HorizontalAlignment = HorizontalAlignment.Right;
            gbl.Margin = new Thickness(0, 0, 3, 0);

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            this.MouseUp += new MouseButtonEventHandler(RibbonGroupBoxPopup_MouseUp);
            this.LostFocus += new RoutedEventHandler(RibbonGroupBoxPopup_LostFocus);
        }

        private void RibbonGroupBoxPopup_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsOpen = false;
        }

        private void RibbonGroupBoxPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.IsOpen = false;
        }
        #endregion

        #region styling
        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            titleLabel.Foreground = RibbonStyleHandler.GroupText;
            mainBorder.Background = RibbonStyleHandler.toLGradientBrush(RibbonStyleHandler.GroupBackgroundHover, 90.0);
        }
        #endregion

        #region assessors

        #region children handler
        public void addRibbonComponent(IRibbonFullControl element)
        {
            if (elements.Contains(element))
            {
                throw new Exception("Element already added to RibbonGroupBoxPopup");
            }
            else
            {
                elements.Add(element);
                contentStackPanel.Children.Add((UIElement)element);
            }
        }

        public void removeRibbonComponent(IRibbonFullControl element)
        {
            if (elements.Contains(element))
            {
                elements.Remove(element);
                contentStackPanel.Children.Remove((UIElement)element);
            }
            else
            {
                throw new Exception("Element not part of RibbonGroupBoxPopup");
            }
        }

        public void removeRibbonComponents()
        {
            elements.RemoveRange(0, elements.Count);
            contentStackPanel.Children.RemoveRange(0, contentStackPanel.Children.Count);
        }
        #endregion

        public String Text
        {
            get
            {
                return titleLabel.Content.ToString();
            }
            set
            {
                titleLabel.Content = value;
            }
        }

        public bool ShowLabelButton
        {
            set
            {
                if (showLabel == true && value == false)
                {
                    labelGrid.Children.Remove(gbl);
                    showLabel = value;
                }
                else if (showLabel == false && value == true)
                {
                    labelGrid.Children.Add(gbl);
                    showLabel = value;
                }
            }
            get
            {
                return showLabel;
            }
        }
        #endregion

        #region event handlers
        internal void fireLabelButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (LabelButtonPressed != null)
            {
                LabelButtonPressed(sender, e);
            }
        }
        #endregion

        #region popup stuff
        #region Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(RibbonGroupBoxPopup));

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
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(RibbonGroupBoxPopup));

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
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(RibbonGroupBoxPopup));

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
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(RibbonGroupBoxPopup));

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
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(RibbonGroupBoxPopup));

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
        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(typeof(RibbonGroupBoxPopup), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

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
            RibbonGroupBoxPopup ctrl = (RibbonGroupBoxPopup)d;

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
    }
}
