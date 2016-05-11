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
    /// Interaction logic for RibbonBar.xaml
    /// </summary>
    public partial class RibbonBar : RibbonControlBase
    {
        private ListenableList<RibbonGroupBox> boxes = new ListenableList<RibbonGroupBox>();
        private List<IRibbonControl> elements = new List<IRibbonControl>();

        #region resizing
        public delegate void RefreshDelegate();
        private double lastNewWidth;
        #endregion

        public RibbonBar()
        {
            InitializeComponent();

            KeyboardAccessVerticalOffset = 16;

            this.Focusable = true;

            boxes.ElementAdded += new ListenableList<RibbonGroupBox>.ElementAddedDelegate(boxes_ElementAdded);
            boxes.ElementRemoved += new ListenableList<RibbonGroupBox>.ElementRemovedDelegate(boxes_ElementRemoved);
        }

        #region children handlers
        private void boxes_ElementRemoved(ListenableList<RibbonGroupBox> sender, ListenableList<RibbonGroupBox>.ElementRemovedEventArgs<RibbonGroupBox> args)
        {
            ribbonPanel.Children.RemoveAt(args.Index);

            if (controller != null)
            {
                if (controller.ParentWindow != null)
                {
                    controller.ParentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        this.resizeBigger();
                        this.resizeSmaller();
                    }));
                }
                else if (controller.ParentPage != null)
                {
                    controller.ParentPage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        this.resizeBigger();
                        this.resizeSmaller();
                    }));
                }
            }
        }

        private void boxes_ElementAdded(ListenableList<RibbonGroupBox> sender, ListenableList<RibbonGroupBox>.ElementAddedEventArgs<RibbonGroupBox> args)
        {
            ribbonPanel.Children.Insert(args.Index, args.Item);
            args.Item.ParentRibbonBar = this;

            if (this.Controller != null)
            {
                args.Item.Controller = this.Controller;
            }

            if (controller != null)
            {
                if (controller.ParentWindow != null)
                {
                    controller.ParentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        this.resizeSmaller();
                        this.resizeBigger();
                    }));
                }
                else if (controller.ParentPage != null)
                {
                    controller.ParentPage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        this.resizeSmaller();
                        this.resizeBigger();
                    }));
                }
            }
        }

        public ListenableList<RibbonGroupBox> Children
        {
            get
            {
                return boxes;
            }
        }

        internal void registerMasterComponent(IRibbonControl element)
        {
            elements.Add(element);
        }

        internal void unRegisterMasterComponent(IRibbonControl element)
        {
            elements.Remove(element);
        }
        #endregion

        #region resizing handlers
        private new void resizeBigger()
        {
            resizeBigger(lastNewWidth);

            //HACK
            if (controller != null)
            {
                if (controller.ParentWindow != null)
                {
                    controller.ParentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        this.resizeSmaller();
                    }));
                }
                else if (controller.ParentPage != null)
                {
                    controller.ParentPage.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshDelegate(delegate()
                    {
                        this.resizeSmaller();
                    }));
                }
            }
        }

        internal void resizeBigger(double newWidth)
        {
            //Console.WriteLine("BIGGER");
            lastNewWidth = newWidth;

            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].Minimised)
                {
                    if (getChildrenWidths() + boxes[i].ResizeIncreaseAmount < newWidth)
                    {
                        boxes[i].resizeBigger();
                        this.UpdateLayout();
                    }
                    else
                    {
                        return;
                    }
                }
            }

            bool primaryResizeFail = false;
            bool primaryResizeOk = false;

            #region secondary
                //Console.WriteLine("Bigger");
                while (!primaryResizeFail && !primaryResizeOk)
                {
                    //sort
                    elements.Sort(new IRibbonControlBaseComparer(false));

                    bool resizedOne = false;
                    for (int i = 0; i < elements.Count; i++)
                    {
                        //Console.WriteLine("Testing [" + i + "]");
                        if (elements[i].CanResize)
                        {
                            #region make bigger
                            if (getChildrenWidths() < newWidth)
                            {
                                if (getChildrenWidths() + elements[i].ResizeIncreaseAmount >= newWidth)
                                {
                                    return;
                                }
                                else if (elements[i].resizeBigger())
                                {
                                    resizedOne = true;
                                    this.UpdateLayout();

                                    #region check completion
                                    if (getChildrenWidths() >= newWidth)
                                    {
                                        elements[i].resizeSmaller();
                                        this.UpdateLayout();
                                        primaryResizeOk = true;
                                    }
                                    else //added the else
                                    {
                                        //primaryResizeFail = true;
                                    }
                                    #endregion

                                    continue;

                                    //Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new RefreshDelegate(resizeSmaller));
                                    //return;
                                }
                            }
                            #endregion
                            #region correct size
                            else
                            {
                                primaryResizeOk = true;
                            }
                            #endregion
                        }
                    }

                    if (!resizedOne)
                    {
                        primaryResizeFail = true; //was uncommented
                    }
                }
            #endregion
        }

        private new void resizeSmaller()
        {
            resizeSmaller(lastNewWidth);
        }

        internal void resizeSmaller(double newWidth)
        {
            //Console.WriteLine("SMALLER");
            lastNewWidth = newWidth;

            bool primaryResizeFail = false;
            bool primaryResizeOk = false;

            #region primary

            //Console.WriteLine("Smaller");
            while (!primaryResizeFail && !primaryResizeOk)
            {
                //sort
                elements.Sort(new IRibbonControlBaseComparer(true));

                bool resizedOne = false;
                for (int i = 0; i < elements.Count && !resizedOne; i++)
                {
                    //Console.WriteLine("Testing [" + i + "]");
                    if (elements[i].CanResize)
                    {
                        if (getChildrenWidths() > newWidth)
                        {
                            if (elements[i].resizeSmaller())
                            {
                                //Console.WriteLine("cWidth = " + getChildrenWidths() + ", newWidth = " + newWidth);
                                this.UpdateLayout();

                                #region check completion
                                if (getChildrenWidths() <= newWidth)
                                {
                                    primaryResizeOk = true;
                                    return;
                                }
                                #endregion

                                //Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new RefreshDelegate(resizeSmaller));
                                //return;
                                resizedOne = true;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                if (!resizedOne)
                {
                    primaryResizeFail = true;
                }
            }
            #endregion

            #region secondary
            if (primaryResizeFail && getChildrenWidths() > newWidth)
            {
                for (int i = boxes.Count - 1; i >= 0; i--)
                {
                    if (!boxes[i].Minimised)
                    {
                        if (getChildrenWidths() > newWidth)
                        {
                            boxes[i].Minimised = true;
                            this.UpdateLayout();
                            //Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new RefreshDelegate(resizeSmaller));
                            //return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            #endregion
        }

        internal double getChildrenWidths()
        {
            double width = 0;
            for (int i = 0; i < boxes.Count; i++)
            {
                width += boxes[i].ActualWidth;
            }
            width += boxes.Count * 5;
            return width + 20;
        }

        private class IRibbonControlBaseComparer : IComparer<IRibbonControl>
        {
            private bool ascending = false;

            public IRibbonControlBaseComparer(bool ascending)
            {
                this.ascending = ascending;
            }

            #region IComparer<RibbonControlBase> Members

            public int Compare(IRibbonControl x, IRibbonControl y)
            {
                if (ascending)
                {
                    return x.ResizePriority.CompareTo(y.ResizePriority);
                }
                else
                {
                    return y.ResizePriority.CompareTo(x.ResizePriority);
                }
            }

            #endregion
        }
        #endregion

        #region assessors
        public override RibbonController Controller
        {
            get
            {
                return controller;
            }
            set
            {
                controller = value;
                for (int i = 0; i < boxes.Count; i++)
                {
                    boxes[i].Controller = value;
                }
            }
        }

        public override List<QuickAccessButton> getQAButtons()
        {
            List<QuickAccessButton> buttons = new List<QuickAccessButton>();

            for (int i = 0; i < boxes.Count; i++)
            {
                List<QuickAccessButton> iButtons = boxes[i].getQAButtons();
                for (int j = 0; j < iButtons.Count; j++)
                {
                    buttons.Add(iButtons[j]);
                }
            }

            return buttons;
        }

        public override List<IRibbonControl> getElements()
        {
            List<IRibbonControl> list = new List<IRibbonControl>();
            list.Add(this);

            for (int i = 0; i < boxes.Count; i++)
            {
                List<IRibbonControl> subList = boxes[i].getElements();
                for (int j = 0; j < subList.Count; j++)
                {
                    list.Add(subList[j]);
                }
            }

            return list;
        }

        public override bool IsMouseInside
        {
            get
            {
                bool result = false;
                foreach (RibbonGroupBox box in boxes)
                {
                    result = result || box.IsMouseInside;
                }
                return base.IsMouseInside || result;
            }
        }
        #endregion

    }
}
