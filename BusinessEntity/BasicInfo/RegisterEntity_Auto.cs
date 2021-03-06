using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 注册证信息
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class RegisterEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RegisterEntity()
        {
            RegisterNo = string.Empty;
            RegisterName = string.Empty;
            StandardCode = string.Empty;
            RegisterNo1 = string.Empty;
            RegisterName1 = string.Empty;
            StandardCode1 = string.Empty;
            StartDate = string.Empty; ;
            EndDate = string.Empty;
            Remark = string.Empty;
        }
        private string _RegisterId;
        /// <summary>
        /// 主键Id
        /// </summary>
        [DataBaseKey]
        [DataMember]
        public string RegisterId
        {
            get { return _RegisterId; }
            set
            {
                if (_RegisterId == value)
                    return;
                _RegisterId = value;
                RaisePropertyChanged("RegisterId");
            }
        }
        private string _RegisterNo;
        /// <summary>
        /// 注册证编号
        /// </summary>
        [DataMember]
        public string RegisterNo
        {
            get { return _RegisterNo; }
            set
            {
                if (_RegisterNo == value)
                    return;
                _RegisterNo = value;
                RaisePropertyChanged("RegisterNo");
            }
        }
        private string _RegisterName;
        /// <summary>
        /// 注册证名称
        /// </summary>
        [DataMember]
        public string RegisterName
        {
            get { return _RegisterName; }
            set
            {
                if (_RegisterName == value)
                    return;
                _RegisterName = value;
                RaisePropertyChanged("RegisterName");
            }
        }
        private string _StandardCode;
        /// <summary>
        /// 标准号
        /// </summary>
        [DataMember]
        public string StandardCode
        {
            get { return _StandardCode; }
            set
            {
                if (_StandardCode == value)
                    return;
                _StandardCode = value;
                RaisePropertyChanged("StandardCode");
            }
        }
        private string _RegisterNo1;
        /// <summary>
        /// 注册证编号(英文)
        /// </summary>
        [DataMember]
        public string RegisterNo1
        {
            get { return _RegisterNo1; }
            set
            {
                if (_RegisterNo1 == value)
                    return;
                _RegisterNo1 = value;
                RaisePropertyChanged("RegisterNo1");
            }
        }
        private string _RegisterName1;
        /// <summary>
        /// 注册证名称(英文)
        /// </summary>
        [DataMember]
        public string RegisterName1
        {
            get { return _RegisterName1; }
            set
            {
                if (_RegisterName1 == value)
                    return;
                _RegisterName1 = value;
                RaisePropertyChanged("RegisterName1");
            }
        }
        private string _StandardCode1;
        /// <summary>
        /// 标准号(英文)
        /// </summary>
        [DataMember]
        public string StandardCode1
        {
            get { return _StandardCode1; }
            set
            {
                if (_StandardCode1 == value)
                    return;
                _StandardCode1 = value;
                RaisePropertyChanged("StandardCode1");
            }
        }
        private string _StartDate;
        /// <summary>
        /// 启用日期
        /// </summary>
        [DataMember]
        public string StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate == value)
                    return;
                _StartDate = value;
                RaisePropertyChanged("StartDate");
            }
        }
        private string _EndDate;
        /// <summary>
        /// 停用日期
        /// </summary>
        [DataMember]
        public string EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate == value)
                    return;
                _EndDate = value;
                RaisePropertyChanged("EndDate");
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
    }
}
