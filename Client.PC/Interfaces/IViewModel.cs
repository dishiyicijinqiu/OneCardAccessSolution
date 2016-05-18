using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IViewModel
    {
        SubscriptionToken ExceptionEventToken { get; set; }
        SubscriptionToken MessageBoxEventToken { get; set; }
        SubscriptionToken CreateViewEventToken { get; set; }
        SubscriptionToken ChangeDataContextEventToken { get; set; }
        SubscriptionToken CloseEventToken { get; set; }
        void Close();
        void OKClose();
        Core.MsgResult ShowMessage(string message);
        Core.MsgResult ShowError(string message);
        Core.MsgResult ShowMessage(Core.MessageBoxEventArgs args);
        void ShowException(Exception ex);
        bool? CreateView(Core.CreateViewEventArgs args);
    }
    public delegate void OnEntityViewEdited<T>(IViewModel vm, Core.EditMessage<T> EditMessage);

    public delegate void OnSelectedItems<T>(List<T> SelectItems);
}
