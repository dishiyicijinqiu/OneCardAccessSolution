using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FengSharp.OneCardAccess.Services
{
    public class BasicInfoService : ServiceBase, IBasicInfoService
    {
        #region Register注册证
        public List<FirstRegisterEntity> GetFirstRegisterEntitys()
        {
            return this.GetEntitys<FirstRegisterEntity>();
        }
        public FirstRegisterEntity GetFirstRegisterEntityById(string RegisterId)
        {
            return this.FindById<FirstRegisterEntity>(new FirstRegisterEntity()
            {
                RegisterId = RegisterId
            });
        }
        public SecondRegisterEntity GetSecondRegisterEntityById(string RegisterId)
        {
            var firstEntity = this.FindById<FirstRegisterEntity>(new FirstRegisterEntity()
            {
                RegisterId = RegisterId
            });
            var secondentity = new SecondRegisterEntity();
            secondentity.CopyValueFrom(firstEntity);
            var dbRegister_FileList = this.GetForeignEntitys<T_Register, T_Register_File>(new T_Register() { RegisterId = secondentity.RegisterId });
            secondentity.Register_FileEntitys = new Register_FileEntity[dbRegister_FileList.Count].ToList();
            ClassValueCopier.CopyArray(secondentity.Register_FileEntitys, dbRegister_FileList);
            return secondentity;
        }
        public string SaveRegisterEntity(SecondRegisterEntity entity)
        {
            return UseTran((tran) =>
            {
                var dbregisterentity = new T_Register();
                dbregisterentity.CopyValueFrom(entity);
                if (string.IsNullOrWhiteSpace(entity.RegisterId) || entity.RegisterId.Length != 36)
                //if (entity.RegisterId.Length != 36)
                {
                    dbregisterentity.LastModifyId = dbregisterentity.CreateId = (string)Session.Current.SessionClientId;
                    dbregisterentity.LastModifyDate = dbregisterentity.CreateDate = System.DateTime.Now;
                    dbregisterentity = CreateEntity(dbregisterentity, tran);
                    if (entity.Register_FileEntitys != null)
                        foreach (var item in entity.Register_FileEntitys)
                        {
                            var dbregisterfileentity = new T_Register_File();
                            dbregisterfileentity.CopyValueFrom(item);
                            dbregisterfileentity.RegisterId = dbregisterentity.RegisterId;
                            CreateEntity(dbregisterfileentity, tran);
                        }
                }
                else
                {
                    dbregisterentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbregisterentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbregisterentity, tran))
                    {
                        throw new BusinessException(FengSharp.OneCardAccess.Services.Properties.Resources.Error_SaveFailed);
                    }
                    DeleteRelationEntitys<T_Register, T_Register_File>(dbregisterentity, tran);
                    if (entity.Register_FileEntitys != null)
                        foreach (var item in entity.Register_FileEntitys)
                        {
                            var dbregisterfileentity = new T_Register_File();
                            dbregisterfileentity.CopyValueFrom(item);
                            dbregisterfileentity.RegisterId = dbregisterentity.RegisterId;
                            CreateEntity(dbregisterfileentity, tran);
                        }
                }
                return dbregisterentity.RegisterId;
            });
        }
        public void DeleteRegisterEntitys(List<RegisterEntity> RegisterEntitys)
        {
            UseTran((tran) =>
            {
                foreach (var entity in RegisterEntitys)
                {
                    var dbentity = new T_Register();
                    dbentity.CopyValueFrom(entity);
                    if (!base.DeleteEntity<T_Register>(dbentity, tran))
                    {
                        throw new BusinessException(Properties.Resources.Error_DeleteFailed);
                    }
                    //base.DeleteRelationEntitys<int, int>(entity.RegisterId, tran);
                }
            });
        }
        #endregion
        #region P_CRTemp产品检验报告
        public List<FirstP_CRTempEntity> GetFirstP_CRTempEntitys()
        {
            return this.GetEntitys<FirstP_CRTempEntity>();
        }

        public FirstP_CRTempEntity GetFirstP_CRTempEntityById(string P_CRTempId)
        {
            return this.FindById<FirstP_CRTempEntity>(new FirstP_CRTempEntity()
            {
                P_CRTempId = P_CRTempId
            });
        }

        public string SaveP_CRTempEntity(P_CRTempEntity entity)
        {
            return UseTran((tran) =>
            {
                var dbregisterentity = new T_P_CRTemp();
                dbregisterentity.CopyValueFrom(entity);
                if (string.IsNullOrWhiteSpace(entity.P_CRTempId) || entity.P_CRTempId.Length != 36)
                //if (entity.P_CRTempId.Length != 36)
                {
                    dbregisterentity.LastModifyId = dbregisterentity.CreateId = (string)Session.Current.SessionClientId;
                    dbregisterentity.LastModifyDate = dbregisterentity.CreateDate = System.DateTime.Now;
                    dbregisterentity = CreateEntity(dbregisterentity, tran);
                }
                else
                {
                    dbregisterentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbregisterentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbregisterentity, tran))
                    {
                        throw new BusinessException(FengSharp.OneCardAccess.Services.Properties.Resources.Error_SaveFailed);
                    }
                }
                return dbregisterentity.P_CRTempId;
            });
        }

        public void DeleteP_CRTempEntitys(List<P_CRTempEntity> P_CRTempEntitys)
        {
            UseTran((tran) =>
            {
                foreach (var entity in P_CRTempEntitys)
                {
                    if (!base.DeleteEntity<P_CRTempEntity>(entity, tran))
                    {
                        throw new BusinessException(Properties.Resources.Error_DeleteFailed);
                    }
                }
            });
        }
        #endregion
    }
}
