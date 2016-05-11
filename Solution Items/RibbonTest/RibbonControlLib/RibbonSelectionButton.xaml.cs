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
using System.Windows.Media.Animation;

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
    /// Interaction logic for RibbonSelectionButton.xaml
    /// </summary>
    public partial class RibbonSelectionButton : UserControl, IKeyboardPopupable
    {
        #region class variables
        private RibbonBar rb = null;
        private bool selected = false;
        private RibbonController controller = null;
        public event MouseButtonEventHandler DoubleClicked;
        private String text = "";
        private String displayText = "";
        #endregion

        public RibbonSelectionButton(RibbonController controller, RibbonBar rb)
        {
            InitializeComponent();
            this.rb = rb;
            this.controller = controller;

            RibbonStyleHandler.styleTabBorder(theBorder, this, this);
            RibbonStyleHandler.styleTabText(theBorder, titleLabel);

            theBorder.ContextMenu = RibbonContextMenu.GetDefaultContextMenu(this, controller, false);
        }

        #region accessors
        public bool IsSelected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                theBorder.IsSelected = value;
            }
        }

        public RibbonBar RibbonBar
        {
            get
            {
                return rb;
            }
            set
            {
                rb = value;
            }
        }

        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value; 
                displayText = value;
                titleLabel.Content = value;
            }
        }
        #endregion

        #region IKeyboardPopup Members

        public RibbonKeyboardAccessKeyCombination KeyboardAccessCombination
        {
            get
            {
                return RibbonBar.KeyboardAccessCombination;
            }
            set
            {
                RibbonBar.KeyboardAccessCombination = value;
            }
        }

        public double KeyboardAccessVerticalOffset
        {
            get
            {
                return RibbonBar.KeyboardAccessVerticalOffset;
            }
            set
            {
                RibbonBar.KeyboardAccessVerticalOffset = value;
            }
        }

        public double KeyboardAccessHorizontalOffset
        {
            get
            {
                return RibbonBar.KeyboardAccessHorizontalOffset;
            }
            set
            {
                RibbonBar.KeyboardAccessHorizontalOffset = value;
            }
        }

        public void fireClickEvent(object sender, MouseButtonEventArgs args)
        {
            controller.SelectedRibbon = this.RibbonBar;
        }

        #endregion

        #region event handlers
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && e.ClickCount == 1)
            {
                controller.SelectedRibbon = this.RibbonBar;
            }
        }

        internal void ribbonBarTextChangedHandler(TextChangedEventArgs args)
        {
            this.Text = args.NewText;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DoubleClicked != null)
            {
                DoubleClicked(this, e);
            }
        }
        #endregion

        #region resizing
        public bool resizeBigger()
        {
            if (displayText.Length == text.Length)
            {
                return false;
            }
            else
            {
                displayText = text.Substring(0, displayText.Length + 1);
                titleLabel.Content = displayText;

                if (displayText.Length == text.Length)
                {
                    this.ToolTip = null;
                }

                return true;
            }
        }

        public bool resizeSmaller()
        {
            if (displayText.Length == 3)
            {
                return false;
            }
            else
            {
                displayText = displayText.Substring(0, displayText.Length - 1);
                titleLabel.Content = displayText;

                if (this.ToolTip == null)
                {
                    this.ToolTip = text;
                }

                return true;
            }
        }

        public bool IsFullSize
        {
            get
            {
                return displayText.Length == text.Length;
            }
        }
        #endregion
    }
}
