using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Windows;

namespace FengSharp.OneCardAccess.Core
{
    public static class Extend
    {

        public static void HandleException(this Exception ex, FrameworkElement owner = null, string policyName = null)
        {
            try
            {
                Exception exceptionToRethrow;
                if (!string.IsNullOrWhiteSpace(policyName))
                {
                    if (ExceptionPolicy.HandleException(ex, policyName, out exceptionToRethrow))
                    {
                        if (exceptionToRethrow != null)
                        {
                            if (owner == null)
                                DXMessageBox.Show(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                DXMessageBox.Show(owner, exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (owner == null)
                                DXMessageBox.Show(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                DXMessageBox.Show(owner, ex.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return;
                    }
                }
                if (ExceptionPolicy.HandleException(ex, "ExceptionPolicy", out exceptionToRethrow))
                {
                    if (exceptionToRethrow != null)
                    {
                        if (owner == null)
                            DXMessageBox.Show(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            DXMessageBox.Show(owner, exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        if (exceptionToRethrow is LoginTimeOutException)
                        {
                            DefaultEventAggregator.Current.GetEvent<LoginTimeOutEvent>().Publish(new LoginTimeOutEventArgs(exceptionToRethrow as LoginTimeOutException));
                        }
                    }
                    else
                    {
                        if (owner == null)
                            DXMessageBox.Show(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            DXMessageBox.Show(owner, ex.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);

                        if (ex is LoginTimeOutException)
                        {
                            DefaultEventAggregator.Current.GetEvent<LoginTimeOutEvent>().Publish(new LoginTimeOutEventArgs(ex as LoginTimeOutException));
                        }
                    }
                    return;
                }
                if (owner == null)
                    DXMessageBox.Show(ex.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    DXMessageBox.Show(owner, ex.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception exception)
            {
                if (owner == null)
                    DXMessageBox.Show(exception.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    DXMessageBox.Show(owner, exception.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
