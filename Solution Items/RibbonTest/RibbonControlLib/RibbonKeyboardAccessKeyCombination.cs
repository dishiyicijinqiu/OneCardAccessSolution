using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNBSoft.WPF.RibbonControl
{
    public class RibbonKeyboardAccessKeyCombination
    {
        private String combinationString = null;

        public RibbonKeyboardAccessKeyCombination()
        {
        }

        public RibbonKeyboardAccessKeyCombination(String keyCombination)
        {
            this.combinationString = keyCombination;
        }

        public String KeyCombination
        {
            get
            {
                return combinationString;
            }
            set
            {
                combinationString = value;
            }
        }
    }
}
