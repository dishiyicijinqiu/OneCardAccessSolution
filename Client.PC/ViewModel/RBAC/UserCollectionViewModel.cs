using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserCollectionViewModel : BaseNotificationObject, 
        IUserConnectionViewModel, IUserCollectionSelectViewModel
    {
    }
}
