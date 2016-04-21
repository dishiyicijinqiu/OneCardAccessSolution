using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using System.Collections.Generic;
using System.ServiceModel;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    [ServiceContract]
    public interface IBasicInfoService
    {
        [OperationContract]
        List<FirstRegisterEntity> GetFirstRegisterList();
        [OperationContract]
        SecondRegisterEntity GetSecondRegisterEntityById(int RegisterId);
        [OperationContract]
        FirstRegisterEntity GetFirstRegisterEntityById(int RegisterId);
        [OperationContract]
        int SaveRegisterEntity(SecondRegisterEntity entity);
    }
}
