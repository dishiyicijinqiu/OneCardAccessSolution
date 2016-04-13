using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
	/// <summary>
	/// 检验报告模板
	/// </summary>
	[DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
	public partial class CheckReportTempEntity
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public CheckReportTempEntity()
		{
			TemplateName=string.Empty;
			TemplatePath=string.Empty;
			MaterIden=string.Empty;
			Remark=string.Empty;
			CateNo=string.Empty;
		}
		/// <summary>
		/// Id
		/// </summary>
		[DataBaseKey]
		[DataMember]
		public int Id { get; set; }
		/// <summary>
		/// 模板名称
		/// </summary>
		[DataMember]
		public string TemplateName { get; set; }
		/// <summary>
		/// 路径
		/// </summary>
		[DataMember]
		public string TemplatePath { get; set; }
		/// <summary>
		/// 材料标识
		/// </summary>
		[DataMember]
		public string MaterIden { get; set; }
		/// <summary>
		/// 是否锁定
		/// </summary>
		[DataMember]
		public bool IsLock { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		[DataMember]
		public string Remark { get; set; }
		/// <summary>
		/// 类别号
		/// </summary>
		[DataMember]
		public string CateNo { get; set; }
	}
}
