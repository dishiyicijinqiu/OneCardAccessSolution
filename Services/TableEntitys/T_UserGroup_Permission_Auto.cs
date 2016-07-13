using FengSharp.OneCardAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 用户组权限
    /// </summary>
    public class T_UserGroup_Permission
    {
        /// <summary>
        /// UserGroupId
        /// </summary>
        public string UserGroupId { get; set; }
        /// <summary>
        /// PermissionId1
        /// </summary>
        public long PermissionId1 { get; set; }
        /// <summary>
        /// PermissionId2
        /// </summary>
        public long PermissionId2 { get; set; }
        /// <summary>
        /// PermissionId3
        /// </summary>
        public long PermissionId3 { get; set; }
        /// <summary>
        /// PermissionId4
        /// </summary>
        public long PermissionId4 { get; set; }
        /// <summary>
        /// PermissionId5
        /// </summary>
        public long PermissionId5 { get; set; }
    }
}
