using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    [ServiceContract]
    public interface IConnectService
    {
        [OperationContract]
        LoginResult Login(string UserNo, string PassWord);
    }
}
