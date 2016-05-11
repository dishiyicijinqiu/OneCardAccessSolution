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
    public class RibbonKeyboardAccessController
    {
        private RibbonController ribbonController = null;
        private QuickAccessToolbar qatToolbar = null;
        private Window parentWindow = null;
        private Page parentPage = null;
        private Dictionary<String, IKeyboardPopupable> elementsDictionary = new Dictionary<string, IKeyboardPopupable>();
        private List<RibbonKeyboardAccessPopup> currentPopups = new List<RibbonKeyboardAccessPopup>();
        private String currentKeyString = "";
        private delegate void RefreshDelegate();

        #region constructors
        public RibbonKeyboardAccessController(Window parentWindow)
        {
            initialize(parentWindow, null, null);
        }

        public RibbonKeyboardAccessController(Window parentWindow, RibbonController ribbonController)
        {
            initialize(parentWindow, ribbonController, null);
        }

        public RibbonKeyboardAccessController(Window parentWindow, QuickAccessToolbar quickAccessToolbar)
        {
            initialize(parentWindow, null, quickAccessToolbar);
        }

        public RibbonKeyboardAccessController(Window parentWindow, RibbonController ribbonController,
                                                    QuickAccessToolbar quickAccessToolbar)
        {
            initialize(parentWindow, ribbonController, quickAccessToolbar);

        }

        public RibbonKeyboardAccessController(Page parentPage, RibbonController ribbonController,
                                                    QuickAccessToolbar quickAccessToolbar)
        {
            initialize(parentPage, ribbonController, quickAccessToolbar);

        }

        private void initialize(Window parentWindow, RibbonController ribbonController,
                                                    QuickAccessToolbar quickAccessToolbar)
        {
            this.parentWindow = parentWindow;
            this.ribbonController = ribbonController;
            this.qatToolbar = quickAccessToolbar;

            if (parentWindow == null)
            {
                throw new Exception("Parent Window cannot be null");
            }

            parentWindow.KeyDown += new KeyEventHandler(parentWindow_KeyDown);
            parentWindow.MouseDown += new MouseButtonEventHandler(parentWindow_MouseDown);
            parentWindow.Deactivated += new EventHandler(parentWindow_Deactivated);
        }

        private void initialize(Page parentPage, RibbonController ribbonController,
                                                    QuickAccessToolbar quickAccessToolbar)
        {
            this.parentPage = parentPage;
            this.ribbonController = ribbonController;
            this.qatToolbar = quickAccessToolbar;

            if (parentPage == null)
            {
                throw new Exception("Parent Page cannot be null");
            }

            parentPage.KeyDown += new KeyEventHandler(parentWindow_KeyDown);
            parentPage.MouseDown += new MouseButtonEventHandler(parentWindow_MouseDown);
            //parentPage.Deactivated += new EventHandler(parentWindow_Deactivated);
        }
        #endregion

        #region keyboard handlers

        private void parentWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.LeftAlt || e.SystemKey == Key.RightAlt || e.SystemKey == Key.F10)
            {
                showPrimaryKeyboardAccessPopups();
            }
            else if (elementsDictionary.Count > 0)
            {
                executeKeyboardAccess(e.Key);
            }
        }

        private void parentWindow_Deactivated(object sender, EventArgs e)
        {
            hideKeyboardAccessPopups();
        }

        private void parentWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hideKeyboardAccessPopups();
        }

        #endregion

        public void executeKeyboardAccess(Key k)
        {
            if (k.ToString().Length >= 1)
            {
                currentKeyString += k.ToString()[k.ToString().Length - 1].ToString();
            }
            else
            {
                currentKeyString += k.ToString();
            }

            Console.WriteLine("Testing shortcut [" + currentKeyString + "]");

            #region match found with displayed popups
            if (elementsDictionary.ContainsKey(currentKeyString))
            {
                if (elementsDictionary[currentKeyString].GetType() == new RibbonBar().GetType())
                {
                    elementsDictionary[currentKeyString].fireClickEvent((object)this, null);
                    ribbonController.SelectedRibbon = (RibbonBar)(elementsDictionary[currentKeyString]);
                    showSecondaryKeyboardAccessPopups();
                }
                else
                {
                    elementsDictionary[currentKeyString].fireClickEvent((object)this, null);
                    hideKeyboardAccessPopups();
                    return;
                }
            }
            #endregion
            else
            {
                bool foundPossibleMatch = false;

                #region test current RibbonBar
                if (ribbonController.SelectedRibbon != null)
                {
                    List<IRibbonControl> list = ribbonController.SelectedRibbon.getElements();

                    foreach (IRibbonControl b in list)
                    {
                        if (b.KeyboardAccessCombination != null && b.KeyboardAccessCombination.KeyCombination != null)
                        {
                            if (b.KeyboardAccessCombination.KeyCombination.Equals(currentKeyString))
                            {
                                b.fireClickEvent((object)this, null);
                                hideKeyboardAccessPopups();
                                return;
                            }
                            else if (b.KeyboardAccessCombination.KeyCombination.StartsWith(currentKeyString))
                            {
                                foundPossibleMatch = true;
                            }
                        }
                    }
                }
                #endregion

                #region test current popups
                foreach (RibbonKeyboardAccessPopup p in currentPopups)
                {
                    if (p.KeyCombination.StartsWith(currentKeyString))
                    {
                        foundPossibleMatch = true;
                    }
                }
                #endregion

                if (!foundPossibleMatch)
                {
                    #region no possible match, reverse and ding
                    currentKeyString = currentKeyString.Substring(0, currentKeyString.Length - 1);

                    BasicSoundPlayer bsp = new BasicSoundPlayer();
                    bsp.play(RibbonFileLocations.RibbonBasePath + @"\ding.wav", BasicSoundPlayer.SND_ASYNC);
                    #endregion
                }
            }
        }

        public void hideKeyboardAccessPopups()
        {
            foreach (RibbonKeyboardAccessPopup p in currentPopups)
            {
                p.IsOpen = false;
                elementsDictionary.Remove(p.KeyCombination);
            }

            currentPopups.Clear();
        }

        public void showPrimaryKeyboardAccessPopups()
        {
            currentKeyString = "";

            if (elementsDictionary.Count == 0)
            {
                #region quick access toolbar
                if (qatToolbar != null)
                {
                    int qaCounter = 1;

                    for (int i = 0; i < qatToolbar.Buttons.Count; i++)
                    {
                        QuickAccessButton qab = qatToolbar.Buttons[i];
                        RibbonKeyboardAccessKeyCombination combination = qab.KeyboardAccessCombination;

                        if (combination.KeyCombination != null)
                        {
                            if (elementsDictionary.ContainsKey(combination.KeyCombination))
                            {
                                throw new Exception("Key combination conflict; key combinations must be unique");
                            }
                            else
                            {
                                elementsDictionary.Add(combination.KeyCombination, qab);
                                RibbonKeyboardAccessPopup kp = new RibbonKeyboardAccessPopup();
                                showPopup(combination.KeyCombination, kp, qab);
                            }
                        }
                        else
                        {
                            while (elementsDictionary.ContainsKey(qaCounter.ToString()))
                            {
                                qaCounter++;
                            }

                            if (qaCounter < 10)
                            {
                                elementsDictionary.Add("0" + qaCounter.ToString(), qab);
                                RibbonKeyboardAccessPopup kp = new RibbonKeyboardAccessPopup();
                                showPopup("0" + qaCounter.ToString(), kp, qab);
                            }
                            else
                            {
                                elementsDictionary.Add(qaCounter.ToString(), qab);
                                RibbonKeyboardAccessPopup kp = new RibbonKeyboardAccessPopup();
                                showPopup(qaCounter.ToString(), kp, qab);
                            }

                            qaCounter++;
                        }
                    }
                }
                #endregion

                #region ribbon
                if (ribbonController != null)
                {
                    for (int i = 0; i < ribbonController.Buttons.Count; i++)
                    {
                        RibbonKeyboardAccessKeyCombination combination = ribbonController.Buttons[i].RibbonBar.KeyboardAccessCombination;

                        if (combination.KeyCombination != null)
                        {
                            if (elementsDictionary.ContainsKey(combination.KeyCombination))
                            {
                                throw new Exception("Key combination conflict; key combinations must be unique");
                            }
                            else
                            {
                                elementsDictionary.Add(combination.KeyCombination, ribbonController.Buttons[i].RibbonBar);
                                RibbonKeyboardAccessPopup kp = new RibbonKeyboardAccessPopup();
                                showPopup(combination.KeyCombination, kp, ribbonController.Buttons[i]);
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {
                hideKeyboardAccessPopups();
            }
        }

        public void showSecondaryKeyboardAccessPopups()
        {
            currentKeyString = "";
            hideKeyboardAccessPopups();

            List<IRibbonControl> list = ribbonController.SelectedRibbon.getElements();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].KeyboardAccessCombination != null && 
                    list[i].KeyboardAccessCombination.KeyCombination != null &&
                    list[i] != ribbonController.SelectedRibbon)
                {
                    RibbonKeyboardAccessPopup kp = new RibbonKeyboardAccessPopup();
                    showPopup(list[i].KeyboardAccessCombination.KeyCombination, kp, list[i]);
                    elementsDictionary.Add(list[i].KeyboardAccessCombination.KeyCombination, list[i]);
                }
            }
        }

        private void showPopup(String keyCombination, RibbonKeyboardAccessPopup popup, IKeyboardPopupable control)
        {
            popup.KeyCombination = keyCombination;
            popup.PlacementTarget = (UIElement)control;
            popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
            popup.VerticalOffset = control.KeyboardAccessVerticalOffset;
            popup.IsOpen = true;

            if (double.IsNaN(control.KeyboardAccessHorizontalOffset))
            {
                popup.HorizontalOffset = (control.ActualWidth / 2.0) - (popup.ActualWidth / 2.0);
            }
            else
            {
                popup.HorizontalOffset = control.KeyboardAccessHorizontalOffset;
            }

            currentPopups.Add(popup);
        }
    }
}
