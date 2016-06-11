using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    /// <summary>
    /// 用户组
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class UserGroupEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserGroupEntity()
        {
            UserGroupId = string.Empty;
            UserGroupNo = string.Empty;
            UserGroupName = string.Empty;
            Remark = string.Empty;
            TreeNo = string.Empty;
            TreeParentNo = string.Empty;
            TreePath = string.Empty;
            CreateId = string.Empty;
            LastModifyId = string.Empty;
        }
        private string _UserGroupId;
        /// <summary>
        /// UserGroupId
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
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
        private string _UserGroupNo;
        /// <summary>
        /// 用户组编号
        /// </summary>
        [DataMember]
        public string UserGroupNo
        {
            get { return _UserGroupNo; }
            set
            {
                if (_UserGroupNo == value)
                    return;
                _UserGroupNo = value;
                RaisePropertyChanged("UserGroupNo");
            }
        }
        private string _UserGroupName;
        /// <summary>
        /// 用户组名称
        /// </summary>
        [DataMember]
        public string UserGroupName
        {
            get { return _UserGroupName; }
            set
            {
                if (_UserGroupName == value)
                    return;
                _UserGroupName = value;
                RaisePropertyChanged("UserGroupName");
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
        private string _TreeNo;
        /// <summary>
        /// 树编号
        /// </summary>
        [DataMember]
        public string TreeNo
        {
            get { return _TreeNo; }
            set
            {
                if (_TreeNo == value)
                    return;
                _TreeNo = value;
                RaisePropertyChanged("TreeNo");
            }
        }
        private string _TreeParentNo;
        /// <summary>
        /// 父亲树编号
        /// </summary>
        [DataMember]
        public string TreeParentNo
        {
            get { return _TreeParentNo; }
            set
            {
                if (_TreeParentNo == value)
                    return;
                _TreeParentNo = value;
                RaisePropertyChanged("TreeParentNo");
            }
        }
        private string _TreePath;
        /// <summary>
        /// 树路径
        /// </summary>
        [DataMember]
        public string TreePath
        {
            get { return _TreePath; }
            set
            {
                if (_TreePath == value)
                    return;
                _TreePath = value;
                RaisePropertyChanged("TreePath");
            }
        }
        private int _TreeSon;
        /// <summary>
        /// 儿子数量
        /// </summary>
        [DataMember]
        public int TreeSon
        {
            get { return _TreeSon; }
            set
            {
                if (_TreeSon == value)
                    return;
                _TreeSon = value;
                RaisePropertyChanged("TreeSon");
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
    }
}
