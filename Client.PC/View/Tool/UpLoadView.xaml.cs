using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.Tool;
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
using System.Globalization;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.View.Tool
{
    /// <summary>
    /// UpLoadView.xaml 的交互逻辑
    /// </summary>
    public partial class UpLoadView : BaseUserControl, IUpLoadView
    {
        public UpLoadView()
        {
            InitializeComponent();
        }
        public UpLoadView(UpLoadViewModel VM) : base(VM)
        {
            InitializeComponent();
        }

        private void BaseUserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpLoadViewModel VM = this.DataContext as UpLoadViewModel;
            VM.IsViewVisible = (bool)e.NewValue;
        }
    }
    public class StartStopContentConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UpLoadState uploadstate = (UpLoadState)value;
            switch (uploadstate)
            {
                default:
                case UpLoadState.Stoped:
                    return Properties.Resources.BarContent_Start;
                case UpLoadState.Running:
                    return Properties.Resources.BarContent_Pause;
                case UpLoadState.Pause:
                    return Properties.Resources.BarContent_Continue;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    public class StartStopGlyphConverter : MarkupExtension, IValueConverter
    {
        #region images
        BitmapImage _LargePauseImage;
        BitmapImage LargePauseImage
        {
            get
            {
                if (_LargePauseImage == null)
                {
                    _LargePauseImage = new BitmapImage(
                        new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/Pause_32x32.png",
                UriKind.Absolute));
                }
                return _LargePauseImage;
            }
        }
        BitmapImage _PauseImage;
        BitmapImage PauseImage
        {
            get
            {
                if (_PauseImage == null)
                {
                    _PauseImage = new BitmapImage(
                        new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/Pause_16x16.png",
                UriKind.Absolute));
                }
                return _PauseImage;
            }
        }

        BitmapImage _LargeStartImage;
        BitmapImage LargeStartImage
        {
            get
            {
                if (_LargeStartImage == null)
                {
                    _LargeStartImage = new BitmapImage(
                        new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/Start_32x32.png",
                UriKind.Absolute));
                }
                return _LargeStartImage;
            }
        }
        BitmapImage _StartImage;
        BitmapImage StartImage
        {
            get
            {
                if (_StartImage == null)
                {
                    _StartImage = new BitmapImage(
                        new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/Start_16x16.png",
                UriKind.Absolute));
                }
                return _StartImage;
            }
        }

        BitmapImage _LargeContinueImage;
        BitmapImage LargeContinueImage
        {
            get
            {
                if (_LargeContinueImage == null)
                {
                    _LargeContinueImage = new BitmapImage(
                        new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/Continue_32x32.png",
                UriKind.Absolute));
                }
                return _LargeContinueImage;
            }
        }
        BitmapImage _ContinueImage;
        BitmapImage ContinueImage
        {
            get
            {
                if (_ContinueImage == null)
                {
                    _ContinueImage = new BitmapImage(
                        new Uri("pack://application:,,,/FengSharp.OneCardAccess.Client.PC;component/Resources/Continue_16x16.png",
                UriKind.Absolute));
                }
                return _ContinueImage;
            }
        }
        #endregion
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UpLoadState uploadstate = (UpLoadState)value;
            string style = parameter.ToString().ToLower();
            switch (uploadstate)
            {
                default:
                case UpLoadState.Stoped:
                    return style == "large" ? LargeStartImage : StartImage;
                case UpLoadState.Running:
                    return style == "large" ? LargePauseImage : PauseImage;
                case UpLoadState.Pause:
                    return style == "large" ? LargeContinueImage : ContinueImage;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    public class UpLoadBackGroundConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UpLoadState uploadstate = (UpLoadState)value;
            return uploadstate == UpLoadState.Running;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
