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
    public partial class RibbonHalfButtonGroup : RibbonControlBase
    {
        private ListenableList<IRibbonHalfControl> controls = new ListenableList<IRibbonHalfControl>();

        protected override void initialize()
        {
            InitializeComponent();

            this.showDefaultContextMenu = false;

            hasQATbutton = false;
            this.EnforceSelectOne = false;

            controls.ElementAdded += new ListenableList<IRibbonHalfControl>.ElementAddedDelegate(controls_ElementAdded);
            controls.ElementRemoved += new ListenableList<IRibbonHalfControl>.ElementRemovedDelegate(controls_ElementRemoved);
        }

        private void controls_ElementRemoved(ListenableList<IRibbonHalfControl> sender, ListenableList<IRibbonHalfControl>.ElementRemovedEventArgs<IRibbonHalfControl> args)
        {
            throw new NotImplementedException();
        }

        private void controls_ElementAdded(ListenableList<IRibbonHalfControl> sender, ListenableList<IRibbonHalfControl>.ElementAddedEventArgs<IRibbonHalfControl> args)
        {
            if (controls.Count == 1)
            {
                controls[0].setFullBorder();
            }
            else if (controls.Count == 2)
            {
                controls[0].setLeftEndBorder();
                controls[1].setRightEndBorder();
            }
            else
            {
                controls[0].setLeftEndBorder();
                for (int i = 1; i < controls.Count - 1; i++)
                {
                    controls[i].setMiddleBorder();
                }
                controls[controls.Count - 1].setRightEndBorder();
            }

            args.Item.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(Item_SelectedChanged);
            contentStack.Children.Insert(args.Index, (UIElement)args.Item);
        }

        private void Item_SelectedChanged(object sender, bool newValue)
        {
            if (EnforceSelectOne && newValue == true)
            {
                for (int i = 0; i < controls.Count; i++)
                {
                    if (controls[i] != sender)
                    {
                        controls[i].IsSelected = false;
                    }
                }
            }
        }

        public ListenableList<IRibbonHalfControl> Children
        {
            get
            {
                return controls;
            }
        }

        public double ChildrenWidth
        {
            get
            {
                double width = 0;
                for (int i = 0; i < controls.Count; i++)
                {
                    width += controls[i].ActualWidth;
                }
                return width;
            }
        }

        public bool EnforceSelectOne { get; set; }

        #region assessors
        public override RibbonController Controller
        {
            get
            {
                return controller;
            }
            set
            {
                base.Controller = value;

                foreach (IRibbonHalfControl c in controls)
                {
                    c.Controller = value;
                }
            }
        }

        public override List<QuickAccessButton> getQAButtons()
        {
            List<QuickAccessButton> buttons = new List<QuickAccessButton>();

            for (int i = 0; i < controls.Count; i++)
            {
                List<QuickAccessButton> iButtons = ((RibbonControlBase)controls[i]).getQAButtons();
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

            for (int i = 0; i < controls.Count; i++)
            {
                List<IRibbonControl> subList = ((IRibbonControl)controls[i]).getElements();
                for (int j = 0; j < subList.Count; j++)
                {
                    list.Add(subList[j]);
                }
            }

            return list;
        }

        public override double KeyboardAccessVerticalOffset
        {
            get
            {
                return keyVerticalOffset;
            }
            set
            {
                keyVerticalOffset = value;
                for (int i = 0; i < controls.Count; i++)
                {
                    ((RibbonControlBase)(controls[i])).KeyboardAccessVerticalOffset = value;
                }
            }
        }

        public override bool IsMouseInside
        {
            get
            {
                bool result = false;
                foreach (IRibbonHalfControl hc in controls)
                {
                    result = result || hc.IsMouseInside;
                }
                return base.IsMouseInside || result;
            }
        }
        #endregion
    }
}
