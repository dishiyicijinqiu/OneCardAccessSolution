using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using DevExpress.Mvvm;
namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    /// <summary>
    /// 用户表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class UserInfoEntity : BindableBase
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
        private int _UserId;
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataBaseKey]
        [DataMember]
        public int UserId
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
        private bool _IsSuper;
        /// <summary>
        /// 是否为超级管理员
        /// </summary>
        [DataMember]
        public bool IsSuper
        {
            get { return _IsSuper; }
            set
            {
                if (_IsSuper == value)
                    return;
                _IsSuper = value;
                RaisePropertyChanged("IsSuper");
            }
        }
    }
}
