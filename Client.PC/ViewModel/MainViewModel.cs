using DevExpress.Mvvm;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using System;
using System.ComponentModel;
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
            Messenger.Default.Register<LoginTimeOutException>(this, x => TryLogin(x));
            App.Current.MainWindow.ShowInTaskbar = false;
            TryLogin(null);
            App.Current.MainWindow.ShowInTaskbar = true;
        }

        public void ShowDocument(DocumentInfo docInfo)
        {
            try
            {
                var DocumentManager = GetService<IDocumentManagerService>();
                IDocument doc = DocumentManager.CreateDocument(docInfo.DocumentType, null, this);
                doc.DestroyOnClose = true;
                doc.Title = docInfo.DocumentName;
                doc.Show();
            }
            catch (Exception ex)
            {
                this.GetService<IMessageBoxService>().HandleException(ex);
            }
        }
        #endregion
        #region Methods
        public void TryLogin(LoginTimeOutException para)
        {
            Messenger.Default.Register<LoginFormResult>(this, x => OnLogined(x));
            var loginwindowservice = this.GetService<IWindowService>("LoginWindowService");
            loginwindowservice.Show("LoginView", null, this);
        }

        private void OnLogined(LoginFormResult msg)
        {
            Messenger.Default.Unregister<LoginFormResult>(this);
            if (msg == LoginFormResult.Failed)
            {
                System.Windows.Application.Current.Shutdown();
            }
            App.Current.MainWindow.Opacity = 100;
        }
        #endregion
        public void Closing(CancelEventArgs args)
        {
            if (Session.Current != null)
            {
                var result = this.GetService<IMessageBoxService>().ShowMessage(Properties.Resources.Info_ConfirmToExit, Properties.Resources.Info_Title, MessageButton.YesNo, MessageIcon.Information);
                if (result != MessageResult.Yes)
                    args.Cancel = true;
            }
        }
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
