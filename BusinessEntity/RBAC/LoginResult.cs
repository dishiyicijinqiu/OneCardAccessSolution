using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.RBAC
{
    //[DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    //public class LoginResult
    //{
    //    [DataMember]
    //    public LoginMessage LoginMessage { get; set; }
    //    [DataMember]
    //    public UserIdentity UserIdentity { get; set; }
    //}
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public enum LoginResult
    {
        [EnumMember]
        Success = 0,
        [EnumMember]
        UserNotExist = 1,
        [EnumMember]
        UserIsLocked = 2,
        [EnumMember]
        ErrorPassWord = 3,
        [EnumMember]
        UserIsEmpty = 4,
    }
}
