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
    public partial class RibbonThreeRowsLayout : RibbonControlBase, IRibbonFullControl 
    {
        #region class variables
        private ListenableList<RibbonThirdLabel> topGroups = new ListenableList<RibbonThirdLabel>();
        private ListenableList<RibbonThirdLabel> middleGroups = new ListenableList<RibbonThirdLabel>();
        private ListenableList<RibbonThirdLabel> bottomGroups = new ListenableList<RibbonThirdLabel>();
        #endregion

        #region constructor / initialize
        protected override void initialize()
        {
            InitializeComponent();

            canResize = true;
            resizePriority = 2;
            showDefaultContextMenu = false;

            topGroups.ElementAdded += new ListenableList<RibbonThirdLabel>.ElementAddedDelegate(elementAdded);
            middleGroups.ElementAdded += new ListenableList<RibbonThirdLabel>.ElementAddedDelegate(elementAdded);
            bottomGroups.ElementAdded += new ListenableList<RibbonThirdLabel>.ElementAddedDelegate(elementAdded);
            topGroups.ElementRemoved += new ListenableList<RibbonThirdLabel>.ElementRemovedDelegate(elementRemoved);
            middleGroups.ElementRemoved += new ListenableList<RibbonThirdLabel>.ElementRemovedDelegate(elementRemoved);
            bottomGroups.ElementRemoved += new ListenableList<RibbonThirdLabel>.ElementRemovedDelegate(elementRemoved);

            hasQATbutton = false;
        }
        #endregion

        #region children handlers
        private void elementRemoved(ListenableList<RibbonThirdLabel> sender, ListenableList<RibbonThirdLabel>.ElementRemovedEventArgs<RibbonThirdLabel> args)
        {
            if (topGrid.Children.Contains((UIElement)args.Item))
            {
                topGrid.Children.Remove((UIElement)args.Item);
            }
            else if (middleGrid.Children.Contains((UIElement)args.Item))
            {
                middleGrid.Children.Remove((UIElement)args.Item);
            }
            else if (bottomGrid.Children.Contains((UIElement)args.Item))
            {
                bottomGrid.Children.Remove((UIElement)args.Item);
            }
        }

        private void elementAdded(ListenableList<RibbonThirdLabel> sender,
            ListenableList<RibbonThirdLabel>.ElementAddedEventArgs<RibbonThirdLabel> args)
        {
            if (sender == topGroups)
            {
                topGrid.Children.Insert(args.Index, args.Item);
                args.Item.KeyboardAccessVerticalOffset = -8;
            }
            else if (sender == middleGroups)
            {
                middleGrid.Children.Insert(args.Index, args.Item);
                args.Item.KeyboardAccessVerticalOffset = 2;
            }
            else if (sender == bottomGroups)
            {
                bottomGrid.Children.Insert(args.Index, args.Item);
                args.Item.KeyboardAccessVerticalOffset = 13;
            }
        }

        public ListenableList<RibbonThirdLabel> TopChildren
        {
            get
            {
                return topGroups;
            }
        }

        public ListenableList<RibbonThirdLabel> MiddleChildren
        {
            get
            {
                return middleGroups;
            }
        }

        public ListenableList<RibbonThirdLabel> BottomChildren
        {
            get
            {
                return bottomGroups;
            }
        }
        #endregion

        #region position handler stuff
        public Position getPosition(UIElement element)
        {
            if (topGrid.Children.Contains(element))
            {
                return Position.top;
            }
            else if (middleGrid.Children.Contains(element))
            {
                return Position.middle;
            }
            else if (bottomGrid.Children.Contains(element))
            {
                return Position.bottom;
            }
            else
            {
                return Position.unset;
            }
        }

        public enum Position
        {
            top, middle, bottom, unset
        }
        #endregion

        #region IRibbonGroupBoxChild Members

        public override bool resizeSmaller()
        {
            this.UpdateLayout();
            bool smaller = false;

            #region top grid
            for (int i = 0; i < topGrid.Children.Count; i++)
            {
                if (((RibbonControlBase)topGrid.Children[i]).CanResize &&
                    ((RibbonControlBase)topGrid.Children[i]).resizeSmaller())
                {
                    smaller = true;
                }
            }
            #endregion

            #region middle grid
            for (int i = 0; i < middleGrid.Children.Count; i++)
            {
                if (((RibbonControlBase)middleGrid.Children[i]).CanResize &&
                    ((RibbonControlBase)middleGrid.Children[i]).resizeSmaller())
                {
                    smaller = true;
                }
            }
            #endregion

            #region bottom grid
            for (int i = 0; i < bottomGrid.Children.Count; i++)
            {
                if (((RibbonControlBase)bottomGrid.Children[i]).CanResize &&
                    ((RibbonControlBase)bottomGrid.Children[i]).resizeSmaller())
                {
                    smaller = true;
                }
            }
            #endregion

            return smaller;
        }

        public override bool resizeBigger()
        {
            bool bigger = false;

            #region top grid
            for (int i = 0; i < topGrid.Children.Count; i++)
            {
                if (((RibbonControlBase)topGrid.Children[i]).CanResize &&
                    ((RibbonControlBase)topGrid.Children[i]).resizeBigger())
                {
                    bigger = true;
                }
            }
            #endregion

            #region middle grid
            for (int i = 0; i < middleGrid.Children.Count; i++)
            {
                if (((RibbonControlBase)middleGrid.Children[i]).CanResize &&
                    ((RibbonControlBase)middleGrid.Children[i]).resizeBigger())
                {
                    bigger = true;
                }
            }
            #endregion

            #region bottom grid
            for (int i = 0; i < bottomGrid.Children.Count; i++)
            {
                if (((RibbonControlBase)bottomGrid.Children[i]).CanResize &&
                    ((RibbonControlBase)bottomGrid.Children[i]).resizeBigger())
                {
                    bigger = true;
                }
            }
            #endregion

            return bigger;
        }

        public override double ResizeIncreaseAmount
        {
            get
            {
                #region top grid
                double topAmount = 0;
                for (int i = 0; i < topGrid.Children.Count; i++)
                {
                    if (((RibbonControlBase)topGrid.Children[i]).CanResize)
                    {
                        topAmount += ((RibbonControlBase)topGrid.Children[i]).ResizeIncreaseAmount;
                    }
                }
                #endregion

                #region middle grid
                double middleAmount = 0;
                for (int i = 0; i < middleGrid.Children.Count; i++)
                {
                    if (((RibbonControlBase)topGrid.Children[i]).CanResize)
                    {
                        middleAmount += ((RibbonControlBase)topGrid.Children[i]).ResizeIncreaseAmount;
                    }
                }
                #endregion

                #region bottom grid
                double bottomAmount = 0;
                for (int i = 0; i < bottomGrid.Children.Count; i++)
                {
                    if (((RibbonControlBase)topGrid.Children[i]).CanResize)
                    {
                        bottomAmount += ((RibbonControlBase)topGrid.Children[i]).ResizeIncreaseAmount;
                    }
                }
                #endregion

                return Math.Max(topAmount, Math.Max(middleAmount, bottomAmount));
            }
        }
        #endregion

        #region accessors
        public override RibbonController Controller
        {
            set
            {
                base.Controller = value;

                for (int i = 0; i < topGrid.Children.Count; i++)
                {
                    ((RibbonControlBase)topGrid.Children[i]).Controller = value;
                }

                for (int i = 0; i < middleGrid.Children.Count; i++)
                {
                    ((RibbonControlBase)middleGrid.Children[i]).Controller = value;
                }

                for (int i = 0; i < bottomGrid.Children.Count; i++)
                {
                    ((RibbonControlBase)bottomGrid.Children[i]).Controller = value;
                }
            }
        }

        public override List<QuickAccessButton> getQAButtons()
        {
            List<QuickAccessButton> buttons = new List<QuickAccessButton>();

            for (int i = 0; i < topGroups.Count; i++)
            {
                List<QuickAccessButton> iButtons = topGroups[i].getQAButtons();
                for (int j = 0; j < iButtons.Count; j++)
                {
                    if (iButtons[j] != null)
                    {
                        buttons.Add(iButtons[j]);
                    }
                }
            }

            for (int i = 0; i < middleGroups.Count; i++)
            {
                List<QuickAccessButton> iButtons = middleGroups[i].getQAButtons();
                for (int j = 0; j < iButtons.Count; j++)
                {
                    if (iButtons[j] != null)
                    {
                        buttons.Add(iButtons[j]);
                    }
                }
            }

            for (int i = 0; i < bottomGroups.Count; i++)
            {
                List<QuickAccessButton> iButtons = bottomGroups[i].getQAButtons();
                for (int j = 0; j < iButtons.Count; j++)
                {
                    if (iButtons[j] != null)
                    {
                        buttons.Add(iButtons[j]);
                    }
                }
            }

            this.Controller = this.Controller; //### HACK FIX ###

            return buttons;
        }

        public override List<IRibbonControl> getElements()
        {
            List<IRibbonControl> list = new List<IRibbonControl>();

            for (int i = 0; i < topGroups.Count; i++)
            {
                List<IRibbonControl> subList = topGroups[i].getElements();
                for (int j = 0; j < subList.Count; j++)
                {
                    list.Add(subList[j]);
                }
            }

            for (int i = 0; i < middleGroups.Count; i++)
            {
                List<IRibbonControl> subList = middleGroups[i].getElements();
                for (int j = 0; j < subList.Count; j++)
                {
                    list.Add(subList[j]);
                }
            }

            for (int i = 0; i < bottomGroups.Count; i++)
            {
                List<IRibbonControl> subList = bottomGroups[i].getElements();
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
                foreach (RibbonThirdLabel group in topGroups)
                {
                    result = result | group.IsMouseInside;
                }
                foreach (RibbonThirdLabel group in middleGroups)
                {
                    result = result | group.IsMouseInside;
                }
                foreach (RibbonThirdLabel group in bottomGroups)
                {
                    result = result | group.IsMouseInside;
                }
                return base.IsMouseInside || result;
            }
        }
        #endregion
    }
}
