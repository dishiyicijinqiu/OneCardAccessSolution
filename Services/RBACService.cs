using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Services
{
    public class RBACService : IRBACService
    {
        public LoginResult Login(string UserNo, string PassWord)
        {
            using (OneCardAccessDbContext db = new OneCardAccessDbContext())
            {
                var user = db.T_UserInfos.FirstOrDefault(t => t.UserNo == UserNo);
                if (user == null)
                    return new LoginResult() { LoginMessage = LoginMessage.UserNotExist };
                if (user.PassWord != PassWord)
                    return new LoginResult() { LoginMessage = LoginMessage.ErrorPassWord };
                if (user.IsLock)
                    return new LoginResult() { LoginMessage = LoginMessage.UserIsLocked };
                return new LoginResult()
                {
                    LoginMessage = LoginMessage.Success,
                    UserIdentity = new UserIdentity()
                    {
                        UserId = user.UserId,
                        UserNo = user.UserNo,
                        UserName = user.UserName
                    }
                };
            }
        }
    }
}
