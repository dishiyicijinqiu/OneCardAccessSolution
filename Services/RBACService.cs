using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using System;
using System.Collections.Generic;

namespace FengSharp.OneCardAccess.Services
{
    public class RBACService : ServiceBase, IRBACService
    {
        public List<FirstUserGroupEntity> GetFirstUserGroupEntitys()
        {
            return this.GetEntitys<FirstUserGroupEntity>();
        }

        public List<FirstUserInfoEntity> GetFirstUserInfoEntitys()
        {
            return this.GetEntitys<FirstUserInfoEntity>();
        }

        public List<FirstUserInfoEntity> GetFirstUserInfoEntitysByUserGroupId(string UserGroupId)
        {
            var primary = new T_UserGroup();
            primary.UserGroupId = UserGroupId;
            return this.GetForeignEntitys<T_UserGroup, FirstUserInfoEntity>(primary);
        }

        public UserInfoEntity GetUserById(string UserId)
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
