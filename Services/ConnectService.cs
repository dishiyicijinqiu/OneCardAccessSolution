using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System;

namespace FengSharp.OneCardAccess.Services
{
    public class ConnectService : ServiceBase, IConnectService
    {
        public LoginResult Login(string UserNo, string PassWord)
        {
            if (string.IsNullOrWhiteSpace(UserNo))
                return LoginResult.UserIsEmpty;
            var dbentity = this.FindByNo<T_UserInfo>(UserNo);
            if (dbentity == null)
                return LoginResult.UserNotExist;
            if (dbentity.PassWord != MD5Encrypt.Encrypt(PassWord))
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
