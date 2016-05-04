using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class WidthAttribute : Attribute
    {
        public WidthAttribute(double Width = 100)
        {
            this.Width = Width;
        }
        public double Width { get; set; }
    }
}
