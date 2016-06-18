using FengSharp.OneCardAccess.BusinessEntity;
using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using System.Collections.Generic;
using System.ServiceModel;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IRBACService”。
    [ServiceContract]
    public interface IRBACService
    {
        #region UserInfo
        [OperationContract]
        FirstUserInfoEntity GetFirstUserInfoEntityById(string UserId);
        [OperationContract]
        List<FirstUserInfoEntity> GetFirstUserInfoEntitys();
        [OperationContract]
        List<FirstUserInfoEntity> GetFirstUserInfoEntitysByUserGroupId(string UserGroupId);
        [OperationContract]
        void ChangePassword(string oldPassword, string newPassword);
        [OperationContract]
        string SaveUserEntity(UserInfoEntity entity);
        [OperationContract]
        void DeleteUserEntitys(List<UserInfoEntity> UserInfoEntitys);
        #endregion
        #region UserGroup
        [OperationContract]
        List<FirstUserGroupEntity> GetFirstUserGroupEntitys();
        [OperationContract]
        FirstUserGroupEntity GetFirstUserGroupEntityById(string UserGroupId);
        [OperationContract]
        string SaveUserGroupEntity(UserGroupEntity entity);
        [OperationContract]
        void DeleteUserGroupEntitys(List<UserGroupEntity> list);
        [OperationContract]
        bool MoveUserGroup(string sourceId, string targetId, MoveTree movetree);
        #endregion
        #region Permission
        [OperationContract]
        List<PermissionCateEntity> GetPermissionCateEntitys();
        [OperationContract]
        List<PermissionEntity> GetPermissionEntitys();
        [OperationContract]
        UserGroup_PermissionEntity GetUserGroupPermissionByUserGroupId(string userGroupId);
        [OperationContract]
        void SaveUserGroup_PermissionEntity(UserGroup_PermissionEntity userGroup_PermissionEntity);
        #endregion
    }
}
