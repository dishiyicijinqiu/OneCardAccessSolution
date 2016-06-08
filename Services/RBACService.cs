using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using FengSharp.OneCardAccess.BusinessEntity;
using System.Security.Cryptography;

namespace FengSharp.OneCardAccess.Services
{
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class RBACService : ServiceBase, IRBACService
    {
        #region User
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

        public FirstUserInfoEntity GetFirstUserInfoEntityById(string UserId)
        {
            return this.FindById<FirstUserInfoEntity>(new FirstUserInfoEntity()
            {
                UserId = UserId
            });
        }
        public string SaveUserEntity(UserInfoEntity entity)
        {

            return UseTran((tran) =>
            {
                var dbusergroupentity = new T_UserInfo();
                dbusergroupentity.CopyValueFrom(entity);
                if (string.IsNullOrWhiteSpace(entity.UserId) || entity.UserId.Length != 36)
                {
                    #region 服务端验证
                    if (string.IsNullOrWhiteSpace(entity.UserNo))
                    {
                        throw new BusinessException(Properties.Resources.Error_NoCanNotEmpty);
                    }
                    if (string.IsNullOrWhiteSpace(entity.UserName))
                    {
                        throw new BusinessException(Properties.Resources.Error_UserGroupNameCanNotEmpty);
                    }

                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_User");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "CreateEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, null);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.UserNo);
                    Database.AddInParameter(cmd, "UserGroupId", DbType.String, entity.UserGroupId);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result != 1)
                    {
                        if (result == -1)
                        {
                            throw new BusinessException(Properties.Resources.Error_NoIsExist);
                        }
                        else if (result == -2)
                        {
                            throw new BusinessException(Properties.Resources.Error_UserGroupIsUnable);
                        }
                        else
                        {
                            throw new BusinessException(Properties.Resources.Error_UnHandleException);
                        }
                    }
                    #endregion
                    dbusergroupentity.LastModifyId = dbusergroupentity.CreateId = (string)Session.Current.SessionClientId;
                    dbusergroupentity = CreateEntity(dbusergroupentity, tran, CreateTreeEntityUnCreateFileds);
                    result = this.SaveEntityFlow(dbusergroupentity, "CreateUserGroupEntity", tran);
                    if (result != 1)
                    {
                        throw new BusinessException(string.Format(Properties.Resources.Error_AddFailed, dbusergroupentity.UserName));
                    }
                }
                else
                {
                    #region 服务端验证
                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_User");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "ModifyEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, entity.UserId);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.UserNo);
                    Database.AddInParameter(cmd, "UserGroupId", DbType.String, entity.UserGroupId);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result != 1)
                    {
                        if (result == -1)
                        {
                            throw new BusinessException(Properties.Resources.Error_NoIsExist);
                        }
                        else if (result == -2)
                        {
                            throw new BusinessException(Properties.Resources.Error_UserGroupIsUnable);
                        }
                        else
                        {
                            throw new BusinessException(Properties.Resources.Error_UnHandleException);
                        }
                    }
                    #endregion
                    dbusergroupentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbusergroupentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbusergroupentity, tran, ModifyTreeEntityUnChangedFileds))
                    {
                        throw new BusinessException(FengSharp.OneCardAccess.Services.Properties.Resources.Error_SaveFailed);
                    }
                }
                return dbusergroupentity.UserId;
            });
        }
        public void DeleteUserEntitys(List<UserInfoEntity> UserInfoEntitys)
        {
            UseTran((tran) =>
            {
                foreach (var entity in UserInfoEntitys)
                {
                    var dbentity = new T_UserInfo();
                    dbentity.CopyValueFrom(entity);
                    int ReturnValue = 0;
                    base.DeleteEntity(dbentity, ref ReturnValue, tran);
                    switch (ReturnValue)
                    {
                        default:
                            throw new Exception(Properties.Resources.Error_UnHandleException);
                        case 1:
                            break;
                        case -1:
                            throw new BusinessException(string.Format(Properties.Resources.Error_ObjIsNotExist,
                                string.Format("{0},{1}", entity.UserNo, entity.UserName)));
                    }
                }
            });
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length <= 3 || newPassword.Length > 20)
            {
                throw new BusinessException(string.Format(Properties.Resources.Error_PasswordLen, 4, 20));
            }
            base.UseTran((tran) =>
            {
                var cmd = this.Database.GetStoredProcCommand("P_Gen_ChangePassword");
                Database.AddInParameter(cmd, "EntityId", DbType.String, Session.Current.SessionClientId);
                Database.AddInParameter(cmd, "OldPassword", DbType.String, MD5Encrypt.Encrypt(oldPassword));
                Database.AddInParameter(cmd, "NewPassword", DbType.String, MD5Encrypt.Encrypt(newPassword));
                Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                Database.ExecuteNonQuery(cmd);
                int ReturnValue = (int)Database.GetParameterValue(cmd, "ReturnValue");
                if (ReturnValue != 1)
                {
                    switch (ReturnValue)
                    {
                        default:
                            throw new BusinessException(Properties.Resources.Error_UnHandleException);
                        case -1:
                            throw new BusinessException(Properties.Resources.Error_OldPassword);
                    }
                }
            });
        }
        #endregion

        #region UserGroup
        public FirstUserGroupEntity GetFirstUserGroupEntityById(string UserGroupId)
        {
            return this.FindById<FirstUserGroupEntity>(new FirstUserGroupEntity()
            {
                UserGroupId = UserGroupId
            });
        }

        public List<FirstUserGroupEntity> GetFirstUserGroupEntitys()
        {
            return this.GetEntitys<FirstUserGroupEntity>();
        }

        public string SaveUserGroupEntity(UserGroupEntity entity)
        {
            return UseTran((tran) =>
            {
                var dbusergroupentity = new T_UserGroup();
                dbusergroupentity.CopyValueFrom(entity);
                if (string.IsNullOrWhiteSpace(entity.UserGroupId) || entity.UserGroupId.Length != 36)
                {
                    #region 服务端验证
                    if (string.IsNullOrWhiteSpace(entity.UserGroupNo))
                    {
                        throw new BusinessException(Properties.Resources.Error_NoCanNotEmpty);
                    }
                    if (string.IsNullOrWhiteSpace(entity.UserGroupName))
                    {
                        throw new BusinessException(Properties.Resources.Error_UserGroupNameCanNotEmpty);
                    }

                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_UserGroup");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "CreateEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, null);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.UserGroupNo);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result != 1)
                    {
                        if (result == -1)
                        {
                            throw new BusinessException(Properties.Resources.Error_NoIsExist);
                        }
                        else
                        {
                            throw new BusinessException(Properties.Resources.Error_UnHandleException);
                        }
                    }
                    #endregion
                    dbusergroupentity.LastModifyId = dbusergroupentity.CreateId = (string)Session.Current.SessionClientId;
                    dbusergroupentity = CreateEntity(dbusergroupentity, tran, CreateTreeEntityUnCreateFileds);
                    result = this.SaveEntityFlow(dbusergroupentity, "CreateUserGroupEntity", tran);
                    if (result != 1)
                    {
                        if (result == -1)
                        {
                            throw new BusinessException(Properties.Resources.Error_FatherNotExist);
                        }
                        if (result == -2)
                        {
                            throw new BusinessException(Properties.Resources.Error_FatherIsUsingCanNotChild);
                        }
                        throw new BusinessException(string.Format(Properties.Resources.Error_AddFailed, dbusergroupentity.UserGroupName));
                    }
                }
                else
                {
                    #region 服务端验证
                    if (string.IsNullOrWhiteSpace(entity.UserGroupNo))
                    {
                        throw new BusinessException(Properties.Resources.Error_NoCanNotEmpty);
                    }
                    if (string.IsNullOrWhiteSpace(entity.UserGroupName))
                    {
                        throw new BusinessException(Properties.Resources.Error_UserGroupNameCanNotEmpty);
                    }

                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_UserGroup");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "ModifyEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, entity.UserGroupId);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.UserGroupNo);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result != 1)
                    {
                        if (result == -1)
                        {
                            throw new BusinessException(Properties.Resources.Error_NoIsExist);
                        }
                        else
                        {
                            throw new BusinessException(Properties.Resources.Error_UnHandleException);
                        }
                    }
                    #endregion
                    dbusergroupentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbusergroupentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbusergroupentity, tran, new List<string>(ModifyTreeEntityUnChangedFileds) { "IsSuper" }))
                    {
                        throw new BusinessException(Properties.Resources.Error_SaveFailed);
                    }
                }
                return dbusergroupentity.UserGroupId;
            });
        }

        public void DeleteUserGroupEntitys(List<UserGroupEntity> UserGroupEntitys)
        {
            UseTran((tran) =>
            {
                foreach (var entity in UserGroupEntitys)
                {
                    var dbentity = new T_UserGroup();
                    dbentity.CopyValueFrom(entity);
                    int ReturnValue = 0;
                    base.DeleteEntity(dbentity, ref ReturnValue, tran);
                    switch (ReturnValue)
                    {
                        default:
                            throw new BusinessException(Properties.Resources.Error_UnHandleException);
                        case 1:
                            break;
                        case -1:
                            throw new BusinessException(string.Format(Properties.Resources.Error_ObjIsNotExist,
                                string.Format("{0},{1}", entity.UserGroupNo, entity.UserGroupName)));
                        case -2:
                            throw new BusinessException(string.Format(Properties.Resources.Error_IsUsing,
                                string.Format("{0},{1}", entity.UserGroupNo, entity.UserGroupName)));
                        case -101:
                            throw new BusinessException(Properties.Resources.Error_SuperUserGroupCanNotDelete);
                    }
                }
            });
        }
        public bool MoveUserGroup(string sourceId, string targetId, MoveTree movetree)
        {
            return UseTran((tran) =>
             {
                 int ReturnValue = base.MoveTree("UserGroup", sourceId, targetId, movetree, tran);
                 switch (ReturnValue)
                 {
                     default:
                         throw new BusinessException(Properties.Resources.Error_UnHandleException);
                     case 1:
                         break;
                     case -200:
                         throw new BusinessException(Properties.Resources.Error_MethodNotImplemented);
                 }
                 return true;
             });
        }
        #endregion
    }
}
