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
    /// Interaction logic for QuickAccessButton.xaml
    /// </summary>
    public partial class QuickAccessButton : UserControl, IKeyboardPopupable
    {
        private bool defaultButton = false;
        private bool defaultMenuButton = false;
        private RibbonKeyboardAccessKeyCombination keyboardAccessCombination = null;
        private double keyboardAccessVerticalOffset = 12;
        private double keyboardAccessHorizontalOffset = double.NaN;
        private IRibbonControl parentControl = null;

        #region constructors
        public QuickAccessButton(IRibbonControl parentControl)
        {
            InitializeComponent();

            theBorder.MouseLeftButtonUp += new MouseButtonEventHandler(theBorder_MouseLeftButtonUp);
            theBorder.MouseDown += new MouseButtonEventHandler(theBorder_MouseDown);
            theBorder.MouseLeave += new MouseEventHandler(theBorder_MouseLeave);
            theBorder.MouseEnter += new MouseEventHandler(theBorder_MouseEnter);

            this.parentControl = parentControl;
            parentControl.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(parentControl_SelectedChanged);
            parentControl.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(parentControl_EnabledChanged);

            #region style drop down button
            List<GradientStop> transparentStops = new List<GradientStop>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), RibbonStyleHandler.ButtonBackgroundNormal[i].Offset));
            }
            RibbonStyleHandler.styleButtonBorder(theBorder, this, this, Color.FromArgb(0, 0, 0, 0), transparentStops);
            #endregion

            updateImage();
        }

        private void theBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (parentControl.IsEnabled)
            {
                if (parentControl.NormalTip != null)
                {
                    //if (false && this.Parent != null
                    //        && ((Panel)this.Parent).Parent != null
                    //        && (UIElement)((Panel)((Panel)this.Parent).Parent).Parent != null)
                    //{
                    //    tip.PlacementTarget = (UIElement)((Panel)((Panel)this.Parent).Parent).Parent;
                    //}
                    //else
                    //{
                    parentControl.NormalTip.PlacementTarget = this;
                    //}

                    parentControl.NormalTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    parentControl.NormalTip.VerticalOffset = 20;
                    //contextMenu.HorizontalOffset = (this.Width / 2.0) - 10;
                    parentControl.NormalTip.IsOpen = true;
                }
            }
            else
            {
                if (parentControl.DisabledTip != null)
                {
                    parentControl.DisabledTip.PlacementTarget = this;

                    parentControl.DisabledTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    parentControl.DisabledTip.VerticalOffset = 20;
                    //contextMenu.HorizontalOffset = (this.Width / 2.0) - 10;
                    parentControl.DisabledTip.IsOpen = true;
                }
                else if (parentControl.NormalTip != null)
                {
                    parentControl.NormalTip.PlacementTarget = this;

                    parentControl.NormalTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    parentControl.NormalTip.VerticalOffset = 20;
                    //contextMenu.HorizontalOffset = (this.Width / 2.0) - 10;
                    parentControl.NormalTip.IsOpen = true;
                }
            }
        }

        private void theBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (parentControl.NormalTip != null && parentControl.NormalTip.IsOpen)
            {
                parentControl.NormalTip.IsOpen = false;
            }
            if (parentControl.DisabledTip != null && parentControl.DisabledTip.IsOpen)
            {
                parentControl.DisabledTip.IsOpen = false;
            }
        }

        private void theBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (parentControl.NormalTip != null && parentControl.NormalTip.IsOpen)
            {
                parentControl.NormalTip.IsOpen = false;
            }
            if (parentControl.DisabledTip != null && parentControl.DisabledTip.IsOpen)
            {
                parentControl.DisabledTip.IsOpen = false;
            }
        }
        #endregion

        #region event handlers
        private void parentControl_EnabledChanged(object sender, bool newValue)
        {
            updateImage();
        }

        private void parentControl_SelectedChanged(object sender, bool newValue)
        {
            updateImage();
        }

        private void theBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            parentControl.fireClickEvent(this, null);
        }
        #endregion

        private void updateImage()
        {
            theImage.Source = this.Image;
        }

        public void fireClickEvent(object sender, MouseButtonEventArgs args)
        {
            parentControl.fireClickEvent(this, args);
        }

        #region accessors
        public bool IsDefaultQuickAccessButton
        {
            get
            {
                return defaultButton;
            }
            set
            {
                defaultButton = value;
            }
        }

        public bool IsDefaultQuickAccessMenuButton
        {
            get
            {
                return defaultMenuButton;
            }
            set
            {
                defaultMenuButton = value;
            }
        }

        public String Text
        {
            get
            {
                return parentControl.Text.Replace("|", " ");
            }
        }

        public ImageSource Image
        {
            get
            {
                if (parentControl.IsEnabled)
                {
                    if (parentControl.NormalImage != null)
                    {
                        return parentControl.NormalImage.Clone();
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (parentControl.DisabledImage != null)
                    {
                        return parentControl.DisabledImage.Clone();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public Guid ControlID
        {
            get
            {
                return parentControl.ControlID;
            }
        }
        #endregion

        #region IKeyboardPopup Members
        public RibbonKeyboardAccessKeyCombination KeyboardAccessCombination
        {
            get
            {
                if (keyboardAccessCombination == null)
                {
                    return new RibbonKeyboardAccessKeyCombination();
                }
                else
                {
                    return keyboardAccessCombination;
                }
            }
            set
            {
                keyboardAccessCombination = value;
            }
        }

        public double KeyboardAccessVerticalOffset
        {
            get
            {
                return keyboardAccessVerticalOffset;
            }
            set
            {
                keyboardAccessVerticalOffset = value;
            }
        }

        public double KeyboardAccessHorizontalOffset
        {
            get
            {
                return keyboardAccessHorizontalOffset;
            }
            set
            {
                keyboardAccessHorizontalOffset = value;
            }
        }
        #endregion
    }
}
