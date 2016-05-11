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
    /// Interaction logic for ApplicationDocument.xaml
    /// </summary>
    public partial class ApplicationDocument : UserControl
    {
        public event MouseButtonEventHandler Clicked;
        public event PinnedChangedEventDelegate PinChanged;
        public delegate void PinnedChangedEventDelegate(bool newValue);


        private ImageSource pinnedImageSource = null;
        private ImageSource unpinnedImageSource = null;
        private bool pinned = false;
        private bool inPin = false;

        public ApplicationDocument()
        {
            InitializeComponent();

            List<GradientStop> transparentStops = new List<GradientStop>();
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.647));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.6471));
            transparentStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 1));
            RibbonStyleHandler.styleButtonBorder(masterBorder, this, this, Color.FromArgb(0, 255, 255, 255), transparentStops);
            RibbonStyleHandler.styleButtonBorder(pinBorder, this, this, Color.FromArgb(0, 255, 255, 255), transparentStops);

            RibbonStyleHandler.StyleChanged += new RibbonStyleHandler.StyleChangedHandler(RibbonStyleHandler_StyleChanged);
            RibbonStyleHandler_StyleChanged(null);
        }

        private void RibbonStyleHandler_StyleChanged(RibbonStyleHandler.StyleChangedEventArgs args)
        {
            theLabel.Foreground = RibbonStyleHandler.ButtonNormalText;
        }

        public String Text
        {
            get
            {
                return theLabel.Content.ToString();
            }
            set
            {
                if (value != null)
                {
                    theLabel.Content = value;
                }
                else
                {
                    theLabel.Content = "";
                }
            }
        }

        public ImageSource PinnedImage
        {
            get
            {
                return pinnedImageSource;
            }
            set
            {
                pinnedImageSource = value;

                if (pinned)
                {
                    theImage.Source = pinnedImageSource;
                }
            }
        }

        public ImageSource UnPinnedImage
        {
            get
            {
                return unpinnedImageSource;
            }
            set
            {
                unpinnedImageSource = value;

                if (!pinned)
                {
                    theImage.Source = unpinnedImageSource;
                }
            }
        }

        public bool Pinned
        {
            get
            {
                return pinned;
            }
            set
            {
                bool fireEvent = false;
                if (pinned != value)
                {
                    fireEvent = true;
                }
                pinned = value;
                if (pinned)
                {
                    theImage.Source = pinnedImageSource;
                }
                else
                {
                    theImage.Source = unpinnedImageSource;
                }

                if (PinChanged != null && fireEvent)
                {
                    PinChanged(value);
                }
            }
        }

        private void pinBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Pinned = !this.Pinned;
        }

        private void pinBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            inPin = true;
        }

        private void pinBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            inPin = false;
        }

        private void masterBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!inPin && Clicked != null)
            {
                Clicked(this, e);
            }
        }
    }
}
