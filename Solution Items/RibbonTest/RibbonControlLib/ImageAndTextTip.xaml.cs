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
using System.Windows.Media.Animation;
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
    /// Interaction logic for ImageAndTextTip.xaml
    /// </summary>
    public partial class ImageAndTextTip : UserControl, IScreenTip
    {
        public Popup parentPopup;
        private Storyboard inStoryboard = null;
        private Storyboard outStoryboard = null;

        public ImageAndTextTip()
        {
            InitializeComponent();

            inStoryboard = (Storyboard)this.FindResource("loadingIn");
            outStoryboard = (Storyboard)this.FindResource("loadingOut");
            outStoryboard.Completed += new EventHandler(outStoryboard_Completed);

            helpImage.Source = new BitmapImage(new Uri(RibbonFileLocations.RibbonBasePath +
                @"\Graphics\help_icon.png", UriKind.RelativeOrAbsolute));

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(new RibbonStyleHandler.StyleChangedEventArgs(RibbonStyleHandler.Style.Not_Set));
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            theBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.GroupBorderNormal);
            theBorder.Background = RibbonStyleHandler.RibbonBarBackground;
            titleLabel.Foreground = RibbonStyleHandler.GroupText;
            contentBlock.Foreground = RibbonStyleHandler.GroupText;
            helpLabel.Foreground = RibbonStyleHandler.GroupText;
            theBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.GroupBorderNormal);
        }

        public String Title
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

        public String Text
        {
            get
            {
                return contentBlock.Text;
            }
            set
            {
                contentBlock.Text = value;
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

        #region popup stuff
        #region Placement
        public static readonly DependencyProperty PlacementProperty = Popup.PlacementProperty.AddOwner(typeof(ImageAndTextTip));

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
        public static readonly DependencyProperty PlacementTargetProperty = Popup.PlacementTargetProperty.AddOwner(typeof(ImageAndTextTip));

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
        public static readonly DependencyProperty PlacementRectangleProperty = Popup.PlacementRectangleProperty.AddOwner(typeof(ImageAndTextTip));

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
        public static readonly DependencyProperty HorizontalOffsetProperty = Popup.HorizontalOffsetProperty.AddOwner(typeof(ImageAndTextTip));

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
        public static readonly DependencyProperty VerticalOffsetProperty = Popup.VerticalOffsetProperty.AddOwner(typeof(ImageAndTextTip));

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
        public static readonly DependencyProperty IsOpenProperty = Popup.IsOpenProperty.AddOwner(typeof(ImageAndTextTip), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnIsOpenChanged)));

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                if (value == true)
                {
                    outStoryboard.Stop(this);
                    inStoryboard.Begin(this);
                    SetValue(IsOpenProperty, value);
                }
                else
                {
                    inStoryboard.Stop(this);
                    outStoryboard.Begin(this);
                }
            }
        }

        private void outStoryboard_Completed(object sender, EventArgs e)
        {
            SetValue(IsOpenProperty, false);
        }

        private static void OnIsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageAndTextTip ctrl = (ImageAndTextTip)d;

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
