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

    public class AddToQuickAccessToolbarEventArgs
    {

    }
    public class CustomiseQuickAccessToolbarEventArgs
    {

    }
    public class ShowQuickAccessToolbarBelowRibbonEventArgs
    {

    }
    public class MinimseRibbonEventArgs
    {

    }

    public delegate void AddToQuickAccessToolbarDelegate(object sender,
                                                    AddToQuickAccessToolbarEventArgs args);
    public delegate void CustomiseQuickAccessToolbarDelegate(object sender,
                                                    CustomiseQuickAccessToolbarEventArgs args);
    public delegate void ShowQuickAccessToolbarBelowRibbonDelegate(object sender,
                                                    ShowQuickAccessToolbarBelowRibbonEventArgs args);
    public delegate void MinimseRibbonDelegate(object sender,
                                                    MinimseRibbonEventArgs args);

    public abstract class RibbonContextMenu
    {
        private static Dictionary<MenuItem, RibbonController> ControllerDict = new Dictionary<MenuItem, RibbonController>();
        private static Dictionary<MenuItem, UIElement> ElementDict = new Dictionary<MenuItem, UIElement>();

        public static ContextMenu GetDefaultContextMenu(UIElement element, RibbonController controller, bool showAddToQuickAccessToolbar)
        {
            if (element == null || controller == null)
            {
                throw new Exception("Element and controller must be non-null values");
            }

            ContextMenu m = new ContextMenu();

            MenuItem m1 = new MenuItem();
            m1.Header = "_Add to Quick Access Toolbar";
            m1.Click += new RoutedEventHandler(addToQuickAccessToolbar_Click);

            Separator m2 = new Separator();

            MenuItem m3 = new MenuItem();
            m3.Header = "_Customise Quick Access Toolbar...";
            m3.Click += new RoutedEventHandler(customiseQuickAccessToolbar_Click);

            MenuItem m4 = new MenuItem();
            m4.Header = "_Show Quick Access Toolbar Below the Ribbon";
            m4.Click += new RoutedEventHandler(showQuickAccessToolbar_Click);

            Separator m5 = new Separator();

            MenuItem m6 = new MenuItem();
            m6.Header = "Min_imize the Ribbon";
            m6.Click += new RoutedEventHandler(minimiseTheRibbon_Click);

            if (showAddToQuickAccessToolbar)
            {
                m.Items.Add((object)m1);
                m.Items.Add((object)m2);
            }
            m.Items.Add((object)m3);
            m.Items.Add((object)m4);
            m.Items.Add((object)m5);
            m.Items.Add((object)m6);

            ControllerDict.Add(m1, controller);
            ElementDict.Add(m1, element);
            ControllerDict.Add(m3, controller);
            ElementDict.Add(m3, element);
            ControllerDict.Add(m4, controller);
            ElementDict.Add(m4, element);
            ControllerDict.Add(m6, controller);
            ElementDict.Add(m6, element);

            return m;
        }

        public static ContextMenu GetDefaultContextMenu(UIElement element, RibbonController controller)
        {
            return GetDefaultContextMenu(element, controller, true);
        }

        private static void minimiseTheRibbon_Click(object sender, RoutedEventArgs e)
        {
            if (ElementDict.ContainsKey((MenuItem)sender))
            {
                UIElement element = ElementDict[(MenuItem)sender];
                RibbonController controller = ControllerDict[(MenuItem)sender];

                controller.fireMinimseRibbonDelegate(element, new MinimseRibbonEventArgs());
            }
        }

        private static void showQuickAccessToolbar_Click(object sender, RoutedEventArgs e)
        {
            if (ElementDict.ContainsKey((MenuItem)sender))
            {
                UIElement element = ElementDict[(MenuItem)sender];
                RibbonController controller = ControllerDict[(MenuItem)sender];

                controller.fireShowQuickAccessToolbarBelowRibbonDelegate(element, new ShowQuickAccessToolbarBelowRibbonEventArgs());
            }
        }

        private static void customiseQuickAccessToolbar_Click(object sender, RoutedEventArgs e)
        {
            if (ElementDict.ContainsKey((MenuItem)sender))
            {
                UIElement element = ElementDict[(MenuItem)sender];
                RibbonController controller = ControllerDict[(MenuItem)sender];

                controller.fireCustomiseQuickAccessToolbarDelegate(element, new CustomiseQuickAccessToolbarEventArgs());
            }
        }

        private static void addToQuickAccessToolbar_Click(object sender, RoutedEventArgs e)
        {
            if (ElementDict.ContainsKey((MenuItem)sender))
            {
                UIElement element = ElementDict[(MenuItem)sender];
                RibbonController controller = ControllerDict[(MenuItem)sender];

                controller.fireAddToQuickAccessToolbarDelegate(element, new AddToQuickAccessToolbarEventArgs());
            }
        }
    }
}
