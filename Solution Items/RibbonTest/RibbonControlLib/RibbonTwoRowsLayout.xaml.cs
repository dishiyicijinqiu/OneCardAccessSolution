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
    public partial class RibbonTwoRowsLayout : RibbonControlBase, IRibbonFullControl 
    {
        #region class variables
        private ListenableList<RibbonHalfButtonGroup> topGroups = new ListenableList<RibbonHalfButtonGroup>();
        private ListenableList<RibbonHalfButtonGroup> bottomGroups = new ListenableList<RibbonHalfButtonGroup>();

        private bool smallSize = false;
        private bool largeSize = true;
        #endregion

        #region constructor / initialization
        protected override void initialize()
        {
            InitializeComponent();

            canResize = true;
            resizePriority = 3;
            showDefaultContextMenu = false;

            topGroups.ElementAdded += new ListenableList<RibbonHalfButtonGroup>.ElementAddedDelegate(elementAdded);
            bottomGroups.ElementAdded += new ListenableList<RibbonHalfButtonGroup>.ElementAddedDelegate(elementAdded);
            topGroups.ElementRemoved += new ListenableList<RibbonHalfButtonGroup>.ElementRemovedDelegate(elementRemoved);
            bottomGroups.ElementRemoved += new ListenableList<RibbonHalfButtonGroup>.ElementRemovedDelegate(elementRemoved);

            hasQATbutton = false;
        }
        #endregion

        #region children handler
        private void elementRemoved(ListenableList<RibbonHalfButtonGroup> sender, ListenableList<RibbonHalfButtonGroup>.ElementRemovedEventArgs<RibbonHalfButtonGroup> args)
        {
            if (topGrid.Children.Contains((UIElement)args.Item))
            {
                topGrid.Children.Remove((UIElement)args.Item);
            }
            else if (bottomGrid.Children.Contains((UIElement)args.Item))
            {
                bottomGrid.Children.Remove((UIElement)args.Item);
            }
        }

        private void elementAdded(ListenableList<RibbonHalfButtonGroup> sender, 
            ListenableList<RibbonHalfButtonGroup>.ElementAddedEventArgs<RibbonHalfButtonGroup> args)
        {
            if (smallSize)
            {
                populateThreeRows();
            }
            else if (sender == topGroups)
            {
                topGrid.Children.Insert(args.Index, args.Item);
                args.Item.KeyboardAccessVerticalOffset = -11;
            }
            else if (sender == bottomGroups)
            {
                bottomGrid.Children.Insert(args.Index, args.Item);
                args.Item.KeyboardAccessVerticalOffset = 22;
            }
        }

        public ListenableList<RibbonHalfButtonGroup> TopChildren
        {
            get
            {
                return topGroups;
            }
        }

        public ListenableList<RibbonHalfButtonGroup> BottomChildren
        {
            get
            {
                return bottomGroups;
            }
        }
        #endregion

        #region position helper stuff
        public Position getPosition(UIElement element)
        {
            if (topGrid.Children.Contains(element))
            {
                return Position.top;
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
            top, bottom, unset
        }

        private class GroupWidthComparer : IComparer<RibbonHalfButtonGroup>
        {
            public GroupWidthComparer()
                : base()
            {

            }

            #region IComparer<RibbonHalfButtonGroup> Members

            public int Compare(RibbonHalfButtonGroup x, RibbonHalfButtonGroup y)
            {
                return y.ChildrenWidth.CompareTo(x.ChildrenWidth);
            }

            #endregion
        }
        #endregion

        #region IRibbonResizing Members

        public override bool resizeSmaller()
        {
            if (largeSize)
            {
                #region remove children from rows
                topGrid.Children.RemoveRange(0, topGrid.Children.Count);
                bottomGrid.Children.RemoveRange(0, bottomGrid.Children.Count);
                #endregion

                #region redistribute rows
                GridLengthConverter myGridLengthConverter = new GridLengthConverter();

                rd1.Height = (GridLength)myGridLengthConverter.ConvertFromString("*");
                rd2.Height = new GridLength(1);
                rd3.Height = (GridLength)myGridLengthConverter.ConvertFromString("*");
                rd4.Height = new GridLength(1);
                rd5.Height = (GridLength)myGridLengthConverter.ConvertFromString("*");
                #endregion

                populateThreeRows();

                largeSize = false;
                smallSize = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void populateThreeRows()
        {
            #region remove children from rows
            topGrid.Children.RemoveRange(0, topGrid.Children.Count);
            bottomGrid.Children.RemoveRange(0, bottomGrid.Children.Count);
            tr1.Children.RemoveRange(0, tr1.Children.Count);
            tr2.Children.RemoveRange(0, tr2.Children.Count);
            tr3.Children.RemoveRange(0, tr3.Children.Count);
            #endregion

            #region make sorted list by width desc
            List<RibbonHalfButtonGroup> groups = new List<RibbonHalfButtonGroup>();
            groups.AddRange(topGroups);
            groups.AddRange(bottomGroups);
            groups.Sort(new GroupWidthComparer());
            #endregion

            #region make new groups
            ListenableList<RibbonHalfButtonGroup> tG = new ListenableList<RibbonHalfButtonGroup>();
            ListenableList<RibbonHalfButtonGroup> mG = new ListenableList<RibbonHalfButtonGroup>();
            ListenableList<RibbonHalfButtonGroup> bG = new ListenableList<RibbonHalfButtonGroup>();

            for (int i = 0; i < groups.Count; i++)
            {
                ListenableList<RibbonHalfButtonGroup> addToGroup = RibbonTwoRowsLayout.getSmallestGroup(tG, mG, bG);
                addToGroup.Add(groups[i]);
            }
            #endregion

            #region populate rows
            for (int i = 0; i < tG.Count; i++)
            {
                tr1.Children.Add(tG[i]);
                tG[i].KeyboardAccessVerticalOffset = -8;
            }

            for (int i = 0; i < mG.Count; i++)
            {
                tr2.Children.Add(mG[i]);
                mG[i].KeyboardAccessVerticalOffset = 2;
            }

            for (int i = 0; i < bG.Count; i++)
            {
                tr3.Children.Add(bG[i]);
                bG[i].KeyboardAccessVerticalOffset = 13;
            }
            #endregion

        }

        public override bool resizeBigger()
        {
            if (smallSize)
            {
                #region remove children from rows
                tr1.Children.RemoveRange(0, tr1.Children.Count);
                tr2.Children.RemoveRange(0, tr2.Children.Count);
                tr3.Children.RemoveRange(0, tr3.Children.Count);
                #endregion

                #region redistribute rows
                GridLengthConverter myGridLengthConverter = new GridLengthConverter();

                rd2.Height = (GridLength)myGridLengthConverter.ConvertFromString("*");
                rd4.Height = (GridLength)myGridLengthConverter.ConvertFromString("*");
                rd1.Height = new GridLength(3);
                rd3.Height = new GridLength(4);
                rd5.Height = new GridLength(3);
                #endregion

                #region populate rows
                for (int i = 0; i < topGroups.Count; i++)
                {
                    topGrid.Children.Add(topGroups[i]);
                    topGroups[i].KeyboardAccessVerticalOffset = -11;
                }
                for (int i = 0; i < bottomGroups.Count; i++)
                {
                    bottomGrid.Children.Add(bottomGroups[i]);
                    bottomGroups[i].KeyboardAccessVerticalOffset = 22;
                }
                #endregion

                smallSize = false;
                largeSize = true;

                return true;
            }
            else
            {
                return false;
            }
        }

        public override double ResizeIncreaseAmount
        {
            get 
            {
                if (smallSize)
                {
                    double largestTwoRow = RibbonTwoRowsLayout.getListGroupWidth(
                        RibbonTwoRowsLayout.getLargestGroup(topGroups, bottomGroups));
                    double largestThreeRow = RibbonTwoRowsLayout.getListGroupWidth(
                        RibbonTwoRowsLayout.getLargestGroup(
                            RibbonTwoRowsLayout.getChildrenList(tr1),
                            RibbonTwoRowsLayout.getChildrenList(tr2),
                            RibbonTwoRowsLayout.getChildrenList(tr3)));

                    return (largestTwoRow - largestThreeRow) + 50;
                }
                else
                {
                    return 100;
                }
            }
        }

        #endregion

        #region statics

        private static ListenableList<RibbonHalfButtonGroup> getChildrenList(StackPanel p)
        {
            ListenableList<RibbonHalfButtonGroup> children = new ListenableList<RibbonHalfButtonGroup>();
            for (int i = 0; i < p.Children.Count; i++)
            {
                children.Add((RibbonHalfButtonGroup)p.Children[i]);
            }
            return children;
        }

        private static ListenableList<RibbonHalfButtonGroup> getSmallestGroup(ListenableList<RibbonHalfButtonGroup> g1,
            ListenableList<RibbonHalfButtonGroup> g2, ListenableList<RibbonHalfButtonGroup> g3)
        {
            double g1Width = RibbonTwoRowsLayout.getListGroupWidth(g1);
            double g2Width = RibbonTwoRowsLayout.getListGroupWidth(g2);
            double g3Width = RibbonTwoRowsLayout.getListGroupWidth(g3);

            if (g1Width <= g2Width && g1Width <= g3Width)
            {
                return g1;
            }
            else if (g2Width <= g1Width && g2Width <= g3Width)
            {
                return g2;
            }
            else
            {
                return g3;
            }
        }

        private static ListenableList<RibbonHalfButtonGroup> getSmallestGroup(ListenableList<RibbonHalfButtonGroup> g1,
            ListenableList<RibbonHalfButtonGroup> g2)
        {
            double g1Width = RibbonTwoRowsLayout.getListGroupWidth(g1);
            double g2Width = RibbonTwoRowsLayout.getListGroupWidth(g2);

            if (g1Width <= g2Width)
            {
                return g1;
            }
            else
            {
                return g2;
            }
        }

        private static ListenableList<RibbonHalfButtonGroup> getLargestGroup(ListenableList<RibbonHalfButtonGroup> g1,
            ListenableList<RibbonHalfButtonGroup> g2, ListenableList<RibbonHalfButtonGroup> g3)
        {
            double g1Width = RibbonTwoRowsLayout.getListGroupWidth(g1);
            double g2Width = RibbonTwoRowsLayout.getListGroupWidth(g2);
            double g3Width = RibbonTwoRowsLayout.getListGroupWidth(g3);

            if (g1Width >= g2Width && g1Width >= g3Width)
            {
                return g1;
            }
            else if (g2Width >= g1Width && g2Width >= g3Width)
            {
                return g2;
            }
            else
            {
                return g3;
            }
        }

        private static ListenableList<RibbonHalfButtonGroup> getLargestGroup(ListenableList<RibbonHalfButtonGroup> g1,
            ListenableList<RibbonHalfButtonGroup> g2)
        {
            double g1Width = RibbonTwoRowsLayout.getListGroupWidth(g1);
            double g2Width = RibbonTwoRowsLayout.getListGroupWidth(g2);

            if (g1Width >= g2Width)
            {
                return g1;
            }
            else
            {
                return g2;
            }
        }

        private static double getListGroupWidth(ListenableList<RibbonHalfButtonGroup> g)
        {
            double width = 0;
            foreach (RibbonHalfButtonGroup i in g)
            {
                width += i.ChildrenWidth;
            }
            return width;
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
                foreach (RibbonHalfButtonGroup group in topGroups)
                {
                    result = result | group.IsMouseInside;
                }
                foreach (RibbonHalfButtonGroup group in bottomGroups)
                {
                    result = result | group.IsMouseInside;
                }
                return base.IsMouseInside || result;
            }
        }
        #endregion
    }
}
