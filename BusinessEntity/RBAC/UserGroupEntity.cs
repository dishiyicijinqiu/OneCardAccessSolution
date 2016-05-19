using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    public partial class UserGroupEntity
    {
        public static UserGroupEntity CreateEntity()
        {
            return new UserGroupEntity();
        }
    }
    public class FirstUserGroupEntity : UserGroupEntity
    {
        public new static FirstUserGroupEntity CreateEntity()
        {
            return new FirstUserGroupEntity()
            {
                Creater = string.Empty,
                LastModifyer = string.Empty,
            };
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
