using DevExpress.Xpf.Core;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using System.Windows;

namespace FengSharp.OneCardAccess.Client.PC.Services
{
    public class MsgBoxService : IMsgBoxService
    {
        public MsgResult Show(string messageText, string caption = null, MsgButton button = MsgButton.OK, MsgImage icon = MsgImage.None, MsgResult defaultResult = MsgResult.None)
        {
            return (MsgResult)DXMessageBox.Show(messageText, caption, (MessageBoxButton)button, (MessageBoxImage)icon, (MessageBoxResult)defaultResult);
        }
        public MsgResult Show(IView owner, string messageText, string caption = null, MsgButton button = MsgButton.OK, MsgImage icon = MsgImage.None, MsgResult defaultResult = MsgResult.None)
        {
            if ((owner as FrameworkElement) == null)
                return (MsgResult)DXMessageBox.Show(messageText, caption, (MessageBoxButton)button, (MessageBoxImage)icon, (MessageBoxResult)defaultResult);
            return (MsgResult)DXMessageBox.Show(owner as FrameworkElement, messageText, caption, (MessageBoxButton)button, (MessageBoxImage)icon, (MessageBoxResult)defaultResult);
        }
    }
}
