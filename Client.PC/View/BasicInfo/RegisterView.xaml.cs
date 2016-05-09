using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Core;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// RegisterView.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterView : BaseUserControl
    {
        public RegisterView(object ParentViewModel, RegisterEditMessage EditMessage)
        {
            InitializeComponent();
            this.ParentDataContext = ParentViewModel;
            this.Loaded += (sender, e) =>
            {
                try
                {
                    this.DataContext = new RegisterViewModel(this.ParentDataContext, EditMessage);
                }
                catch (System.Exception ex)
                {
                    ex.HandleException(this);
                    InterCloseDocument();
                }
            };
        }
    }
}
