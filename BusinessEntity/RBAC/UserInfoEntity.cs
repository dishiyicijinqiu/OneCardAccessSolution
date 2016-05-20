using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    [KnownType(typeof(FirstUserInfoEntity))]
    public partial class UserInfoEntity
    {
        public static UserInfoEntity CreateEntity()
        {
            return new UserInfoEntity();
        }
    }

    public class FirstUserInfoEntity : UserInfoEntity
    {
        public new static FirstUserInfoEntity CreateEntity()
        {
            return new FirstUserInfoEntity()
            {
                Creater = string.Empty,
                LastModifyer = string.Empty,
                UserGroupNo = string.Empty,
                UserGroupName = string.Empty,
            };
        }
        private string _UserGroupNo;
        [DataMember]
        public string UserGroupNo
        {
            get { return _UserGroupNo; }
            set
            {
                _UserGroupNo = value;
                RaisePropertyChanged("UserGroupNo");
            }
        }
        private string _UserGroupName;
        [DataMember]
        public string UserGroupName
        {
            get { return _UserGroupName; }
            set
            {
                _UserGroupName = value;
                RaisePropertyChanged("UserGroupName");
            }
        }
        private string _Creater;
        [DataMember]
        public string Creater
        {
            get { return _Creater; }
            set
            {
                _Creater = value;
                RaisePropertyChanged("Creater");
            }
        }
        private string _LastModifyer;
        [DataMember]
        public string LastModifyer
        {
            get { return _LastModifyer; }
            set
            {
                _LastModifyer = value;
                RaisePropertyChanged("LastModifyer");
            }
        }
    }
}
