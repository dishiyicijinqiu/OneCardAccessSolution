using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Core
{
    public abstract class EditMessage<T>
    {
        public bool IsContinue { get; set; }
        public EntityEditMode EntityEditMode { get; protected set; }
        public T Key { get; set; }
    }
    public enum EntityEditMode
    {
        Add = 0,
        CopyAdd = 1,
        Edit = 2,
        Cate = 3,
        See = 4
    }
    public enum ViewStyle
    {
        View,
        OneSelect,
        MulSelect
    }
}
