using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    public partial class RegisterEntity
    {
        public static RegisterEntity CreateEntity()
        {
            return new RegisterEntity()
            {
                Creater = string.Empty,
                LastModifyer = string.Empty,
            };
        }
        [DataMember]
        public string Creater { get; set; }
        [DataMember]
        public string LastModifyer { get; set; }
    }
}
