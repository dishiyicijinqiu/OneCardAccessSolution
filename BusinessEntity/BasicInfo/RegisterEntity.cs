using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    [KnownType(typeof(FirstRegisterEntity))]
    [KnownType(typeof(SecondRegisterEntity))]
    public partial class RegisterEntity
    {
        public static RegisterEntity CreateEntity()
        {
            return new RegisterEntity();
        }
    }
    public class FirstRegisterEntity : RegisterEntity
    {
        public new static FirstRegisterEntity CreateEntity()
        {
            return new FirstRegisterEntity()
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
    [KnownType(typeof(Register_FileEntity))]
    public class SecondRegisterEntity : FirstRegisterEntity
    {
        public new static SecondRegisterEntity CreateEntity()
        {
            return new SecondRegisterEntity()
            {
                Creater = string.Empty,
                LastModifyer = string.Empty,
                Register_FileEntitys = new List<Register_FileEntity>()
            };
        }
        private List<Register_FileEntity> _Register_FileEntitys;

        [DataMember]
        public List<Register_FileEntity> Register_FileEntitys
        {
            get { return _Register_FileEntitys; }
            set
            {
                _Register_FileEntitys = value;
                RaisePropertyChanged("Register_FileEntitys");
            }
        }
    }
}
