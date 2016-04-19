using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using System;

namespace FengSharp.OneCardAccess.Services
{
    public class RBACService : ServiceBase, IRBACService
    {
        public UserInfoEntity GetUserById(int UserId)
        {
            var dbentity = this.FindById<T_UserInfo>(new T_UserInfo()
            {
                UserId = UserId,
            });
            UserInfoEntity result = new UserInfoEntity();
            result.CopyValueFrom(dbentity);
            return result;
        }
    }
}
