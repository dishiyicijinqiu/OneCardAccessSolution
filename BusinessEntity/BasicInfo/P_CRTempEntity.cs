using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    public partial class P_CRTempEntity
    {
        public static P_CRTempEntity CreateEntity()
        {
            return new P_CRTempEntity();
        }
    }
    public class FirstP_CRTempEntity : P_CRTempEntity
    {
        public new static FirstP_CRTempEntity CreateEntity()
        {
            return new FirstP_CRTempEntity()
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
