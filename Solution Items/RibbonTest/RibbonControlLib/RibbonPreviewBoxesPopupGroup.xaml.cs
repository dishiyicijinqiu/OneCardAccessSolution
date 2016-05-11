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
    /// Interaction logic for RibbonPreviewBoxesPopupGroup.xaml
    /// </summary>
    public partial class RibbonPreviewBoxesPopupGroup : UserControl
    {
        private ListenableList<RibbonPreviewBox> boxes = new ListenableList<RibbonPreviewBox>();

        public RibbonPreviewBoxesPopupGroup(String name)
        {
            InitializeComponent();

            theLabel.Content = name;

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);

            boxes.ElementAdded += new ListenableList<RibbonPreviewBox>.ElementAddedDelegate(boxes_ElementAdded);
            boxes.ElementRemoved += new ListenableList<RibbonPreviewBox>.ElementRemovedDelegate(boxes_ElementRemoved);
        }

        public void clear()
        {
            boxes.Clear();
            previewWraps.Children.Clear();
        }

        private void boxes_ElementRemoved(ListenableList<RibbonPreviewBox> sender, ListenableList<RibbonPreviewBox>.ElementRemovedEventArgs<RibbonPreviewBox> args)
        {
            try
            {
                previewWraps.Children.RemoveAt(args.Index);
            }
            catch (Exception)
            {
            }
        }

        private void boxes_ElementAdded(ListenableList<RibbonPreviewBox> sender, ListenableList<RibbonPreviewBox>.ElementAddedEventArgs<RibbonPreviewBox> args)
        {
            previewWraps.Children.Insert(args.Index, args.Item);
        }

        public String Text
        {
            get
            {
                return theLabel.Content.ToString();
            }
            set
            {
                theLabel.Content = value;
            }
        }

        public ListenableList<RibbonPreviewBox> Boxes
        {
            get
            {
                return boxes;
            }
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            theLabel.Background = RibbonPreviewBoxesPopupGroup.ToBrush(RibbonStyleHandler.GroupLabelBackgroundNormal, 90.0);
            theLabel.Foreground = RibbonStyleHandler.GroupText;
        }

        public static Brush ToBrush(List<GradientStop> stops, double angle)
        {
            GradientStopCollection c = new GradientStopCollection();
            for (int i = 0; i < stops.Count; i++)
            {
                c.Add(stops[i]);
            }
            return new LinearGradientBrush(c, angle);
        }
    }
}
