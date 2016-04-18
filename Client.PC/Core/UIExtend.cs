using DevExpress.Mvvm;
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
    }
}
