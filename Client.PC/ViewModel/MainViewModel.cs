using DevExpress.Mvvm;
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
            //DevExpress.Xpf.Core.ThemeManager.ApplicationThemeName = DevExpress.Xpf.Core.Theme.Office2010BlueName;
            var idialogservice = this.GetService<IDialogService>("LoginWindowService");
            var loginvm = new LoginViewModel();
            DevExpress.Xpf.Core.DXSplashScreen.Close();
            UICommand result = idialogservice.ShowDialog(loginvm.DialogCommands, Properties.Resources.LoginFormTItle, "LoginView", loginvm);
            if (result == loginvm.CancelUICommand)
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
