using FengSharp.OneCardAccess.Common;
using System;
using System.ComponentModel.DataAnnotations;
namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 注册证附件表
    /// </summary>
    public class T_Register_File
    {
        /// <summary>
        /// 注册证附件Id
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        public string Id { get; set; }
        /// <summary>
        /// 注册证Id
        /// </summary>
        public string RegisterId { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo { get; set; }
    }
}
