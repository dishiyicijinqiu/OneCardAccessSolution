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
    /// Interaction logic for RibbonGroupBox.xaml
    /// </summary>
    public partial class RibbonGroupBox : RibbonControlBase
    {
        #region class variables
        private ListenableList<IRibbonFullControl> elements = new ListenableList<IRibbonFullControl>();
        private bool showLabel = false;
        private RibbonGroupBoxLabelButton gbl = null;
        public event MouseButtonEventHandler LabelButtonPressed;
        private RibbonBar parentBar = null;
        private bool minimised = false;
        #endregion

        #region initialisation
        protected override void initialize()
        {
            InitializeComponent();

            RibbonStyleHandler.styleGroupBorder(mainBorder, labelBorder, this, this);

            gbl = new RibbonGroupBoxLabelButton();
            gbl.HorizontalAlignment = HorizontalAlignment.Right;
            gbl.Margin = new Thickness(0, 0, 3, 0);
            gbl.Clicked += new MouseButtonEventHandler(gbl_Clicked);

            this.TextChanged += new TextChangedEventDelegate(RibbonGroupBox_TextChanged);

            canResize = true;
            resizePriority = 9;
            showDefaultContextMenu = false;

            hasQATbutton = false;

            elements.ElementAdded += new ListenableList<IRibbonFullControl>.ElementAddedDelegate(elements_ElementAdded);
            elements.ElementRemoved += new ListenableList<IRibbonFullControl>.ElementRemovedDelegate(elements_ElementRemoved);
        }

        private void gbl_Clicked(object sender, MouseButtonEventArgs e)
        {
            fireLabelButtonPressed(gbl, e);
        }

        private void RibbonGroupBox_TextChanged(TextChangedEventArgs args)
        {
            titleLabel.Content = args.NewText;
        }
        #endregion

        public override void updateStyle(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            titleLabel.Foreground = RibbonStyleHandler.GroupText;
        }

        #region assessors

        #region children handler
        public ListenableList<IRibbonFullControl> Children
        {
            get
            {
                return elements;
            }
        }

        private void elements_ElementRemoved(ListenableList<IRibbonFullControl> sender, ListenableList<IRibbonFullControl>.ElementRemovedEventArgs<IRibbonFullControl> args)
        {
            contentStackPanel.Children.Remove((UIElement)args.Item);

            if (parentBar != null)
            {
                parentBar.unRegisterMasterComponent(args.Item);
            }
        }

        private void elements_ElementAdded(ListenableList<IRibbonFullControl> sender, ListenableList<IRibbonFullControl>.ElementAddedEventArgs<IRibbonFullControl> args)
        {
            contentStackPanel.Children.Insert(args.Index, (UIElement)args.Item);

            if (parentBar != null)
            {
                parentBar.registerMasterComponent(args.Item);
            }

            if (this.Controller != null)
            {
                args.Item.Controller = this.Controller;
            }
        }
        #endregion

        internal RibbonBar ParentRibbonBar
        {
            get
            {
                return parentBar;
            }
            set
            {
                #region unregister old components
                if (parentBar != null)
                {
                    foreach (RibbonControlBase r in elements)
                    {
                        parentBar.unRegisterMasterComponent(r);
                    }
                }
                #endregion

                parentBar = value;
                #region resister new component
                if (parentBar != null)
                {
                    foreach (RibbonControlBase r in elements)
                    {
                        parentBar.registerMasterComponent(r);
                    }
                }
                #endregion
            }
        }

        public bool ShowLabelButton
        {
            set
            {
                if (showLabel == true && value == false)
                {
                    labelGrid.Children.Remove(gbl);
                    showLabel = value;
                }
                else if (showLabel == false && value == true)
                {
                    labelGrid.Children.Add(gbl);
                    showLabel = value;
                }
            }
            get
            {
                return showLabel;
            }
        }
        #endregion

        #region event handlers
        internal void fireLabelButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (LabelButtonPressed != null)
            {
                LabelButtonPressed(sender, e);
            }
        }
        #endregion

        #region minimising
        private List<IRibbonFullControl> minE = new List<IRibbonFullControl>();
        private object minOldContent = null;
        private RibbonDoubleButton rdb = null;
        private RibbonGroupBoxPopup rPopup = null;
        private double lastWidth = 0;

        internal bool Minimised
        {
            get
            {
                return minimised;
            }
            set
            {
                #region minimise
                if (minimised == false && value == true)
                {
                    lastWidth = this.ActualWidth;

                    #region maximise all children
                    for (int i = 0; i < elements.Count; i++)
                    {
                        //while (elements[i].resizeBigger())
                        {
                        }
                    }
                    #endregion

                    #region remove and store children
                    for (int i = elements.Count - 1; i >= 0; i--)
                    {
                        minE.Add(elements[i]);
                        //removeRibbonComponent(elements[i]);
                        this.Children.Remove(elements[i]);
                    }
                    #endregion

                    #region remove old content / set new
                    minOldContent = this.Content;

                    if (rdb == null)
                    {
                        rdb = new RibbonDoubleButton();
                        rdb.Height = 88;
                        rdb.Text = this.Text; 
                        rdb.Margin = new Thickness(0);
                        rdb.BottomButtonPressed += new MouseButtonEventHandler(rdb_BottomButtonPressed);
                    }
                    this.Content = rdb;
                    #endregion

                    #region make popup
                    rPopup = new RibbonGroupBoxPopup(this.Text);
                    for (int i = minE.Count - 1; i >= 0; i--)
                    {
                        rPopup.addRibbonComponent(minE[i]);
                    }
                    #endregion

                    #region resize child components to max width
                    //ribbonpreviewboxes must not keep being called!!!!
                    for (int i = 0; i < minE.Count; i++)
                    {
                        if (minE[i].GetType().Equals(Type.GetType("DNBSoft.WPF.RibbonControl.RibbonPreviewBoxes")))
                        {
                            minE[i].resizeBigger();
                        }
                        else if (minE[i].GetType().Equals(Type.GetType("DNBSoft.WPF.RibbonControl.RibbonThreeButtonsGroupLayout")))
                        {
                            minE[i].resizeBigger();
                            minE[i].resizeBigger();
                            minE[i].resizeBigger();
                            minE[i].resizeBigger();
                        }
                        else
                        {
                            if (minE[i].CanResize)
                            {
                                while (minE[i].resizeBigger())
                                {
                                }
                            }
                        }

                    }

                    #endregion

                    minimised = true;
                }
                #endregion
                #region restore
                else if (minimised == true && value == false)
                {
                    if (rPopup != null)
                    {
                        //remove children
                        rPopup.removeRibbonComponents();

                        //remove button
                        this.Content = minOldContent;



                        #region resize child components to min width
                        //ribbonpreviewboxes must not keep being called!!!!
                        for (int i = 0; i < minE.Count; i++)
                        {
                            if (minE[i].GetType().Equals(Type.GetType("DNBSoft.WPF.RibbonControl.RibbonThreeButtonsGroupLayout")))
                            {
                                RibbonThreeButtonsGroupLayout tbg = (RibbonThreeButtonsGroupLayout)minE[i];
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                                tbg.resizeSmaller();
                            }
                            else if (minE[i].CanResize)
                            {
                                while (minE[i].resizeSmaller())
                                {
                                }
                            }

                        }
                        #endregion

                        //add components
                        for (int i = minE.Count - 1; i >= 0; i--)
                        {
                            //addRibbonComponent(minE[i]);
                            this.Children.Add(minE[i]);
                        }

                        //empty minE
                        minE = new List<IRibbonFullControl>();

                        minimised = false;
                    }
                }
                #endregion
            }
        }

        private void rdb_BottomButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (rPopup != null)
            {
                rPopup.PlacementTarget = this;
                rPopup.VerticalOffset = (this.Height * -1.0) + this.Height;
                rPopup.IsOpen = true;
            }
        }
        #endregion

        #region resizing

        public override bool resizeSmaller()
        {
            if (Minimised)
            {
                return false;
            }
            else
            {
                Minimised = true;
                return true;
            }
        }

        public override bool resizeBigger()
        {
            if (Minimised)
            {
                Minimised = false;
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
                return lastWidth - this.ActualWidth; 
            }
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
                base.Controller = value;

                for (int count = 0; count < elements.Count; count++)
                {
                    elements[count].Controller = value;
                }
            }
        }

        public override List<QuickAccessButton> getQAButtons()
        {
            List<QuickAccessButton> buttons = new List<QuickAccessButton>();
            buttons.Add(this.getQAButton());

            for (int i = 0; i < elements.Count; i++)
            {
                List<QuickAccessButton> iButtons = elements[i].getQAButtons();
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
            list.Add(this);

            if (gbl != null && showLabel)
            {
                list.Add(gbl);
            }

            for (int i = 0; i < elements.Count; i++)
            {
                List<IRibbonControl> subList = elements[i].getElements();
                for (int j = 0; j < subList.Count; j++)
                {
                    list.Add(subList[j]);
                }
            }

            for (int i = 0; i < minE.Count; i++)
            {
                List<IRibbonControl> subList = minE[i].getElements();
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
                foreach (RibbonControlBase b in elements)
                {
                    result = result || b.IsMouseInside;
                }
                return base.IsMouseInside || result;
            }
        }
        #endregion
    }
}
