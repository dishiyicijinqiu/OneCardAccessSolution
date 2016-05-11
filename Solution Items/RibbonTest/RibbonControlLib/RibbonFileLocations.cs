using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNBSoft.WPF.RibbonControl
{
    public abstract class RibbonFileLocations
    {
        private static String ribbonBasePath = Environment.CurrentDirectory;
        public static String RibbonBasePath
        {
            get
            {
                return ribbonBasePath;
            }
            set
            {
                RibbonFileLocations.ribbonBasePath = value;
            }
        }
    }
}
