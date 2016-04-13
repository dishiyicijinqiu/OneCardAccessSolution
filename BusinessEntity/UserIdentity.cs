using System.Runtime.Serialization;
namespace FengSharp.OneCardAccess.BusinessEntity
{
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public class UserIdentity
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string UserNo { get; set; }
        [DataMember]
        public string UserName { get; set; }
        public static UserIdentity Current { get; set; }
    }
}
