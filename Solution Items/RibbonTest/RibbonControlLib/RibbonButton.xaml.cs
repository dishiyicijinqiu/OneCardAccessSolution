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
    public partial class RibbonButton : RibbonControlBase, IRibbonFullControl 
    {
        protected override void initialize()
        {
            InitializeComponent();

            List<GradientStop> transparentStops = new List<GradientStop>();
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.647));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.6471));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 1));
            RibbonStyleHandler.styleButtonBorder(topControl, topBorder, bottomBorder, this, this, Color.FromArgb(0, 0, 0, 0), transparentStops);
            RibbonStyleHandler.styleButtonText(bottomBorder, descriptionLabel);
            RibbonStyleHandler.styleButtonText(bottomBorder, descriptionLabel2);

            this.ImageChanged += new ImageChangedEventDelegate(RibbonButton_ImageChanged);
            this.TextChanged += new TextChangedEventDelegate(RibbonButton_TextChanged);

            addClickElement((UIElement)topBorder);
            addClickElement((UIElement)bottomBorder);
            this.styledBorders.Add(topBorder);
            this.styledBorders.Add(bottomBorder);

            this.KeyboardAccessVerticalOffset = 58;
        }

        private void RibbonButton_TextChanged(TextChangedEventArgs args)
        {
            descriptionLabel.Content = args.NewText;
            descriptionLabel2.Content = "";

            if (args.NewText.Contains("|"))
            {
                descriptionLabel.Content = args.NewText.Split(new char[] { '|' })[0];
                descriptionLabel2.Content = args.NewText.Split(new char[] { '|' })[1];
            }
        }

        private void RibbonButton_ImageChanged(ImageChangedEventArgs args)
        {
            imageBox.Source = args.NewSource;
        }

        public override bool IsEnabled
        {
            get
            {
                return base.IsEnabled;
            }
            set
            {
                base.IsEnabled = value;

                if (value == false)
                {
                    
                }
            }
        }
    }
}
