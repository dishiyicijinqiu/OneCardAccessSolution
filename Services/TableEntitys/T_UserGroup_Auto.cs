using System;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 用户组
    /// </summary>
    public class T_UserGroup
    {
        /// <summary>
        /// UserGroupId
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        public string UserGroupId { get; set; }
        /// <summary>
        /// 用户组编号
        /// </summary>
        public string UserGroupNo { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string UserGroupName { get; set; }
        /// <summary>
        /// 锁定标识
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        public bool IsSuper { get; set; }
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeNo { get; set; }
        /// <summary>
        /// 父亲树编号
        /// </summary>
        public string TreeParentNo { get; set; }
        /// <summary>
        /// 树路径
        /// </summary>
        public string TreePath { get; set; }
        /// <summary>
        /// 儿子数量
        /// </summary>
        public int TreeSon { get; set; }
        /// <summary>
        /// 子孙数量
        /// </summary>
        public int TreeTotal { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更改人Id
        /// </summary>
        public string LastModifyId { get; set; }
        /// <summary>
        /// 最后更改日期
        /// </summary>
        public DateTime LastModifyDate { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public bool Deleted { get; set; }
    }
}
