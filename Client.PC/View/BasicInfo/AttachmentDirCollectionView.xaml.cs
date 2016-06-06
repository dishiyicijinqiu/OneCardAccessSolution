using DevExpress.Xpf.Grid.Native;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
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

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// AttachmentCollectionView.xaml 的交互逻辑
    /// </summary>
    public partial class AttachmentDirCollectionView : BaseUserControl, IAttachmentDirCollectionView
    {
        public AttachmentDirCollectionView()
        {
            InitializeComponent();
        }
        public AttachmentDirCollectionView(AttachmentDirCollectionViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
    }
}
