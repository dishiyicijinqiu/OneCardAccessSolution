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
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;
using System.Windows.Media.Effects;
using System.Windows.Media.Animation;
using DNBSoft.WPF;

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
    public class RibbonStyleHandler
    {
        #region brushes
        #region button brushes
        public static Color ButtonBorderNormal = Color.FromArgb(255, 117, 150, 191);
        public static Color ButtonBorderHover = Color.FromArgb(255, 192, 167, 118);
        public static Color ButtonBorderPressed = Color.FromArgb(255, 128, 255, 128);
        public static Color ButtonBorderDisabled = Color.FromArgb(255, 128, 255, 128);
        public static Color ButtonBorderSelected = Color.FromArgb(255, 128, 255, 128);
        public static Color ButtonBorderSelectedHover = Color.FromArgb(255, 128, 255, 128);

        public static List<GradientStop> ButtonBackgroundNormal = new List<GradientStop>();
        public static List<GradientStop> ButtonBackgroundHover = new List<GradientStop>();
        public static List<GradientStop> ButtonBackgroundPressed = new List<GradientStop>();
        public static List<GradientStop> ButtonBackgroundDisabled = new List<GradientStop>();
        public static List<GradientStop> ButtonBackgroundSelected = new List<GradientStop>();
        public static List<GradientStop> ButtonBackgroundSelectedHover = new List<GradientStop>();

        public static Brush ButtonNormalText = new SolidColorBrush(Color.FromArgb(255, 35, 146, 18));
        public static Brush ButtonSelectedText = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        public static Brush ButtonDisabledText = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));
        #endregion

        #region tab brushes
        public static Color TabBorderNormal = Color.FromArgb(255, 117, 150, 191);
        public static Color TabBorderHover = Color.FromArgb(255, 192, 167, 118);
        public static Color TabBorderPressed = Color.FromArgb(255, 128, 255, 128);
        public static Color TabBorderDisabled = Color.FromArgb(255, 128, 255, 128);
        public static Color TabBorderSelected = Color.FromArgb(255, 128, 255, 128);
        public static Color TabBorderSelectedHover = Color.FromArgb(255, 128, 255, 128);

        public static List<GradientStop> TabBackgroundNormal = new List<GradientStop>();
        public static List<GradientStop> TabBackgroundHover = new List<GradientStop>();
        public static List<GradientStop> TabBackgroundPressed = new List<GradientStop>();
        public static List<GradientStop> TabBackgroundDisabled = new List<GradientStop>();
        public static List<GradientStop> TabBackgroundSelected = new List<GradientStop>();
        public static List<GradientStop> TabBackgroundSelectedHover = new List<GradientStop>();

        public static Brush TabNormalText = new SolidColorBrush(Color.FromArgb(255, 35, 146, 18));
        public static Brush TabSelectedText = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        public static Brush TabDisabledText = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));
        #endregion

        #region group brushes //
        public static Color GroupBorderNormal = Color.FromArgb(255, 31, 132, 16);
        public static Color GroupBorderHover = Color.FromArgb(255, 64, 162, 50);

        public static List<GradientStop> GroupBackgroundNormal = new List<GradientStop>();
        public static List<GradientStop> GroupBackgroundHover = new List<GradientStop>();

        public static Color GroupLabelBorderNormal = Color.FromArgb(255, 31, 132, 16);
        public static Color GroupLabelBorderHover = Color.FromArgb(255, 64, 162, 50);

        public static List<GradientStop> GroupLabelBackgroundNormal = new List<GradientStop>();
        public static List<GradientStop> GroupLabelBackgroundHover = new List<GradientStop>();

        public static Brush GroupText = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));// 210, 240, 205));
        #endregion

        #region background brushes//
        public static Brush RibbonBarBackground = new SolidColorBrush();//
        public static Brush RibbonTitleBackground = new SolidColorBrush(Color.FromArgb(255, 191, 219, 255));//
        public static Brush PreviewBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        #endregion

        #region quick access toolbar brushes
        public static Brush QuickAccessBackground = new SolidColorBrush();
        #endregion
        #endregion

        #region timing
        public static TimeSpan EnterTime = TimeSpan.FromMilliseconds(300);
        public static TimeSpan LeaveTime = TimeSpan.FromMilliseconds(900);
        #endregion

        #region storage
        private static int NameIndex = 0;

        private static Dictionary<object, List<Storyboard>> EnterStoryboardsNormal = new Dictionary<object, List<Storyboard>>();
        private static Dictionary<object, List<Storyboard>> LeaveStoryboardsNormal = new Dictionary<object, List<Storyboard>>();
        private static Dictionary<object, List<Storyboard>> EnterStoryboardsSelected = new Dictionary<object, List<Storyboard>>();
        private static Dictionary<object, List<Storyboard>> LeaveStoryboardsSelected = new Dictionary<object, List<Storyboard>>();
        private static Dictionary<RibbonBorder, Brush> BackgroundBrushesNormal = new Dictionary<RibbonBorder, Brush>();
        private static Dictionary<RibbonBorder, Brush> BorderBrushesNormal = new Dictionary<RibbonBorder, Brush>();
        private static Dictionary<RibbonBorder, Brush> BackgroundBrushesSelected = new Dictionary<RibbonBorder, Brush>();
        private static Dictionary<RibbonBorder, Brush> BorderBrushesSelected = new Dictionary<RibbonBorder, Brush>();
        private static Dictionary<object, FrameworkElement> ContainingElements = new Dictionary<object, FrameworkElement>();
        private static Dictionary<UIElement, List<RibbonBorder>> MultiBorderLinks = new Dictionary<UIElement, List<RibbonBorder>>();
        private static Dictionary<RibbonBorder, List<Label>> StyledButtonLabels = new Dictionary<RibbonBorder, List<Label>>();
        private static Dictionary<RibbonBorder, List<Label>> StyledTabLabels = new Dictionary<RibbonBorder, List<Label>>();

        private static RibbonStyleHandler thisStyle = new RibbonStyleHandler();

        private static List<StyleParameters> CurrentStyles = new List<StyleParameters>();
        private static List<Panel> BarBackgrounds = new List<Panel>();
        private static List<Panel> TitleBackgrounds = new List<Panel>();
        private static List<RibbonBorder> TabBorders = new List<RibbonBorder>();

        private static Dictionary<object, Brush> PriorPressedBrushes = new Dictionary<object, Brush>();
        #endregion

        #region events
        public static event RibbonStyleHandler.StyleChangedHandler StyleChanged;

        public delegate void StyleChangedHandler(RibbonStyleHandler.StyleChangedEventArgs args);
        #endregion

        #region initialisation
        private RibbonStyleHandler()
        {
            setBlueStyle();
        }
        #endregion

        #region static style application
        public static void styleButtonBorder(RibbonBorder element, UIElement parent, FrameworkElement containingElement)
        {
            RibbonStyleHandler.styleButtonBorder(element, parent, containingElement, RibbonStyleHandler.ButtonBorderNormal);
        }

        public static void styleButtonBorder(RibbonBorder element, UIElement parent, FrameworkElement containingElement, Color initialBorderColor)
        {
            RibbonStyleHandler.styleButtonBorder(element, parent, containingElement, initialBorderColor, RibbonStyleHandler.ButtonBackgroundNormal);
        }

        public static void styleButtonBorder(RibbonBorder element, UIElement parent, FrameworkElement containingElement, List<GradientStop> initialBackgroundBrush)
        {
            RibbonStyleHandler.styleButtonBorder(element, parent, containingElement, RibbonStyleHandler.ButtonBorderNormal, initialBackgroundBrush);
        }

        public static void styleButtonBorder(RibbonBorder element, UIElement parent, FrameworkElement containingElement, Color initialBorderColor, List<GradientStop> initialBackgroundBrush)
        {
            #region add to styled list
            StyleParameters sp = new StyleParameters();
            sp.type = StyleType.Button;
            sp.element1 = element;
            sp.parent = parent;
            sp.parent2 = containingElement;
            sp.initialBorderColour = initialBorderColor;
            sp.initialBackgroundBrush = initialBackgroundBrush;
            sp.useInitialBorderColour = initialBorderColor == RibbonStyleHandler.ButtonBorderNormal;
            sp.useInitialBackgroundBrush = initialBackgroundBrush == RibbonStyleHandler.ButtonBackgroundNormal;
            RibbonStyleHandler.CurrentStyles.Add(sp);
            #endregion

            #region create and link brushs
            #region normal
            #region border
            SolidColorBrush borderNormalBrush = new SolidColorBrush(initialBorderColor);
            element.BorderBrush = borderNormalBrush;
            String borderBrushNormalPropertyTargetName = "borderNormalBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            element.RegisterName(borderBrushNormalPropertyTargetName, borderNormalBrush);
            #endregion

            #region background
            Dictionary<GradientStop, String> gsNormalNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection gscNormal = new GradientStopCollection();
            for (int i = 0; i < initialBackgroundBrush.Count; i++)
            {
                GradientStop gs = initialBackgroundBrush[i].Clone();
                gscNormal.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                element.RegisterName(backgroundStopPropertyTargetName, gs);

                gsNormalNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush backgroundNormalBrush = new LinearGradientBrush(gscNormal, 90.0);

            //Attach brush
            element.Background = backgroundNormalBrush;
            #endregion
            #endregion

            #region selected
            #region border
            SolidColorBrush borderSelectedBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderSelected);
            //element.BorderBrush = borderSelectedBrush; ###
            String borderBrushSelectedPropertyTargetName = "borderSelectedBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            element.RegisterName(borderBrushSelectedPropertyTargetName, borderSelectedBrush);
            #endregion

            #region background
            Dictionary<GradientStop, String> gsSelectedNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection gscSelected = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                GradientStop gs = RibbonStyleHandler.ButtonBackgroundSelected[i].Clone();
                gscSelected.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                element.RegisterName(backgroundStopPropertyTargetName, gs);

                gsSelectedNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush backgroundSelectedBrush = new LinearGradientBrush(gscSelected, 90.0);

            //Attach brush
            //element.Background = backgroundBrush; ###
            #endregion
            #endregion
            #endregion

            #region create annimation sequence
            #region normal
            #region border
            #region enter
            ColorAnimation borderEnterNormalAnimation = new ColorAnimation();
            borderEnterNormalAnimation.From = initialBorderColor;
            borderEnterNormalAnimation.To = RibbonStyleHandler.ButtonBorderHover;
            borderEnterNormalAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            borderEnterNormalAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation borderLeaveNormalAnimation = new ColorAnimation();
            borderLeaveNormalAnimation.From = RibbonStyleHandler.ButtonBorderHover;
            borderLeaveNormalAnimation.To = initialBorderColor;
            borderLeaveNormalAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            borderLeaveNormalAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region background
            #region enter
            List<ColorAnimation> backgroundEnterNormalAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = initialBackgroundBrush[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                backgroundEnterNormalAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> backgroundLeaveNormalAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = initialBackgroundBrush[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                backgroundLeaveNormalAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region enter
            ColorAnimation borderEnterSelectedAnimation = new ColorAnimation();
            borderEnterSelectedAnimation.From = RibbonStyleHandler.ButtonBorderSelected;
            borderEnterSelectedAnimation.To = RibbonStyleHandler.ButtonBorderSelectedHover;
            borderEnterSelectedAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            borderEnterSelectedAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation borderLeaveSelectedAnimation = new ColorAnimation();
            borderLeaveSelectedAnimation.From = RibbonStyleHandler.ButtonBorderSelectedHover;
            borderLeaveSelectedAnimation.To = RibbonStyleHandler.ButtonBorderSelected;
            borderLeaveSelectedAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            borderLeaveSelectedAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region background
            #region enter
            List<ColorAnimation> backgroundEnterSelectedAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Color;// initialBackgroundBrush[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.ButtonBackgroundSelectedHover[i].Clone().Color;// RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                backgroundEnterSelectedAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> backgroundLeaveSelectedAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.ButtonBackgroundSelectedHover[i].Clone().Color;//RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Color;// initialBackgroundBrush[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                backgroundLeaveSelectedAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region create storyboard
            #region normal
            #region border
            #region enter
            Storyboard.SetTargetName(borderEnterNormalAnimation, borderBrushNormalPropertyTargetName);
            Storyboard.SetTargetProperty(borderEnterNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderEnterNormalStoryboard = new Storyboard();
            borderEnterNormalStoryboard.Children.Add(borderEnterNormalAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(borderLeaveNormalAnimation, borderBrushNormalPropertyTargetName);
            Storyboard.SetTargetProperty(borderLeaveNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderLeaveNormalStoryboard = new Storyboard();
            borderLeaveNormalStoryboard.Children.Add(borderLeaveNormalAnimation);
            #endregion
            #endregion

            #region background
            #region enter
            Storyboard backgroundEnterNormalStoryboard = new Storyboard();

            for (int i = 0; i < backgroundEnterNormalAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundEnterNormalAnimations[i], gsNormalNames[gscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundEnterNormalAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundEnterNormalStoryboard.Children.Add(backgroundEnterNormalAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard backgroundLeaveNormalStoryboard = new Storyboard();

            for (int i = 0; i < backgroundLeaveNormalAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundLeaveNormalAnimations[i], gsNormalNames[gscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundLeaveNormalAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundLeaveNormalStoryboard.Children.Add(backgroundLeaveNormalAnimations[i]);
            }
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region enter
            Storyboard.SetTargetName(borderEnterSelectedAnimation, borderBrushSelectedPropertyTargetName);
            Storyboard.SetTargetProperty(borderEnterSelectedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderEnterSelectedStoryboard = new Storyboard();
            borderEnterSelectedStoryboard.Children.Add(borderEnterSelectedAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(borderLeaveSelectedAnimation, borderBrushSelectedPropertyTargetName);
            Storyboard.SetTargetProperty(borderLeaveSelectedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderLeaveSelectedStoryboard = new Storyboard();
            borderLeaveSelectedStoryboard.Children.Add(borderLeaveSelectedAnimation);
            #endregion
            #endregion

            #region background
            #region enter
            Storyboard backgroundEnterSelectedStoryboard = new Storyboard();

            for (int i = 0; i < backgroundEnterSelectedAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundEnterSelectedAnimations[i], gsSelectedNames[gscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundEnterSelectedAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundEnterSelectedStoryboard.Children.Add(backgroundEnterSelectedAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard backgroundLeaveSelectedStoryboard = new Storyboard();

            for (int i = 0; i < backgroundLeaveSelectedAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundLeaveSelectedAnimations[i], gsSelectedNames[gscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundLeaveSelectedAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundLeaveSelectedStoryboard.Children.Add(backgroundLeaveSelectedAnimations[i]);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region store parameters
            List<Storyboard> enterNormalStoryboards = new List<Storyboard>();
            List<Storyboard> leaveNormalStoryboards = new List<Storyboard>();
            List<Storyboard> enterSelectedStoryboards = new List<Storyboard>();
            List<Storyboard> leaveSelectedStoryboards = new List<Storyboard>();

            enterNormalStoryboards.Add(backgroundEnterNormalStoryboard);
            enterNormalStoryboards.Add(borderEnterNormalStoryboard);
            leaveNormalStoryboards.Add(borderLeaveNormalStoryboard);
            leaveNormalStoryboards.Add(backgroundLeaveNormalStoryboard);

            enterSelectedStoryboards.Add(backgroundEnterSelectedStoryboard);
            enterSelectedStoryboards.Add(borderEnterSelectedStoryboard);
            leaveSelectedStoryboards.Add(borderLeaveSelectedStoryboard);
            leaveSelectedStoryboards.Add(backgroundLeaveSelectedStoryboard);

            RibbonStyleHandler.EnterStoryboardsNormal.Add((object)element, enterNormalStoryboards);
            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)element, leaveNormalStoryboards);
            RibbonStyleHandler.ContainingElements.Add((object)element, containingElement);

            RibbonStyleHandler.EnterStoryboardsSelected.Add((object)element, enterSelectedStoryboards);
            RibbonStyleHandler.LeaveStoryboardsSelected.Add((object)element, leaveSelectedStoryboards);

            RibbonStyleHandler.BackgroundBrushesNormal.Add(element, backgroundNormalBrush);
            RibbonStyleHandler.BorderBrushesNormal.Add(element, borderNormalBrush);

            RibbonStyleHandler.BackgroundBrushesSelected.Add(element, backgroundSelectedBrush);
            RibbonStyleHandler.BorderBrushesSelected.Add(element, borderSelectedBrush);
            #endregion

            #region handle events
            element.MouseEnter += new MouseEventHandler(element_MouseEnter);
            element.MouseLeave += new MouseEventHandler(element_MouseLeave);
            element.MouseDown += new MouseButtonEventHandler(buttonElement_MouseDown);
            element.MouseUp += new MouseButtonEventHandler(buttonElement_MouseUp);
            element.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(element_EnabledChanged);
            element.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(element_SelectedChanged);
            #endregion

            #region fix initial status
            if (element.IsSelected)
            {
                element_SelectedChanged(element, true);
            }
            if (!element.IsEnabled)
            {
                element_EnabledChanged(element, false);
            }
            #endregion
        }

        public static void styleButtonBorder(UIElement topControl, RibbonBorder topBorder, RibbonBorder bottomBorder, UIElement parent, FrameworkElement containingElement, Color initialBorderColor, List<GradientStop> initialBackgroundBrush)
        {
            #region add to styled list
            StyleParameters sp = new StyleParameters();
            sp.type = StyleType.Button;
            sp.topControl = topControl;
            sp.element1 = topBorder;
            sp.element2 = bottomBorder;
            sp.parent = parent;
            sp.parent2 = containingElement;
            sp.initialBorderColour = initialBorderColor;
            sp.initialBackgroundBrush = initialBackgroundBrush;
            sp.useInitialBorderColour = initialBorderColor == RibbonStyleHandler.ButtonBorderNormal;
            sp.useInitialBackgroundBrush = initialBackgroundBrush == RibbonStyleHandler.ButtonBackgroundNormal;
            RibbonStyleHandler.CurrentStyles.Add(sp);
            #endregion

            #region create and link brushs
            #region normal
            #region border
            #region main
            SolidColorBrush mainBorderNormalBrush = new SolidColorBrush(initialBorderColor);
            topBorder.BorderBrush = mainBorderNormalBrush;
            String mainBorderNormalBrushPropertyTargetName = "borderBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            topBorder.RegisterName(mainBorderNormalBrushPropertyTargetName, mainBorderNormalBrush);
            #endregion

            #region label
            SolidColorBrush labelBorderNormalBrush = new SolidColorBrush(initialBorderColor);
            bottomBorder.BorderBrush = labelBorderNormalBrush;
            String labelBorderNormalBrushPropertyTargetName = "borderBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            bottomBorder.RegisterName(labelBorderNormalBrushPropertyTargetName, labelBorderNormalBrush);
            #endregion
            #endregion

            #region background
            #region main
            Dictionary<GradientStop, String> mainGsNormalNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection mainGscNormal = new GradientStopCollection();
            for (int i = 0; i < initialBackgroundBrush.Count; i++)
            {
                GradientStop gs = initialBackgroundBrush[i].Clone();
                mainGscNormal.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                topBorder.RegisterName(backgroundStopPropertyTargetName, gs);

                mainGsNormalNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush mainBackgroundNormalBrush = new LinearGradientBrush(mainGscNormal, 90.0);

            //Attach brush
            topBorder.Background = mainBackgroundNormalBrush;
            #endregion

            #region label
            Dictionary<GradientStop, String> labelGsNormalNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection labelGscNormal = new GradientStopCollection();
            for (int i = 0; i < initialBackgroundBrush.Count; i++)
            {
                GradientStop gs = new GradientStop(initialBackgroundBrush[initialBackgroundBrush.Count - 1].Clone().Color, initialBackgroundBrush[i].Clone().Offset);
                labelGscNormal.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                bottomBorder.RegisterName(backgroundStopPropertyTargetName, gs);

                labelGsNormalNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush labelBackgroundNormalBrush = new LinearGradientBrush(labelGscNormal, 90.0);

            //Attach brush
            bottomBorder.Background = labelBackgroundNormalBrush;
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region main
            SolidColorBrush mainBorderSelectedBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderSelected);
            String mainBorderSelectedBrushPropertyTargetName = "borderBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            topBorder.RegisterName(mainBorderSelectedBrushPropertyTargetName, mainBorderSelectedBrush);
            #endregion

            #region label
            SolidColorBrush labelBorderSelectedBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderSelected);
            String labelBorderSelectedBrushPropertyTargetName = "borderBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            bottomBorder.RegisterName(labelBorderSelectedBrushPropertyTargetName, labelBorderSelectedBrush);
            #endregion
            #endregion

            #region background
            #region main
            Dictionary<GradientStop, String> mainGsSelectedNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection mainGscSelected = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                GradientStop gs = RibbonStyleHandler.ButtonBackgroundSelected[i].Clone();
                mainGscSelected.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                topBorder.RegisterName(backgroundStopPropertyTargetName, gs);

                mainGsSelectedNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush mainBackgroundSelectedBrush = new LinearGradientBrush(mainGscSelected, 90.0);

            //Attach brush
            #endregion

            #region label
            Dictionary<GradientStop, String> labelGsSelectedNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection labelGscSelected = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                GradientStop gs = new GradientStop(RibbonStyleHandler.ButtonBackgroundSelected[RibbonStyleHandler.ButtonBackgroundSelected.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Offset);
                labelGscSelected.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                bottomBorder.RegisterName(backgroundStopPropertyTargetName, gs);

                labelGsSelectedNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush labelBackgroundSelectedBrush = new LinearGradientBrush(labelGscSelected, 90.0);

            //Attach brush
            #endregion
            #endregion
            #endregion
            #endregion

            #region create annimation sequence
            #region normal
            #region border
            #region main
            #region enter
            ColorAnimation mainBorderNormalEnterAnimation = new ColorAnimation();
            mainBorderNormalEnterAnimation.From = initialBorderColor;
            mainBorderNormalEnterAnimation.To = RibbonStyleHandler.ButtonBorderHover;
            mainBorderNormalEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            mainBorderNormalEnterAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation mainBorderNormalLeaveAnimation = new ColorAnimation();
            mainBorderNormalLeaveAnimation.From = RibbonStyleHandler.ButtonBorderHover;
            mainBorderNormalLeaveAnimation.To = initialBorderColor;
            mainBorderNormalLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            mainBorderNormalLeaveAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region label
            #region enter
            ColorAnimation labelBorderNormalEnterAnimation = new ColorAnimation();
            labelBorderNormalEnterAnimation.From = initialBorderColor;
            labelBorderNormalEnterAnimation.To = RibbonStyleHandler.ButtonBorderHover;
            labelBorderNormalEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            labelBorderNormalEnterAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation labelBorderNormalLeaveAnimation = new ColorAnimation();
            labelBorderNormalLeaveAnimation.From = RibbonStyleHandler.ButtonBorderHover;
            labelBorderNormalLeaveAnimation.To = initialBorderColor;
            labelBorderNormalLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            labelBorderNormalLeaveAnimation.AutoReverse = false;
            #endregion
            #endregion
            #endregion

            #region background
            #region main
            #region enter
            List<ColorAnimation> mainBackgroundNormalEnterAnimations = new List<ColorAnimation>();
            for (int i = 0; i < initialBackgroundBrush.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = initialBackgroundBrush[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                mainBackgroundNormalEnterAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> mainBackgroundNormalLeaveAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = initialBackgroundBrush[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                mainBackgroundNormalLeaveAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion

            #region label
            #region enter
            List<ColorAnimation> labelBackgroundNormalEnterAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = new GradientStop(initialBackgroundBrush[initialBackgroundBrush.Count - 1].Clone().Color, initialBackgroundBrush[i].Clone().Offset).Color;
                backgroundEnterAnimation.To = new GradientStop(RibbonStyleHandler.ButtonBackgroundHover[initialBackgroundBrush.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Offset).Color;
                    //RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                labelBackgroundNormalEnterAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> labelBackgroundNormalLeaveAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.To = new GradientStop(initialBackgroundBrush[initialBackgroundBrush.Count - 1].Clone().Color, initialBackgroundBrush[i].Clone().Offset).Color;
                backgroundLeaveAnimation.From = new GradientStop(RibbonStyleHandler.ButtonBackgroundHover[initialBackgroundBrush.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Offset).Color;
                //backgroundLeaveAnimation.From = new GradientStop(initialBackgroundBrush[initialBackgroundBrush.Count - 1].Clone().Color, initialBackgroundBrush[i].Clone().Offset).Color;
                //backgroundLeaveAnimation.To = new GradientStop(RibbonStyleHandler.ButtonBackgroundHover[initialBackgroundBrush.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Offset).Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                labelBackgroundNormalLeaveAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region main
            #region enter
            ColorAnimation mainBorderSelectedEnterAnimation = new ColorAnimation();
            mainBorderSelectedEnterAnimation.From = RibbonStyleHandler.ButtonBorderSelected;
            mainBorderSelectedEnterAnimation.To = RibbonStyleHandler.ButtonBorderSelectedHover;
            mainBorderSelectedEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            mainBorderSelectedEnterAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation mainBorderSelectedLeaveAnimation = new ColorAnimation();
            mainBorderSelectedLeaveAnimation.From = RibbonStyleHandler.ButtonBorderSelectedHover;
            mainBorderSelectedLeaveAnimation.To = RibbonStyleHandler.ButtonBorderSelected;
            mainBorderSelectedLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            mainBorderSelectedLeaveAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region label
            #region enter
            ColorAnimation labelBorderSelectedEnterAnimation = new ColorAnimation();
            labelBorderSelectedEnterAnimation.From = RibbonStyleHandler.ButtonBorderSelected;
            labelBorderSelectedEnterAnimation.To = RibbonStyleHandler.ButtonBorderSelectedHover;
            labelBorderSelectedEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            labelBorderSelectedEnterAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation labelBorderSelectedLeaveAnimation = new ColorAnimation();
            labelBorderSelectedLeaveAnimation.From = RibbonStyleHandler.ButtonBorderSelectedHover;
            labelBorderSelectedLeaveAnimation.To = RibbonStyleHandler.ButtonBorderSelected;
            labelBorderSelectedLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            labelBorderSelectedLeaveAnimation.AutoReverse = false;
            #endregion
            #endregion
            #endregion

            #region background
            #region main
            #region enter
            List<ColorAnimation> mainBackgroundSelectedEnterAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.ButtonBackgroundSelectedHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                mainBackgroundSelectedEnterAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> mainBackgroundSelectedLeaveAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.ButtonBackgroundSelectedHover[i].Clone().Color;
                backgroundLeaveAnimation.To = RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                mainBackgroundSelectedLeaveAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion

            #region label
            #region enter
            List<ColorAnimation> labelBackgroundSelectedEnterAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = new GradientStop(RibbonStyleHandler.ButtonBackgroundSelected[RibbonStyleHandler.ButtonBackgroundSelected.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Offset).Color;
                backgroundEnterAnimation.To = new GradientStop(RibbonStyleHandler.ButtonBackgroundSelectedHover[RibbonStyleHandler.ButtonBackgroundSelected.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundSelectedHover[i].Clone().Offset).Color;
                //RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                labelBackgroundSelectedEnterAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> labelBackgroundSelectedLeaveAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundSelected.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.To = new GradientStop(RibbonStyleHandler.ButtonBackgroundSelected[RibbonStyleHandler.ButtonBackgroundSelected.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundSelected[i].Clone().Offset).Color;
                backgroundLeaveAnimation.From = new GradientStop(RibbonStyleHandler.ButtonBackgroundSelectedHover[RibbonStyleHandler.ButtonBackgroundSelected.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundSelectedHover[i].Clone().Offset).Color;
                //backgroundLeaveAnimation.From = new GradientStop(initialBackgroundBrush[initialBackgroundBrush.Count - 1].Clone().Color, initialBackgroundBrush[i].Clone().Offset).Color;
                //backgroundLeaveAnimation.To = new GradientStop(RibbonStyleHandler.ButtonBackgroundHover[initialBackgroundBrush.Count - 1].Clone().Color, RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Offset).Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                labelBackgroundSelectedLeaveAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion

            #region create storyboard
            #region normal
            #region border
            #region main
            #region enter
            Storyboard.SetTargetName(mainBorderNormalEnterAnimation, mainBorderNormalBrushPropertyTargetName);
            Storyboard.SetTargetProperty(mainBorderNormalEnterAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard mainBorderNormalEnterStoryboard = new Storyboard();
            mainBorderNormalEnterStoryboard.Children.Add(mainBorderNormalEnterAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(mainBorderNormalLeaveAnimation, mainBorderNormalBrushPropertyTargetName);
            Storyboard.SetTargetProperty(mainBorderNormalLeaveAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard mainBorderNormalLeaveStoryboard = new Storyboard();
            mainBorderNormalLeaveStoryboard.Children.Add(mainBorderNormalLeaveAnimation);
            #endregion
            #endregion

            #region label
            #region enter
            Storyboard.SetTargetName(labelBorderNormalEnterAnimation, labelBorderNormalBrushPropertyTargetName);
            Storyboard.SetTargetProperty(labelBorderNormalEnterAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard labelBorderNormalEnterStoryboard = new Storyboard();
            labelBorderNormalEnterStoryboard.Children.Add(labelBorderNormalEnterAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(labelBorderNormalLeaveAnimation, labelBorderNormalBrushPropertyTargetName);
            Storyboard.SetTargetProperty(labelBorderNormalLeaveAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard labelBorderNormalLeaveStoryboard = new Storyboard();
            labelBorderNormalLeaveStoryboard.Children.Add(labelBorderNormalLeaveAnimation);
            #endregion
            #endregion
            #endregion

            #region background
            #region main
            #region enter
            Storyboard mainBackgroundNormalEnterStoryboard = new Storyboard();

            for (int i = 0; i < mainBackgroundNormalEnterAnimations.Count; i++)
            {
                Storyboard.SetTargetName(mainBackgroundNormalEnterAnimations[i], mainGsNormalNames[mainGscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(mainBackgroundNormalEnterAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                mainBackgroundNormalEnterStoryboard.Children.Add(mainBackgroundNormalEnterAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard mainBackgroundNormalLeaveStoryboard = new Storyboard();

            for (int i = 0; i < mainBackgroundNormalLeaveAnimations.Count; i++)
            {
                Storyboard.SetTargetName(mainBackgroundNormalLeaveAnimations[i], mainGsNormalNames[mainGscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(mainBackgroundNormalLeaveAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                mainBackgroundNormalLeaveStoryboard.Children.Add(mainBackgroundNormalLeaveAnimations[i]);
            }
            #endregion
            #endregion

            #region label
            #region enter
            Storyboard labelBackgroundNormalEnterStoryboard = new Storyboard();

            for (int i = 0; i < labelBackgroundNormalEnterAnimations.Count; i++)
            {
                Storyboard.SetTargetName(labelBackgroundNormalEnterAnimations[i], labelGsNormalNames[labelGscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(labelBackgroundNormalEnterAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                labelBackgroundNormalEnterStoryboard.Children.Add(labelBackgroundNormalEnterAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard labelBackgroundNormalLeaveStoryboard = new Storyboard();

            for (int i = 0; i < labelBackgroundNormalLeaveAnimations.Count; i++)
            {
                Storyboard.SetTargetName(labelBackgroundNormalLeaveAnimations[i], labelGsNormalNames[labelGscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(labelBackgroundNormalLeaveAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                labelBackgroundNormalLeaveStoryboard.Children.Add(labelBackgroundNormalLeaveAnimations[i]);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region main
            #region enter
            Storyboard.SetTargetName(mainBorderSelectedEnterAnimation, mainBorderSelectedBrushPropertyTargetName);
            Storyboard.SetTargetProperty(mainBorderSelectedEnterAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard mainBorderSelectedEnterStoryboard = new Storyboard();
            mainBorderSelectedEnterStoryboard.Children.Add(mainBorderSelectedEnterAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(mainBorderSelectedLeaveAnimation, mainBorderSelectedBrushPropertyTargetName);
            Storyboard.SetTargetProperty(mainBorderSelectedLeaveAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard mainBorderSelectedLeaveStoryboard = new Storyboard();
            mainBorderSelectedLeaveStoryboard.Children.Add(mainBorderSelectedLeaveAnimation);
            #endregion
            #endregion

            #region label
            #region enter
            Storyboard.SetTargetName(labelBorderSelectedEnterAnimation, labelBorderSelectedBrushPropertyTargetName);
            Storyboard.SetTargetProperty(labelBorderSelectedEnterAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard labelBorderSelectedEnterStoryboard = new Storyboard();
            labelBorderSelectedEnterStoryboard.Children.Add(labelBorderSelectedEnterAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(labelBorderSelectedLeaveAnimation, labelBorderSelectedBrushPropertyTargetName);
            Storyboard.SetTargetProperty(labelBorderSelectedLeaveAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard labelBorderSelectedLeaveStoryboard = new Storyboard();
            labelBorderSelectedLeaveStoryboard.Children.Add(labelBorderSelectedLeaveAnimation);
            #endregion
            #endregion
            #endregion

            #region background
            #region main
            #region enter
            Storyboard mainBackgroundSelectedEnterStoryboard = new Storyboard();

            for (int i = 0; i < mainBackgroundSelectedEnterAnimations.Count; i++)
            {
                Storyboard.SetTargetName(mainBackgroundSelectedEnterAnimations[i], mainGsSelectedNames[mainGscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(mainBackgroundSelectedEnterAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                mainBackgroundSelectedEnterStoryboard.Children.Add(mainBackgroundSelectedEnterAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard mainBackgroundSelectedLeaveStoryboard = new Storyboard();

            for (int i = 0; i < mainBackgroundSelectedLeaveAnimations.Count; i++)
            {
                Storyboard.SetTargetName(mainBackgroundSelectedLeaveAnimations[i], mainGsSelectedNames[mainGscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(mainBackgroundSelectedLeaveAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                mainBackgroundSelectedLeaveStoryboard.Children.Add(mainBackgroundSelectedLeaveAnimations[i]);
            }
            #endregion
            #endregion

            #region label
            #region enter
            Storyboard labelBackgroundSelectedEnterStoryboard = new Storyboard();

            for (int i = 0; i < labelBackgroundSelectedEnterAnimations.Count; i++)
            {
                Storyboard.SetTargetName(labelBackgroundSelectedEnterAnimations[i], labelGsSelectedNames[labelGscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(labelBackgroundSelectedEnterAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                labelBackgroundSelectedEnterStoryboard.Children.Add(labelBackgroundSelectedEnterAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard labelBackgroundSelectedLeaveStoryboard = new Storyboard();

            for (int i = 0; i < labelBackgroundSelectedLeaveAnimations.Count; i++)
            {
                Storyboard.SetTargetName(labelBackgroundSelectedLeaveAnimations[i], labelGsSelectedNames[labelGscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(labelBackgroundSelectedLeaveAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                labelBackgroundSelectedLeaveStoryboard.Children.Add(labelBackgroundSelectedLeaveAnimations[i]);
            }
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion

            #region store parameters
            List<Storyboard> enterNormalStoryboards = new List<Storyboard>();
            List<Storyboard> leaveNormalStoryboards = new List<Storyboard>();
            List<Storyboard> enterSelectedStoryboards = new List<Storyboard>();
            List<Storyboard> leaveSelectedStoryboards = new List<Storyboard>();

            enterNormalStoryboards.Add(mainBackgroundNormalEnterStoryboard);
            enterNormalStoryboards.Add(mainBorderNormalEnterStoryboard);
            leaveNormalStoryboards.Add(mainBorderNormalLeaveStoryboard);
            leaveNormalStoryboards.Add(mainBackgroundNormalLeaveStoryboard);
            enterNormalStoryboards.Add(labelBackgroundNormalEnterStoryboard);
            enterNormalStoryboards.Add(labelBorderNormalEnterStoryboard);
            leaveNormalStoryboards.Add(labelBorderNormalLeaveStoryboard);
            leaveNormalStoryboards.Add(labelBackgroundNormalLeaveStoryboard);

            enterSelectedStoryboards.Add(mainBackgroundSelectedEnterStoryboard);
            enterSelectedStoryboards.Add(mainBorderSelectedEnterStoryboard);
            leaveSelectedStoryboards.Add(mainBorderSelectedLeaveStoryboard);
            leaveSelectedStoryboards.Add(mainBackgroundSelectedLeaveStoryboard);
            enterSelectedStoryboards.Add(labelBackgroundSelectedEnterStoryboard);
            enterSelectedStoryboards.Add(labelBorderSelectedEnterStoryboard);
            leaveSelectedStoryboards.Add(labelBorderSelectedLeaveStoryboard);
            leaveSelectedStoryboards.Add(labelBackgroundSelectedLeaveStoryboard);

            RibbonStyleHandler.EnterStoryboardsNormal.Add((object)topControl, enterNormalStoryboards);
            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)topControl, leaveNormalStoryboards);
            RibbonStyleHandler.EnterStoryboardsSelected.Add((object)topControl, enterSelectedStoryboards);
            RibbonStyleHandler.LeaveStoryboardsSelected.Add((object)topControl, leaveSelectedStoryboards);
            RibbonStyleHandler.ContainingElements.Add((object)topControl, containingElement);

            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)topBorder, new List<Storyboard>());
            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)bottomBorder, new List<Storyboard>());
            RibbonStyleHandler.LeaveStoryboardsSelected.Add((object)topBorder, new List<Storyboard>());
            RibbonStyleHandler.LeaveStoryboardsSelected.Add((object)bottomBorder, new List<Storyboard>());
            RibbonStyleHandler.ContainingElements.Add((object)topBorder, containingElement);
            RibbonStyleHandler.ContainingElements.Add((object)bottomBorder, containingElement);

            RibbonStyleHandler.BorderBrushesNormal.Add(topBorder, mainBorderNormalBrush);
            RibbonStyleHandler.BorderBrushesNormal.Add(bottomBorder, labelBorderNormalBrush);
            RibbonStyleHandler.BackgroundBrushesNormal.Add(topBorder, mainBackgroundNormalBrush);
            RibbonStyleHandler.BackgroundBrushesNormal.Add(bottomBorder, labelBackgroundNormalBrush);
            RibbonStyleHandler.BorderBrushesSelected.Add(topBorder, mainBorderSelectedBrush);
            RibbonStyleHandler.BorderBrushesSelected.Add(bottomBorder, labelBorderSelectedBrush);
            RibbonStyleHandler.BackgroundBrushesSelected.Add(topBorder, mainBackgroundSelectedBrush);
            RibbonStyleHandler.BackgroundBrushesSelected.Add(bottomBorder, labelBackgroundSelectedBrush);

            List<RibbonBorder> borders = new List<RibbonBorder>();
            borders.Add(topBorder);
            borders.Add(bottomBorder);
            RibbonStyleHandler.MultiBorderLinks.Add(topControl, borders);
            #endregion

            #region handle events
            //mainElement.MouseEnter += new MouseEventHandler(element_MouseEnter);
            //mainElement.MouseLeave += new MouseEventHandler(element_MouseLeave);
            //labelElement.MouseEnter += new MouseEventHandler(element_MouseEnter);
            //labelElement.MouseLeave += new MouseEventHandler(element_MouseLeave);

            topControl.MouseEnter += new MouseEventHandler(element_MouseEnter);
            topControl.MouseLeave += new MouseEventHandler(element_MouseLeave);
            topBorder.MouseDown += new MouseButtonEventHandler(buttonElement_MouseDown);
            topBorder.MouseUp += new MouseButtonEventHandler(buttonElement_MouseUp);
            topBorder.MouseLeave += new MouseEventHandler(element_MouseLeave);
            bottomBorder.MouseDown += new MouseButtonEventHandler(buttonElement_MouseDown);
            bottomBorder.MouseUp += new MouseButtonEventHandler(buttonElement_MouseUp);
            bottomBorder.MouseLeave += new MouseEventHandler(element_MouseLeave);
            topBorder.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(element_EnabledChanged);
            topBorder.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(element_SelectedChanged);
            bottomBorder.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(element_EnabledChanged);
            bottomBorder.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(element_SelectedChanged);
            #endregion

            #region fix initial status
            if (topBorder.IsSelected)
            {
                element_SelectedChanged(topBorder, true);
            }
            if (!topBorder.IsEnabled)
            {
                element_EnabledChanged(topBorder, false);
            }
            if (bottomBorder.IsSelected)
            {
                element_SelectedChanged(bottomBorder, true);
            }
            if (!bottomBorder.IsEnabled)
            {
                element_EnabledChanged(bottomBorder, false);
            }
            #endregion
        }

        public static void styleTabBorder(RibbonBorder element, UIElement parent, FrameworkElement containingElement)
        {
            #region add to styled list
            StyleParameters sp = new StyleParameters();
            sp.type = StyleType.Tab;
            sp.element1 = element;
            sp.parent = parent;
            sp.parent2 = containingElement;
            RibbonStyleHandler.CurrentStyles.Add(sp);

            if (!RibbonStyleHandler.TabBorders.Contains(element))
            {
                RibbonStyleHandler.TabBorders.Add(element);
            }
            #endregion

            #region create and link brushs
            #region normal
            #region border
            SolidColorBrush borderNormalBrush = new SolidColorBrush(RibbonStyleHandler.TabBorderNormal);
            element.BorderBrush = borderNormalBrush;
            String borderBrushNormalPropertyTargetName = "borderNormalBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            element.RegisterName(borderBrushNormalPropertyTargetName, borderNormalBrush);
            #endregion

            #region background
            Dictionary<GradientStop, String> gsNormalNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection gscNormal = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.TabBackgroundNormal.Count; i++)
            {
                GradientStop gs = RibbonStyleHandler.TabBackgroundNormal[i].Clone();
                gscNormal.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                element.RegisterName(backgroundStopPropertyTargetName, gs);

                gsNormalNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush backgroundNormalBrush = new LinearGradientBrush(gscNormal, 90.0);

            //Attach brush
            element.Background = backgroundNormalBrush;
            #endregion
            #endregion

            #region selected
            #region border
            SolidColorBrush borderSelectedBrush = new SolidColorBrush(RibbonStyleHandler.TabBorderSelected);
            //element.BorderBrush = borderSelectedBrush; ###
            String borderBrushSelectedPropertyTargetName = "borderSelectedBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            element.RegisterName(borderBrushSelectedPropertyTargetName, borderSelectedBrush);
            #endregion

            #region background
            Dictionary<GradientStop, String> gsSelectedNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection gscSelected = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.TabBackgroundSelected.Count; i++)
            {
                GradientStop gs = RibbonStyleHandler.TabBackgroundSelected[i].Clone();
                gscSelected.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                element.RegisterName(backgroundStopPropertyTargetName, gs);

                gsSelectedNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush backgroundSelectedBrush = new LinearGradientBrush(gscSelected, 90.0);

            //Attach brush
            //element.Background = backgroundBrush; ###
            #endregion
            #endregion
            #endregion

            #region create annimation sequence
            #region normal
            #region border
            #region enter
            ColorAnimation borderEnterNormalAnimation = new ColorAnimation();
            borderEnterNormalAnimation.From = RibbonStyleHandler.TabBorderNormal;
            borderEnterNormalAnimation.To = RibbonStyleHandler.TabBorderHover;
            borderEnterNormalAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            borderEnterNormalAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation borderLeaveNormalAnimation = new ColorAnimation();
            borderLeaveNormalAnimation.From = RibbonStyleHandler.TabBorderHover;
            borderLeaveNormalAnimation.To = RibbonStyleHandler.TabBorderNormal;
            borderLeaveNormalAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            borderLeaveNormalAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region background
            #region enter
            List<ColorAnimation> backgroundEnterNormalAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.TabBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = RibbonStyleHandler.TabBackgroundNormal[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.TabBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                backgroundEnterNormalAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> backgroundLeaveNormalAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.TabBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = RibbonStyleHandler.TabBackgroundNormal[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                backgroundLeaveNormalAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region enter
            ColorAnimation borderEnterSelectedAnimation = new ColorAnimation();
            borderEnterSelectedAnimation.From = RibbonStyleHandler.TabBorderSelected;
            borderEnterSelectedAnimation.To = RibbonStyleHandler.TabBorderSelectedHover;
            borderEnterSelectedAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            borderEnterSelectedAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation borderLeaveSelectedAnimation = new ColorAnimation();
            borderLeaveSelectedAnimation.From = RibbonStyleHandler.TabBorderSelectedHover;
            borderLeaveSelectedAnimation.To = RibbonStyleHandler.TabBorderSelected;
            borderLeaveSelectedAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            borderLeaveSelectedAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region background
            #region enter
            List<ColorAnimation> backgroundEnterSelectedAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.TabBackgroundSelected.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = RibbonStyleHandler.TabBackgroundSelected[i].Clone().Color;// initialBackgroundBrush[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.TabBackgroundSelectedHover[i].Clone().Color;// RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                backgroundEnterSelectedAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> backgroundLeaveSelectedAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.TabBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.TabBackgroundSelectedHover[i].Clone().Color;//RibbonStyleHandler.ButtonBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = RibbonStyleHandler.TabBackgroundSelected[i].Clone().Color;// initialBackgroundBrush[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                backgroundLeaveSelectedAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region create storyboard
            #region normal
            #region border
            #region enter
            Storyboard.SetTargetName(borderEnterNormalAnimation, borderBrushNormalPropertyTargetName);
            Storyboard.SetTargetProperty(borderEnterNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderEnterNormalStoryboard = new Storyboard();
            borderEnterNormalStoryboard.Children.Add(borderEnterNormalAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(borderLeaveNormalAnimation, borderBrushNormalPropertyTargetName);
            Storyboard.SetTargetProperty(borderLeaveNormalAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderLeaveNormalStoryboard = new Storyboard();
            borderLeaveNormalStoryboard.Children.Add(borderLeaveNormalAnimation);
            #endregion
            #endregion

            #region background
            #region enter
            Storyboard backgroundEnterNormalStoryboard = new Storyboard();

            for (int i = 0; i < backgroundEnterNormalAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundEnterNormalAnimations[i], gsNormalNames[gscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundEnterNormalAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundEnterNormalStoryboard.Children.Add(backgroundEnterNormalAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard backgroundLeaveNormalStoryboard = new Storyboard();

            for (int i = 0; i < backgroundLeaveNormalAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundLeaveNormalAnimations[i], gsNormalNames[gscNormal[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundLeaveNormalAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundLeaveNormalStoryboard.Children.Add(backgroundLeaveNormalAnimations[i]);
            }
            #endregion
            #endregion
            #endregion

            #region selected
            #region border
            #region enter
            Storyboard.SetTargetName(borderEnterSelectedAnimation, borderBrushSelectedPropertyTargetName);
            Storyboard.SetTargetProperty(borderEnterSelectedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderEnterSelectedStoryboard = new Storyboard();
            borderEnterSelectedStoryboard.Children.Add(borderEnterSelectedAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(borderLeaveSelectedAnimation, borderBrushSelectedPropertyTargetName);
            Storyboard.SetTargetProperty(borderLeaveSelectedAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard borderLeaveSelectedStoryboard = new Storyboard();
            borderLeaveSelectedStoryboard.Children.Add(borderLeaveSelectedAnimation);
            #endregion
            #endregion

            #region background
            #region enter
            Storyboard backgroundEnterSelectedStoryboard = new Storyboard();

            for (int i = 0; i < backgroundEnterSelectedAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundEnterSelectedAnimations[i], gsSelectedNames[gscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundEnterSelectedAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundEnterSelectedStoryboard.Children.Add(backgroundEnterSelectedAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard backgroundLeaveSelectedStoryboard = new Storyboard();

            for (int i = 0; i < backgroundLeaveSelectedAnimations.Count; i++)
            {
                Storyboard.SetTargetName(backgroundLeaveSelectedAnimations[i], gsSelectedNames[gscSelected[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(backgroundLeaveSelectedAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                backgroundLeaveSelectedStoryboard.Children.Add(backgroundLeaveSelectedAnimations[i]);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region store parameters
            List<Storyboard> enterNormalStoryboards = new List<Storyboard>();
            List<Storyboard> leaveNormalStoryboards = new List<Storyboard>();
            List<Storyboard> enterSelectedStoryboards = new List<Storyboard>();
            List<Storyboard> leaveSelectedStoryboards = new List<Storyboard>();

            enterNormalStoryboards.Add(backgroundEnterNormalStoryboard);
            enterNormalStoryboards.Add(borderEnterNormalStoryboard);
            leaveNormalStoryboards.Add(borderLeaveNormalStoryboard);
            leaveNormalStoryboards.Add(backgroundLeaveNormalStoryboard);

            enterSelectedStoryboards.Add(backgroundEnterSelectedStoryboard);
            enterSelectedStoryboards.Add(borderEnterSelectedStoryboard);
            leaveSelectedStoryboards.Add(borderLeaveSelectedStoryboard);
            leaveSelectedStoryboards.Add(backgroundLeaveSelectedStoryboard);

            RibbonStyleHandler.EnterStoryboardsNormal.Add((object)element, enterNormalStoryboards);
            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)element, leaveNormalStoryboards);
            RibbonStyleHandler.ContainingElements.Add((object)element, containingElement);

            RibbonStyleHandler.EnterStoryboardsSelected.Add((object)element, enterSelectedStoryboards);
            RibbonStyleHandler.LeaveStoryboardsSelected.Add((object)element, leaveSelectedStoryboards);

            RibbonStyleHandler.BackgroundBrushesNormal.Add(element, backgroundNormalBrush);
            RibbonStyleHandler.BorderBrushesNormal.Add(element, borderNormalBrush);

            RibbonStyleHandler.BackgroundBrushesSelected.Add(element, backgroundSelectedBrush);
            RibbonStyleHandler.BorderBrushesSelected.Add(element, borderSelectedBrush);
            #endregion

            #region handle events
            element.MouseEnter += new MouseEventHandler(element_MouseEnter);
            element.MouseLeave += new MouseEventHandler(element_MouseLeave);
            element.MouseDown += new MouseButtonEventHandler(buttonElement_MouseDown);
            element.MouseUp += new MouseButtonEventHandler(buttonElement_MouseUp);
            element.EnabledChanged += new RibbonEventResources.EnableChangedDelegate(element_EnabledChanged);
            element.SelectedChanged += new RibbonEventResources.SelectedChangedDelegate(element_SelectedChanged);
            #endregion

            #region fix initial status
            if (element.IsSelected)
            {
                element_SelectedChanged(element, true);
            }
            if (!element.IsEnabled)
            {
                element_EnabledChanged(element, false);
            }
            #endregion
        }

        public static void styleGroupBorder(RibbonBorder mainElement, RibbonBorder labelElement, UIElement parent, FrameworkElement containingElement)
        {
            #region add to styled list
            StyleParameters sp = new StyleParameters();
            sp.type = StyleType.Group;
            sp.element1 = mainElement;
            sp.element2 = labelElement;
            sp.parent = parent;
            sp.parent2 = containingElement;
            RibbonStyleHandler.CurrentStyles.Add(sp);
            #endregion

            #region create and link brushs
            #region border
            #region main
            SolidColorBrush mainBorderBrush = new SolidColorBrush(RibbonStyleHandler.GroupBorderNormal);
            mainElement.BorderBrush = mainBorderBrush;
            String mainBorderBrushPropertyTargetName = "borderBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            mainElement.RegisterName(mainBorderBrushPropertyTargetName, mainBorderBrush);
            #endregion

            #region label
            SolidColorBrush labelBorderBrush = new SolidColorBrush(RibbonStyleHandler.GroupLabelBorderNormal);
            labelElement.BorderBrush = labelBorderBrush;
            String labelBorderBrushPropertyTargetName = "borderBrush" + RibbonStyleHandler.NameIndex;
            RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
            labelElement.RegisterName(labelBorderBrushPropertyTargetName, labelBorderBrush);
            #endregion
            #endregion

            #region background
            #region main
            Dictionary<GradientStop, String> mainGsNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection mainGsc = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.GroupBackgroundNormal.Count; i++)
            {
                GradientStop gs = RibbonStyleHandler.GroupBackgroundNormal[i].Clone();
                mainGsc.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                mainElement.RegisterName(backgroundStopPropertyTargetName, gs);

                mainGsNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush mainBackgroundBrush = new LinearGradientBrush(mainGsc, 90.0);

            //Attach brush
            mainElement.Background = mainBackgroundBrush;
            #endregion

            #region label
            Dictionary<GradientStop, String> labelGsNames = new Dictionary<GradientStop, String>();

            //Make brush
            GradientStopCollection labelGsc = new GradientStopCollection();
            for (int i = 0; i < RibbonStyleHandler.GroupLabelBackgroundNormal.Count; i++)
            {
                GradientStop gs = RibbonStyleHandler.GroupLabelBackgroundNormal[i].Clone();
                labelGsc.Add(gs);

                //Link
                String backgroundStopPropertyTargetName = "gradientStop" + RibbonStyleHandler.NameIndex;
                RibbonStyleHandler.NameIndex = RibbonStyleHandler.NameIndex + 1;
                labelElement.RegisterName(backgroundStopPropertyTargetName, gs);

                labelGsNames.Add(gs, backgroundStopPropertyTargetName);
            }
            LinearGradientBrush labelBackgroundBrush = new LinearGradientBrush(labelGsc, 90.0);

            //Attach brush
            labelElement.Background = labelBackgroundBrush;
            #endregion
            #endregion
            #endregion

            #region create annimation sequence
            #region border
            #region main
            #region enter
            ColorAnimation mainBorderEnterAnimation = new ColorAnimation();
            mainBorderEnterAnimation.From = RibbonStyleHandler.GroupBorderNormal;
            mainBorderEnterAnimation.To = RibbonStyleHandler.GroupBorderHover;
            mainBorderEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            mainBorderEnterAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation mainBorderLeaveAnimation = new ColorAnimation();
            mainBorderLeaveAnimation.From = RibbonStyleHandler.GroupBorderHover;
            mainBorderLeaveAnimation.To = RibbonStyleHandler.GroupBorderNormal;
            mainBorderLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            mainBorderLeaveAnimation.AutoReverse = false;
            #endregion
            #endregion

            #region label
            #region enter
            ColorAnimation labelBorderEnterAnimation = new ColorAnimation();
            labelBorderEnterAnimation.From = RibbonStyleHandler.GroupLabelBorderNormal;
            labelBorderEnterAnimation.To = RibbonStyleHandler.GroupLabelBorderHover;
            labelBorderEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
            labelBorderEnterAnimation.AutoReverse = false;
            #endregion

            #region leave
            ColorAnimation labelBorderLeaveAnimation = new ColorAnimation();
            labelBorderLeaveAnimation.From = RibbonStyleHandler.GroupLabelBorderHover;
            labelBorderLeaveAnimation.To = RibbonStyleHandler.GroupLabelBorderNormal;
            labelBorderLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
            labelBorderLeaveAnimation.AutoReverse = false;
            #endregion
            #endregion
            #endregion

            #region background
            #region main
            #region enter
            List<ColorAnimation> mainBackgroundEnterAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.GroupBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = RibbonStyleHandler.GroupBackgroundNormal[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.GroupBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                mainBackgroundEnterAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> mainBackgroundLeaveAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.GroupBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.GroupBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = RibbonStyleHandler.GroupBackgroundNormal[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                mainBackgroundLeaveAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion

            #region label
            #region enter
            List<ColorAnimation> labelBackgroundEnterAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.GroupLabelBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundEnterAnimation = new ColorAnimation();
                backgroundEnterAnimation.From = RibbonStyleHandler.GroupLabelBackgroundNormal[i].Clone().Color;
                backgroundEnterAnimation.To = RibbonStyleHandler.GroupLabelBackgroundHover[i].Clone().Color;
                backgroundEnterAnimation.Duration = new Duration(RibbonStyleHandler.EnterTime);
                backgroundEnterAnimation.AutoReverse = false;

                labelBackgroundEnterAnimations.Add(backgroundEnterAnimation);
            }
            #endregion

            #region leave
            List<ColorAnimation> labelBackgroundLeaveAnimations = new List<ColorAnimation>();
            for (int i = 0; i < RibbonStyleHandler.GroupLabelBackgroundNormal.Count; i++)
            {
                ColorAnimation backgroundLeaveAnimation = new ColorAnimation();
                backgroundLeaveAnimation.From = RibbonStyleHandler.GroupLabelBackgroundHover[i].Clone().Color;
                backgroundLeaveAnimation.To = RibbonStyleHandler.GroupLabelBackgroundNormal[i].Clone().Color;
                backgroundLeaveAnimation.Duration = new Duration(RibbonStyleHandler.LeaveTime);
                backgroundLeaveAnimation.AutoReverse = false;

                labelBackgroundLeaveAnimations.Add(backgroundLeaveAnimation);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region create storyboard
            #region border
            #region main
            #region enter
            Storyboard.SetTargetName(mainBorderEnterAnimation, mainBorderBrushPropertyTargetName);
            Storyboard.SetTargetProperty(mainBorderEnterAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard mainBorderEnterStoryboard = new Storyboard();
            mainBorderEnterStoryboard.Children.Add(mainBorderEnterAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(mainBorderLeaveAnimation, mainBorderBrushPropertyTargetName);
            Storyboard.SetTargetProperty(mainBorderLeaveAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard mainBorderLeaveStoryboard = new Storyboard();
            mainBorderLeaveStoryboard.Children.Add(mainBorderLeaveAnimation);
            #endregion
            #endregion

            #region label
            #region enter
            Storyboard.SetTargetName(labelBorderEnterAnimation, labelBorderBrushPropertyTargetName);
            Storyboard.SetTargetProperty(labelBorderEnterAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard labelBorderEnterStoryboard = new Storyboard();
            labelBorderEnterStoryboard.Children.Add(labelBorderEnterAnimation);
            #endregion

            #region leave
            Storyboard.SetTargetName(labelBorderLeaveAnimation, labelBorderBrushPropertyTargetName);
            Storyboard.SetTargetProperty(labelBorderLeaveAnimation, new PropertyPath(SolidColorBrush.ColorProperty));

            Storyboard labelBorderLeaveStoryboard = new Storyboard();
            labelBorderLeaveStoryboard.Children.Add(labelBorderLeaveAnimation);
            #endregion
            #endregion
            #endregion

            #region background
            #region main
            #region enter
            Storyboard mainBackgroundEnterStoryboard = new Storyboard();

            for (int i = 0; i < mainBackgroundEnterAnimations.Count; i++)
            {
                Storyboard.SetTargetName(mainBackgroundEnterAnimations[i], mainGsNames[mainGsc[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(mainBackgroundEnterAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                mainBackgroundEnterStoryboard.Children.Add(mainBackgroundEnterAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard mainBackgroundLeaveStoryboard = new Storyboard();

            for (int i = 0; i < mainBackgroundLeaveAnimations.Count; i++)
            {
                Storyboard.SetTargetName(mainBackgroundLeaveAnimations[i], mainGsNames[mainGsc[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(mainBackgroundLeaveAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                mainBackgroundLeaveStoryboard.Children.Add(mainBackgroundLeaveAnimations[i]);
            }
            #endregion
            #endregion

            #region label
            #region enter
            Storyboard labelBackgroundEnterStoryboard = new Storyboard();

            for (int i = 0; i < labelBackgroundEnterAnimations.Count; i++)
            {
                Storyboard.SetTargetName(labelBackgroundEnterAnimations[i], labelGsNames[labelGsc[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(labelBackgroundEnterAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                labelBackgroundEnterStoryboard.Children.Add(labelBackgroundEnterAnimations[i]);
            }
            #endregion

            #region leave
            Storyboard labelBackgroundLeaveStoryboard = new Storyboard();

            for (int i = 0; i < labelBackgroundLeaveAnimations.Count; i++)
            {
                Storyboard.SetTargetName(labelBackgroundLeaveAnimations[i], labelGsNames[labelGsc[i]]); //gradient stop thing
                Storyboard.SetTargetProperty(labelBackgroundLeaveAnimations[i], new PropertyPath(GradientStop.ColorProperty));

                labelBackgroundLeaveStoryboard.Children.Add(labelBackgroundLeaveAnimations[i]);
            }
            #endregion
            #endregion
            #endregion
            #endregion

            #region store parameters
            List<Storyboard> enterStoryboards = new List<Storyboard>();
            List<Storyboard> leaveStoryboards = new List<Storyboard>();

            enterStoryboards.Add(mainBackgroundEnterStoryboard);
            enterStoryboards.Add(mainBorderEnterStoryboard);
            leaveStoryboards.Add(mainBorderLeaveStoryboard);
            leaveStoryboards.Add(mainBackgroundLeaveStoryboard);
            enterStoryboards.Add(labelBackgroundEnterStoryboard);
            enterStoryboards.Add(labelBorderEnterStoryboard);
            leaveStoryboards.Add(labelBorderLeaveStoryboard);
            leaveStoryboards.Add(labelBackgroundLeaveStoryboard);

            RibbonStyleHandler.EnterStoryboardsNormal.Add((object)mainElement, enterStoryboards);
            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)mainElement, leaveStoryboards);
            RibbonStyleHandler.ContainingElements.Add((object)mainElement, containingElement);
            RibbonStyleHandler.EnterStoryboardsNormal.Add((object)labelElement, enterStoryboards);
            RibbonStyleHandler.LeaveStoryboardsNormal.Add((object)labelElement, leaveStoryboards);
            RibbonStyleHandler.ContainingElements.Add((object)labelElement, containingElement);
            #endregion

            #region handle events
            mainElement.MouseEnter += new MouseEventHandler(element_MouseEnter);
            mainElement.MouseLeave += new MouseEventHandler(element_MouseLeave);
            labelElement.MouseEnter += new MouseEventHandler(element_MouseEnter);
            labelElement.MouseLeave += new MouseEventHandler(element_MouseLeave);
            #endregion
        }

        public static void styleButtonText(RibbonBorder border, Label label)
        {
            if (RibbonStyleHandler.StyledButtonLabels.ContainsKey(border) != true)
            {
                RibbonStyleHandler.StyledButtonLabels.Add(border, new List<Label>());
            }

            List<Label> lList = RibbonStyleHandler.StyledButtonLabels[border];
            lList.Add(label);

            if (!border.IsEnabled)
            {
                label.Foreground = RibbonStyleHandler.ButtonDisabledText;
            }
            else if (border.IsSelected)
            {
                label.Foreground = RibbonStyleHandler.ButtonSelectedText;
            }
            else
            {
                label.Foreground = RibbonStyleHandler.ButtonNormalText;
            }
        }

        public static void styleTabText(RibbonBorder border, Label label)
        {
            if (RibbonStyleHandler.StyledTabLabels.ContainsKey(border) != true)
            {
                RibbonStyleHandler.StyledTabLabels.Add(border, new List<Label>());
            }

            List<Label> lList = RibbonStyleHandler.StyledTabLabels[border];
            lList.Add(label);

            if (!border.IsEnabled)
            {
                label.Foreground = RibbonStyleHandler.TabDisabledText;
            }
            else if (border.IsSelected)
            {
                label.Foreground = RibbonStyleHandler.TabSelectedText;
            }
            else
            {
                label.Foreground = RibbonStyleHandler.TabNormalText;
            }
        }

        public static void styleRibbonBarBackground(Panel element)
        {
            element.Background = RibbonStyleHandler.RibbonBarBackground;
            RibbonStyleHandler.BarBackgrounds.Add(element);
        }

        public static void styleRibbonTitleBackground(Panel element)
        {
            element.Background = RibbonStyleHandler.RibbonTitleBackground;
            RibbonStyleHandler.TitleBackgrounds.Add(element);
        }

        public static void unstyle(object o)
        {
            StyleParameters s = null;
            for (int i = 0; i < RibbonStyleHandler.CurrentStyles.Count; i++)
            {
                StyleParameters test = RibbonStyleHandler.CurrentStyles[i];
                if (test.element1 == o || test.element2 == o || test.topControl == o 
                    || test.parent == o || test.parent2 == o)
                {
                    s = test;
                }
            }

            if (s == null)
            {
                throw new Exception("Styled object not found");
            }

            removeOldStyle(s);
        }
        #endregion

        #region enabled / selected
        private static void element_EnabledChanged(object sender, bool newValue)
        {
            RibbonBorder b = (RibbonBorder)sender;

            if (newValue == true)
            {
                if (b.IsSelected == true)
                {
                    b.Background = RibbonStyleHandler.BackgroundBrushesSelected[b];
                    b.BorderBrush = RibbonStyleHandler.BorderBrushesSelected[b];

                    #region style button text
                    if (StyledButtonLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledButtonLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.ButtonSelectedText;
                        }
                    }

                    if (StyledTabLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledTabLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.TabSelectedText;
                        }
                    }
                    #endregion
                }
                else
                {
                    b.Background = RibbonStyleHandler.BackgroundBrushesNormal[b];
                    b.BorderBrush = RibbonStyleHandler.BorderBrushesNormal[b];

                    #region style button text
                    if (StyledButtonLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledButtonLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.ButtonNormalText;
                        }
                    }

                    if (StyledTabLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledTabLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.TabNormalText;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                b.Background = RibbonStyleHandler.toLGradientBrush(RibbonStyleHandler.ButtonBackgroundDisabled, 90.0);
                b.BorderBrush = new SolidColorBrush(RibbonStyleHandler.ButtonBorderDisabled);

                #region style button text
                if (StyledButtonLabels.ContainsKey(b))
                {
                    foreach (Label l in RibbonStyleHandler.StyledButtonLabels[b])
                    {
                        l.Foreground = RibbonStyleHandler.ButtonDisabledText;
                    }
                }

                if (StyledTabLabels.ContainsKey(b))
                {
                    foreach (Label l in RibbonStyleHandler.StyledTabLabels[b])
                    {
                        l.Foreground = RibbonStyleHandler.TabDisabledText;
                    }
                }
                #endregion
            }
        }

        private static void element_SelectedChanged(object sender, bool newValue)
        {
            RibbonBorder b = (RibbonBorder)sender;

            if (b.IsEnabled)
            {
                if (newValue == true)
                {
                    b.Background = RibbonStyleHandler.BackgroundBrushesSelected[b];
                    b.BorderBrush = RibbonStyleHandler.BorderBrushesSelected[b];

                    #region style button text
                    if (StyledButtonLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledButtonLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.ButtonSelectedText;
                        }
                    }

                    if (StyledTabLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledTabLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.TabSelectedText;
                        }
                    }
                    #endregion
                }
                else
                {
                    b.Background = RibbonStyleHandler.BackgroundBrushesNormal[b];
                    b.BorderBrush = RibbonStyleHandler.BorderBrushesNormal[b];

                    #region style button text
                    if (StyledButtonLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledButtonLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.ButtonNormalText;
                        }
                    }

                    if (StyledTabLabels.ContainsKey(b))
                    {
                        foreach (Label l in RibbonStyleHandler.StyledTabLabels[b])
                        {
                            l.Foreground = RibbonStyleHandler.TabNormalText;
                        }
                    }
                    #endregion

                    if (!b.IsMouseDirectlyOver)
                    {
                        element_MouseLeave(sender, null);
                    }
                }
            }
        }
        #endregion

        #region reapply styling
        public static void reStyle(Style style)
        {
            #region apply colours etc.
            if (style == Style.Blue || style == Style.Default)
            {
                RibbonStyleHandler.setBlueStyle();
            }
            else if (style == Style.Gray)
            {
                RibbonStyleHandler.setGrayStyle();
            }
            else if (style == Style.Black)
            {
                RibbonStyleHandler.setBlackStyle();
            }
            else if (style == Style.Green)
            {
                RibbonStyleHandler.setGreenStyle();
            }
            #endregion
            
            RibbonStyleHandler.reStyleBase();

            if (RibbonStyleHandler.StyleChanged != null)
            {
                RibbonStyleHandler.StyleChanged(new StyleChangedEventArgs(style));
            }
        }

        public static void reStyle()
        {
            RibbonStyleHandler.reStyleBase();

            if (RibbonStyleHandler.StyleChanged != null)
            {
                RibbonStyleHandler.StyleChanged(new StyleChangedEventArgs(Style.Not_Set));
            }
        }

        public static void reStyleBase()
        {
            #region style buttons / groups
            List<StyleParameters> current = RibbonStyleHandler.CurrentStyles;
            RibbonStyleHandler.CurrentStyles = new List<StyleParameters>();

            for (int i = 0; i < current.Count; i++)
            {
                try
                {
                    RibbonStyleHandler.removeOldStyle(current[i]);

                    #region group styling
                    if (current[i].type == StyleType.Group)
                    {
                        RibbonStyleHandler.styleGroupBorder(current[i].element1, current[i].element2,
                            current[i].parent, current[i].parent2);
                    }
                    #endregion
                    #region button styling
                    else if (current[i].type == StyleType.Button)
                    {
                        //Console.WriteLine(current[i].parent.GetType() + ", " + current[i].useInitialBackgroundBrush + ", " + current[i].useInitialBorderColour);

                        if (current[i].element2 != null)
                        {
                            //Console.WriteLine("0");
                            RibbonStyleHandler.styleButtonBorder(current[i].topControl, current[i].element1, current[i].element2, current[i].parent,
                                current[i].parent2, current[i].initialBorderColour, current[i].initialBackgroundBrush);
                        }
                        else if (current[i].useInitialBackgroundBrush && current[i].useInitialBorderColour)
                        {
                            //Console.WriteLine("1");
                            RibbonStyleHandler.styleButtonBorder(current[i].element1, current[i].parent,
                                current[i].parent2);
                        }
                        else if (current[i].useInitialBackgroundBrush && !current[i].useInitialBorderColour)
                        {
                            //Console.WriteLine("2");
                            RibbonStyleHandler.styleButtonBorder(current[i].element1, current[i].parent,
                                current[i].parent2, current[i].initialBorderColour);
                        }
                        else if (!current[i].useInitialBackgroundBrush && current[i].useInitialBorderColour)
                        {
                            //Console.WriteLine("3");
                            RibbonStyleHandler.styleButtonBorder(current[i].element1, current[i].parent,
                                current[i].parent2, current[i].initialBackgroundBrush);
                        }
                        else if (!current[i].useInitialBackgroundBrush && !current[i].useInitialBorderColour)
                        {
                            //Console.WriteLine("4");
                            RibbonStyleHandler.styleButtonBorder(current[i].element1, current[i].parent,
                                current[i].parent2, current[i].initialBorderColour, current[i].initialBackgroundBrush);
                        }
                    }
                    #endregion
                    #region tab styling
                    else if (current[i].type == StyleType.Tab)
                    {
                        RibbonStyleHandler.styleTabBorder(current[i].element1, current[i].parent, current[i].parent2);
                    }
                    #endregion
                }
                catch (Exception err)
                {
                    Console.WriteLine("Error during restyle : " + err.ToString());
                }
            }
            #endregion

            #region style backgrounds
            List<Panel> barBackgrounds = RibbonStyleHandler.BarBackgrounds;
            List<Panel> titleBackgrounds = RibbonStyleHandler.TitleBackgrounds;
            RibbonStyleHandler.BarBackgrounds = new List<Panel>();
            RibbonStyleHandler.TitleBackgrounds = new List<Panel>();

            for (int i = 0; i < barBackgrounds.Count; i++)
            {
                RibbonStyleHandler.styleRibbonBarBackground(barBackgrounds[i]);
            }

            for (int i = 0; i < titleBackgrounds.Count; i++)
            {
                RibbonStyleHandler.styleRibbonTitleBackground(titleBackgrounds[i]);
            }
            #endregion
        }

        private static void removeOldStyle(StyleParameters s)
        {
            RibbonStyleHandler.EnterStoryboardsNormal.Remove((object)s.element1);
            RibbonStyleHandler.LeaveStoryboardsNormal.Remove((object)s.element1);
            RibbonStyleHandler.EnterStoryboardsSelected.Remove((object)s.element1);
            RibbonStyleHandler.LeaveStoryboardsSelected.Remove((object)s.element1);
            RibbonStyleHandler.ContainingElements.Remove((object)s.element1);
            RibbonStyleHandler.BackgroundBrushesNormal.Remove(s.element1);
            RibbonStyleHandler.BorderBrushesNormal.Remove(s.element1);
            RibbonStyleHandler.BackgroundBrushesSelected.Remove(s.element1);
            RibbonStyleHandler.BorderBrushesSelected.Remove(s.element1);

            s.element1.MouseDown -= new MouseButtonEventHandler(buttonElement_MouseDown);
            s.element1.MouseUp -= new MouseButtonEventHandler(buttonElement_MouseUp);
            s.element1.MouseLeave -= new MouseEventHandler(element_MouseLeave);

            if (s.element2 != null)
            {
                RibbonStyleHandler.EnterStoryboardsNormal.Remove((object)s.element2);
                RibbonStyleHandler.LeaveStoryboardsNormal.Remove((object)s.element2);
                RibbonStyleHandler.EnterStoryboardsSelected.Remove((object)s.element2);
                RibbonStyleHandler.LeaveStoryboardsSelected.Remove((object)s.element2);
                RibbonStyleHandler.ContainingElements.Remove((object)s.element2);
                RibbonStyleHandler.BackgroundBrushesNormal.Remove(s.element2);
                RibbonStyleHandler.BorderBrushesNormal.Remove(s.element2);
                RibbonStyleHandler.BackgroundBrushesSelected.Remove(s.element2);
                RibbonStyleHandler.BorderBrushesSelected.Remove(s.element2);

                s.element2.MouseDown -= new MouseButtonEventHandler(buttonElement_MouseDown);
                s.element2.MouseUp -= new MouseButtonEventHandler(buttonElement_MouseUp);
                s.element2.MouseLeave -= new MouseEventHandler(element_MouseLeave);
            }

            if (s.topControl != null)
            {
                RibbonStyleHandler.EnterStoryboardsNormal.Remove((object)s.topControl);
                RibbonStyleHandler.LeaveStoryboardsNormal.Remove((object)s.topControl);
                RibbonStyleHandler.EnterStoryboardsSelected.Remove((object)s.topControl);
                RibbonStyleHandler.LeaveStoryboardsSelected.Remove((object)s.topControl);
                RibbonStyleHandler.ContainingElements.Remove((object)s.topControl);
                RibbonStyleHandler.MultiBorderLinks.Remove((UIElement)s.topControl);

                s.topControl.MouseDown -= new MouseButtonEventHandler(buttonElement_MouseDown);
                s.topControl.MouseUp -= new MouseButtonEventHandler(buttonElement_MouseUp);
                s.topControl.MouseLeave -= new MouseEventHandler(element_MouseLeave);
            }
        }

        #region set styles
        private static void setBlueStyle()
        {
            #region button brushes
            RibbonStyleHandler.ButtonBorderNormal = Color.FromArgb(255, 117, 150, 191);
            RibbonStyleHandler.ButtonBorderHover = Color.FromArgb(255, 192, 167, 118);
            RibbonStyleHandler.ButtonBorderPressed = Color.FromArgb(255, 128, 255, 128);
            RibbonStyleHandler.ButtonBorderDisabled = Color.FromArgb(0, 220, 220, 220);
            RibbonStyleHandler.ButtonBorderSelected = Color.FromArgb(255, 255, 141, 5); //255, 228, 127);
            RibbonStyleHandler.ButtonBorderSelectedHover = Color.FromArgb(255, 255, 179, 27);

            RibbonStyleHandler.ButtonBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundHover = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundPressed = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundDisabled = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundSelected = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundSelectedHover = new List<GradientStop>();

            RibbonStyleHandler.ButtonNormalText = new SolidColorBrush(Color.FromArgb(255, 21, 66, 139));
            RibbonStyleHandler.ButtonSelectedText = new SolidColorBrush(Color.FromArgb(255, 21, 66, 139));
            RibbonStyleHandler.ButtonDisabledText = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));

            #region button backgrounds
            #region create button normal background gradient stops
            List<GradientStop> backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 222, 235, 254), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 203, 222, 246), 0.647));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 193, 213, 241), 0.6471));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 207, 224, 247), 1));
            RibbonStyleHandler.ButtonBackgroundNormal = backgroundNormal;
            #endregion

            #region create button hover background gradient stops
            List<GradientStop> backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 253, 219), 0));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 231, 144), 0.647));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 211, 72), 0.6471));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 211, 72), 1));
            RibbonStyleHandler.ButtonBackgroundHover = backgroundHover;
            #endregion

            #region create button pressed background gradient stops
            List<GradientStop> backgroundPressed = new List<GradientStop>();
            backgroundPressed.Add(new GradientStop(Color.FromRgb(219, 180, 149), 0));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(237, 134, 65), 0.647));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(233, 102, 14), 0.6471));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(249, 150, 70), 1));
            RibbonStyleHandler.ButtonBackgroundPressed = backgroundPressed;
            #endregion

            #region create button disabled background gradient stops
            List<GradientStop> backgroundDisabled = new List<GradientStop>();
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 0));
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 0.647));
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 0.6471));
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 1));
            RibbonStyleHandler.ButtonBackgroundDisabled = backgroundDisabled;
            #endregion

            #region create button selected background gradient stops
            List<GradientStop> backgroundSelected = new List<GradientStop>();
            backgroundSelected.Add(new GradientStop(Color.FromRgb(252, 177, 109), 0));
            backgroundSelected.Add(new GradientStop(Color.FromRgb(255, 155, 35), 0.647));
            backgroundSelected.Add(new GradientStop(Color.FromRgb(255, 141, 5), 0.6471));
            backgroundSelected.Add(new GradientStop(Color.FromRgb(255, 169, 80), 1));
            RibbonStyleHandler.ButtonBackgroundSelected = backgroundSelected;
            #endregion

            #region create button selected hover background gradient stops
            List<GradientStop> backgroundSelectedHover = new List<GradientStop>();
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(251, 219, 181), 0));
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(254, 199, 120), 0.647));
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(254, 180, 86), 0.6471));
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(253, 235, 159), 1));
            RibbonStyleHandler.ButtonBackgroundSelectedHover = backgroundSelectedHover;
            #endregion
            #endregion
            #endregion

            #region tab brushes
            RibbonStyleHandler.TabBorderNormal = Color.FromArgb(0, 117, 150, 191);
            RibbonStyleHandler.TabBorderHover = Color.FromArgb(0, 192, 167, 118);
            RibbonStyleHandler.TabBorderPressed = Color.FromArgb(0, 128, 255, 128);
            RibbonStyleHandler.TabBorderDisabled = Color.FromArgb(0, 220, 220, 220);
            RibbonStyleHandler.TabBorderSelected = Color.FromArgb(255, 117, 150, 191); //255, 228, 127);
            RibbonStyleHandler.TabBorderSelectedHover = Color.FromArgb(255, 192, 167, 118);

            RibbonStyleHandler.TabBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.TabBackgroundHover = new List<GradientStop>();
            RibbonStyleHandler.TabBackgroundPressed = new List<GradientStop>();
            RibbonStyleHandler.TabBackgroundDisabled = new List<GradientStop>();
            RibbonStyleHandler.TabBackgroundSelected = new List<GradientStop>();
            RibbonStyleHandler.TabBackgroundSelectedHover = new List<GradientStop>();

            RibbonStyleHandler.TabNormalText = new SolidColorBrush(Color.FromArgb(220, 21, 66, 139));
            RibbonStyleHandler.TabSelectedText = new SolidColorBrush(Color.FromArgb(220, 21, 66, 139));
            RibbonStyleHandler.TabDisabledText = new SolidColorBrush(Color.FromArgb(220, 21, 66, 139));

            #region tab backgrounds
            #region create tab normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 222, 235, 254), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 203, 222, 246), 0.647));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 193, 213, 241), 0.6471));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 207, 224, 247), 1));
            RibbonStyleHandler.TabBackgroundNormal = backgroundNormal;
            #endregion

            #region create tab hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 253, 219), 0));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 231, 144), 0.647));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 211, 72), 0.6471));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 211, 72), 1));
            RibbonStyleHandler.TabBackgroundHover = backgroundHover;
            #endregion

            #region create tab pressed background gradient stops
            backgroundPressed = new List<GradientStop>();
            backgroundPressed.Add(new GradientStop(Color.FromRgb(255, 253, 219), 0));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(255, 231, 144), 0.647));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(255, 211, 72), 0.6471));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(255, 211, 72), 1));
            RibbonStyleHandler.TabBackgroundPressed = backgroundPressed;
            #endregion

            #region create tab disabled background gradient stops
            backgroundDisabled = new List<GradientStop>();
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 0));
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 0.647));
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 0.6471));
            backgroundDisabled.Add(new GradientStop(Color.FromArgb(0, 240, 240, 240), 1));
            RibbonStyleHandler.TabBackgroundDisabled = backgroundDisabled;
            #endregion

            #region create tab selected background gradient stops
            backgroundSelected = new List<GradientStop>();
            backgroundSelected.Add(new GradientStop(Color.FromRgb(232, 236, 242), 0));
            backgroundSelected.Add(new GradientStop(Color.FromRgb(207, 222, 240), 0.647));
            backgroundSelected.Add(new GradientStop(Color.FromRgb(207, 222, 240), 0.6471));
            backgroundSelected.Add(new GradientStop(Color.FromRgb(232, 236, 242), 1));
            RibbonStyleHandler.TabBackgroundSelected = backgroundSelected;
            #endregion

            #region create tab selected hover background gradient stops
            backgroundSelectedHover = new List<GradientStop>();
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(232, 236, 242), 0));
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(207, 222, 240), 0.647));
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(207, 222, 240), 0.6471));
            backgroundSelectedHover.Add(new GradientStop(Color.FromRgb(232, 236, 242), 1));
            RibbonStyleHandler.TabBackgroundSelectedHover = backgroundSelectedHover;
            #endregion
            #endregion
            #endregion

            #region group brushes
            RibbonStyleHandler.GroupBorderNormal = Color.FromArgb(255, 117, 150, 191); //158, 192, 219);//
            RibbonStyleHandler.GroupBorderHover = Color.FromArgb(255, 126, 173, 211);//

            RibbonStyleHandler.GroupBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupLabelBorderNormal = Color.FromArgb(255, 117, 150, 191); //161, 189, 213);//
            RibbonStyleHandler.GroupLabelBorderHover = Color.FromArgb(255, 126, 173, 211);//

            RibbonStyleHandler.GroupLabelBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupLabelBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupText = new SolidColorBrush(Color.FromArgb(220, 21, 66, 139));

            #region group backgrounds
            #region create group normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 228, 239, 253), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 220, 232, 248), 1));
            RibbonStyleHandler.GroupBackgroundNormal = backgroundNormal;
            #endregion

            #region create group hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 228, 239, 253), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 220, 232, 248), 1));
            RibbonStyleHandler.GroupBackgroundHover = backgroundHover;
            #endregion

            #region create group label normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 194, 216, 241), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 192, 216, 239), 1));
            RibbonStyleHandler.GroupLabelBackgroundNormal = backgroundNormal;
            #endregion

            #region create group label hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 200, 224, 255), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 214, 237, 255), 1));
            RibbonStyleHandler.GroupLabelBackgroundHover = backgroundHover;
            #endregion
            #endregion
            #endregion

            #region background brushes
            RibbonStyleHandler.RibbonBarBackground = new SolidColorBrush();
            RibbonStyleHandler.RibbonTitleBackground = new SolidColorBrush(Color.FromArgb(255, 191, 219, 255));
            RibbonStyleHandler.PreviewBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            #endregion

            #region timing
            RibbonStyleHandler.EnterTime = TimeSpan.FromMilliseconds(300);
            RibbonStyleHandler.LeaveTime = TimeSpan.FromMilliseconds(900);
            #endregion

            #region ribbon bar background
            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(255, 232, 236, 242), 0)); //219, 230, 244), 0));
            gsc.Add(new GradientStop(Color.FromArgb(255, 207, 222, 240), 0.333));
            gsc.Add(new GradientStop(Color.FromArgb(255, 207, 222, 240), 0.666));
            gsc.Add(new GradientStop(Color.FromArgb(255, 232, 236, 242), 1));//231, 242, 255), 1));
            RibbonStyleHandler.RibbonBarBackground = new LinearGradientBrush(gsc, 90.0);
            #endregion

            #region quick access ToolBar bushes
            gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(100, 255, 255, 255), 0)); //219, 230, 244), 0));
            gsc.Add(new GradientStop(Color.FromArgb(80, 255, 255, 255), 0.1));
            gsc.Add(new GradientStop(Color.FromArgb(30, 255, 255, 255), 0.666));
            gsc.Add(new GradientStop(Color.FromArgb(80, 255, 255, 255), 1));//231, 242, 255), 1));
            RibbonStyleHandler.QuickAccessBackground = new LinearGradientBrush(gsc, 90.0);
            #endregion
        }

        private static void setBlackStyle()
        {
            #region button brushes
            RibbonStyleHandler.ButtonBorderNormal = Color.FromArgb(255, 141, 150, 155);
            RibbonStyleHandler.ButtonBorderHover = Color.FromArgb(255, 192, 168, 119);
            RibbonStyleHandler.ButtonBorderPressed = Color.FromArgb(255, 255, 0, 0);

            RibbonStyleHandler.ButtonBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundHover = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundPressed = new List<GradientStop>();

            RibbonStyleHandler.ButtonNormalText = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            RibbonStyleHandler.ButtonSelectedText = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            RibbonStyleHandler.ButtonDisabledText = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));
            #endregion

            #region group brushes
            RibbonStyleHandler.GroupBorderNormal = Color.FromArgb(255, 130, 130, 130);
            RibbonStyleHandler.GroupBorderHover = Color.FromArgb(255, 137, 138, 139);

            RibbonStyleHandler.GroupBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupLabelBorderNormal = Color.FromArgb(255, 130, 130, 130); //161, 189, 213);//
            RibbonStyleHandler.GroupLabelBorderHover = Color.FromArgb(255, 137, 138, 139);//

            RibbonStyleHandler.GroupLabelBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupLabelBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupText = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            #endregion

            #region background brushes
            RibbonStyleHandler.RibbonBarBackground = new SolidColorBrush();
            RibbonStyleHandler.RibbonTitleBackground = new SolidColorBrush(Color.FromArgb(255, 83, 83, 83));
            RibbonStyleHandler.PreviewBackground = new SolidColorBrush(Color.FromRgb(218, 226, 226));
            #endregion

            #region timing
            RibbonStyleHandler.EnterTime = TimeSpan.FromMilliseconds(300);
            RibbonStyleHandler.LeaveTime = TimeSpan.FromMilliseconds(900);
            #endregion

            #region create button normal background gradient stops
            List<GradientStop> backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 214, 222, 223), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 216, 224, 225), 0.647));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 223, 228, 230), 0.6471));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 223, 228, 230), 1));
            RibbonStyleHandler.ButtonBackgroundNormal = backgroundNormal;
            #endregion

            #region create button hover background gradient stops
            List<GradientStop> backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 252, 230), 0));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 241, 190), 0.647));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 220, 97), 0.6471));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 220, 97), 1));
            RibbonStyleHandler.ButtonBackgroundHover = backgroundHover;
            #endregion

            #region create button pressed background gradient stops
            List<GradientStop> backgroundPressed = new List<GradientStop>();
            backgroundPressed.Add(new GradientStop(Color.FromRgb(219, 180, 149), 0));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(237, 134, 65), 0.647));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(233, 102, 14), 0.6471));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(249, 150, 70), 1));
            RibbonStyleHandler.ButtonBackgroundPressed = backgroundPressed;
            #endregion

            #region create group normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 240, 242, 243), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 240, 242, 243), 1));
            RibbonStyleHandler.GroupBackgroundNormal = backgroundNormal;
            #endregion

            #region create group hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 240, 242, 243), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 240, 242, 243), 1));
            RibbonStyleHandler.GroupBackgroundHover = backgroundHover;
            #endregion

            #region create group label normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 179, 181, 181), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 179, 181, 181), 1));
            RibbonStyleHandler.GroupLabelBackgroundNormal = backgroundNormal;
            #endregion

            #region create group label hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 166, 167, 167), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 120, 121, 121), 1));
            RibbonStyleHandler.GroupLabelBackgroundHover = backgroundHover;
            #endregion

            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(255, 214, 218, 226), 0)); //219, 230, 244), 0));
            gsc.Add(new GradientStop(Color.FromArgb(255, 181, 188, 197), 0.333));
            gsc.Add(new GradientStop(Color.FromArgb(255, 181, 188, 197), 0.666));
            gsc.Add(new GradientStop(Color.FromArgb(255, 69, 69, 69), 1));//231, 242, 255), 1));
            RibbonStyleHandler.RibbonBarBackground = new LinearGradientBrush(gsc, 90.0);
        }

        private static void setGrayStyle()
        {
            #region button brushes
            RibbonStyleHandler.ButtonBorderNormal = Color.FromArgb(255, 169, 177, 184);
            RibbonStyleHandler.ButtonBorderHover = Color.FromArgb(255, 157, 29, 0);
            RibbonStyleHandler.ButtonBorderPressed = Color.FromArgb(255, 255, 0, 0);

            RibbonStyleHandler.ButtonBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundHover = new List<GradientStop>();
            RibbonStyleHandler.ButtonBackgroundPressed = new List<GradientStop>();

            RibbonStyleHandler.ButtonNormalText = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
            RibbonStyleHandler.ButtonSelectedText = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            RibbonStyleHandler.ButtonDisabledText = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));
            #endregion

            #region group brushes
            RibbonStyleHandler.GroupBorderNormal = Color.FromArgb(255, 134, 134, 134);
            RibbonStyleHandler.GroupBorderHover = Color.FromArgb(255, 151, 151, 152);

            RibbonStyleHandler.GroupBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupLabelBorderNormal = Color.FromArgb(255, 134, 134, 134); //161, 189, 213);//
            RibbonStyleHandler.GroupLabelBorderHover = Color.FromArgb(255, 151, 151, 151);//

            RibbonStyleHandler.GroupLabelBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupLabelBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupText = new SolidColorBrush(Color.FromArgb(220, 51, 51, 51));
            #endregion

            #region background brushes
            RibbonStyleHandler.RibbonBarBackground = new SolidColorBrush();
            RibbonStyleHandler.RibbonTitleBackground = new SolidColorBrush(Color.FromArgb(255, 208, 212, 221));
            RibbonStyleHandler.PreviewBackground = new SolidColorBrush(Color.FromRgb(232, 234, 236));
            #endregion

            #region timing
            RibbonStyleHandler.EnterTime = TimeSpan.FromMilliseconds(300);
            RibbonStyleHandler.LeaveTime = TimeSpan.FromMilliseconds(900);
            #endregion

            #region create button normal background gradient stops
            List<GradientStop> backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 241, 243, 243), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 241, 243, 243), 0.647));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 232, 235, 239), 0.6471));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 232, 235, 239), 1));
            RibbonStyleHandler.ButtonBackgroundNormal = backgroundNormal;
            #endregion

            #region create button hover background gradient stops
            List<GradientStop> backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromRgb(249, 225, 44), 0));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 159, 18), 0.647));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 109, 0), 0.6471));
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 109, 0), 1));
            RibbonStyleHandler.ButtonBackgroundHover = backgroundHover;
            #endregion

            #region create button pressed background gradient stops
            List<GradientStop> backgroundPressed = new List<GradientStop>();
            backgroundPressed.Add(new GradientStop(Color.FromRgb(219, 180, 149), 0));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(237, 134, 65), 0.647));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(233, 102, 14), 0.6471));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(249, 150, 70), 1));
            RibbonStyleHandler.ButtonBackgroundPressed = backgroundPressed;
            #endregion

            #region create group normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 232, 238, 241), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 232, 238, 241), 1));
            RibbonStyleHandler.GroupBackgroundNormal = backgroundNormal;
            #endregion

            #region create group hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 232, 238, 241), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 232, 238, 241), 1));
            RibbonStyleHandler.GroupBackgroundHover = backgroundHover;
            #endregion

            #region create group label normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 220, 225, 237), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 199, 203, 213), 1));
            RibbonStyleHandler.GroupLabelBackgroundNormal = backgroundNormal;
            #endregion

            #region create group label hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 220, 225, 237), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 199, 203, 213), 1));
            RibbonStyleHandler.GroupLabelBackgroundHover = backgroundHover;
            #endregion

            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(255, 238, 241, 246), 0)); //219, 230, 244), 0));
            gsc.Add(new GradientStop(Color.FromArgb(255, 213, 219, 231), 0.333));
            gsc.Add(new GradientStop(Color.FromArgb(255, 213, 219, 231), 0.666));
            gsc.Add(new GradientStop(Color.FromArgb(255, 239, 249, 251), 1));//231, 242, 255), 1));
            RibbonStyleHandler.RibbonBarBackground = new LinearGradientBrush(gsc, 90.0);
        }

        private static void setGreenStyle()
        {
            #region button brushes
            RibbonStyleHandler.ButtonBorderNormal = Color.FromArgb(255, 31, 132, 16);//
            RibbonStyleHandler.ButtonBorderHover = Color.FromArgb(255, 163, 104, 250);//
            RibbonStyleHandler.ButtonBorderPressed = Color.FromArgb(255, 25, 48, 255);//

            RibbonStyleHandler.ButtonBackgroundNormal = new List<GradientStop>();//
            RibbonStyleHandler.ButtonBackgroundHover = new List<GradientStop>();//
            RibbonStyleHandler.ButtonBackgroundPressed = new List<GradientStop>();

            RibbonStyleHandler.ButtonNormalText = new SolidColorBrush(Color.FromArgb(255, 27, 64, 13)); //
            RibbonStyleHandler.ButtonSelectedText = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            RibbonStyleHandler.ButtonDisabledText = new SolidColorBrush(Color.FromArgb(255, 140, 140, 140));
            #endregion

            #region group brushes //
            RibbonStyleHandler.GroupBorderNormal = Color.FromArgb(255, 31, 132, 16); 
            RibbonStyleHandler.GroupBorderHover = Color.FromArgb(255, 64, 162, 50);

            RibbonStyleHandler.GroupBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupLabelBorderNormal = Color.FromArgb(255, 31, 132, 16); 
            RibbonStyleHandler.GroupLabelBorderHover = Color.FromArgb(255, 64, 162, 50);

            RibbonStyleHandler.GroupLabelBackgroundNormal = new List<GradientStop>();
            RibbonStyleHandler.GroupLabelBackgroundHover = new List<GradientStop>();

            RibbonStyleHandler.GroupText = new SolidColorBrush(Color.FromArgb(220, 255,255,255));
            #endregion

            #region background brushes //
            RibbonStyleHandler.RibbonBarBackground = new SolidColorBrush();
            RibbonStyleHandler.RibbonTitleBackground = new SolidColorBrush(Color.FromArgb(255, 44, 113, 34));//
            RibbonStyleHandler.PreviewBackground = new SolidColorBrush(Color.FromRgb(213, 255, 207));
            #endregion

            #region timing
            RibbonStyleHandler.EnterTime = TimeSpan.FromMilliseconds(300);
            RibbonStyleHandler.LeaveTime = TimeSpan.FromMilliseconds(900);
            #endregion

            #region create button normal background gradient stops //
            List<GradientStop> backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 232, 255, 228), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 149, 246, 134), 0.647));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 118, 242, 100), 0.6471));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 118, 242, 100), 1));
            RibbonStyleHandler.ButtonBackgroundNormal = backgroundNormal;
            #endregion

            #region create button hover background gradient stops
            List<GradientStop> backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0));
            backgroundHover.Add(new GradientStop(Color.FromRgb(178, 138, 236), 0.647));
            backgroundHover.Add(new GradientStop(Color.FromRgb(166, 122, 231), 0.6471));
            backgroundHover.Add(new GradientStop(Color.FromRgb(163, 104, 250), 1));
            RibbonStyleHandler.ButtonBackgroundHover = backgroundHover;
            #endregion

            #region create button pressed background gradient stops
            List<GradientStop> backgroundPressed = new List<GradientStop>();
            backgroundPressed.Add(new GradientStop(Color.FromRgb(198, 174, 232), 0));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(161, 111, 234), 0.647));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(143, 84, 231), 0.6471));
            backgroundPressed.Add(new GradientStop(Color.FromRgb(136, 57, 252), 1));
            RibbonStyleHandler.ButtonBackgroundPressed = backgroundPressed;
            #endregion

            #region create group normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 228, 239, 253), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(0, 220, 232, 248), 1));
            RibbonStyleHandler.GroupBackgroundNormal = backgroundNormal;
            #endregion

            #region create group hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 182, 248, 157), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 182, 248, 157), 1));
            RibbonStyleHandler.GroupBackgroundHover = backgroundHover;
            #endregion

            #region create group label normal background gradient stops
            backgroundNormal = new List<GradientStop>();
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 31, 132, 16), 0));
            backgroundNormal.Add(new GradientStop(Color.FromArgb(255, 31, 132, 16), 1));
            RibbonStyleHandler.GroupLabelBackgroundNormal = backgroundNormal;
            #endregion

            #region create group label hover background gradient stops
            backgroundHover = new List<GradientStop>();
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 64, 162, 50), 0));
            backgroundHover.Add(new GradientStop(Color.FromArgb(255, 64, 162, 50), 1));
            RibbonStyleHandler.GroupLabelBackgroundHover = backgroundHover;
            #endregion
            
            //done
            GradientStopCollection gsc = new GradientStopCollection();
            gsc.Add(new GradientStop(Color.FromArgb(255, 118, 242, 100), 0)); //219, 230, 244), 0));
            gsc.Add(new GradientStop(Color.FromArgb(255, 149, 246, 134), 0.333));
            gsc.Add(new GradientStop(Color.FromArgb(255, 118, 242, 100), 0.666));
            gsc.Add(new GradientStop(Color.FromArgb(255, 232, 255, 228), 1));//231, 242, 255), 1));
            RibbonStyleHandler.RibbonBarBackground = new LinearGradientBrush(gsc, 90.0);
        }
        #endregion
        #endregion

        #region event handlers
        private static void element_MouseLeave(object sender, MouseEventArgs e)
        {
            #region single border
            if (sender.GetType() == Type.GetType("DNBSoft.WPF.RibbonControl.RibbonBorder"))
            {
                RibbonBorder b = (RibbonBorder)sender;

                if (b.IsEnabled)
                {
                    #region equivilent to mouse up?
                    if (RibbonStyleHandler.PriorPressedBrushes.ContainsKey(sender))
                    {
                        RibbonBorder theBorder = (RibbonBorder)sender;
                        if (theBorder.IsEnabled && theBorder.IsSelected)
                        {
                            theBorder.Background = RibbonStyleHandler.BackgroundBrushesSelected[theBorder];
                        }
                        else if (theBorder.IsEnabled)
                        {
                            theBorder.Background = RibbonStyleHandler.BackgroundBrushesNormal[theBorder];
                        }
                        else
                        {
                            theBorder.Background = RibbonStyleHandler.toLGradientBrush(RibbonStyleHandler.ButtonBackgroundDisabled, 90.0);
                        }
                        RibbonStyleHandler.PriorPressedBrushes.Remove(sender);
                    }
                    #endregion

                    if (RibbonStyleHandler.ContainingElements.ContainsKey(sender))
                    {
                        FrameworkElement containingElement = RibbonStyleHandler.ContainingElements[sender];
                        List<Storyboard> leaveStoryboards = null;

                        #region get storyboards
                        if (b.IsSelected && RibbonStyleHandler.LeaveStoryboardsSelected.ContainsKey(sender))
                        {
                            leaveStoryboards = RibbonStyleHandler.LeaveStoryboardsSelected[sender];
                        }
                        else if (b.IsSelected != true && RibbonStyleHandler.LeaveStoryboardsNormal.ContainsKey(sender))
                        {
                            leaveStoryboards = RibbonStyleHandler.LeaveStoryboardsNormal[sender];
                        }
                        #endregion

                        #region run storyboard
                        for (int i = 0; i < leaveStoryboards.Count; i++)
                        {
                            leaveStoryboards[i].Begin(containingElement);
                        }
                        #endregion
                    }
                }
            }
            #endregion
            #region double border
            else
            {
                UIElement tElement = (UIElement)sender;
                RibbonBorder b = RibbonStyleHandler.MultiBorderLinks[tElement][0];

                if (b.IsEnabled)
                {
                    #region equivilent to mouse up?
                    if (RibbonStyleHandler.PriorPressedBrushes.ContainsKey(sender))
                    {
                        RibbonBorder theBorder = (RibbonBorder)sender;
                        if (theBorder.IsEnabled && theBorder.IsSelected)
                        {
                            theBorder.Background = RibbonStyleHandler.BackgroundBrushesSelected[theBorder];
                        }
                        else if (theBorder.IsEnabled)
                        {
                            theBorder.Background = RibbonStyleHandler.BackgroundBrushesNormal[theBorder];
                        }
                        else
                        {
                            theBorder.Background = RibbonStyleHandler.toLGradientBrush(RibbonStyleHandler.ButtonBackgroundDisabled, 90.0);
                        }
                        RibbonStyleHandler.PriorPressedBrushes.Remove(sender);
                    }
                    #endregion

                    if (RibbonStyleHandler.ContainingElements.ContainsKey(sender))
                    {
                        FrameworkElement containingElement = RibbonStyleHandler.ContainingElements[sender];
                        List<Storyboard> leaveStoryboards = null;

                        #region get storyboards
                        if (b.IsSelected && RibbonStyleHandler.LeaveStoryboardsSelected.ContainsKey(sender))
                        {
                            leaveStoryboards = RibbonStyleHandler.LeaveStoryboardsSelected[sender];
                        }
                        else if (b.IsSelected != true && RibbonStyleHandler.LeaveStoryboardsNormal.ContainsKey(sender))
                        {
                            leaveStoryboards = RibbonStyleHandler.LeaveStoryboardsNormal[sender];
                        }
                        #endregion

                        #region run storyboard
                        for (int i = 0; i < leaveStoryboards.Count; i++)
                        {
                            leaveStoryboards[i].Begin(containingElement);
                        }
                        #endregion
                    }
                }
            }
            #endregion
        }

        public static void forceLeaveAnnimation(Border theBorder, MouseEventArgs e)
        {
            element_MouseLeave((object)theBorder, e);
        }

        private static void element_MouseEnter(object sender, MouseEventArgs e)
        {
            #region standard single border
            if (sender.GetType() == Type.GetType("DNBSoft.WPF.RibbonControl.RibbonBorder"))
            {
                RibbonBorder b = (RibbonBorder)sender;

                if (b.IsEnabled)
                {
                    #region mouse enters with button down
                    if (e.LeftButton == MouseButtonState.Pressed && RibbonStyleHandler.BackgroundBrushesNormal.ContainsKey(b))
                    {
                        RibbonBorder theBorder = (RibbonBorder)sender;

                        if (RibbonStyleHandler.PriorPressedBrushes.ContainsKey(sender))
                        {
                            //RibbonStyleHandler.PriorPressedBrushes.Remove(sender);
                        }
                        else
                        {
                            RibbonStyleHandler.PriorPressedBrushes.Add(sender, theBorder.Background);
                        }

                        GradientStopCollection gsc = new GradientStopCollection();//((LinearGradientBrush)(theBorder.Background)).GradientStops;
                        if (RibbonStyleHandler.TabBorders.Contains(theBorder))
                        {
                            for (int i = 0; i < RibbonStyleHandler.TabBackgroundPressed.Count; i++)
                            {
                                gsc.Add(RibbonStyleHandler.TabBackgroundPressed[i].Clone());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundPressed.Count; i++)
                            {
                                gsc.Add(RibbonStyleHandler.ButtonBackgroundPressed[i].Clone());
                            }
                        }
                        theBorder.Background = new LinearGradientBrush(gsc, 90.0);
                    }
                    #endregion

                    if (RibbonStyleHandler.ContainingElements.ContainsKey(sender))
                    {
                        FrameworkElement containingElement = RibbonStyleHandler.ContainingElements[sender];
                        List<Storyboard> enterStoryboards = null;

                        #region get storyboard
                        if (b.IsSelected && RibbonStyleHandler.EnterStoryboardsSelected.ContainsKey(sender))
                        {
                            enterStoryboards = RibbonStyleHandler.EnterStoryboardsSelected[sender];
                        }
                        else if (b.IsSelected != true && RibbonStyleHandler.EnterStoryboardsNormal.ContainsKey(sender))
                        {
                            enterStoryboards = RibbonStyleHandler.EnterStoryboardsNormal[sender];
                        }
                        #endregion

                        #region run storyboard
                        if (enterStoryboards != null)
                        {
                            for (int i = 0; i < enterStoryboards.Count; i++)
                            {
                                enterStoryboards[i].Begin(containingElement);
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion
            #region double border via grid
            else
            {
                UIElement tElement = (UIElement)sender;
                RibbonBorder b = RibbonStyleHandler.MultiBorderLinks[tElement][0];

                if (b.IsEnabled)
                {
                    #region mouse enters with button down
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        Border theBorder = (Border)sender;

                        if (RibbonStyleHandler.PriorPressedBrushes.ContainsKey(sender))
                        {
                            //RibbonStyleHandler.PriorPressedBrushes.Remove(sender);
                        }
                        else
                        {
                            RibbonStyleHandler.PriorPressedBrushes.Add(sender, theBorder.Background);
                        }

                        GradientStopCollection gsc = new GradientStopCollection();//((LinearGradientBrush)(theBorder.Background)).GradientStops;
                        for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundPressed.Count; i++)
                        {
                            gsc.Add(RibbonStyleHandler.ButtonBackgroundPressed[i].Clone());
                        }
                        theBorder.Background = new LinearGradientBrush(gsc, 90.0);
                    }
                    #endregion

                    if (RibbonStyleHandler.ContainingElements.ContainsKey(sender))
                    {
                        FrameworkElement containingElement = RibbonStyleHandler.ContainingElements[sender];
                        List<Storyboard> enterStoryboards = null;

                        #region get storyboard
                        if (b.IsSelected && RibbonStyleHandler.EnterStoryboardsSelected.ContainsKey(sender))
                        {
                            enterStoryboards = RibbonStyleHandler.EnterStoryboardsSelected[sender];
                        }
                        else if (b.IsSelected != true && RibbonStyleHandler.EnterStoryboardsNormal.ContainsKey(sender))
                        {
                            enterStoryboards = RibbonStyleHandler.EnterStoryboardsNormal[sender];
                        }
                        #endregion

                        #region run storyboard
                        if (enterStoryboards != null)
                        {
                            for (int i = 0; i < enterStoryboards.Count; i++)
                            {
                                enterStoryboards[i].Begin(containingElement);
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion
        }

        private static void buttonElement_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RibbonBorder theBorder = (RibbonBorder)sender;

            if (theBorder.IsEnabled && RibbonStyleHandler.PriorPressedBrushes.ContainsKey(sender))
            {
                if (theBorder.IsEnabled && theBorder.IsSelected)
                {
                    theBorder.Background = RibbonStyleHandler.BackgroundBrushesSelected[theBorder];
                }
                else if (theBorder.IsEnabled)
                {
                    theBorder.Background = RibbonStyleHandler.BackgroundBrushesNormal[theBorder];
                }
                else
                {
                    theBorder.Background = RibbonStyleHandler.toLGradientBrush(RibbonStyleHandler.ButtonBackgroundDisabled, 90.0);
                }
                RibbonStyleHandler.PriorPressedBrushes.Remove(sender);
            }
        }

        private static void buttonElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RibbonBorder theBorder = (RibbonBorder)sender;

            if (e.LeftButton == MouseButtonState.Pressed && theBorder.IsEnabled)
            {
                if (RibbonStyleHandler.PriorPressedBrushes.ContainsKey(sender))
                {
                    //RibbonStyleHandler.PriorPressedBrushes.Remove(sender);
                }
                else
                {
                    RibbonStyleHandler.PriorPressedBrushes.Add(sender, theBorder.Background);
                }

                GradientStopCollection gsc = new GradientStopCollection();//((LinearGradientBrush)(theBorder.Background)).GradientStops;
                if (RibbonStyleHandler.TabBorders.Contains(theBorder))
                {
                    for (int i = 0; i < RibbonStyleHandler.TabBackgroundPressed.Count; i++)
                    {
                        gsc.Add(RibbonStyleHandler.TabBackgroundPressed[i].Clone());
                    }
                }
                else
                {
                    for (int i = 0; i < RibbonStyleHandler.ButtonBackgroundPressed.Count; i++)
                    {
                        gsc.Add(RibbonStyleHandler.ButtonBackgroundPressed[i].Clone());
                    }
                }
                theBorder.Background = new LinearGradientBrush(gsc, 90.0);
            }
        }
        #endregion

        #region classes / enumerations
        private class StyleParameters
        {
            public StyleType type = StyleType.Not_Set;
            public UIElement topControl = null;
            public RibbonBorder element1 = null;
            public RibbonBorder element2 = null;
            public UIElement parent = null;
            public FrameworkElement parent2 = null;
            public Color initialBorderColour = new Color();
            public bool useInitialBorderColour = false;
            public List<GradientStop> initialBackgroundBrush = new List<GradientStop>();
            public bool useInitialBackgroundBrush = false;
        }

        private enum StyleType
        {
            Button, Group, Tab, Not_Set
        }

        public enum Style
        {
            Blue, Default, Black, Gray, Green, Not_Set
        }

        public class StyleChangedEventArgs : EventArgs
        {
            private RibbonStyleHandler.Style lastStyle = Style.Not_Set;

            public StyleChangedEventArgs(RibbonStyleHandler.Style style) : base()
            {
                this.lastStyle = style;
            }

            public RibbonStyleHandler.Style LastStyle
            {
                get
                {
                    return lastStyle;
                }
                set
                {
                    lastStyle = value;
                }
            }
        }
        #endregion

        #region statics
        public static LinearGradientBrush toLGradientBrush(List<GradientStop> stops, double angle)
        {
            GradientStopCollection gsc = new GradientStopCollection();
            for (int i = 0; i < stops.Count; i++)
            {
                gsc.Add(stops[i]);
            }
            return new LinearGradientBrush(gsc, angle);
        }
        #endregion
    }
}
