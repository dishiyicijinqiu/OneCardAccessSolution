using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DNBSoft.WPF.RibbonControl
{
    internal class NonClientRegionAPI
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;      // width of left ribbon:RibbonBorder that retains its size
            public int cxRightWidth;     // width of right ribbon:RibbonBorder that retains its size
            public int cyTopHeight;      // height of top ribbon:RibbonBorder that retains its size
            public int cyBottomHeight;   // height of bottom ribbon:RibbonBorder that retains its size
        };


        [DllImport("DwmApi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
            IntPtr hwnd,
            ref MARGINS pMarInset);
    }
}
