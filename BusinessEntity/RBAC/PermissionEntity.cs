using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    public class PermissionEntity : NotificationObject
    {
        private string _Id;

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
}
