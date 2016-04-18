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
        public LoginResult Login(string UserNo, string PassWord)
        {
            var dbentity = this.FindByNo<T_UserInfo>(UserNo);
            if (dbentity == null)
                return LoginResult.UserNotExist;
            if (dbentity.PassWord != PassWord)
                return LoginResult.ErrorPassWord;
            if (dbentity.IsLock)
                return LoginResult.UserIsLocked;
            var session = new Session(Guid.NewGuid().ToString(), dbentity.UserId, dbentity.UserNo, dbentity.UserName);
            SessionState.JoinSession(session.Ticket, session);
            Session.Current = session;
            return LoginResult.Success;
        }
    }
}
