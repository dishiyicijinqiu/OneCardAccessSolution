using System;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.TEntity
{
	/// <summary>
	/// 产品检验报告模板
	/// </summary>
	public class T_P_CRTemp
	{
        /// <summary>
        /// P_CRTempId
        /// </summary>
        [DataBaseKey]
		public int P_CRTempId { get; set; }
		/// <summary>
		/// 模板名称
		/// </summary>
		public string CRTempName { get; set; }
		/// <summary>
		/// 路径
		/// </summary>
		public string CRTempPath { get; set; }
		/// <summary>
		/// 材料标识
		/// </summary>
		public string MaterIden { get; set; }
		/// <summary>
		/// 是否启用
		/// </summary>
		public bool IsEnable { get; set; }
		/// <summary>
		/// 类别号
		/// </summary>
		public string CateNo { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		/// 创建人Id
		/// </summary>
		public int CreateId { get; set; }
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime CreateDate { get; set; }
		/// <summary>
		/// 最后更改人Id
		/// </summary>
		public int LastModifyId { get; set; }
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
