using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    [KnownType(typeof(FirstAttachmentDirEntity))]
    public partial class AttachmentDirEntity
    {
        public static AttachmentDirEntity CreateEntity()
        {
            return new AttachmentDirEntity();
        }
    }

    public class FirstAttachmentDirEntity : AttachmentDirEntity
    {
        public new static FirstAttachmentDirEntity CreateEntity()
        {
            return new FirstAttachmentDirEntity()
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
