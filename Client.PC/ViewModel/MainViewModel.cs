﻿using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Windows.Markup;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel
{
    public class MainViewModel
    {
        DelegateCommand<DocumentInfo> ShowDocumentCommand;
        public MainViewModel()
        {
            DefaultEventAggregator.Current.GetEvent<LoginTimeOutEvent>().Subscribe(TryLogin);
            ShowDocumentCommand = new DelegateCommand<DocumentInfo>(ShowDocument);
        }
        #region commandMethods
        public void ShowDocument(DocumentInfo docInfo)
        {
            try
            {
                DefaultEventAggregator.Current.GetEvent<ShowDocumentEvent>().
                    Publish(this, new ShowDocumentEventArgs(docInfo));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<MainViewModel>>().
                    Publish(this, new ExceptionEventArgs(ex));
            }
        }
        #endregion
        #region Events
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

    public class ShowDocumentEvent : BaseSenderEvent<MainViewModel, ShowDocumentEventArgs>
    {

    }
    public class ShowDocumentEventArgs : NullEventArgs
    {
        public ShowDocumentEventArgs(DocumentInfo documentInfo)
        {
            this.DocumentInfo = documentInfo;
        }
        public DocumentInfo DocumentInfo { get; set; }
    }
}
