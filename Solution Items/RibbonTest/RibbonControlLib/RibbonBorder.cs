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

namespace DNBSoft.WPF.RibbonControl
{
    public class RibbonBorder : Border
    {
        private bool isSelected = false;
        private bool isEnabled = true;

        public event RibbonEventResources.EnableChangedDelegate EnabledChanged;
        public event RibbonEventResources.SelectedChangedDelegate SelectedChanged;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                bool fireEvent = false;
                if (isSelected != value)
                {
                    fireEvent = true;
                }

                isSelected = value;

                if (fireEvent && SelectedChanged != null)
                {
                    SelectedChanged(this, isSelected);
                }
            }
        }

        public new bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                bool fireEvent = false;
                if (isEnabled != value)
                {
                    fireEvent = true;
                }

                isEnabled = value;

                if (fireEvent && EnabledChanged != null)
                {
                    EnabledChanged(this, isEnabled);
                }
            }
        }
    }
}
