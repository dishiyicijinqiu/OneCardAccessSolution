using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Services
{
    public class RBACService : ServiceBase, IRBACService
    {
        public LoginResult Login(string UserNo, string PassWord)
        {
            var dbentity = this.FindByNo<T_UserInfo>(UserNo);
            if (dbentity == null)
                return new LoginResult() { LoginMessage = LoginMessage.UserNotExist };
            if (dbentity.PassWord != PassWord)
                return new LoginResult() { LoginMessage = LoginMessage.ErrorPassWord };
            if (dbentity.IsLock)
                return new LoginResult() { LoginMessage = LoginMessage.UserIsLocked };
            return new LoginResult()
            {
                LoginMessage = LoginMessage.Success,
                UserIdentity = new UserIdentity()
                {
                    UserId = dbentity.UserId,
                    UserNo = dbentity.UserNo,
                    UserName = dbentity.UserName
                }
            };
        }
    }
}
