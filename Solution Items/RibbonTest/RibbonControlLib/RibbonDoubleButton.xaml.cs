﻿using System;
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
    /// Interaction logic for RibbonDoubleButton.xaml
    /// </summary>
    public partial class RibbonDoubleButton : RibbonControlBase, IRibbonFullControl 
    {
        public event MouseButtonEventHandler BottomButtonPressed;

        protected override void initialize()
        {
            InitializeComponent();

            List<GradientStop> transparentStops = new List<GradientStop>();
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.647));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.6471));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 1));
            RibbonStyleHandler.styleButtonBorder(theControl, topBorder, bottomBorder, this, this, Color.FromArgb(0, 0, 0, 0), transparentStops);
            RibbonStyleHandler.styleButtonText(bottomBorder, descriptionLabel);
            RibbonStyleHandler.styleButtonText(bottomBorder, descriptionLabel2);
            RibbonStyleHandler.styleButtonText(bottomBorder, arrowLabel);

            this.ImageChanged += new ImageChangedEventDelegate(RibbonDoubleButton_ImageChanged);
            this.TextChanged += new TextChangedEventDelegate(RibbonDoubleButton_TextChanged);
            bottomBorder.MouseDown += new MouseButtonEventHandler(bottomBorder_MouseDown);

            addClickElement(topBorder);
            addSubMenuOpener(bottomBorder);
            this.styledBorders.Add(topBorder);
            this.styledBorders.Add(bottomBorder);

            this.KeyboardAccessVerticalOffset = 58;
        }

        private void bottomBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BottomButtonPressed != null)
            {
                BottomButtonPressed(sender, e);
            }
        }

        private void RibbonDoubleButton_TextChanged(TextChangedEventArgs args)
        {
            descriptionLabel.Content = args.NewText;
            descriptionLabel2.Content = "";

            if (args.NewText.Contains("|"))
            {
                descriptionLabel.Content = args.NewText.Split(new char[] { '|' })[0];
                descriptionLabel2.Content = args.NewText.Split(new char[] { '|' })[1];
            }
        }

        private void RibbonDoubleButton_ImageChanged(ImageChangedEventArgs args)
        {
            imageBox.Source = args.NewSource;
        }

        public override void updateStyle(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            base.updateStyle(args);

            //arrowLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            //descriptionLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
            //descriptionLabel2.Foreground = RibbonStyleHandler.ButtonNormalText;
        }

        protected override void subMenu_Closed(object sender, RoutedEventArgs e)
        {
            base.subMenu_Closed(sender, e);
            RibbonStyleHandler.forceLeaveAnnimation(bottomBorder, null);
        }
    }
}
