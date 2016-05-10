using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    [KnownType(typeof(FirstRegisterEntity))]
    [KnownType(typeof(SecondRegisterEntity))]
    public partial class RegisterEntity
    {
        public static RegisterEntity CreateEntity()
        {
            return new RegisterEntity()
            {
                StartDate = System.DateTime.Now.ToString("yyyy-MM-dd"),
                EndDate = System.DateTime.Now.ToString("yyyy-MM-dd"),
            };
        }
    }
    public class FirstRegisterEntity : RegisterEntity
    {
        public new static FirstRegisterEntity CreateEntity()
        {
            return new FirstRegisterEntity()
            {
                StartDate = System.DateTime.Now.ToString("yyyy-MM-dd"),
                EndDate = System.DateTime.Now.ToString("yyyy-MM-dd"),
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
                StartDate = System.DateTime.Now.ToString("yyyy-MM-dd"),
                EndDate = System.DateTime.Now.ToString("yyyy-MM-dd"),
                Creater = string.Empty,
                LastModifyer = string.Empty,
                Register_FileEntitys = new ObservableCollection<Register_FileEntity>()
            };
        }
        private ObservableCollection<Register_FileEntity> _Register_FileEntitys;

        [DataMember]
        public ObservableCollection<Register_FileEntity> Register_FileEntitys
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
