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
    /// Interaction logic for RibbonHalfComboBox.xaml
    /// </summary>
    public partial class RibbonHalfComboBox : RibbonControlBase, IRibbonHalfControl
    {
        protected override void initialize()
        {
            InitializeComponent();
        }

        public void setComboBox(ComboBox newBox)
        {
            comboBox = newBox;
            newBox.Margin = new Thickness(0);
            //newBox.Margin = new Thickness(2, 2, 2, 0); 
            newBox.Height = 24;// 19;
            newBox.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            //newBox.VerticalAlignment = VerticalAlignment.Top;

            theBorder.Child = newBox;
        }

        #region IRibbonHalfControl Members

        public void setLeftEndBorder()
        {
            theBorder.BorderThickness = new Thickness(1, 1, 1, 1);
            theBorder.CornerRadius = new CornerRadius(5, 0, 0, 5);
        }

        public void setRightEndBorder()
        {
            theBorder.BorderThickness = new Thickness(0, 1, 1, 1);
            theBorder.CornerRadius = new CornerRadius(0, 5, 5, 0);
        }

        public void setMiddleBorder()
        {
            theBorder.BorderThickness = new Thickness(0, 1, 1, 1);
            theBorder.CornerRadius = new CornerRadius(0, 0, 0, 0);
        }

        public void setFullBorder()
        {
            theBorder.BorderThickness = new Thickness(1, 1, 1, 1);
            theBorder.CornerRadius = new CornerRadius(5, 5, 5, 5);
        }
        #endregion

        
    }
}
