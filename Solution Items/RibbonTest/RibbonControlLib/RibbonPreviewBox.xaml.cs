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
    /// Interaction logic for RibbonPreviewBox.xaml
    /// </summary>
    public partial class RibbonPreviewBox : UserControl
    {
        public event MouseButtonEventHandler Click;
        public new event MouseEventHandler MouseEnter;
        public new event MouseEventHandler MouseLeave;
        private bool selected = false;
        private Brush nonSelectedBorderBrush = null;
        private String header = "Default";

        public RibbonPreviewBox()
        {
            InitializeComponent();
            nonSelectedBorderBrush = theBorder.BorderBrush;
        }

        public ImageSource Image
        {
            get
            {
                return imageBox.Source;
            }
            set
            {
                imageBox.Source = value;
            }
        }

        public String Text
        {
            get
            {
                if (labelBox.Content != null)
                {
                    return labelBox.Content.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                labelBox.Content = value;
            }
        }

        public RibbonPreviewBox Clone()
        {
            RibbonPreviewBox c = new RibbonPreviewBox();
            if (this.Image != null)
            {
                c.Image = this.Image.Clone();
            }
            if (this.Text != null)
            {
                c.Text = this.Text.Clone().ToString();
            }
            if (this.Header != null)
            {
                c.Header = this.Header.Clone().ToString();
            }

            return c;
        }

        public String Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        private void theBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        internal void fireClick()
        {
            if (Click != null)
            {
                Click(this, null);
            }
        }

        private void theBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MouseEnter != null)
            {
                MouseEnter(this, e);
            }
        }

        private void theBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            if (MouseLeave != null)
            {
                MouseLeave(this, e);
            }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (!selected && value)
                {
                    selected = value;
                    theBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderPressed);
                }
                else if (selected && !value)
                {
                    selected = value;
                    theBorder.BorderBrush = nonSelectedBorderBrush;
                }
            }
        }
    }
}
