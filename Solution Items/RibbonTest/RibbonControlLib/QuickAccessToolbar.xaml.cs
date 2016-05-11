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
    /// Interaction logic for QuickAccessToolbar.xaml
    /// </summary>
    public partial class QuickAccessToolbar : UserControl
    {
        private ListenableList<QuickAccessButton> buttons = new ListenableList<QuickAccessButton>();
        private ContextMenu subMenu = new ContextMenu();

        #region events
        public event AddToQuickAccessToolbarDelegate AddToQuickAccessToolbarEvent;
        public event CustomiseQuickAccessToolbarDelegate CustomiseQuickAccessToolbarEvent;
        public event ShowQuickAccessToolbarBelowRibbonDelegate ShowQuickAccessToolbarBelowRibbonEvent;
        public event MinimseRibbonDelegate MinimseRibbonEvent;
        #endregion

        public QuickAccessToolbar()
        {
            InitializeComponent();

            #region style drop down button
            List<GradientStop> transparentStops = new List<GradientStop>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), RibbonStyleHandler.ButtonBackgroundNormal[i].Offset));
            }
            RibbonStyleHandler.styleButtonBorder(dropDownBorder, this, this, Color.FromArgb(0, 0, 0, 0), transparentStops);
            #endregion

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);

            buttons.ElementAdded += new ListenableList<QuickAccessButton>.ElementAddedDelegate(buttons_ElementAdded);
            buttons.ElementRemoved += new ListenableList<QuickAccessButton>.ElementRemovedDelegate(buttons_ElementRemoved);
            subMenu.Closed += new RoutedEventHandler(subMenu_Closed);
        }

        private void subMenu_Closed(object sender, RoutedEventArgs e)
        {
            RibbonStyleHandler.forceLeaveAnnimation(dropDownBorder, null);//new MouseEventArgs(new MouseDevice(), 0));
        }

        #region children handlers
        private void buttons_ElementRemoved(ListenableList<QuickAccessButton> sender, ListenableList<QuickAccessButton>.ElementRemovedEventArgs<QuickAccessButton> args)
        {
            buttonsStackPanel.Children.Remove(args.Item);
        }

        private void buttons_ElementAdded(ListenableList<QuickAccessButton> sender, ListenableList<QuickAccessButton>.ElementAddedEventArgs<QuickAccessButton> args)
        {
            buttonsStackPanel.Children.Insert(args.Index, args.Item);

            if (AddToQuickAccessToolbarEvent != null)
            {
                AddToQuickAccessToolbarEvent((object)sender, new AddToQuickAccessToolbarEventArgs());
            }
        }

        public ListenableList<QuickAccessButton> Buttons
        {
            get
            {
                return buttons;
            }
        }

        public ContextMenu SubMenu
        {
            get
            {
                return subMenu;
            }
        }

        public void resetToDefault(RibbonController controller)
        {
            try
            {
                this.Buttons.Clear();
                subMenu.Items.Clear();

                for (int i = 0; i < controller.Ribbons.Count; i++)
                {
                    List<QuickAccessButton> iButtons = controller.Ribbons[i].getQAButtons();

                    for (int j = 0; j < iButtons.Count; j++)
                    {
                        if (iButtons[j] != null)
                        {
                            if (iButtons[j].IsDefaultQuickAccessButton)
                            {
                                this.Buttons.Add(iButtons[j]);
                            }

                            if (iButtons[j].IsDefaultQuickAccessMenuButton)
                            {
                                MenuItem m = new MenuItem();
                                m.Header = iButtons[j].Text;
                                if (iButtons[j] != null)
                                {
                                    Image theImage = new Image();
                                    theImage.Source = iButtons[j].Image;
                                    theImage.Width = 16;
                                    theImage.Height = 16;
                                    m.Icon = theImage;
                                }
                                m.Click += new RoutedEventHandler(addMenuItem_Click);
                                m.Tag = iButtons[j];
                                subMenu.Items.Add(m);
                            }
                        }
                    }
                }

                subMenu.Items.Add(new Separator());

                MenuItem mi = new MenuItem();
                mi.Header = "_More Commands";
                mi.Click += new RoutedEventHandler(customize_Click);
                subMenu.Items.Add(mi);
                mi = new MenuItem();
                mi.Header = "_Show Below the Ribbon";
                mi.Click += new RoutedEventHandler(showBelowRibbon_Click);
                subMenu.Items.Add(mi);

                subMenu.Items.Add(new Separator());

                mi = new MenuItem();
                mi.Header = "Mi_nimize the Ribbon";
                mi.Click += new RoutedEventHandler(minimizeRibbon_Click);
                subMenu.Items.Add(mi);
            }
            catch (Exception err)
            {
                Console.WriteLine("Quick Access Toolbar reset error:" + err.ToString());
            }
        }

        private void showBelowRibbon_Click(object sender, RoutedEventArgs e)
        {
            if (ShowQuickAccessToolbarBelowRibbonEvent != null)
            {
                ShowQuickAccessToolbarBelowRibbonEvent(sender, new ShowQuickAccessToolbarBelowRibbonEventArgs());
            }
        }

        private void minimizeRibbon_Click(object sender, RoutedEventArgs e)
        {
            if (MinimseRibbonEvent != null)
            {
                MinimseRibbonEvent(sender, new MinimseRibbonEventArgs());
            }
        }

        private void customize_Click(object sender, RoutedEventArgs e)
        {
            if (CustomiseQuickAccessToolbarEvent != null)
            {
                CustomiseQuickAccessToolbarEvent(sender, new CustomiseQuickAccessToolbarEventArgs());
            }
        }

        private void addMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            QuickAccessButton q = (QuickAccessButton)(mi.Tag);

            if (!this.Buttons.Contains(q))
            {
                this.Buttons.Add(q);
            }
        }
        #endregion

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            leftPath.Fill = RibbonStyleHandler.QuickAccessBackground.Clone();
            middlePath.Background = RibbonStyleHandler.QuickAccessBackground.Clone();
            rightPath.Fill = RibbonStyleHandler.QuickAccessBackground.Clone();
        }

        private void popupButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && false)
            {
                subMenu.PlacementTarget = this;
                subMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                subMenu.VerticalOffset = 66;
                subMenu.IsOpen = true;
            }
            
            dropDownBorder.ContextMenu = subMenu;
        }

        #region persistance
        public List<Guid> getCurrentButtonControlIDs()
        {
            List<Guid> output = new List<Guid>();
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].ControlID.Equals(Guid.Empty) != true)
                {
                    output.Add(buttons[i].ControlID);
                }
            }
            return output;
        }

        public void loadButtons(List<Guid> ids, RibbonController controller)
        {
            try
            {
                this.Buttons.Clear();
                subMenu.Items.Clear();

                for (int i = 0; i < controller.Ribbons.Count; i++)
                {
                    List<QuickAccessButton> iButtons = controller.Ribbons[i].getQAButtons();

                    for (int j = 0; j < iButtons.Count; j++)
                    {
                        if (iButtons[j] != null)
                        {
                            if (ids.Contains(iButtons[j].ControlID) && iButtons[j].ControlID != Guid.Empty)
                            {
                                this.Buttons.Add(iButtons[j]);
                            }

                            if (iButtons[j].IsDefaultQuickAccessMenuButton)
                            {
                                MenuItem m = new MenuItem();
                                m.Header = iButtons[j].Text;
                                if (iButtons[j] != null)
                                {
                                    Image theImage = new Image();
                                    theImage.Source = iButtons[j].Image;
                                    theImage.Width = 16;
                                    theImage.Height = 16;
                                    m.Icon = theImage;
                                }
                                m.Click += new RoutedEventHandler(addMenuItem_Click);
                                m.Tag = iButtons[j];
                                subMenu.Items.Add(m);
                            }
                        }
                    }
                }

                subMenu.Items.Add(new Separator());

                MenuItem mi = new MenuItem();
                mi.Header = "_More Commands";
                mi.Click += new RoutedEventHandler(customize_Click);
                subMenu.Items.Add(mi);
                mi = new MenuItem();
                mi.Header = "_Show Below the Ribbon";
                mi.Click += new RoutedEventHandler(showBelowRibbon_Click);
                subMenu.Items.Add(mi);

                subMenu.Items.Add(new Separator());

                mi = new MenuItem();
                mi.Header = "Mi_nimize the Ribbon";
                mi.Click += new RoutedEventHandler(minimizeRibbon_Click);
                subMenu.Items.Add(mi);
            }
            catch (Exception err)
            {
                Console.WriteLine("Quick Access Toolbar load error:" + err.ToString());
            }
        }
        #endregion
    }
}
