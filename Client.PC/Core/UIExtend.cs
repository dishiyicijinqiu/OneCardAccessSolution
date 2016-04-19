﻿using DevExpress.Mvvm;
using FengSharp.OneCardAccess.Common;
using System;
using System.Windows;

namespace FengSharp.OneCardAccess.Core
{
    public static class UIExtend
    {
        public static void HandleException(this IMessageBoxService MessageBoxService, Exception exception, string policyName = null)
        {
            MessageBoxExceptionProcess.HandleException(MessageBoxService, exception, policyName);
        }
        public static void Error(this IMessageBoxService MessageBoxService, Exception exception)
        {
            try
            {
                if (MessageBoxService == null)
                {
                    MessageBox.Show(Client.PC.Properties.Resources.Error_UnHandler);
                    return;
                }
                if (exception == null)
                {
                    MessageBoxService.ShowMessage(Client.PC.Properties.Resources.Error_UnHandler, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                    return;
                }
                MessageBoxService.ShowMessage(exception.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                if (exception is LoginTimeOutException)
                {
                    Messenger.Default.Send(exception as LoginTimeOutException);
                }
            }
            catch (Exception ex)
            {
                if (MessageBoxService == null)
                {
                    MessageBox.Show(Client.PC.Properties.Resources.Error_UnHandler);
                    return;
                }
                MessageBoxService.ShowMessage(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
            }
        }
    }
}
