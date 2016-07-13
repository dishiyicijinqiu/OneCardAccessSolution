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
                Exception handleException = null;
                if (ex is Microsoft.Practices.Unity.ResolutionFailedException && ex.InnerException != null)
                    handleException = ex.InnerException;
                else
                    handleException = ex;
                for (int i = 0; i < 10; i++)
                {
                    if (handleException is Microsoft.Practices.Unity.ResolutionFailedException && handleException.InnerException != null)
                        handleException = handleException.InnerException;
                    else
                        break;
                }
                Exception exceptionToRethrow;
                if (!string.IsNullOrWhiteSpace(policyName))
                {
                    if (ExceptionPolicy.HandleException(handleException, policyName, out exceptionToRethrow))
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
                                DXMessageBox.Show(handleException.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                DXMessageBox.Show(owner, handleException.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        return;
                    }
                }
                if (ExceptionPolicy.HandleException(handleException, "ExceptionPolicy", out exceptionToRethrow))
                {
                    if (exceptionToRethrow != null)
                    {
                        if (owner == null)
                            DXMessageBox.Show(exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            DXMessageBox.Show(owner, exceptionToRethrow.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        if (exceptionToRethrow is LoginTimeOutException)
                        {
                            var token = ServiceLoader.LoadService<Client.PC.Interfaces.IMainViewModel>().LoginEventSubscriptionToken;
                            DefaultEventAggregator.Current.GetEvent<Client.PC.ViewModel.LoginEvent>().Publish(token,
                                new Client.PC.ViewModel.LoginEventArgs(Client.PC.ViewModel.LoginState.TimeOutLogin));
                        }
                    }
                    else
                    {
                        if (owner == null)
                            DXMessageBox.Show(handleException.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            DXMessageBox.Show(owner, handleException.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);

                        if (handleException is LoginTimeOutException)
                        {
                            var token = ServiceLoader.LoadService<Client.PC.Interfaces.IMainViewModel>().LoginEventSubscriptionToken;
                            DefaultEventAggregator.Current.GetEvent<Client.PC.ViewModel.LoginEvent>().Publish(token,
                                new Client.PC.ViewModel.LoginEventArgs(Client.PC.ViewModel.LoginState.TimeOutLogin));
                        }
                    }
                    return;
                }
                if (owner == null)
                    DXMessageBox.Show(handleException.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    DXMessageBox.Show(owner, handleException.Message, Client.PC.Properties.Resources.Error_Title, MessageBoxButton.OK, MessageBoxImage.Error);
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
