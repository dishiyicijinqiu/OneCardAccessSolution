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
    /// <summary>
    /// Interaction logic for RibbonPreviewBoxes.xaml
    /// </summary>
    public partial class RibbonPreviewBoxes : RibbonControlBase, IRibbonFullControl 
    {
        private ListenableList<RibbonPreviewBox> previews = new ListenableList<RibbonPreviewBox>();
        private int position = 0;
        private int shownBoxes = 3;
        private bool showPopupGroups = true;
        private RibbonPreviewBoxesPopup rpb = null;

        private Grid normalView = null;
        private Thickness normalMargin = new Thickness();

        private RibbonDoubleButton minimisedButton = null;

        private RibbonPreviewBox selectedBox = null;
        private delegate void RefreshDelegate();

        protected override void initialize()
        {
            InitializeComponent();
            normalView = previewGrid;
            normalMargin = this.Margin;

            RibbonStyleHandler.styleButtonBorder(upBorder, this, this);
            RibbonStyleHandler.styleButtonBorder(downBorder, this, this);
            RibbonStyleHandler.styleButtonBorder(popupBorder, this, this);

            canResize = true;

            previews.ElementAdded += new ListenableList<RibbonPreviewBox>.ElementAddedDelegate(previews_ElementAdded);
            previews.ElementRemoved += new ListenableList<RibbonPreviewBox>.ElementRemovedDelegate(previews_ElementRemoved);

            hasQATbutton = false;
        }

        private void previews_ElementRemoved(ListenableList<RibbonPreviewBox> sender, ListenableList<RibbonPreviewBox>.ElementRemovedEventArgs<RibbonPreviewBox> args)
        {
            throw new NotImplementedException();
        }

        private void previews_ElementAdded(ListenableList<RibbonPreviewBox> sender, ListenableList<RibbonPreviewBox>.ElementAddedEventArgs<RibbonPreviewBox> args)
        {
            Console.WriteLine(args.Index + " --- " + args.Item.ToString());
            //previewWraps.Children.Insert(args.Index, args.Item);
            repopulate();
        }

        public override void updateStyle(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            contentBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
            upButton.Foreground = RibbonStyleHandler.ButtonNormalText;
            downButton.Foreground = RibbonStyleHandler.ButtonNormalText;
            popupButton.Foreground = RibbonStyleHandler.ButtonNormalText;
        }

        public ListenableList<RibbonPreviewBox> Children
        {
            get
            {
                return previews;
            }
        }

        public void setSelected(RibbonPreviewBox box)
        {
            selectedBox = box;
            if (box != null)
            {
                selectedBox.fireClick();
                if (minimisedButton != null)
                {
                    if (selectedBox.Image != null)
                    {
                        minimisedButton.NormalImage = selectedBox.Image.Clone();
                    }
                    else
                    {
                        minimisedButton.NormalImage = null;
                    }
                }
                displayPreview();
            }
            else
            {
                repopulate();
            }
            rpb = null;
        }

        public void displayPreview(RibbonPreviewBox box)
        {
            if (previews.Contains(box) != true)
            {
                throw new Exception("Preview Box not part of control");
            }

            #region rewind to beginning
            while (position != 0)
            {
                upBorder_MouseDown(null, null);
            }
            #endregion

            if (!IsPreviewShown(box))
            {
                while (moveDown())
                {
                    if (IsPreviewShown(box))
                    {
                        return;
                    }
                }
            }
            else
            {
                repopulate();
            }
        }

        public void displayPreview()
        {
            if (selectedBox != null)
            {
                displayPreview(selectedBox);
            }
        }

        public bool IsPreviewShown(RibbonPreviewBox box)
        {
            for (int i = position; i < position + shownBoxes; i++)
            {
                if (previews[i] == box)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsSelectedShown
        {
            get
            {
                if (selectedBox == null)
                {
                    throw new Exception("No previews added");
                }

                return IsPreviewShown(selectedBox);
            }
        }

        public void deSelectAll()
        {
            foreach (RibbonPreviewBox b in previews)
            {
                b.Selected = false;
            }
        }

        public override String Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                if (minimisedButton != null)
                {
                    minimisedButton.Text = base.Text;
                }
            }
        }

        public override ImageSource NormalImage
        {
            get { return base.NormalImage; }
            set
            {
                base.NormalImage = value;

                if (minimisedButton != null)
                {
                    minimisedButton.NormalImage = base.NormalImage;
                }
            }
        }

        public override bool IsMouseInside
        {
            get
            {
                if (rpb != null)
                {
                    return base.IsMouseInside || rpb.IsMouseInside;
                }
                else
                {
                    return base.IsMouseInside;
                }
            }
        }

        public bool ShowPopupGroups
        {
            get
            {
                return showPopupGroups;
            }
            set
            {
                showPopupGroups = value;
            }
        }

        #region button handlers
        private void downBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            moveDown();
        }

        private bool moveDown()
        {
            removeAll();

            position += shownBoxes;
            //Console.WriteLine("Position: " + position);

            if (position > previews.Count - 1)
            {
                position -= shownBoxes;
                repopulate();

                return false;
            }
            else
            {
                repopulate();
                return true;
            }
        }

        private void upBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            removeAll();

            position -= shownBoxes;

            if (position < 0)
            {
                position = 0;
            }

            repopulate();
        }

        private void popupBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            removeAll();
            rpb = new RibbonPreviewBoxesPopup(previews, new RibbonPreviewBoxesPopup.PopupClosed(popupReturn), this.ShowPopupGroups);
            rpb.PlacementTarget = this;
            rpb.VerticalOffset = this.Height * -1.0;
            rpb.IsOpen = true;
            rpb.Width = this.ActualWidth;
        }

        private void minimisedButton_BottomButtonPressed(object sender, MouseButtonEventArgs e)
        {
            RibbonPreviewBoxesPopup rpb = new RibbonPreviewBoxesPopup(previews, new RibbonPreviewBoxesPopup.PopupClosed(popupReturn), this.showPopupGroups);
            rpb.PlacementTarget = this;
            rpb.VerticalOffset = this.Height * -1.0;
            rpb.IsOpen = true;
            rpb.Width = 207;
        }

        private void popupReturn(RibbonPreviewBox clicked)
        {
            setSelected(clicked);
        }

        private void removeAll()
        {
            #region remove old
            previewWraps.Children.RemoveRange(0, previewWraps.Children.Count);
            #endregion
        }

        private void repopulate()
        {
            removeAll();
            for (int i = 0; i < shownBoxes && position + i < previews.Count; i++)
            {
                //Console.Write("pop [" + i + "], ");
                previewWraps.Children.Add(previews[position + i]);
            }
            //Console.WriteLine("");
        }
        #endregion

        #region IRibbonGroupBoxChild Members

        public override bool resizeSmaller()
        {
            if (shownBoxes > 3)
            {
                shownBoxes--;
                this.Width -= 61;
                removeAll();
                repopulate();
                return true;
            }
            else if (shownBoxes == 3)
            {
                RibbonStyleHandler.unstyle(upBorder);
                RibbonStyleHandler.unstyle(downBorder);
                RibbonStyleHandler.unstyle(popupBorder);

                shownBoxes = 1;

                if (minimisedButton == null)
                {
                    minimisedButton = new RibbonDoubleButton();
                    minimisedButton.Margin = new Thickness(0);
                    minimisedButton.Text = base.Text;
                    if (selectedBox != null)
                    {
                        minimisedButton.NormalImage = selectedBox.Image.Clone();
                    }
                    minimisedButton.BottomButtonPressed += new MouseButtonEventHandler(minimisedButton_BottomButtonPressed);
                }
                else
                {
                    minimisedButton.updateStyle(new RibbonStyleHandler.StyleChangedEventArgs(RibbonStyleHandler.Style.Not_Set));
                    if (selectedBox != null)
                    {
                        minimisedButton.NormalImage = selectedBox.Image.Clone();
                    }
                }

                this.Content = minimisedButton;
                this.MinWidth = 50; 
                this.Width = 50;
                this.Width = double.NaN;
                //this.Margin = new Thickness(0);
                //this.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool resizeBigger()
        {
            if (shownBoxes >= 3)
            {
                if (shownBoxes > previews.Count)
                {
                    return false;
                }
                else
                {
                    shownBoxes++;
                    this.Width += 61;
                    removeAll();
                    repopulate();
                    return true;
                }
            }
            else if (shownBoxes == 1)
            {
                this.Content = normalView;
                this.Margin = normalMargin;
                shownBoxes = 3;
                removeAll();
                repopulate();
                this.Width = 204;

                RibbonStyleHandler.styleButtonBorder(upBorder, this, this);
                RibbonStyleHandler.styleButtonBorder(downBorder, this, this);
                RibbonStyleHandler.styleButtonBorder(popupBorder, this, this);

                contentBorder.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderNormal);
                upButton.Foreground = RibbonStyleHandler.ButtonNormalText;
                downButton.Foreground = RibbonStyleHandler.ButtonNormalText;
                popupButton.Foreground = RibbonStyleHandler.ButtonNormalText;

                return true;
            }
            else
            {
                return false;
            }
        }

        public override double ResizePriority
        {
            get
            {
                if (shownBoxes == 3)
                {
                    return 4;
                }
                else
                {
                    return 1;
                }
            }
        }

        public override double ResizeIncreaseAmount
        {
            get 
            {
                if (shownBoxes == 1)
                {
                    return 200;
                }
                else
                {
                    return 90;
                }
            }
        }

        #endregion
    }
}
