using DevExpress.Mvvm;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;

namespace FengSharp.OneCardAccess.Core
{
    public class MessageBoxExceptionProcess
    {
        public static void HandleException(IMessageBoxService MessageBoxService, Exception ex, string policyName = null)
        {
            try
            {
                Exception exceptionToRethrow;
                if (!string.IsNullOrWhiteSpace(policyName))
                {
                    if (ExceptionPolicy.HandleException(ex, policyName, out exceptionToRethrow))
                    {
                        if (exceptionToRethrow != null)
                            MessageBoxService.ShowMessage(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        else
                            MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        return;
                    }
                }
                if (ExceptionPolicy.HandleException(ex, "ExceptionPolicy", out exceptionToRethrow))
                {
                    if (exceptionToRethrow != null)
                    {
                        MessageBoxService.ShowMessage(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        if (exceptionToRethrow is LoginTimeOutException)
                        {
                            Messenger.Default.Send(exceptionToRethrow as LoginTimeOutException);
                        }
                    }
                    else
                    {
                        MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                        if (ex is LoginTimeOutException)
                        {
                            Messenger.Default.Send(ex as LoginTimeOutException);
                        }
                    }
                    return;
                }
                MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBoxService.ShowMessage(exception.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
            }
        }
    }
}
