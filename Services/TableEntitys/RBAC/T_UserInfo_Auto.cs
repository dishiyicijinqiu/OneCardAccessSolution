
using System;
using System.ComponentModel.DataAnnotations;
namespace FengSharp.OneCardAccess.TEntity.RBAC
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class T_UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserNo { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
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
    }
}
