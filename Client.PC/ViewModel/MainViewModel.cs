using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Events;
using System;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            DefaultEventAggregator.Current.GetEvent<LoginTimeOutEvent>().Subscribe(TryLogin);
            TryLogin(null);
        }
        #region commandMethods
        public void ShowDocument(DocumentInfo docInfo)
        {
            try
            {
                //var DocumentManager = ServiceLoader.LoadService<IDocManagerService>();
                //IDoc doc = DocumentManager.CreateDoc(docInfo.DocumentType, this);
                //doc.DestroyOnClose = true;
                //doc.Title = docInfo.DocumentName;
                //doc.Show();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<MainViewModel>>().
                    Publish(this, new ExceptionEventArgs(ex));
            }
        }
        #endregion
        #region Events and Methods
        public void TryLogin(LoginTimeOutEventArgs para)
        {
            if (para == null || para.LoginTimeOutException == null)
            {
                DefaultEventAggregator.Current.GetEvent<LoginEvent>().Publish(new LoginEventArgs(LoginState.NewLogin));
            }
            else
            {
                DefaultEventAggregator.Current.GetEvent<LoginEvent>().Publish(new LoginEventArgs(LoginState.TimeOutLogin));
            }
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
