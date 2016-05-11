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
    /// Interaction logic for RibbonPreviewBoxesPopup.xaml
    /// </summary>
    public partial class RibbonPreviewBoxesPopup : UserControl
    {
        public delegate void PopupClosed(RibbonPreviewBox clicked);

        private ListenableList<RibbonPreviewBox> previews = new ListenableList<RibbonPreviewBox>();
        private Dictionary<RibbonPreviewBox, RibbonPreviewBox> translateClone = new Dictionary<RibbonPreviewBox, RibbonPreviewBox>();
        private Dictionary<String, RibbonPreviewBoxesPopupGroup> groups = new Dictionary<string, RibbonPreviewBoxesPopupGroup>();
        public Popup parentPopup;
        private PopupClosed returnMethod;
        private bool inside = false;
        private RibbonPreviewBox clickedBox = null;

        public RibbonPreviewBoxesPopup(ListenableList<RibbonPreviewBox> previews, PopupClosed returnMethod, bool showGroups)
        {
            InitializeComponent();
            this.returnMethod = returnMethod;

            #region populate gui
            for (int i = 0; i < previews.Count; i++)
            {
                RibbonPreviewBox pClone = previews[i].Clone();
                translateClone.Add(pClone, previews[i]);

                this.previews.Add(pClone);
                //previewWraps.Children.Add(pClone);
                this.previews[i].MouseDown += new MouseButtonEventHandler(previewWraps_MouseDown);

                if (showGroups)
                {
                    if (!groups.ContainsKey(pClone.Header))
                    {
                        groups.Add(pClone.Header, new RibbonPreviewBoxesPopupGroup(pClone.Header));
                        theStack.Children.Add(groups[pClone.Header]);
                    }
                    RibbonPreviewBoxesPopupGroup group = groups[pClone.Header];
                    group.Boxes.Add(pClone);
                }
                else
                {
                    if (!groups.ContainsKey("All"))
                    {
                        groups.Add("All", new RibbonPreviewBoxesPopupGroup("All"));
                        theStack.Children.Add(groups["All"]);
                    }
                    RibbonPreviewBoxesPopupGroup group = groups["All"];
                    group.Boxes.Add(pClone);
                }
            }
            #endregion

            #region styling
            backgroundGrid.Background = RibbonStyleHandler.PreviewBackground;
            theBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
            #endregion

            this.theStack.LostFocus += new RoutedEventHandler(backgroundGrid_LostFocus);
        }

        private void previewWraps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickedBox = translateClone[(RibbonPreviewBox)sender];
            IsOpen = false;
            //removeAll();

            //if (returnMethod != null)
            //{
            //    returnMethod.Invoke(translateClone[(RibbonPreviewBox)sender]);
            //}
        }

        private void removeAll()
        {
            for (int i = 0; i < theStack.Children.Count; i++)
            {
                RibbonPreviewBoxesPopupGroup rpbpg = (RibbonPreviewBoxesPopupGroup)theStack.Children[i];
                rpbpg.clear();
            }
            theStack.Children.Clear();
        }

        #region popup stuff
        #region Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(RibbonPreviewBoxesPopup));

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
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(RibbonPreviewBoxesPopup));

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
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(RibbonPreviewBoxesPopup));

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
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(RibbonPreviewBoxesPopup));

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
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(RibbonPreviewBoxesPopup));

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
        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(typeof(RibbonPreviewBoxesPopup), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

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
            RibbonPreviewBoxesPopup ctrl = (RibbonPreviewBoxesPopup)d;

            if ((bool)e.NewValue)
            {
                if (ctrl.parentPopup == null)
                {
                    ctrl.HookupParentPopup();
                }

                #region auto close
                Thread t = new Thread(delegate()
                {
                    bool clickOutside = false;
                    while (!clickOutside)
                    {
                        Thread.Sleep(100);

                        ctrl.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RibbonEventResources.RefreshDelegate(delegate()
                        {
                            if (!ctrl.inside)
                            {
                                if (RibbonEventResources.IsMouseButtonPressed())
                                {
                                    if (!ctrl.inside)
                                    {
                                        clickOutside = true;
                                    }
                                }
                            }
                        }));
                    }

                    ctrl.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RibbonEventResources.RefreshDelegate(delegate()
                    {
                        ctrl.IsOpen = false;
                    }));
                });
                t.Name = "Auto Close RibbonPreviewBoxes Popup Thread";
                t.Start();
                #endregion
            }
            else
            {
                ctrl.removeAll();

                if (ctrl.returnMethod != null)
                {
                    ctrl.returnMethod.Invoke(ctrl.clickedBox);
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

        #region popup close stuff
        private void backgroundGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            removeAll();

            IsOpen = false;
            if (returnMethod != null)
            {
                returnMethod.Invoke(null);
            }
        }

        private void backgroundGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            inside = true;
        }

        private void backgroundGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            inside = false;
        }

        public bool IsMouseInside
        {
            get
            {
                return inside;
            }
        }
        #endregion
    }
}
