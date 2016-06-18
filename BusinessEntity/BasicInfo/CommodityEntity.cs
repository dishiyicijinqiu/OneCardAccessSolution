using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    [KnownType(typeof(FirstProductEntity))]
    public partial class CommodityEntity
    {
        public static CommodityEntity CreateEntity()
        {
            return new CommodityEntity()
            {
            };
        }
    }

    public class FirstCommodityEntity : CommodityEntity
    {
        public new static FirstCommodityEntity CreateEntity()
        {
            return new FirstCommodityEntity()
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
