using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Common;
using System.Collections.Generic;
using System.ServiceModel;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IRBACService”。
    [ServiceContract]
    public interface IRBACService
    {
        [OperationContract]
        UserInfoEntity GetUserById(string UserId);
        [OperationContract]
        List<FirstUserGroupEntity> GetFirstUserGroupEntitys();
        [OperationContract]
        List<FirstUserInfoEntity> GetFirstUserInfoEntitys();
        [OperationContract]
        List<FirstUserInfoEntity> GetFirstUserInfoEntitysByUserGroupId(string UserGroupId);
        [OperationContract]
        FirstUserGroupEntity GetFirstUserGroupEntityById(string UserGroupId);
        [OperationContract]
        string SaveUserGroupEntity(UserGroupEntity entity);
        [OperationContract]
        void DeleteUserGroupEntitys(List<UserGroupEntity> list);
    }
}
