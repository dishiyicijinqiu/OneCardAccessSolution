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
        SecondRegisterEntity GetSecondRegisterEntityById(string RegisterId);
        [OperationContract]
        FirstRegisterEntity GetFirstRegisterEntityById(string RegisterId);
        [OperationContract]
        string SaveRegisterEntity(SecondRegisterEntity entity);
        [OperationContract]
        void DeleteRegisterEntitys(List<RegisterEntity> RegisterEntitys);
        #endregion
        #region P_CRTemp
        [OperationContract]
        List<FirstP_CRTempEntity> GetFirstP_CRTempEntitys();
        [OperationContract]
        FirstP_CRTempEntity GetFirstP_CRTempEntityById(string P_CRTempId);
        [OperationContract]
        string SaveP_CRTempEntity(P_CRTempEntity entity);
        [OperationContract]
        void DeleteP_CRTempEntitys(List<P_CRTempEntity> P_CRTempEntitys);
        #endregion
        #region Attachment
        [OperationContract]
        List<FirstAttachmentDirEntity> GetFirstAttachmentDirEntitys();
        [OperationContract]
        FirstAttachmentDirEntity GetFirstAttachmentDirEntityById(string attachmentdirid);
        [OperationContract]
        string SaveAttachmentDirEntity(AttachmentDirEntity entity);
        [OperationContract]
        void DeleteAttachmentDirEntitys(List<AttachmentDirEntity> list);
        #endregion
    }

    //[ServiceContract]
    //public interface IBasicInfoService
    //{
    //    // Methods
    //    [OperationContract]
    //    FirstRegisterEntity GetFirstRegisterEntityById(int RegisterId);
    //    [OperationContract]
    //    List<FirstRegisterEntity> GetFirstRegisterList();
    //    [OperationContract]
    //    SecondRegisterEntity GetSecondRegisterEntityById(int RegisterId);
    //    [OperationContract]
    //    int SaveRegisterEntity(SecondRegisterEntity entity);
    //}



}
