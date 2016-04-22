using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using System.Collections.Generic;
using System.ServiceModel;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    [ServiceContract]
    public interface IBasicInfoService
    {
        #region Register
        [OperationContract]
        List<FirstRegisterEntity> GetFirstRegisterEntitys();
        [OperationContract]
        SecondRegisterEntity GetSecondRegisterEntityById(int RegisterId);
        [OperationContract]
        FirstRegisterEntity GetFirstRegisterEntityById(int RegisterId);
        [OperationContract]
        int SaveRegisterEntity(SecondRegisterEntity entity);
        [OperationContract]
        void DeleteRegisterEntitys(List<RegisterEntity> RegisterEntitys);
        #endregion
        #region P_CRTemp
        [OperationContract]
        List<P_CRTempEntity> GetP_CRTempEntitys();
        [OperationContract]
        P_CRTempEntity GetP_CRTempEntityById(int P_CRTempId);
        [OperationContract]
        int SaveP_CRTempEntity(P_CRTempEntity entity);
        [OperationContract]
        void DeleteP_CRTempEntitys(List<P_CRTempEntity> P_CRTempEntitys);
        #endregion
    }
}
