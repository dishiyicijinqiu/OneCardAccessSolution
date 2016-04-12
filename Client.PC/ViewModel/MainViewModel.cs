using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region command
        public void OnLoaded()
        {
            var loginwindowservice = this.GetService<IWindowService>("LoginWindowService");
            Messenger.Default.Register<LoginFormResult>(this, x => OnLogined(x));
            DXSplashScreen.Close();
            loginwindowservice.Show("LoginView", null, null, this);
        }

        private void OnLogined(LoginFormResult msg)
        {
            Messenger.Default.Unregister<LoginFormResult>(this);
            if (msg == LoginFormResult.Failed)
                System.Windows.Application.Current.Shutdown();
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
        public void Closing(object obj)
        {
            CancelEventArgs args = obj as CancelEventArgs;
            var result = MessageBoxService.ShowMessage(Properties.Resources.Info_ConfirmToExit, Properties.Resources.Info_Title, MessageButton.YesNo, MessageIcon.Information);
            if (result != MessageResult.Yes)
                args.Cancel = true;
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
