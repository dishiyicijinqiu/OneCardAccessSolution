using FengSharp.OneCardAccess.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 注册证信息
    /// </summary>
    public class T_Register
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DataBaseKey]
        public string RegisterId { get; set; }
        /// <summary>
        /// 注册证编号
        /// </summary>
        public string RegisterNo { get; set; }
        /// <summary>
        /// 注册证名称
        /// </summary>
        public string RegisterName { get; set; }
        /// <summary>
        /// 标准号
        /// </summary>
        public string StandardCode { get; set; }
        /// <summary>
        /// 注册证编号(英文)
        /// </summary>
        public string RegisterNo1 { get; set; }
        /// <summary>
        /// 注册证名称(英文)
        /// </summary>
        public string RegisterName1 { get; set; }
        /// <summary>
        /// 标准号(英文)
        /// </summary>
        public string StandardCode1 { get; set; }
        /// <summary>
        /// 启用日期
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 停用日期
        /// </summary>
        public string EndDate { get; set; }
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public bool Deleted { get; set; }
    }
}
