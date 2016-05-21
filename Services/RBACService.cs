using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace FengSharp.OneCardAccess.Services
{
    public class RBACService : ServiceBase, IRBACService
    {
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
                    if (result == 1)
                    {
                        throw new BusinessException(Properties.Resources.Error_NoIsExist);
                    }
                    #endregion
                    dbusergroupentity.LastModifyId = dbusergroupentity.CreateId = (string)Session.Current.SessionClientId;
                    dbusergroupentity = CreateEntity(dbusergroupentity, tran, CreateTreeEntityUnCreateFileds);
                    result = this.SaveEntityFlow(dbusergroupentity, "CreateUserGroupEntity", tran);
                    if (result != 1)
                    {
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
                    if (result == 1)
                    {
                        throw new BusinessException(Properties.Resources.Error_NoIsExist);
                    }
                    #endregion
                    dbusergroupentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbusergroupentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbusergroupentity, tran, new List<string>(ModifyTreeEntityUnChangedFileds) { "IsSuper" }))
                    {
                        throw new BusinessException(FengSharp.OneCardAccess.Services.Properties.Resources.Error_SaveFailed);
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
                            throw new Exception(Properties.Resources.Error_UnHandleException);
                        case 1:
                            break;
                        case -1:
                            throw new BusinessException(string.Format(Properties.Resources.Error_ObjIsNotExist,
                                string.Format("{0},{1}", entity.UserGroupNo, entity.UserGroupName)));
                        case -2:
                            throw new BusinessException(string.Format(Properties.Resources.Error_IsUsing,
                                string.Format("{0},{1}", entity.UserGroupNo, entity.UserGroupName)));
                        case -101:
                            throw new Exception(Properties.Resources.Error_SuperUserGroupCanNotDelete);
                    }
                }
            });
        }
        #endregion

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
