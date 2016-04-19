using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.BusinessEntity;
using FengSharp.OneCardAccess.Client.PC.UI;
using FengSharp.OneCardAccess.Common;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

        }
        #region command
        public void OnLoaded()
        {
            //App.Current.MainWindow.Visibility = System.Windows.Visibility.Collapsed;
            Messenger.Default.Register<LoginFormResult>(this, x => OnLogined(x));
            var loginwindowservice = this.GetService<IWindowService>("LoginWindowService");
            DXSplashScreen.Close();
            loginwindowservice.Show("LoginView", null, this);
        }

        private void OnLogined(LoginFormResult msg)
        {
            App.Current.MainWindow.Visibility = System.Windows.Visibility.Visible;
            Messenger.Default.Unregister<LoginFormResult>(this);
            if (msg == LoginFormResult.Failed)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        public void ShowDocument(DocumentInfo docInfo)
        {
            var DocumentManager = GetService<IDocumentManagerService>();
            IDocument doc = DocumentManager.CreateDocument(docInfo.DocumentType, null, this);
            doc.DestroyOnClose = true;
            doc.Title = docInfo.DocumentName;
            doc.Show();
        }
        #endregion
        public void Closing(CancelEventArgs args)
        {
            if (ApplicationContext.Current.Count > 0)
            {
                var result = MessageBoxService.ShowMessage(Properties.Resources.Info_ConfirmToExit, Properties.Resources.Info_Title, MessageButton.YesNo, MessageIcon.Information);
                if (result != MessageResult.Yes)
                    args.Cancel = true;
            }
        }
        protected virtual IMessageBoxService MessageBoxService { get { return null; } }
    }
    public class DocumentInfo
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
    }
    [MarkupExtensionReturnType(typeof(System.Windows.PropertyPath))]
    public class DocumentInfoExtension : MarkupExtension
    {
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new DocumentInfo()
            {
                DocumentName = this.DocumentName,
                DocumentType = this.DocumentType
            };
        }
    }
}
