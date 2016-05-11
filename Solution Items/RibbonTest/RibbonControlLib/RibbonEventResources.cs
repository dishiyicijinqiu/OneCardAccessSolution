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
using System.Threading;

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
    public abstract class RibbonEventResources
    {
        public delegate void HoverDelegate();
        public delegate void RefreshDelegate();
        public delegate void EnableChangedDelegate(object sender, bool newValue);
        public delegate void SelectedChangedDelegate(object sender, bool newValue);

        internal static void MouseDownToButtonPressed(object sender, MouseButtonEventArgs e, MouseButtonEventHandler ButtonPressed)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ButtonPressed(sender, new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left));
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                ButtonPressed(sender, new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Right));
            }
            else if (e.MiddleButton == MouseButtonState.Pressed)
            {
                ButtonPressed(sender, new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Middle));
            }
            else if (e.XButton1 == MouseButtonState.Pressed)
            {
                ButtonPressed(sender, new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.XButton1));
            }
            else if (e.XButton2 == MouseButtonState.Pressed)
            {
                ButtonPressed(sender, new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.XButton2));
            }
        }

        internal static void MouseHoverDetection(UIElement element, HoverDelegate eventMethod)
        {
            for (int i = 0; i < 100; i++)
            {
                if (!element.IsMouseOver)
                {
                    return;
                }

                Thread.Sleep(100);
            }

            element.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, eventMethod);
        }

        internal static bool IsMouseButtonPressed()
        {
            return Mouse.LeftButton == MouseButtonState.Pressed ||
                Mouse.RightButton == MouseButtonState.Pressed ||
                Mouse.MiddleButton == MouseButtonState.Pressed ||
                Mouse.XButton1 == MouseButtonState.Pressed ||
                Mouse.XButton2 == MouseButtonState.Pressed;
        }
    }

    public class ImageChangedEventArgs
    {
        private ImageSource oldSource;
        private ImageSource newSource;

        public ImageChangedEventArgs()
        {
            oldSource = null;
            newSource = null;
        }

        public ImageChangedEventArgs(ImageSource oldSource, ImageSource newSource)
        {
            this.oldSource = oldSource;
            this.newSource = newSource;
        }

        public ImageSource OldSource
        {
            get { return oldSource; }
            set { oldSource = value; }
        }

        public ImageSource NewSource
        {
            get { return newSource; }
            set { newSource = value; }
        }
    }
    public delegate void ImageChangedEventDelegate(ImageChangedEventArgs args);

    public class TextChangedEventArgs
    {
        private String oldText;
        private String newText;

        public TextChangedEventArgs()
        {
            oldText = null;
            newText = null;
        }

        public TextChangedEventArgs(String oldText, String newText)
        {
            this.oldText = oldText;
            this.newText = newText;
        }

        public String OldText
        {
            get { return oldText; }
            set { oldText = value; }
        }

        public String NewText
        {
            get { return newText; }
            set { newText = value; }
        }
    }
    public delegate void TextChangedEventDelegate(TextChangedEventArgs args);

    public class ScreenTipChangedEventArgs
    {
        private IScreenTip oldTip;
        private IScreenTip newTip;

        public ScreenTipChangedEventArgs()
        {
            oldTip = null;
            newTip = null;
        }

        public ScreenTipChangedEventArgs(IScreenTip oldTip, IScreenTip newTip)
        {
            this.oldTip = oldTip;
            this.newTip = newTip;
        }

        public IScreenTip OldScreenTip
        {
            get { return oldTip; }
            set { oldTip = value; }
        }

        public IScreenTip NewScreenTip
        {
            get { return newTip; }
            set { newTip = value; }
        }
    }
    public delegate void ScreenTipChangedEventDelegate(ScreenTipChangedEventArgs args);

    public class ControllerChangedEventArgs
    {
        private RibbonController oldController;
        private RibbonController newController;

        public ControllerChangedEventArgs()
        {
            oldController = null;
            newController = null;
        }

        public ControllerChangedEventArgs(RibbonController oldController, RibbonController newController)
        {
            this.oldController = oldController;
            this.newController = newController;
        }

        public RibbonController OldController
        {
            get { return oldController; }
            set { oldController = value; }
        }

        public RibbonController NewController
        {
            get { return newController; }
            set { newController = value; }
        }
    }
    public delegate void ControllerChangedEventDelegate(ControllerChangedEventArgs args);

    public class SubMenuChangedEventArgs
    {
        private ContextMenu oldSubMenu;
        private ContextMenu newSubMenu;

        public SubMenuChangedEventArgs()
        {
            oldSubMenu = null;
            newSubMenu = null;
        }

        public SubMenuChangedEventArgs(ContextMenu oldSubMenu, ContextMenu newSubMenu)
        {
            this.oldSubMenu = oldSubMenu;
            this.newSubMenu = newSubMenu;
        }

        public ContextMenu OldSubMenu
        {
            get { return oldSubMenu; }
            set { oldSubMenu = value; }
        }

        public ContextMenu NewSubMenu
        {
            get { return newSubMenu; }
            set { newSubMenu = value; }
        }
    }
    public delegate void SubMenuChangedEventDelegate(SubMenuChangedEventArgs args);
}
