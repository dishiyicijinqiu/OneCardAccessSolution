using System;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 附件目录表
    /// </summary>
    public class T_AttachmentDir
    {
        /// <summary>
        /// 附件目录I的
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        public string AttachmentDirId { get; set; }
        /// <summary>
        /// 附件目录编号
        /// </summary>
        public string AttachmentDirNo { get; set; }
        /// <summary>
        /// 附件目录名称
        /// </summary>
        public string AttachmentDirName { get; set; }
        /// <summary>
        /// 文件名筛选器字符串
        /// </summary>
        public string Filter { get; set; }
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
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
