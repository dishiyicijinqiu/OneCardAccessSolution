using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;
using System.Windows;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : BaseUserControl, IView
    {
        //public RegisterCollectionView() : this(CollectionViewStyle.CollectionView)
        //{

        //}
        //public RegisterCollectionView(CollectionViewStyle style)
        //{
        //    InitializeComponent();
        //    this.Loaded += (sender, e) =>
        //    {
        //        try
        //        {
        //            this.DataContext = new RegisterCollectionViewModel(style);
        //        }
        //        catch (Exception ex)
        //        {
        //            ex.HandleException(this);
        //            InterCloseDocument();
        //        }
        //    };
        //}
        public RegisterCollectionView(RegisterCollectionViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
    }
}
