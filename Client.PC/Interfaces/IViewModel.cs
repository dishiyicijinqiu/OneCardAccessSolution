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
        SubscriptionToken CloseEventToken { get; set; }
        SubscriptionToken MessageBoxEventToken { get; set; }
        SubscriptionToken CreateViewEventToken { get; set; }
        SubscriptionToken ChangeDataContextEventToken { get; set; }
    }
}
