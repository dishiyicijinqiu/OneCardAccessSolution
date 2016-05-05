using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using FengSharp.OneCardAccess.Core;

namespace FengSharp.OneCardAccess.Client.PC.View.BasicInfo
{
    /// <summary>
    /// P_CRTempView.xaml 的交互逻辑
    /// </summary>
    public partial class P_CRTempView : BaseUserControl
    {
        public P_CRTempView(object ParentViewModel, P_CRTempEditMessage EditMessage)
        {
            InitializeComponent();
            this.ParentDataContext = ParentViewModel;
            this.Parameter = EditMessage;
            this.Loaded += P_CRTempView_Loaded;
        }

        private void P_CRTempView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.DataContext = new P_CRTempViewModel(ParentDataContext, Parameter as P_CRTempEditMessage);
            }
            catch (System.Exception ex)
            {
                ex.HandleException(this);
                InterCloseDocument();
            }
        }
    }
}
