using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using System.Collections.Generic;
using System.ServiceModel;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    [ServiceContract]
    public interface IBasicInfoService
    {
        [OperationContract]
        List<RegisterEntity> GetRegisterList();
    }
}
