using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
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
        [DataMember]
        public string Creater { get; set; }
        [DataMember]
        public string LastModifyer { get; set; }
    }

    public class SecondRegisterEntity : FirstRegisterEntity
    {
        public new static SecondRegisterEntity CreateEntity()
        {
            return new SecondRegisterEntity()
            {
                Creater = string.Empty,
                LastModifyer = string.Empty,
            };
        }
        public List<Register_FileEntity> Register_FileEntitys { get; set; }
    }
}
