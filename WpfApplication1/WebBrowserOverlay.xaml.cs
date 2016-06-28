using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>  
    /// Displays a WebBrowser control over a given placement target element in a WPF Window.  
    /// The owner window can be transparent, but not this one, due mixing DirectX and GDI drawing.   
    /// WebBrowserOverlayWF uses WinForms to avoid this limitation.  
    /// </summary>  
    public partial class WebBrowserOverlay : Window
    {
        FrameworkElement _placementTarget;


        public System.Windows.Forms.WebBrowser WebBrowser { get { return _wb; } }


        public WebBrowserOverlay(FrameworkElement placementTarget)
        {
            InitializeComponent();


            _placementTarget = placementTarget;
            Window owner = Window.GetWindow(placementTarget);
            Debug.Assert(owner != null);


            //owner.SizeChanged += delegate { OnSizeLocationChanged(); };  
            owner.LocationChanged += delegate { OnSizeLocationChanged(); };
            _placementTarget.SizeChanged += delegate { OnSizeLocationChanged(); };


            if (owner.IsVisible)
            {
                Owner = owner;
                Show();
            }
            else
                owner.IsVisibleChanged += delegate
                {
                    if (owner.IsVisible)
                    {
                        Owner = owner;
                        Show();
                    }
                };


            //owner.LayoutUpdated += new EventHandler(OnOwnerLayoutUpdated);  
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!e.Cancel)
                // Delayed call to avoid crash due to Window bug.  
                Dispatcher.BeginInvoke((Action)delegate
                {
                    Owner.Close();
                });
        }


        void OnSizeLocationChanged()
        {
            Point offset = _placementTarget.TranslatePoint(new Point(), Owner);
            Point size = new Point(_placementTarget.ActualWidth, _placementTarget.ActualHeight);
            HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(Owner);
            CompositionTarget ct = hwndSource.CompositionTarget;
            offset = ct.TransformToDevice.Transform(offset);
            size = ct.TransformToDevice.Transform(size);


            Win32.POINT screenLocation = new Win32.POINT(offset);
            Win32.ClientToScreen(hwndSource.Handle, ref screenLocation);
            Win32.POINT screenSize = new Win32.POINT(size);


            Win32.MoveWindow(((HwndSource)HwndSource.FromVisual(this)).Handle, screenLocation.X, screenLocation.Y, screenSize.X, screenSize.Y, true);
        }
        static class Win32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int X;
                public int Y;

                public POINT(int x, int y)
                {
                    this.X = x;
                    this.Y = y;
                }
                public POINT(Point pt)
                {
                    X = Convert.ToInt32(pt.X);
                    Y = Convert.ToInt32(pt.Y);
                }
            };

            [DllImport("user32.dll")]
            internal static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

            [DllImport("user32.dll")]
            internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        }
    }
}
