using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 附件目录表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class AttachmentDirEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AttachmentDirEntity()
        {
            AttachmentDirId = string.Empty;
            AttachmentDirNo = string.Empty;
            AttachmentDirName = string.Empty;
            Filter = string.Empty;
            TreeNo = string.Empty;
            TreeParentNo = string.Empty;
            TreePath = string.Empty;
            Remark = string.Empty;
            CreateId = string.Empty;
            LastModifyId = string.Empty;
        }
        private string _AttachmentDirId;
        /// <summary>
        /// 附件目录Id
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        [DataMember]
        public string AttachmentDirId
        {
            get { return _AttachmentDirId; }
            set
            {
                if (_AttachmentDirId == value)
                    return;
                _AttachmentDirId = value;
                RaisePropertyChanged("AttachmentDirId");
            }
        }
        private string _AttachmentDirNo;
        /// <summary>
        /// 附件目录编号
        /// </summary>
        [DataMember]
        public string AttachmentDirNo
        {
            get { return _AttachmentDirNo; }
            set
            {
                if (_AttachmentDirNo == value)
                    return;
                _AttachmentDirNo = value;
                RaisePropertyChanged("AttachmentDirNo");
            }
        }
        private string _AttachmentDirName;
        /// <summary>
        /// 附件目录名称
        /// </summary>
        [DataMember]
        public string AttachmentDirName
        {
            get { return _AttachmentDirName; }
            set
            {
                if (_AttachmentDirName == value)
                    return;
                _AttachmentDirName = value;
                RaisePropertyChanged("AttachmentDirName");
            }
        }
        private string _Filter;
        /// <summary>
        /// 文件名筛选器字符串
        /// </summary>
        [DataMember]
        public string Filter
        {
            get { return _Filter; }
            set
            {
                if (_Filter == value)
                    return;
                _Filter = value;
                RaisePropertyChanged("Filter");
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
    }
}
