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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class QuickAccessToolBarConfigurationWindow : Window
    {
        #region class variables
        private QuickAccessToolbar toolbar;
        private RibbonController controller;
        private TextOnlyTip chooseCommandsFromTip = new TextOnlyTip();
        private TextOnlyTip customizeCommandsFromTip = new TextOnlyTip();
        #endregion

        #region constructor
        public QuickAccessToolBarConfigurationWindow(QuickAccessToolbar toolbar, RibbonController controller)
        {
            InitializeComponent();

            this.toolbar = toolbar;
            this.controller = controller;

            #region setup from List
            chooseCommandsFromComboBox.Items.Add("All Ribbons");

            for (int i = 0; i < controller.Ribbons.Count; i++)
            {
                chooseCommandsFromComboBox.Items.Add(controller.Ribbons[i].Text);
            }
            chooseCommandsFromComboBox.SelectionChanged += new SelectionChangedEventHandler(chooseCommandsFromComboBox_SelectionChanged);
            chooseCommandsFromComboBox.SelectedIndex = 0;
            #endregion

            #region setup customize List
            customizeCommandsFromComboBox.Items.Add("For all documents (default)");
            customizeCommandsFromComboBox.Items.Add("This document only");

            customizeCommandsFromComboBox.SelectedItem = 0;
            customizeCommandsFromComboBox.SelectionChanged += new SelectionChangedEventHandler(customizeCommandsFromComboBox_SelectionChanged);

            for (int i = 0; i < toolbar.Buttons.Count; i++)
            {
                customiseCommandsFromListView.Items.Add(new QuickAccessToolbarConfigPanel(toolbar.Buttons[i]));
            }
            #endregion

            #region setup tips
                chooseCommandsFromTip.Title = "Choose commands from";
            chooseCommandsFromTip.Text = "In addition to using this dialog box, you can also add any item to the Quick Access Toolbar by right-clicking it in the Ribbon.";

            customizeCommandsFromTip.Title = "Quick Access Toolbar";
            customizeCommandsFromTip.Text = "Add frequently-used commands to the Quick Access Toolbar to ensure that they are always a single click away.";
            #endregion

            #region setup styling
            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);
            #endregion
        }
        #endregion

        #region scope handlers
        private void customizeCommandsFromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(this, "This function has not been implemented yet", "Implementation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
        }

        private void chooseCommandsFromComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chooseCommandsFromListView.Items.Clear();

            if (chooseCommandsFromComboBox.SelectedItem != null)
            {
                for (int i = 0; i < controller.Ribbons.Count; i++)
                {
                    if (controller.Ribbons[i].Text.Equals(chooseCommandsFromComboBox.SelectedItem.ToString())
                        || chooseCommandsFromComboBox.SelectedItem.ToString().Equals("All Ribbons"))
                    {
                        //add inner boxes
                        List<QuickAccessButton> buttons = controller.Ribbons[i].getQAButtons();

                        for (int j = 0; j < buttons.Count; j++)
                        {
                            if (buttons[j] != null)
                            {
                                chooseCommandsFromListView.Items.Add(new QuickAccessToolbarConfigPanel(buttons[j]));
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region styling
        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            titleLabel.Foreground = RibbonStyleHandler.GroupText;
            ((LinearGradientBrush)row0Grid.Background).GradientStops[0].Color = RibbonStyleHandler.GroupLabelBackgroundNormal[0].Color;
        }
        #endregion

        #region accesors
        public ImageSource HelpIconSource
        {
            get
            {
                return chooseCommandsFromHelp.Source.Clone();
            }
            set
            {
                chooseCommandsFromHelp.Source = value.Clone();
                customizeCommandsFromHelp.Source = value.Clone();
            }
        }

        public ImageSource ConfigIconSource
        {
            get
            {
                return configIconImage.Source.Clone();
            }
            set
            {
                configIconImage.Source = value;
            }
        }
        #endregion

        #region ScreenTips handlers
        private void customizeCommandsFromHelp_MouseEnter(object sender, MouseEventArgs e)
        {
            customizeCommandsFromTip.PlacementTarget = customizeCommandsFromHelp;
            customizeCommandsFromTip.VerticalOffset = 15;
            customizeCommandsFromTip.IsOpen = true;
        }

        private void customizeCommandsFromHelp_MouseLeave(object sender, MouseEventArgs e)
        {
            customizeCommandsFromTip.IsOpen = false;
        }

        private void chooseCommandsFromHelp_MouseEnter(object sender, MouseEventArgs e)
        {
            chooseCommandsFromTip.PlacementTarget = chooseCommandsFromHelp;
            chooseCommandsFromTip.VerticalOffset = 15;
            chooseCommandsFromTip.IsOpen = true;
        }

        private void chooseCommandsFromHelp_MouseLeave(object sender, MouseEventArgs e)
        {
            chooseCommandsFromTip.IsOpen = false;
        }
        #endregion

        #region add / remove from QuickAccessToolbar
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (chooseCommandsFromListView.SelectedItems != null)
            {
                for (int i = 0; i < chooseCommandsFromListView.SelectedItems.Count; i++)
                {
                    QuickAccessButton b = ((QuickAccessToolbarConfigPanel)chooseCommandsFromListView.SelectedItems[i]).Button;
                    if (toolbar.Buttons.Contains(b) != true)
                    {
                        toolbar.Buttons.Add(b);
                        customiseCommandsFromListView.Items.Insert(toolbar.Buttons.IndexOf(b), new QuickAccessToolbarConfigPanel(b));
                    }
                }
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (customiseCommandsFromListView.SelectedItem != null)
            {
                for (int i = customiseCommandsFromListView.SelectedItems.Count - 1; i >= 0; i--)
                {
                    QuickAccessButton b = ((QuickAccessToolbarConfigPanel)customiseCommandsFromListView.SelectedItems[i]).Button;
                    int index = toolbar.Buttons.IndexOf(b);

                    if (toolbar.Buttons.Contains(b) == true)
                    {
                        toolbar.Buttons.Remove(b);
                        customiseCommandsFromListView.Items.RemoveAt(index);
                    }
                }
            }
        }
        #endregion

        #region move up/down
        private void moveUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (customiseCommandsFromListView.SelectedItem != null
                && customiseCommandsFromListView.SelectedItems.Count == 1)
            {
                QuickAccessToolbarConfigPanel tcp = (QuickAccessToolbarConfigPanel)customiseCommandsFromListView.SelectedItem;
                QuickAccessButton b = tcp.Button;
                int index = toolbar.Buttons.IndexOf(b);

                if (index > 0)
                {
                    toolbar.Buttons.Remove(b);
                    toolbar.Buttons.Insert(index - 1, b);
                    customiseCommandsFromListView.Items.Remove(tcp);
                    customiseCommandsFromListView.Items.Insert(index - 1, tcp);
                    customiseCommandsFromListView.SelectedIndex = index - 1;
                }
            }
        }

        private void moveDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (customiseCommandsFromListView.SelectedItem != null
                && customiseCommandsFromListView.SelectedItems.Count == 1)
            {
                QuickAccessToolbarConfigPanel tcp = (QuickAccessToolbarConfigPanel)customiseCommandsFromListView.SelectedItem;
                QuickAccessButton b = tcp.Button;
                int index = toolbar.Buttons.IndexOf(b);

                if (index < toolbar.Buttons.Count - 1)
                {
                    toolbar.Buttons.Remove(b);
                    toolbar.Buttons.Insert(index + 1, b);
                    customiseCommandsFromListView.Items.Remove(tcp);
                    customiseCommandsFromListView.Items.Insert(index + 1, tcp);
                    customiseCommandsFromListView.SelectedIndex = index + 1;
                }
            }
        }
        #endregion

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            toolbar.resetToDefault(controller);
            customiseCommandsFromListView.Items.Clear();
            for (int i = 0; i < toolbar.Buttons.Count; i++)
            {
                customiseCommandsFromListView.Items.Add(new QuickAccessToolbarConfigPanel(toolbar.Buttons[i]));
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
