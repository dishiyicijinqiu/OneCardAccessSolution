using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    /// <summary>
    /// 用户表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class UserInfoEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserInfoEntity()
        {
            UserId = string.Empty;
            UserNo = string.Empty;
            UserName = string.Empty;
            PassWord = string.Empty;
            Remark = string.Empty;
            CreateId = string.Empty;
            LastModifyId = string.Empty;
            UserGroupId = string.Empty;
        }
        private string _UserId;
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        [DataMember]
        public string UserId
        {
            get { return _UserId; }
            set
            {
                if (_UserId == value)
                    return;
                _UserId = value;
                RaisePropertyChanged("UserId");
            }
        }
        private string _UserNo;
        /// <summary>
        /// 用户账号
        /// </summary>
        [DataMember]
        public string UserNo
        {
            get { return _UserNo; }
            set
            {
                if (_UserNo == value)
                    return;
                _UserNo = value;
                RaisePropertyChanged("UserNo");
            }
        }
        private string _UserName;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (_UserName == value)
                    return;
                _UserName = value;
                RaisePropertyChanged("UserName");
            }
        }
        private string _PassWord;
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string PassWord
        {
            get { return _PassWord; }
            set
            {
                if (_PassWord == value)
                    return;
                _PassWord = value;
                RaisePropertyChanged("PassWord");
            }
        }
        private bool _IsLock;
        /// <summary>
        /// 锁定标识
        /// </summary>
        [DataMember]
        public bool IsLock
        {
            get { return _IsLock; }
            set
            {
                if (_IsLock == value)
                    return;
                _IsLock = value;
                RaisePropertyChanged("IsLock");
            }
        }
        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark == value)
                    return;
                _Remark = value;
                RaisePropertyChanged("Remark");
            }
        }
        private string _CreateId;
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DataMember]
        public string CreateId
        {
            get { return _CreateId; }
            set
            {
                if (_CreateId == value)
                    return;
                _CreateId = value;
                RaisePropertyChanged("CreateId");
            }
        }
        private DateTime _CreateDate;
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate == value)
                    return;
                _CreateDate = value;
                RaisePropertyChanged("CreateDate");
            }
        }
        private string _LastModifyId;
        /// <summary>
        /// 最后更改人Id
        /// </summary>
        [DataMember]
        public string LastModifyId
        {
            get { return _LastModifyId; }
            set
            {
                if (_LastModifyId == value)
                    return;
                _LastModifyId = value;
                RaisePropertyChanged("LastModifyId");
            }
        }
        private DateTime _LastModifyDate;
        /// <summary>
        /// 最后更改日期
        /// </summary>
        [DataMember]
        public DateTime LastModifyDate
        {
            get { return _LastModifyDate; }
            set
            {
                if (_LastModifyDate == value)
                    return;
                _LastModifyDate = value;
                RaisePropertyChanged("LastModifyDate");
            }
        }
        private string _UserGroupId;
        /// <summary>
        /// 用户组Id
        /// </summary>
        [DataMember]
        public string UserGroupId
        {
            get { return _UserGroupId; }
            set
            {
                if (_UserGroupId == value)
                    return;
                _UserGroupId = value;
                RaisePropertyChanged("UserGroupId");
            }
        }
    }
}
