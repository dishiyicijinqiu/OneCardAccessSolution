using System;
using System.Runtime.Serialization;
namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
	/// <summary>
	/// 注册证附件表
	/// </summary>
	[DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
	public class Register_FileEntity
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public Register_FileEntity()
		{
			Id=string.Empty;
			FileName=string.Empty;
			FilePath=string.Empty;
		}
		/// <summary>
		/// 注册证附件Id
		/// </summary>
		[DataMember]
		public string Id { get; set; }
		/// <summary>
		/// 注册证Id
		/// </summary>
		[DataMember]
		public int RegisterId { get; set; }
		/// <summary>
		/// 文件名
		/// </summary>
		[DataMember]
		public string FileName { get; set; }
		/// <summary>
		/// 保存路径
		/// </summary>
		[DataMember]
		public string FilePath { get; set; }
		/// <summary>
		/// 序号
		/// </summary>
		[DataMember]
		public int SortNo { get; set; }
	}
}
