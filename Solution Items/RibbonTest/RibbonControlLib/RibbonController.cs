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
    public class RibbonController
    {
        #region class variables
        private Dictionary<String, RibbonBar> bars = new Dictionary<string, RibbonBar>();
        private Dictionary<RibbonBar, String> barsId = new Dictionary<RibbonBar, String>();
        private Dictionary<String, RibbonSelectionButton> buttons = new Dictionary<string, RibbonSelectionButton>();
        private ListenableList<RibbonBar> listenableBarsList = new ListenableList<RibbonBar>();
        private ListenableList<RibbonSelectionButton> listenableButtonsList = new ListenableList<RibbonSelectionButton>();
        private List<RibbonTabSeperator> tabSeperators = new List<RibbonTabSeperator>();

        private Grid barParent = null;
        private Grid titleParent = null;
        private Window parentWindow = null;
        private Page parentPage = null;
        private StackPanel titleStack = null;

        private RibbonSelectionButton selectedButton = null;

        private RibbonBar currentBar = null;
        private bool firstRun = true;

        private delegate void VoidDelegate();

        private bool hideRibbons = false;
        private RibbonBarPopup lastRibbonPopup = null;

        private RibbonContextController contextController = null;
        #endregion

        #region events
        public event AddToQuickAccessToolbarDelegate AddToQuickAccessToolbarEvent;
        public event CustomiseQuickAccessToolbarDelegate CustomiseQuickAccessToolbarEvent;
        public event ShowQuickAccessToolbarBelowRibbonDelegate ShowQuickAccessToolbarBelowRibbonEvent;
        public event MinimseRibbonDelegate MinimseRibbonEvent;
        #endregion

        #region constructors / loaders
        public RibbonController(Grid barParent, Grid titleParent, Window parentWindow)
        {
            titleStack = new StackPanel();
            titleStack.Orientation = Orientation.Horizontal;
            titleParent.Children.Add(titleStack);
            titleParent.MouseWheel += new MouseWheelEventHandler(titleParent_MouseWheel);

            this.RibbonBarParent = barParent;
            this.TitleBarParent = titleParent;
            this.parentWindow = parentWindow;

            parentWindow.SizeChanged += new SizeChangedEventHandler(parentWindow_SizeChanged);
            parentWindow.Loaded += new RoutedEventHandler(parentWindow_Loaded);
            listenableBarsList.ElementAdded += new ListenableList<RibbonBar>.ElementAddedDelegate(listenableBarsList_ElementAdded);
            listenableBarsList.ElementRemoved += new ListenableList<RibbonBar>.ElementRemovedDelegate(listenableBarsList_ElementRemoved);

            this.MinimseRibbonEvent += new MinimseRibbonDelegate(RibbonController_MinimseRibbonEvent);

            contextController = new RibbonContextController(this);
        }

        private void parentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            forceResizeBiggerUpdate(parentWindow.ActualWidth);
            forceResizeSmallerUpdate(parentWindow.ActualWidth);
        }

        public RibbonController(Grid barParent, Grid titleParent, Page parentPage)
        {
            titleStack = new StackPanel();
            titleStack.Orientation = Orientation.Horizontal;
            titleParent.Children.Add(titleStack);
            titleParent.MouseWheel += new MouseWheelEventHandler(titleParent_MouseWheel);

            this.RibbonBarParent = barParent;
            this.TitleBarParent = titleParent;
            this.parentPage = parentPage;

            parentPage.SizeChanged += new SizeChangedEventHandler(parentWindow_SizeChanged);
            listenableBarsList.ElementAdded += new ListenableList<RibbonBar>.ElementAddedDelegate(listenableBarsList_ElementAdded);
            listenableBarsList.ElementRemoved += new ListenableList<RibbonBar>.ElementRemovedDelegate(listenableBarsList_ElementRemoved);
        }
        #endregion

        #region accessors
        internal Window ParentWindow
        {
            get
            {
                return parentWindow;
            }
        }

        internal Page ParentPage
        {
            get
            {
                return parentPage;
            }
        }

        public bool HideRibbons
        {
            get
            {
                return hideRibbons;
            }
        }

        public RibbonContextController ContextController
        {
            get
            {
                return contextController;
            }
        }
        #endregion

        #region add / remove ribbons
        private void listenableBarsList_ElementRemoved(ListenableList<RibbonBar> sender, ListenableList<RibbonBar>.ElementRemovedEventArgs<RibbonBar> args)
        {
            if (barsId.ContainsKey(args.Item))
            {
                String id = barsId[args.Item];
                RibbonBar bar = bars[id];
                RibbonSelectionButton button = buttons[id];

                if (this.SelectedRibbon == bar)
                {
                    this.SelectedRibbon = listenableBarsList[0];
                }
                titleStack.Children.RemoveAt(args.Index * 2);//button
                titleStack.Children.RemoveAt(args.Index * 2);//seperator

                bars.Remove(id);
                buttons.Remove(id);
                barsId.Remove(bar);
            }
            else
            {
                throw new Exception("RibbonBar not found");
            }
        }

        private void listenableBarsList_ElementAdded(ListenableList<RibbonBar> sender, ListenableList<RibbonBar>.ElementAddedEventArgs<RibbonBar> args)
        {
            String id = Guid.NewGuid().ToString();
            bars.Add(id, args.Item);
            barsId.Add(args.Item, id);

            RibbonSelectionButton rsb = new RibbonSelectionButton(this, args.Item);
            rsb.Text = args.Item.Text;
            buttons.Add(id, rsb);
            titleStack.Children.Insert(args.Index * 2, rsb);
            RibbonTabSeperator separator = new RibbonTabSeperator();
            tabSeperators.Add(separator);
            titleStack.Children.Insert(args.Index * 2 + 1, separator);
            rsb.DoubleClicked += new MouseButtonEventHandler(SelectionButtonDoubleClicked);
            args.Item.TextChanged += new TextChangedEventDelegate(rsb.ribbonBarTextChangedHandler);

            listenableButtonsList.Insert(args.Index, rsb);

            #region First bar added, make selected
            if (bars.Count == 1)
            {
                rsb.IsSelected = true;
                //rsb.unselect();
                barParent.Children.Add(args.Item);
                selectedButton = rsb;
                currentBar = args.Item;

                if (parentWindow.IsLoaded)
                {
                    parentWindow.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new VoidDelegate(delegate()
                    {
                        forceResizeSmallerUpdate(parentWindow.ActualWidth);
                        forceResizeBiggerUpdate(parentWindow.ActualWidth);

                        RibbonStyleHandler.reStyle();
                    }
                        ));
                }
            }
            #endregion

            #region link
            args.Item.Controller = this;
            #endregion
        }

        internal ListenableList<RibbonSelectionButton> Buttons
        {
            get
            {
                return listenableButtonsList;
            }
        }

        public ListenableList<RibbonBar> Ribbons
        {
            get
            {
                return listenableBarsList;
            }
        }
        #endregion

        #region window resizing
        private void parentWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            #region size ribbons
            if (currentBar != null)
            {
                //Console.WriteLine(parentWindow.Width + " = window width, " + currentBar.ActualWidth);
                if (firstRun)
                {
                    currentBar.resizeSmaller(e.NewSize.Width);
                    currentBar.resizeBigger(e.NewSize.Width);
                    //currentBar.resizeSmaller(e.NewSize.Width);
                    firstRun = false;
                }
                else
                {
                    if (e.PreviousSize.Width >= 300 && e.NewSize.Width < 300)
                    {
                        //hide
                        barParent.MaxHeight = 0;
                        titleParent.MaxHeight = 0;
                    }
                    else if (e.PreviousSize.Width < 300 && e.NewSize.Width >= 300)
                    {
                        //show
                        barParent.MaxHeight = 100;
                        titleParent.MaxHeight = 100;
                    }

                    if (currentBar != null)
                    {
                        try
                        {
                            if (e.PreviousSize.Width + 4 < e.NewSize.Width)
                            {
                                currentBar.resizeBigger(e.NewSize.Width);
                            }
                            else if (e.PreviousSize.Width > e.NewSize.Width)
                            {
                                currentBar.resizeSmaller(e.NewSize.Width);
                            }
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine("Error processing window size change: " + err.ToString());
                        }

                    }
                }
            }
            #endregion

            resizeTabHeadings();
        }

        public void forceResizeSmallerUpdate(double width)
        {
            if (currentBar != null)
            {
                currentBar.resizeSmaller(width);
            }
        }

        public void forceResizeBiggerUpdate(double width)
        {
            if (currentBar != null)
            {
                currentBar.resizeBigger(width);
            }
        }

        public void resizeTabHeadings()
        {
            #region build array
            RibbonSelectionButton[] rButtons = new RibbonSelectionButton[listenableButtonsList.Count];
            for (int i = 0; i < listenableButtonsList.Count; i++)
            {
                rButtons[i] = listenableButtonsList[i];
            }
            #endregion

            #region resize bigger
            bool changedOne = true;

        next_iteration_bigger:
            while (changedOne && headingsWidth() + 110 < parentWindow.ActualWidth)
            {
                Array.Sort(rButtons, new RibbonSelectionButtonComparer());

                for (int i = rButtons.Length - 1; i >= 0; i--)
                {
                    if (rButtons[i].resizeBigger())
                    {
                        titleStack.UpdateLayout();
                        rButtons[i].UpdateLayout();
                        goto next_iteration_bigger;
                    }
                }

                changedOne = false;
            }
            #endregion

            #region resize smaller
            changedOne = true;

        next_iteration_smaller:
            while (changedOne && headingsWidth() + 110 > parentWindow.ActualWidth)
            {
                //Console.WriteLine("new title size : " + (headingsWidth() + 200) + ", parentWindow.ActualWidth = " + parentWindow.ActualWidth);
                Array.Sort(rButtons, new RibbonSelectionButtonComparer());

                for (int i = 0; i < rButtons.Length; i++)
                {
                    if (rButtons[i].resizeSmaller())
                    {
                        titleStack.UpdateLayout();
                        rButtons[i].UpdateLayout();
                        goto next_iteration_smaller;
                    }
                }

                changedOne = false;
            }
            #endregion

            #region show / hide seperators
            bool allFull = true;
            foreach (RibbonSelectionButton r in listenableButtonsList)
            {
                if (!r.IsFullSize)
                {
                    allFull = false;
                }
            }

            if (allFull)
            {
                foreach (RibbonTabSeperator t in tabSeperators)
                {
                    t.IsVisible = false;
                }
            }
            else
            {
                foreach (RibbonTabSeperator t in tabSeperators)
                {
                    t.IsVisible = true;
                }
            }
            #endregion
        }

        private double headingsWidth()
        {
            double width = 0.0;
            foreach (RibbonSelectionButton r in listenableButtonsList)
            {
                width += r.ActualWidth;
            }
            return width + (tabSeperators.Count * 3);
        }
        #endregion

        #region hooks
        public Grid RibbonBarParent
        {
            get
            {
                return barParent;
            }
            set
            {
                barParent = value;
            }
        }

        public Grid TitleBarParent
        {
            get
            {
                return titleParent;
            }
            set
            {
                titleParent = value;
            }
        }
        #endregion

        #region selections
        public RibbonBar SelectedRibbon
        {
            get
            {
                if (selectedButton == null)
                {
                    return null;
                }
                else
                {
                    return selectedButton.RibbonBar;
                }
            }
            set
            {
                if (this.HideRibbons)
                {
                    selectedButton.IsSelected = false;
                    barParent.Children.Clear();

                    #region find corresponding ribbon selection button
                    RibbonSelectionButton button = null;
                    IEnumerator<RibbonSelectionButton> de = Buttons.GetEnumerator();
                    while (de.MoveNext() && button == null)
                    {
                        if (de.Current.RibbonBar == value)
                        {
                            button = de.Current;
                        }
                    }
                    #endregion

                    #region add new
                    selectedButton = button;
                    //selectedButton.select();
                    //barParent.Children.Add(value);
                    currentBar = value;
                    #endregion

                    //must NOT call selected until we are displaying as per normal
                    RibbonBarPopup popup = new RibbonBarPopup(value, parentWindow);
                    if (parentWindow != null)
                    {
                        popup.PlacementTarget = parentWindow;
                    }
                    else if (parentPage != null)
                    {
                        popup.PlacementTarget = parentPage;
                    }
                    popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    popup.VerticalOffset = 52;
                    popup.HorizontalOffset = 0;
                    popup.IsOpen = true;
                    lastRibbonPopup = popup;
                    return;
                }
                else if (Ribbons.Contains(value))
                {
                    #region remove old
                    selectedButton.IsSelected = false;
                    barParent.Children.Clear();
                    #endregion

                    #region find corresponding ribbon selection button
                    RibbonSelectionButton button = null;
                    IEnumerator<RibbonSelectionButton> de = Buttons.GetEnumerator();
                    while (de.MoveNext() && button == null)
                    {
                        if (de.Current.RibbonBar == value)
                        {
                            button = de.Current;
                        }
                    }
                    #endregion

                    #region add new
                    selectedButton = button;
                    selectedButton.IsSelected = true;
                    barParent.Children.Add(value);
                    currentBar = value;
                    #endregion

                    #region resize
                    currentBar.resizeSmaller(parentWindow.ActualWidth);
                    currentBar.resizeBigger(parentWindow.ActualWidth);
                    #endregion
                }
                else
                {
                    throw new Exception("Ribbon not part of Ribbons collection");
                }
            }
        }

        #region minimise ribbon
        private void SelectionButtonDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            if (!hideRibbons)
            {
                hideRibbons = true;
                barParent.Children.Clear();
                if (selectedButton != null)
                {
                    Thread t = new Thread(delegate()
                    {
                        selectedButton.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.ContextIdle, new RibbonEventResources.RefreshDelegate(delegate()
                        {
                            selectedButton.IsSelected = false;
                        }));
                    });
                    t.Start();
                }

            }
            else
            {
                if (lastRibbonPopup != null && lastRibbonPopup.IsOpen == true)
                {
                    lastRibbonPopup.IsOpen = false;
                }
                hideRibbons = false;
                selectedButton.IsSelected = true;
                this.SelectedRibbon = ((RibbonSelectionButton)sender).RibbonBar;
            }
        }

        private void RibbonController_MinimseRibbonEvent(object sender, MinimseRibbonEventArgs args)
        {
            SelectionButtonDoubleClicked(null, null);
        }
        #endregion

        private void titleParent_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!hideRibbons)
            {
                RibbonBar sBar = this.SelectedRibbon;
                int sBarIndex = listenableBarsList.IndexOf(sBar);

                if (e.Delta > 0 && sBarIndex < listenableBarsList.Count - 1)
                {
                    this.SelectedRibbon = listenableBarsList[sBarIndex + 1];
                }
                else if (e.Delta < 0 && sBarIndex > 0)
                {
                    this.SelectedRibbon = listenableBarsList[sBarIndex - 1];
                }
            }
        }
        #endregion

        #region event firing
        public void fireAddToQuickAccessToolbarDelegate(object sender,
                                                        AddToQuickAccessToolbarEventArgs args)
        {
            if (AddToQuickAccessToolbarEvent != null)
            {
                AddToQuickAccessToolbarEvent(sender, args);
            }
        }

        public void fireCustomiseQuickAccessToolbarDelegate(object sender,
                                                        CustomiseQuickAccessToolbarEventArgs args)
        {
            if (CustomiseQuickAccessToolbarEvent != null)
            {
                CustomiseQuickAccessToolbarEvent(sender, args);
            }
        }

        public void fireShowQuickAccessToolbarBelowRibbonDelegate(object sender,
                                                        ShowQuickAccessToolbarBelowRibbonEventArgs args)
        {
            if (ShowQuickAccessToolbarBelowRibbonEvent != null)
            {
                ShowQuickAccessToolbarBelowRibbonEvent(sender, args);
            }
        }

        public void fireMinimseRibbonDelegate(object sender,
                                                        MinimseRibbonEventArgs args)
        {
            if (MinimseRibbonEvent != null)
            {
                MinimseRibbonEvent(sender, args);
            }
        }
        #endregion

        #region internal classes
        internal class RibbonSelectionButtonComparer : IComparer<RibbonSelectionButton>
        {
            #region IComparer<RibbonSelectionButton> Members

            public int Compare(RibbonSelectionButton x, RibbonSelectionButton y)
            {
                return y.ActualWidth.CompareTo(x.ActualWidth);
            }

            #endregion
        }
        #endregion
    }
}
