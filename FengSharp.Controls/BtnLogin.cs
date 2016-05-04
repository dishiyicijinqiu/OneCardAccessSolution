using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FengSharp.Controls
{
    public class BtnLogin : Button
    {
        static BtnLogin()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BtnLogin), new FrameworkPropertyMetadata(typeof(BtnLogin)));//去掉默认属性

            DefaultStyleKeyProperty.OverrideMetadata(typeof(BtnLogin),
                new FrameworkPropertyMetadata(typeof(BtnLogin)),
                Button.IsDefaultProperty.key
                );
        }

        public BtnLogin()
        {
        }
        public bool IsDefault
        {
            get { return (double)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }

        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize", typeof(double), typeof(ImageButton),
            new FrameworkPropertyMetadata(30.0, FrameworkPropertyMetadataOptions.AffectsRender));
    }
}
