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
    public class RibbonControlBase : UserControl, IKeyboardPopupable, IRibbonControl
    {
        #region storage
        protected String text = "";
        protected ImageSource normalImage = null;
        protected ImageSource disabledImage = null;
        protected IScreenTip normalTip = null;
        protected IScreenTip disabledTip = null;
        protected RibbonController controller = null;
        protected bool canResize = false;
        protected double resizePriority = double.MaxValue;
        protected double resizeIncreaseAmount = 0;
        protected bool showDefaultContextMenu = true;
        protected ContextMenu subMenu = null;
        private bool mouseIn = false;
        private List<QuickAccessButton> qaButtons = new List<QuickAccessButton>();
        private bool defaultButton = false;
        private bool defaultMenuButton = false;
        private RibbonKeyboardAccessKeyCombination keyCombination = new RibbonKeyboardAccessKeyCombination();
        protected double keyVerticalOffset = 12;
        protected double keyHorizonalOffset = double.NaN;
        protected bool disableResize = false;
        protected bool hasQATbutton = true;
        protected bool isEnabled = true;
        protected bool isSelected = false;
        protected List<RibbonBorder> styledBorders = new List<RibbonBorder>();
        private Guid controlID = Guid.Empty;
        #endregion

        #region events
        public event ImageChangedEventDelegate ImageChanged;
        public event TextChangedEventDelegate TextChanged;
        public event ScreenTipChangedEventDelegate ScreenTipChanged;
        public event ControllerChangedEventDelegate ControllerChanged;
        public event SubMenuChangedEventDelegate SubMenuChanged;
        public event MouseButtonEventHandler Clicked;
        public event RibbonEventResources.EnableChangedDelegate EnabledChanged;
        public event RibbonEventResources.SelectedChangedDelegate SelectedChanged;
        #endregion

        #region initialize
        public RibbonControlBase()
        {
            initialize();
            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(updateStyle);
            updateStyle(new RibbonStyleHandler.StyleChangedEventArgs(RibbonStyleHandler.Style.Not_Set));

            this.MouseEnter += new MouseEventHandler(RibbonControlBase_MouseEnter);
            this.MouseLeave += new MouseEventHandler(RibbonControlBase_MouseLeave);
            this.MouseDown += new MouseButtonEventHandler(RibbonControlBase_MouseDown);
            this.MouseUp += new MouseButtonEventHandler(RibbonControlBase_MouseUp);
        }

        protected virtual void initialize()
        {
        }

        public virtual void updateStyle(RibbonStyleHandler.StyleChangedEventArgs args)
        {

        }
        #endregion

        #region assesors
        public virtual bool DisableResize
        {
            get
            {
                return disableResize;
            }
            set
            {
                disableResize = value;
            }
        }

        public virtual String Text
        {
            get
            {
                return text;
            }
            set
            {
                String oldValue = text;
                text = value;

                if (text == null)
                {
                    text = "";
                }

                if (TextChanged != null)
                {
                    TextChanged(new TextChangedEventArgs(oldValue, text));
                }
            }
        }

        public virtual ImageSource NormalImage
        {
            get
            {
                return normalImage;
            }
            set
            {
                ImageSource oldValue = normalImage;
                normalImage = value;

                if (ImageChanged != null && this.IsEnabled)
                {
                    ImageChanged(new ImageChangedEventArgs(oldValue, normalImage));
                }
            }
        }

        public virtual ImageSource DisabledImage
        {
            get
            {
                return disabledImage;
            }
            set
            {
                ImageSource oldValue = disabledImage;
                disabledImage = value;

                if (ImageChanged != null && this.IsEnabled != true && disabledImage != null)
                {
                    ImageChanged(new ImageChangedEventArgs(oldValue, disabledImage));
                }
            }
        }

        public virtual IScreenTip NormalTip
        {
            get
            {
                return normalTip;
            }
            set
            {
                IScreenTip oldValue = normalTip;
                normalTip = value;

                if (ScreenTipChanged != null && this.IsEnabled)
                {
                    ScreenTipChanged(new ScreenTipChangedEventArgs(oldValue, value));
                }
            }
        }

        public virtual IScreenTip DisabledTip
        {
            get
            {
                return disabledTip;
            }
            set
            {
                IScreenTip oldValue = disabledTip;
                disabledTip = value;

                if (ScreenTipChanged != null && this.IsEnabled == false)
                {
                    ScreenTipChanged(new ScreenTipChangedEventArgs(oldValue, value));
                }
            }
        }

        public virtual RibbonController Controller
        {
            get
            {
                return controller;
            }
            set
            {
                RibbonController oldValue = controller;
                controller = value;

                if (controller != null && showDefaultContextMenu)
                {
                    this.ContextMenu = RibbonContextMenu.GetDefaultContextMenu(this, value);
                }

                if (ControllerChanged != null)
                {
                    ControllerChanged(new ControllerChangedEventArgs(oldValue, value));
                }
            }
        }

        public new ContextMenu ContextMenu
        {
            get
            {
                return base.ContextMenu;
            }
            set
            {
                base.ContextMenu = value;
            }
        }

        public virtual ContextMenu SubMenu
        {
            get 
            { 
                return subMenu;
            }
            set
            {
                ContextMenu oldValue = subMenu;
                if (oldValue != null)
                {
                    oldValue.Closed -= new RoutedEventHandler(subMenu_Closed);
                }

                subMenu = value;
                if (subMenu != null)
                {
                    subMenu.Closed += new RoutedEventHandler(subMenu_Closed);
                }

                if (SubMenuChanged != null)
                {
                    SubMenuChanged(new SubMenuChangedEventArgs(oldValue, value));
                }
            }
        }

        public virtual bool CanResize
        {
            get
            {
                if (!disableResize)
                {
                    return canResize;
                }
                else
                {
                    return false;
                }
            }
        }

        public virtual double ResizePriority
        {
            get
            {
                return resizePriority;
            }
        }

        public virtual double ResizeIncreaseAmount
        {
            get
            {
                return resizeIncreaseAmount;
            }
        }

        public bool IsDefaultQuickAccessButton
        {
            get
            {
                return defaultButton;
            }
            set
            {
                defaultButton = value;
            }
        }

        public bool IsDefaultQuickAccessMenuButton
        {
            get
            {
                return defaultMenuButton;
            }
            set
            {
                defaultMenuButton = value;
            }
        }

        public virtual bool IsMouseInside
        {
            get
            {
                return mouseIn;
            }
        }

        public new virtual bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                bool valueChanged = false;
                if (value != isEnabled)
                {
                    valueChanged = true;
                }

                isEnabled = value;
                for (int i = 0; i < styledBorders.Count; i++)
                {
                    styledBorders[i].IsEnabled = value;
                }

                if (valueChanged && EnabledChanged != null)
                {
                    EnabledChanged(this, value);
                }

                if (valueChanged)
                {
                    if (ImageChanged != null && this.IsEnabled != true && disabledImage != null)
                    {
                        ImageChanged(new ImageChangedEventArgs(normalImage, disabledImage));
                    }
                    else
                    {
                        ImageChanged(new ImageChangedEventArgs(disabledImage, normalImage));
                    }
                }

                if (ScreenTipChanged != null)
                {
                    if (value && this.DisabledTip != null)
                    {
                        ScreenTipChanged(new ScreenTipChangedEventArgs(this.DisabledTip, this.NormalTip));
                    }
                    else if (value == false && this.DisabledTip != null)
                    {
                        ScreenTipChanged(new ScreenTipChangedEventArgs(this.NormalTip, this.DisabledTip));
                    }
                }
            }
        }

        public virtual bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                bool valueChanged = false;
                if (value != isSelected)
                {
                    valueChanged = true;
                }

                isSelected = value;
                for (int i = 0; i < styledBorders.Count; i++)
                {
                    styledBorders[i].IsSelected = value;
                }

                if (valueChanged && SelectedChanged != null)
                {
                    SelectedChanged(this, value);
                }
            }
        }

        public Guid ControlID
        {
            get
            {
                return controlID;
            }
            set
            {
                controlID = value;
            }
        }
        #endregion

        #region resize handlers
        public virtual bool resizeSmaller()
        {
            return false;
        }

        public virtual bool resizeBigger()
        {
            return false;
        }
        #endregion

        #region sub menu handlers
        protected void addSubMenuOpener(UIElement element)
        {
            element.MouseDown += new MouseButtonEventHandler(subMenuOpen_Click);
        }

        protected void removeSubMenuOpener(UIElement element)
        {
            element.MouseDown -= new MouseButtonEventHandler(subMenuOpen_Click);
        }

        private void subMenuOpen_Click(object sender, MouseButtonEventArgs e)
        {
            if (subMenu != null && this.IsEnabled)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    /**
                    if (false && this.Parent != null
                        && ((Panel)this.Parent).Parent != null
                        && (UIElement)((Panel)((Panel)this.Parent).Parent).Parent != null)
                    {
                        subMenu.PlacementTarget = (UIElement)((Panel)((Panel)this.Parent).Parent).Parent;
                    }
                    else
                    {
                    }
                     **/

                    subMenu.PlacementTarget = this;
                    subMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    subMenu.VerticalOffset = keyVerticalOffset;
                    subMenu.IsOpen = true;
                }
            }
        }

        protected virtual void subMenu_Closed(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region other events
        #region Clicked
        protected void addClickElement(UIElement element)
        {
            element.MouseDown += new MouseButtonEventHandler(element_MouseDown);
        }

        protected void removeClickElement(UIElement element)
        {
            element.MouseDown -= new MouseButtonEventHandler(element_MouseDown);
        }

        private void element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Clicked != null && e.LeftButton == MouseButtonState.Pressed && this.IsEnabled == true)
            {
                Clicked(this, e);
            }
        }
        #endregion

        #region mouse handlers
        private void RibbonControlBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //mouseDown = false;

            if (mouseIn)
            {
                RibbonControlBase_MouseEnter(sender, e);
            }
        }

        private void RibbonControlBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //mouseDown = true;

            if (normalTip != null && normalTip.IsOpen)
            {
                normalTip.IsOpen = false;
            }
            if (disabledTip != null && disabledTip.IsOpen)
            {
                disabledTip.IsOpen = false;
            }
        }

        private void RibbonControlBase_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseIn = false;

            if (normalTip != null && normalTip.IsOpen)
            {
                normalTip.IsOpen = false;
            }
            if (disabledTip != null && disabledTip.IsOpen)
            {
                disabledTip.IsOpen = false;
            }
        }

        private void RibbonControlBase_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseIn = true;

            if (this.IsEnabled)
            {
                if (normalTip != null)
                {
                    //if (false && this.Parent != null
                    //        && ((Panel)this.Parent).Parent != null
                    //        && (UIElement)((Panel)((Panel)this.Parent).Parent).Parent != null)
                    //{
                    //    tip.PlacementTarget = (UIElement)((Panel)((Panel)this.Parent).Parent).Parent;
                    //}
                    //else
                    //{
                    normalTip.PlacementTarget = this;
                    //}

                    normalTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    normalTip.VerticalOffset = 89;
                    //contextMenu.HorizontalOffset = (this.Width / 2.0) - 10;
                    normalTip.IsOpen = true;
                }
            }
            else
            {
                if (disabledTip != null)
                {
                    disabledTip.PlacementTarget = this;

                    disabledTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    disabledTip.VerticalOffset = 89;
                    //contextMenu.HorizontalOffset = (this.Width / 2.0) - 10;
                    disabledTip.IsOpen = true;
                }
                else if (normalTip != null)
                {
                    normalTip.PlacementTarget = this;

                    normalTip.Placement = System.Windows.Controls.Primitives.PlacementMode.Relative;
                    normalTip.VerticalOffset = 89;
                    //contextMenu.HorizontalOffset = (this.Width / 2.0) - 10;
                    normalTip.IsOpen = true;
                }
            }
        }
        #endregion

        #region quick access toolbar methods
        public QuickAccessButton getQAButton()
        {
            if (hasQATbutton)
            {
                QuickAccessButton b = new QuickAccessButton(this);
                b.IsDefaultQuickAccessButton = this.IsDefaultQuickAccessButton;
                b.IsDefaultQuickAccessMenuButton = this.IsDefaultQuickAccessMenuButton;

                qaButtons.Add(b);
                return b;
            }
            else
            {
                return null;
            }
        }

        public virtual List<QuickAccessButton> getQAButtons()
        {
            List<QuickAccessButton> buttons = new List<QuickAccessButton>();
            QuickAccessButton qabButton = getQAButton();
            if (qabButton != null)
            {
                buttons.Add(getQAButton());
            }
            return buttons;
        }

        private void quickAccessButton_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(this, e);
            }
        }
        #endregion
        #endregion

        #region IKeyboardPopup Members

        public virtual RibbonKeyboardAccessKeyCombination KeyboardAccessCombination
        {
            get
            {
                return keyCombination;
            }
            set
            {
                if (value == null)
                {
                    keyCombination = new RibbonKeyboardAccessKeyCombination();
                }
                else
                {
                    keyCombination = value;
                }
            }
        }

        public virtual double KeyboardAccessHorizontalOffset
        {
            get
            {
                return keyHorizonalOffset;
            }
            set
            {
                keyHorizonalOffset = value;
            }
        }

        public virtual double KeyboardAccessVerticalOffset
        {
            get
            {
                return keyVerticalOffset;
            }
            set
            {
                keyVerticalOffset = value;
            }
        }

        public virtual void fireClickEvent(object sender, MouseButtonEventArgs args)
        {
            if (Clicked != null)
            {
                Clicked(sender, args);
            }
        }

        #endregion

        public virtual List<IRibbonControl> getElements()
        {
            List<IRibbonControl> list = new List<IRibbonControl>();
            list.Add(this);
            return list;
        }
    }
}
