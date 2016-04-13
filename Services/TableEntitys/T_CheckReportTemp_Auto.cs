using System;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.TEntity
{
	/// <summary>
	/// 检验报告模板
	/// </summary>
	public class T_CheckReportTemp
	{
		/// <summary>
		/// Id
		/// </summary>
		[DataBaseKey]
		public int Id { get; set; }
		/// <summary>
		/// 模板名称
		/// </summary>
		public string TemplateName { get; set; }
		/// <summary>
		/// 路径
		/// </summary>
		public string TemplatePath { get; set; }
		/// <summary>
		/// 材料标识
		/// </summary>
		public string MaterIden { get; set; }
		/// <summary>
		/// 是否锁定
		/// </summary>
		public bool IsLock { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		/// 类别号
		/// </summary>
		public string CateNo { get; set; }
	}
}
