using DevExpress.Mvvm;
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
                if (ExceptionPolicy.HandleException(ex, policyName ?? "ErrorPolicy", out exceptionToRethrow))
                {
                    if (exceptionToRethrow != null)
                        MessageBoxService.ShowMessage(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                    else
                        MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                }
                else
                    MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBoxService.ShowMessage(exception.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
            }
        }

        //public static void HandleException(Exception ex, string policyName = null)
        //{
        //    try
        //    {
        //        Exception exceptionToRethrow;
        //        if (ExceptionPolicy.HandleException(ex, policyName ?? "ErrorPolicy", out exceptionToRethrow))
        //        {
        //            if (exceptionToRethrow != null)
        //                MessageBoxService.ShowMessage(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
        //            else
        //                MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
        //        }
        //        else
        //            MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
        //    }
        //    catch (Exception exception)
        //    {
        //        MessageBoxService.ShowMessage(exception.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
        //    }
        //}
    }
}
