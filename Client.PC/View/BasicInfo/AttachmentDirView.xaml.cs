using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// AttachmentView.xaml 的交互逻辑
    /// </summary>
    public partial class AttachmentDirView : BaseUserControl, IAttachmentDirView
    {
        public AttachmentDirView(AttachmentDirViewModel VM) : base(VM)
        {
            InitializeComponent();
        }
        public AttachmentDirView()
        {
            InitializeComponent();
        }
    }
}
