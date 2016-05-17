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
    /// P_CRTempCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class P_CRTempCollectionView : BaseUserControl, IView
    {
        public P_CRTempCollectionView(P_CRTempCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public P_CRTempCollectionView()
        {
            InitializeComponent();
        }
    }
}
