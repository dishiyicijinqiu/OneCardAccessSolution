using System;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.TEntity
{
	/// <summary>
	/// 用户表
	/// </summary>
	public class T_UserInfo
	{
		/// <summary>
		/// 用户Id
		/// </summary>
		[DataBaseKey(DataBaseKeyType.Guid)]
		public string UserId { get; set; }
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
		/// <summary>
		/// 用户组Id
		/// </summary>
		public string UserGroupId { get; set; }
	}
}
