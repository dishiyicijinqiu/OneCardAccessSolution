using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public class PermissionEntity : NotificationObject
    {
        private string _Id;
        [DataMember]
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                RaisePropertyChanged("Id");
            }
        }
        private string _PermissionCateId;
        [DataMember]
        public string PermissionCateId
        {
            get { return _PermissionCateId; }
            set
            {
                _PermissionCateId = value;
                RaisePropertyChanged("PermissionCateId");
            }
        }
        private string _PermissionName;
        [DataMember]
        public string PermissionName
        {
            get { return _PermissionName; }
            set
            {
                _PermissionName = value;
                RaisePropertyChanged("PermissionName");
            }
        }

        private long _PermissionId1;
        [DataMember]
        public long PermissionId1
        {
            get { return _PermissionId1; }
            set
            {
                _PermissionId1 = value;
                RaisePropertyChanged("PermissionId1");
            }
        }

        private long _PermissionId2;
        [DataMember]
        public long PermissionId2
        {
            get { return _PermissionId2; }
            set
            {
                _PermissionId2 = value;
                RaisePropertyChanged("PermissionId2");
            }
        }
        private long _PermissionId3;
        [DataMember]
        public long PermissionId3
        {
            get { return _PermissionId3; }
            set
            {
                _PermissionId3 = value;
                RaisePropertyChanged("PermissionId3");
            }
        }
        private long _PermissionId4;
        [DataMember]
        public long PermissionId4
        {
            get { return _PermissionId4; }
            set
            {
                _PermissionId4 = value;
                RaisePropertyChanged("PermissionId4");
            }
        }
        private long _PermissionId5;
        [DataMember]
        public long PermissionId5
        {
            get { return _PermissionId5; }
            set
            {
                _PermissionId5 = value;
                RaisePropertyChanged("PermissionId5");
            }
        }
        private string _Remark;
        [DataMember]
        public string Remark
        {
            get { return _Remark; }
            set
            {
                _Remark = value;
                RaisePropertyChanged("Remark");
            }
        }

    }
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public class PermissionCateEntity : NotificationObject
    {

        private string _PermissionCateId;
        [DataMember]
        public string PermissionCateId
        {
            get { return _PermissionCateId; }
            set
            {
                _PermissionCateId = value;
                RaisePropertyChanged("PermissionCateId");
            }
        }
        private string _PermissionCateName;

        [DataMember]
        public string PermissionCateName
        {
            get { return _PermissionCateName; }
            set
            {
                _PermissionCateName = value;
                RaisePropertyChanged("PermissionCateName");
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
    }
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public class UserGroup_PermissionEntity : NotificationObject
    {
        private string _UserGroupId;
        [DataMember]
        public string UserGroupId
        {
            get { return _UserGroupId; }
            set
            {
                _UserGroupId = value;
                RaisePropertyChanged("UserGroupId");
            }
        }
        private long _PermissionId1;
        [DataMember]
        public long PermissionId1
        {
            get { return _PermissionId1; }
            set
            {
                _PermissionId1 = value;
                RaisePropertyChanged("PermissionId1");
            }
        }

        private long _PermissionId2;
        [DataMember]
        public long PermissionId2
        {
            get { return _PermissionId2; }
            set
            {
                _PermissionId2 = value;
                RaisePropertyChanged("PermissionId2");
            }
        }
        private long _PermissionId3;
        [DataMember]
        public long PermissionId3
        {
            get { return _PermissionId3; }
            set
            {
                _PermissionId3 = value;
                RaisePropertyChanged("PermissionId3");
            }
        }
        private long _PermissionId4;
        [DataMember]
        public long PermissionId4
        {
            get { return _PermissionId4; }
            set
            {
                _PermissionId4 = value;
                RaisePropertyChanged("PermissionId4");
            }
        }
        private long _PermissionId5;
        [DataMember]
        public long PermissionId5
        {
            get { return _PermissionId5; }
            set
            {
                _PermissionId5 = value;
                RaisePropertyChanged("PermissionId5");
            }
        }

        public bool HasPermission(PermissionEntity permissionEntity)
        {
            bool result = (this.PermissionId1 & permissionEntity.PermissionId1) == permissionEntity.PermissionId1;
            if (!result) return false;
            if (permissionEntity.PermissionId1 < maxpermissionId)
                return result;
            result = (this.PermissionId2 & permissionEntity.PermissionId2) == permissionEntity.PermissionId2;
            if (!result) return false;
            if (permissionEntity.PermissionId2 < maxpermissionId)
                return result;
            result = (this.PermissionId3 & permissionEntity.PermissionId3) == permissionEntity.PermissionId3;
            if (!result) return false;
            if (permissionEntity.PermissionId3 < maxpermissionId)
                return result;
            result = (this.PermissionId4 & permissionEntity.PermissionId4) == permissionEntity.PermissionId4;
            if (!result) return false;
            if (permissionEntity.PermissionId4 < maxpermissionId)
                return result;
            result = (this.PermissionId5 & permissionEntity.PermissionId5) == permissionEntity.PermissionId5;
            return result;
        }
        const long maxpermissionId = 4611686018427387904;
    }
}
