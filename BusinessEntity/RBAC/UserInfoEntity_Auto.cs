using FengSharp.OneCardAccess.Common;
using System;
using System.Runtime.Serialization;
namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    /// <summary>
    /// 用户表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public class UserInfoEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoEntity()
        {
            UserNo = string.Empty;
            UserName = string.Empty;
            PassWord = string.Empty;
            Remark = string.Empty;
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        [DataBaseKey]
        public int UserId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        [DataMember]
        public string UserNo { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string PassWord { get; set; }
        /// <summary>
        /// 锁定标识
        /// </summary>
        [DataMember]
        public bool IsLock { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        [DataMember]
        public bool IsSuper { get; set; }
    }
}
