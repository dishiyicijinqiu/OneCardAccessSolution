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
using System.Windows.Media.Animation;

//------------------------------------------------------------------------------//
//                                                                              //
// Author:  Derek Bartram                                                       //
// Date:    31/01/2008                                                          //
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
    public partial class ApplicationButton : UserControl
    {
        #region class variables
        private Color baseColor = Color.FromRgb(19, 37, 144);
        private Color highlightColor = Color.FromRgb(138, 228, 250);
        private Color baseHoverColor = Color.FromRgb(255, 211, 72);
        private Color highlightHoverColor = Color.FromRgb(255, 253, 219);
        private Color basePressedColor = Color.FromRgb(233, 102, 14);
        private Color highlightPressedColor = Color.FromRgb(219, 180, 149);

        private SolidColorBrush backgroundBrush = null;
        private RadialGradientBrush topBrush = null;
        private RadialGradientBrush bottomBrush = null;
        private GradientStopCollection bottomStops = null;
        private GradientStopCollection topStops = null;

        private ColorAnimation backgroundEnterAnnimation = null;
        private ColorAnimation backgroundLeaveAnnimation = null;
        private List<ColorAnimation> bottomStopsEnterAnnimations = new List<ColorAnimation>();
        private List<ColorAnimation> bottomStopsLeaveAnnimations = new List<ColorAnimation>();
        private List<ColorAnimation> topStopsEnterAnnimations = new List<ColorAnimation>();
        private List<ColorAnimation> topStopsLeaveAnnimations = new List<ColorAnimation>();

        private Storyboard enterStoryboard = null;
        private Storyboard leaveStoryboard = null;

        private bool mouseOver = false;
        private bool mouseDown = false;

        public event MouseEventHandler Clicked;
        #endregion

        public ApplicationButton()
        {
            InitializeComponent();

            #region store brushes / brush components
            backgroundBrush = (SolidColorBrush)(backgroundRound.Fill);
            bottomStops = ((RadialGradientBrush)bottomRound.Fill).GradientStops;
            topStops = ((RadialGradientBrush)topRound.Fill).GradientStops;
            bottomBrush = (RadialGradientBrush)bottomRound.Fill;
            topBrush = (RadialGradientBrush)topRound.Fill;
            #endregion

            #region link brushs
            #region background
            this.RegisterName("backgroundBrush", backgroundBrush);
            #endregion

            #region bottom stops
            for (int i = 0; i < bottomStops.Count; i++)
            {
                this.RegisterName("bottomStops" + i, bottomStops[i]);
            }
            #endregion

            #region top stops
            for (int i = 0; i < topStops.Count; i++)
            {
                this.RegisterName("topStops" + i, topStops[i]);
            }

            #endregion
            #endregion

            #region create annimation sequence
            #region background
            #region enter
            backgroundEnterAnnimation = new ColorAnimation();
            backgroundEnterAnnimation.From = baseColor;
            backgroundEnterAnnimation.To = baseHoverColor;
            backgroundEnterAnnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            backgroundEnterAnnimation.AutoReverse = false;
            #endregion

            #region leave
            backgroundLeaveAnnimation = new ColorAnimation();
            backgroundLeaveAnnimation.From = baseHoverColor;
            backgroundLeaveAnnimation.To = baseColor;
            backgroundLeaveAnnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
            backgroundLeaveAnnimation.AutoReverse = false;
            #endregion
            #endregion

            #region bottom stops
            #region enter
            for (int i = 0; i < bottomStops.Count; i++)
            {
                ColorAnimation enterAnimation = new ColorAnimation();
                enterAnimation.From = bottomStops[i].Clone().Color;
                Color otherColor = Color.FromArgb(bottomStops[i].Color.A,
                                                highlightHoverColor.R,
                                                highlightHoverColor.G,
                                                highlightHoverColor.B);
                enterAnimation.To = otherColor;
                enterAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                enterAnimation.AutoReverse = false;

                bottomStopsEnterAnnimations.Add(enterAnimation);
            }
            #endregion

            #region leave
            for (int i = 0; i < bottomStops.Count; i++)
            {
                ColorAnimation leaveAnimation = new ColorAnimation();
                Color otherColor = Color.FromArgb(bottomStops[i].Color.A,
                                                highlightHoverColor.R,
                                                highlightHoverColor.G,
                                                highlightHoverColor.B);
                leaveAnimation.From = otherColor;
                leaveAnimation.To = bottomStops[i].Clone().Color;
                leaveAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
                leaveAnimation.AutoReverse = false;

                bottomStopsLeaveAnnimations.Add(leaveAnimation);
            }
            #endregion
            #endregion

            #region top stops
            #region enter
            for (int i = 0; i < topStops.Count; i++)
            {
                ColorAnimation enterAnimation = new ColorAnimation();
                enterAnimation.From = topStops[i].Clone().Color;
                Color otherColor = Color.FromArgb(topStops[i].Color.A,
                                                highlightHoverColor.R,
                                                highlightHoverColor.G,
                                                highlightHoverColor.B);
                enterAnimation.To = otherColor;
                enterAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                enterAnimation.AutoReverse = false;

                topStopsEnterAnnimations.Add(enterAnimation);
            }
            #endregion

            #region leave
            for (int i = 0; i < topStops.Count; i++)
            {
                ColorAnimation leaveAnimation = new ColorAnimation();
                Color otherColor = Color.FromArgb(topStops[i].Color.A,
                                                highlightHoverColor.R,
                                                highlightHoverColor.G,
                                                highlightHoverColor.B);
                leaveAnimation.From = otherColor;
                leaveAnimation.To = topStops[i].Clone().Color;
                leaveAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
                leaveAnimation.AutoReverse = false;

                topStopsLeaveAnnimations.Add(leaveAnimation);
            }
            #endregion
            #endregion
            #endregion

            #region create storyboard
            enterStoryboard = new Storyboard();
            leaveStoryboard = new Storyboard();

            #region background
            #region enter
            Storyboard.SetTargetName(backgroundEnterAnnimation, "backgroundBrush");
            Storyboard.SetTargetProperty(backgroundEnterAnnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            enterStoryboard.Children.Add(backgroundEnterAnnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(backgroundLeaveAnnimation, "backgroundBrush");
            Storyboard.SetTargetProperty(backgroundLeaveAnnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            leaveStoryboard.Children.Add(backgroundLeaveAnnimation);
            #endregion
            #endregion

            #region bottom stops
            #region enter
            for (int i = 0; i < bottomStops.Count; i++)
            {
                Storyboard.SetTargetName(bottomStopsEnterAnnimations[i], "bottomStops" + i);
                Storyboard.SetTargetProperty(bottomStopsEnterAnnimations[i], new PropertyPath(GradientStop.ColorProperty));
                enterStoryboard.Children.Add(bottomStopsEnterAnnimations[i]);
            }
            #endregion

            #region leave
            for (int i = 0; i < bottomStops.Count; i++)
            {
                Storyboard.SetTargetName(bottomStopsLeaveAnnimations[i], "bottomStops" + i);
                Storyboard.SetTargetProperty(bottomStopsLeaveAnnimations[i], new PropertyPath(GradientStop.ColorProperty));
                leaveStoryboard.Children.Add(bottomStopsLeaveAnnimations[i]);
            }
            #endregion
            #endregion

            #region top stops
            #region enter
            for (int i = 0; i < topStops.Count; i++)
            {
                Storyboard.SetTargetName(topStopsEnterAnnimations[i], "topStops" + i);
                Storyboard.SetTargetProperty(topStopsEnterAnnimations[i], new PropertyPath(GradientStop.ColorProperty));
                enterStoryboard.Children.Add(topStopsEnterAnnimations[i]);
            }
            #endregion

            #region leave
            for (int i = 0; i < topStops.Count; i++)
            {
                Storyboard.SetTargetName(topStopsLeaveAnnimations[i], "topStops" + i);
                Storyboard.SetTargetProperty(topStopsLeaveAnnimations[i], new PropertyPath(GradientStop.ColorProperty));
                leaveStoryboard.Children.Add(topStopsLeaveAnnimations[i]);
            }
            #endregion
            #endregion
            #endregion

            #region event handler setup
            this.MouseEnter += new MouseEventHandler(ApplicationButton_MouseEnter);
            this.MouseLeave += new MouseEventHandler(ApplicationButton_MouseLeave);
            this.MouseDown += new MouseButtonEventHandler(ApplicationButton_MouseDown);
            this.MouseUp += new MouseButtonEventHandler(ApplicationButton_MouseUp);
            #endregion
        }

        #region event handlers
        private void ApplicationButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                ApplicationButton_MouseUp(sender, new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left));
            }

            leaveStoryboard.Begin(this);
            mouseOver = false;
        }

        private void ApplicationButton_MouseEnter(object sender, MouseEventArgs e)
        {
            enterStoryboard.Begin(this);
            mouseOver = true;
        }

        private void ApplicationButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDown = false;

            backgroundRound.Fill = backgroundBrush;
            topRound.Fill = topBrush;
            bottomRound.Fill = bottomBrush;
        }

        private void ApplicationButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;

            backgroundRound.Fill = new SolidColorBrush(basePressedColor);

            #region top
            GradientStopCollection topGsc = new GradientStopCollection();
            for (int i = 0; i < topStops.Count; i++)
            {
                topGsc.Add(new GradientStop(Color.FromArgb(topStops[i].Color.A,
                                                        highlightPressedColor.R,
                                                        highlightPressedColor.G,
                                                        highlightPressedColor.B),
                                                            topStops[i].Offset));
            }
            topRound.Fill = new RadialGradientBrush(topGsc);
            #endregion

            #region bottom
            GradientStopCollection bottomGsc = new GradientStopCollection();
            for (int i = 0; i < bottomStops.Count; i++)
            {
                bottomGsc.Add(new GradientStop(Color.FromArgb(bottomStops[i].Color.A,
                                                        highlightPressedColor.R,
                                                        highlightPressedColor.G,
                                                        highlightPressedColor.B),
                                                            bottomStops[i].Offset));
            }
            topRound.Fill = new RadialGradientBrush(bottomGsc);
            #endregion
        }
        #endregion

        #region color updates
        public void autoGenerateHighlightColors()
        {
            highlightColor = Color.FromRgb(
                (byte)Math.Min(255, baseColor.R + 127),
                (byte)Math.Min(255, baseColor.G + 196),
                (byte)Math.Min(255, baseColor.B + 111));

            highlightHoverColor = Color.FromRgb(
                (byte)Math.Min(255, baseHoverColor.R + 127),
                (byte)Math.Min(255, baseHoverColor.G + 196),
                (byte)Math.Min(255, baseHoverColor.B + 111));

            updateColors();
        }

        private void updateColors()
        {
            #region update background
            if (!mouseOver)
            {
                backgroundBrush.Color = baseColor;
                for (int i = 0; i < bottomStops.Count; i++)
                {
                    bottomStops[i].Color = Color.FromArgb(bottomStops[i].Color.A,
                                                                highlightColor.R,
                                                                highlightColor.G,
                                                                highlightColor.B);
                }
                for (int i = 0; i < bottomStops.Count; i++)
                {
                    topStops[i].Color = Color.FromArgb(topStops[i].Color.A,
                                                                highlightColor.R,
                                                                highlightColor.G,
                                                                highlightColor.B);
                }
            }
            #endregion

            #region update background annimations
            backgroundEnterAnnimation.From = baseColor;
            backgroundEnterAnnimation.To = baseHoverColor;
            backgroundLeaveAnnimation.From = baseHoverColor;
            backgroundLeaveAnnimation.To = baseColor;
            #endregion

            #region update bottom stops annimations
            for (int i = 0; i < bottomStops.Count; i++)
            {
                Color otherColor = Color.FromArgb(bottomStops[i].Color.A,
                                                highlightHoverColor.R,
                                                highlightHoverColor.G,
                                                highlightHoverColor.B);

                ColorAnimation enterAnimation = bottomStopsEnterAnnimations[i];
                enterAnimation.From = bottomStops[i].Clone().Color;
                enterAnimation.To = otherColor;
                ColorAnimation leaveAnimation = bottomStopsLeaveAnnimations[i];
                leaveAnimation.To = bottomStops[i].Clone().Color;
                leaveAnimation.From = otherColor;

            }
            #endregion

            #region update top stops annimations
            for (int i = 0; i < topStops.Count; i++)
            {
                Color otherColor = Color.FromArgb(topStops[i].Color.A,
                                                highlightHoverColor.R,
                                                highlightHoverColor.G,
                                                highlightHoverColor.B);

                ColorAnimation enterAnimation = topStopsEnterAnnimations[i];
                enterAnimation.From = topStops[i].Clone().Color;
                enterAnimation.To = otherColor;
                ColorAnimation leaveAnimation = topStopsLeaveAnnimations[i];
                leaveAnimation.To = topStops[i].Clone().Color;
                leaveAnimation.From = otherColor;
            }
            #endregion
        }
        #endregion

        #region assesors
        public ImageSource Image
        {
            get
            {
                return image.Source;
            }
            set
            {
                image.Source = value;
            }
        }

        public Color BaseColor
        {
            get
            {
                return baseColor;
            }
            set
            {
                baseColor = value;
                updateColors();
            }
        }

        public Color HighlightColor
        {
            get
            {
                return highlightColor;
            }
            set
            {
                highlightColor = value;
                updateColors();
            }
        }

        public Color BaseHoverColor
        {
            get
            {
                return baseHoverColor;
            }
            set
            {
                baseHoverColor = value;
                updateColors();
            }
        }

        public Color HighlightHoverColor
        {
            get
            {
                return highlightHoverColor;
            }
            set
            {
                highlightHoverColor = value;
                updateColors();
            }
        }

        public Color BasePressedColor
        {
            get
            {
                return basePressedColor;
            }
            set
            {
                basePressedColor = value;
                updateColors();
            }
        }

        public Color HighlightPressedColor
        {
            get
            {
                return highlightPressedColor;
            }
            set
            {
                highlightPressedColor = value;
                updateColors();
            }
        }
        #endregion

        private void backgroundRound_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Clicked != null)
            {
                Clicked(this, null);
            }
        }

        public ApplicationButton getPressedClone()
        {
            ApplicationButton button = new ApplicationButton();
            button.BaseColor = this.BasePressedColor;
            button.HighlightColor = this.HighlightPressedColor;
            button.BaseHoverColor = this.BasePressedColor;
            button.HighlightHoverColor = this.HighlightPressedColor;
            button.BasePressedColor = this.BasePressedColor;
            button.HighlightPressedColor = this.HighlightPressedColor;

            if (this.Image != null)
            {
                button.Image = this.Image.Clone();
            }

            return button;
        }

    }
}
