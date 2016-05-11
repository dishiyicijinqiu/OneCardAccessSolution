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
    /// Interaction logic for RibbonUserControl.xaml
    /// </summary>
    public partial class RibbonUserControl : RibbonControlBase, IRibbonFullControl
    {
        public RibbonUserControl()
        {
            InitializeComponent();

            this.canResize = true;
            resizePriority = 0.9;
            hasQATbutton = false;
        }

        #region resize handlers
        public override bool resizeBigger()
        {
            this.UpdateLayout();
            if (this.Width == this.MaxWidth)
            {
                return false;
            }
            else if (this.Width + 50 <= this.MaxWidth)
            {
                this.Width += 50;
                return true;
            }
            else
            {
                this.Width = this.MaxWidth;
                return true;
            }
        }

        public override bool resizeSmaller()
        {
            this.UpdateLayout();
            if (this.Width == this.MinWidth)
            {
                return false;
            }
            else if (this.Width - 50 >= this.MinWidth)
            {
                this.Width -= 50;
                return true;
            }
            else
            {
                this.Width = this.MinWidth;
                return true;
            }
        }
        #endregion

        public new object Content
        {
            get
            {
                return base.Content;
            }
            set
            {
                base.Content = value;
            }
        }
    }
}
