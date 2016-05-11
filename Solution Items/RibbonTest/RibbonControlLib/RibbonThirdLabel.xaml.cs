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
    public partial class RibbonThirdLabel : RibbonControlBase
    {
        #region class variables
        private double labelWidth = 0;
        private bool minamised = false;
        #endregion

        #region initialize
        protected override void initialize()
        {
            InitializeComponent();

            List<GradientStop> backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 222, 235, 254), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 203, 222, 246), 0.647));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 193, 213, 241), 0.6471));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 207, 224, 247), 1));
            RibbonStyleHandler.styleButtonBorder(theBorder, this, this, Color.FromArgb(0, 0, 0, 0), backgroundNormal);
            RibbonStyleHandler.styleButtonText(theBorder, label);
            RibbonStyleHandler.styleButtonText(theBorder, contextArrow);

            this.ImageChanged += new ImageChangedEventDelegate(RibbonThirdLabel_ImageChanged);
            this.TextChanged += new TextChangedEventDelegate(RibbonThirdLabel_TextChanged);
            this.SubMenuChanged += new SubMenuChangedEventDelegate(RibbonThirdLabel_SubMenuChanged);

            this.styledBorders.Add(theBorder);
            this.resizePriority = 2.5;
            addSubMenuOpener(contextArrow);
            addClickElement(imagePanel);
            addClickElement(theBorder);

            this.KeyboardAccessHorizontalOffset = 8;
        }
        #endregion

        #region internal event handlers
        private void RibbonThirdLabel_SubMenuChanged(SubMenuChangedEventArgs args)
        {
            if (args.NewSubMenu == null && args.OldSubMenu != null)
            {
                contextArrow.Width = 0;
            }
            else if (args.NewSubMenu != null && args.OldSubMenu == null)
            {
                contextArrow.Width = 22;
            }
        }

        private void RibbonThirdLabel_TextChanged(TextChangedEventArgs args)
        {
            if (args.NewText == null && args.OldText != null)
            {
                label.Content = "";
                label.Width = 0;
                labelWidth = 0;
            }
            else if (args.NewText != null)
            {
                label.Content = args.NewText;

                if (!minamised)
                {
                    label.Width = double.NaN;
                    labelWidth = label.ActualWidth;
                }
            }
        }

        private void RibbonThirdLabel_ImageChanged(ImageChangedEventArgs args)
        {
            if (args.NewSource == null)
            {
                imagePanel.Width = 0;
                imagePanel.Height = 0;
            }
            else
            {
                imagePanel.Width = 18;
                imagePanel.Height = 18;
            }

            imagePanel.Source = args.NewSource;
        }
        #endregion

        #region methods
        protected override void subMenu_Closed(object sender, RoutedEventArgs e)
        {
            base.subMenu_Closed(sender, e);
            RibbonStyleHandler.forceLeaveAnnimation(theBorder, null);
        }
        #endregion

        #region resizing
        public override bool resizeSmaller()
        {
            if (!minamised && label.ActualWidth > 0)
            {
                label.Width = 0;
                minamised = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool resizeBigger()
        {
            if (label.Width == 0 && this.Text.Trim().Equals("") != true && minamised) 
            {
                label.Width = double.NaN;
                if (labelWidth == 0)
                {
                    labelWidth = label.ActualWidth;
                }
                minamised = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool CanResize
        {
            get
            {
                if (!this.Text.Trim().Equals(""))
                {
                    return true;
                }
                {
                    return false;
                }
            }
        }

        public override double ResizeIncreaseAmount
        {
            get 
            { 
                return labelWidth + 80; 
            }
        }
        #endregion
    }
}
