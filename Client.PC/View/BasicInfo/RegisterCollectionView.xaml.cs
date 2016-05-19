﻿using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterCollectionView : BaseUserControl, IRegisterCollectionView
    {
        public RegisterCollectionView(RegisterCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public RegisterCollectionView()
        {
            InitializeComponent();
        }
    }
}
