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
    /// Interaction logic for RibbonThreeButtonsGroupLayout.xaml
    /// </summary>
    public partial class RibbonThreeButtonsGroupLayout : RibbonControlBase, IRibbonFullControl 
    {
        private bool maximised = true;
        private bool minimisedMinLayout = false;

        private RibbonControlBase button1 = null;
        private RibbonControlBase button2 = null;
        private RibbonControlBase button3 = null;

        private RibbonThirdLabel minButton1 = null;
        private RibbonThirdLabel minButton2 = null;
        private RibbonThirdLabel minButton3 = null;

        private Grid maximisedLayout = null;
        private RibbonThreeRowsLayout minimisedLayout = null;

        public RibbonThreeButtonsGroupLayout()
        {
            InitializeComponent();

            canResize = true;
            resizePriority = 1.2;
            showDefaultContextMenu = false;

            #region make maximised layout
            maximisedLayout = new Grid();
            maximisedLayout.ColumnDefinitions.Add(new ColumnDefinition());
            maximisedLayout.ColumnDefinitions.Add(new ColumnDefinition());
            maximisedLayout.ColumnDefinitions.Add(new ColumnDefinition());
            maximisedLayout.Margin = new Thickness(0, -3, 0, -1);
            layout.Content = maximisedLayout;
            #endregion

            #region make minimsed layout
            minButton1 = new RibbonThirdLabel();
            minButton2 = new RibbonThirdLabel();
            minButton3 = new RibbonThirdLabel();

            minButton1.Clicked += new MouseButtonEventHandler(minButton1_Clicked);
            minButton2.Clicked += new MouseButtonEventHandler(minButton2_Clicked);
            minButton3.Clicked += new MouseButtonEventHandler(minButton3_Clicked);

            minimisedLayout = new RibbonThreeRowsLayout();
            minimisedLayout.TopChildren.Add(minButton1);
            minimisedLayout.MiddleChildren.Add(minButton2);
            minimisedLayout.BottomChildren.Add(minButton3);
            minimisedLayout.Margin = new Thickness(0, -3, 0, -1);
            #endregion
        }

        #region button click handlers
        private void minButton3_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (button3 != null)
            {
                button3.fireClickEvent(button3, e);
            }
        }

        private void minButton2_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (button2 != null)
            {
                button2.fireClickEvent(button2, e);
            }
        }

        private void minButton1_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (button1 != null)
            {
                button1.fireClickEvent(button1, e);
            }
        }
        #endregion

        #region accessors
        public RibbonControlBase Button1
        {
            get
            {
                return button1;
            }
            set
            {
                if (button1 != null)
                {
                    maximisedLayout.Children.Remove(button1);
                    button1.TextChanged -= new TextChangedEventDelegate(button1_TextChanged);
                    button1.ImageChanged -= new ImageChangedEventDelegate(button1_ImageChanged);
                    value.SelectedChanged -= new RibbonEventResources.SelectedChangedDelegate(value_SelectedChanged);
                    value.EnabledChanged -= new RibbonEventResources.EnableChangedDelegate(value_EnabledChanged);

                    if (!maximised)
                    {
                        ContextMenu subMenu = minButton1.SubMenu;
                        minButton1.SubMenu = null;
                        button1.SubMenu = subMenu;
                    }
                }
                button1 = value;

                if (value != null)
                {
                    maximisedLayout.Children.Add(button1);
                    Grid.SetColumn(button1, 0);

                    value.TextChanged += new TextChangedEventDelegate(button1_TextChanged);
                    button1_TextChanged(null);

                    minButton1.NormalImage = value.NormalImage.Clone();
                    value.ImageChanged += new ImageChangedEventDelegate(button1_ImageChanged);
                    value.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(value_SelectedChanged);
                    value.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(value_EnabledChanged);
                    value_EnabledChanged(value, value.IsEnabled);
                    value_SelectedChanged(value, value.IsSelected);

                    if (this.Controller != null && value.Controller != null)
                    {
                        value.Controller = this.Controller;
                    }

                    if (!maximised)
                    {
                        ContextMenu subMenu = button1.SubMenu;
                        button1.SubMenu = null;
                        minButton1.SubMenu = subMenu;
                    }
                }
                else
                {
                    minButton1.Text = "";
                    minButton1.NormalImage = null;
                }
            }
        }

        public RibbonControlBase Button2
        {
            get
            {
                return button2;
            }
            set
            {
                if (button2 != null)
                {
                    maximisedLayout.Children.Remove(button2);
                    button2.TextChanged -= new TextChangedEventDelegate(button2_TextChanged);
                    button2.ImageChanged -= new ImageChangedEventDelegate(button2_ImageChanged);
                    value.EnabledChanged -= new RibbonEventResources.EnableChangedDelegate(value_EnabledChanged);
                    value.SelectedChanged -= new RibbonEventResources.SelectedChangedDelegate(value_SelectedChanged);

                    if (!maximised)
                    {
                        ContextMenu subMenu = minButton2.SubMenu;
                        minButton2.SubMenu = null;
                        button2.SubMenu = subMenu;
                    }
                }
                button2 = value;

                if (value != null)
                {
                    maximisedLayout.Children.Add(button2);
                    Grid.SetColumn(button2, 1);

                    value.TextChanged += new TextChangedEventDelegate(button2_TextChanged);
                    button2_TextChanged(null);

                    minButton2.NormalImage = value.NormalImage.Clone();
                    value.ImageChanged += new ImageChangedEventDelegate(button2_ImageChanged);
                    value.EnabledChanged +=new RibbonEventResources.EnableChangedDelegate(value_EnabledChanged);
                    value.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(value_SelectedChanged);
                    value_EnabledChanged(value, value.IsEnabled);
                    value_SelectedChanged(value, value.IsSelected);

                    if (this.Controller != null && value.Controller != null)
                    {
                        value.Controller = this.Controller;
                    }

                    if (!maximised)
                    {
                        ContextMenu subMenu = button2.SubMenu;
                        button2.SubMenu = null;
                        minButton2.SubMenu = subMenu;
                    }
                }
                else
                {
                    minButton2.Text = "";
                    minButton2.NormalImage = null;
                }
            }
        }

        public RibbonControlBase Button3
        {
            get
            {
                return button3;
            }
            set
            {
                if (button3 != null)
                {
                    maximisedLayout.Children.Remove(button3);
                    button3.TextChanged -= new TextChangedEventDelegate(button3_TextChanged);
                    button3.ImageChanged -= new ImageChangedEventDelegate(button3_ImageChanged);
                    value.EnabledChanged -= new RibbonEventResources.EnableChangedDelegate(value_EnabledChanged);
                    value.SelectedChanged -= new RibbonEventResources.SelectedChangedDelegate(value_SelectedChanged);

                    if (!maximised)
                    {
                        ContextMenu subMenu = minButton3.SubMenu;
                        minButton3.SubMenu = null;
                        button3.SubMenu = subMenu;
                    }
                }
                button3 = value;

                if (value != null)
                {
                    maximisedLayout.Children.Add(button3);
                    Grid.SetColumn(button3, 2);

                    value.TextChanged += new TextChangedEventDelegate(button3_TextChanged);
                    button3_TextChanged(null);

                    minButton3.NormalImage = value.NormalImage.Clone();
                    value.ImageChanged += new ImageChangedEventDelegate(button3_ImageChanged);
                    value.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(value_EnabledChanged);
                    value.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(value_SelectedChanged);
                    value_EnabledChanged(value, value.IsEnabled);
                    value_SelectedChanged(value, value.IsSelected);

                    if (this.Controller != null && value.Controller != null)
                    {
                        value.Controller = this.Controller;
                    }

                    if (!maximised)
                    {
                        ContextMenu subMenu = button3.SubMenu;
                        button3.SubMenu = null;
                        minButton3.SubMenu = subMenu;
                    }
                }
                else
                {
                    minButton3.Text = "";
                    minButton3.NormalImage = null;
                }
            }
        }
        #endregion

        #region button event handlers
        private void button1_ImageChanged(ImageChangedEventArgs args)
        {
            minButton1.NormalImage = button1.NormalImage.Clone();
        }

        private void button1_TextChanged(TextChangedEventArgs args)
        {
            minButton1.Text = button1.Text.Replace("|", " ");
        }

        private void button2_ImageChanged(ImageChangedEventArgs args)
        {
            minButton2.NormalImage = button2.NormalImage.Clone();
        }

        private void button2_TextChanged(TextChangedEventArgs args)
        {
            minButton2.Text = button2.Text.Replace("|", " ");
        }

        private void button3_ImageChanged(ImageChangedEventArgs args)
        {
            minButton3.NormalImage = button3.NormalImage.Clone();
        }

        private void button3_TextChanged(TextChangedEventArgs args)
        {
            minButton3.Text = button3.Text.Replace("|", " ");
        }

        private void value_EnabledChanged(object sender, bool newValue)
        {
            if (sender == this.Button1)
            {
                minButton1.DisabledImage = this.Button1.DisabledImage;
                minButton1.NormalImage = this.Button1.NormalImage;
                minButton1.IsEnabled = newValue;
            }
            else if (sender == this.Button2)
            {
                minButton2.DisabledImage = this.Button2.DisabledImage;
                minButton2.NormalImage = this.Button2.NormalImage;
                minButton2.IsEnabled = newValue;
            }
            else if (sender == this.Button3)
            {
                minButton3.DisabledImage = this.Button3.DisabledImage;
                minButton3.NormalImage = this.Button3.NormalImage;
                minButton3.IsEnabled = newValue;
            }
        }

        private void value_SelectedChanged(object sender, bool newValue)
        {
            if (sender == this.Button1)
            {
                minButton1.IsSelected = newValue;
            }
            else if (sender == this.Button2)
            {
                minButton2.IsSelected = newValue;
            }
            else if (sender == this.Button3)
            {
                minButton3.IsSelected = newValue;
            }
        }
        #endregion

        #region resize handlers
        public override bool CanResize
        {
            get
            {
                return Button1 != null && Button2 != null && Button3 != null;
            }
        }
        public override bool resizeSmaller()
        {
            this.UpdateLayout();
            if (maximised)
            {
                layout.Content = minimisedLayout;

                #region move sub menus
                if (button1 != null)
                {
                    ContextMenu subMenu1 = button1.SubMenu;
                    button1.SubMenu = null;
                    minButton1.SubMenu = subMenu1;
                }

                if (button2 != null)
                {
                    ContextMenu subMenu2 = button2.SubMenu;
                    button2.SubMenu = null;
                    minButton2.SubMenu = subMenu2;
                }

                if (button3 != null)
                {
                    ContextMenu subMenu3 = button3.SubMenu;
                    button3.SubMenu = null;
                    minButton3.SubMenu = subMenu3;
                }
                #endregion

                maximised = false;
                return true;
            }
            else
            {
                if (!minimisedMinLayout)
                {
                    minimisedMinLayout = minimisedLayout.resizeSmaller();
                    return minimisedMinLayout;
                }

                return false;
            }
        }

        public override bool resizeBigger()
        {
            if (minimisedMinLayout)
            {
                minimisedMinLayout = minimisedLayout.resizeBigger();
                return minimisedMinLayout;
            }
            else if (!maximised)
            {
                layout.Content = maximisedLayout;

                #region move sub menus
                ContextMenu subMenu1 = minButton1.SubMenu;
                ContextMenu subMenu2 = minButton2.SubMenu;
                ContextMenu subMenu3 = minButton3.SubMenu;

                minButton1.SubMenu = null;
                minButton2.SubMenu = null;
                minButton3.SubMenu = null;

                button1.SubMenu = subMenu1;
                button2.SubMenu = subMenu2;
                button3.SubMenu = subMenu3;
                #endregion

                maximised = true;
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
                if (minimisedMinLayout)
                {
                    return minimisedLayout.ResizeIncreaseAmount;
                }
                else
                {
                    return maximisedLayout.ActualWidth - minimisedLayout.ActualWidth;
                }
            }
        }
        #endregion

        #region accessors
        public override RibbonController Controller
        {
            set
            {
                base.Controller = value;

                if (base.Controller != null)
                {
                    if (button1 != null)
                    {
                        button1.Controller = value;
                    }

                    if (button2 != null)
                    {
                        button2.Controller = value;
                    }

                    if (button3 != null)
                    {
                        button3.Controller = value;
                    }
                }
            }
            get
            {
                return base.Controller;
            }
        }

        public override List<QuickAccessButton> getQAButtons()
        {
            List<QuickAccessButton> buttons = new List<QuickAccessButton>();

            if (button1 != null)
            {
                QuickAccessButton qabButton = button1.getQAButton();
                if (qabButton != null)
                {
                    buttons.Add(qabButton);
                }
            }

            if (button2 != null)
            {
                QuickAccessButton qabButton = button2.getQAButton();
                if (qabButton != null)
                {
                    buttons.Add(qabButton);
                }
            }

            if (button3 != null)
            {
                QuickAccessButton qabButton = button3.getQAButton();
                if (qabButton != null)
                {
                    buttons.Add(qabButton);
                }
            }

            this.Controller = this.Controller; //### HACK FIX ###

            return buttons;
        }

        public override List<IRibbonControl> getElements()
        {
            List<IRibbonControl> list = new List<IRibbonControl>();

            List<IRibbonControl> subList = button1.getElements();
            for (int j = 0; j < subList.Count; j++)
            {
                list.Add(subList[j]);
            }


            subList = button1.getElements();
            for (int j = 0; j < subList.Count; j++)
            {
                list.Add(subList[j]);
            }

            subList = button1.getElements();
            for (int j = 0; j < subList.Count; j++)
            {
                list.Add(subList[j]);
            }

            return list;
        }

        public override bool IsMouseInside
        {
            get
            {
                bool result = false;
                if (maximised)
                {
                    if (button1 != null)
                    {
                        result = result || button1.IsMouseInside;
                    }
                    if (button2 != null)
                    {
                        result = result || button2.IsMouseInside;
                    }
                    if (button3 != null)
                    {
                        result = result || button3.IsMouseInside;
                    }
                }
                else
                {
                    if (minButton1 != null)
                    {
                        result = result || minButton1.IsMouseInside;
                    }
                    if (minButton2 != null)
                    {
                        result = result || minButton2.IsMouseInside;
                    }
                    if (minButton3 != null)
                    {
                        result = result || minButton3.IsMouseInside;
                    }
                }
                return base.IsMouseInside;
            }
        }
        #endregion
    }
}
