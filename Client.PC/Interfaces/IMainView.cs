﻿using FengSharp.OneCardAccess.Client.PC.ViewModel;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IMainViewModel : IViewModel
    {
        SubscriptionToken ShowDocumentEventSubscriptionToken { get; set; }
        SubscriptionToken UICloseDocumentEventSubscriptionToken { get; set; }
        SubscriptionToken LoginEventSubscriptionToken { get; set; }
        SubscriptionToken LoginSucessEventSubscriptionToken { get; set; }
    }
    public interface IMainView : IView
    {
    }
}
