using DevExpress.Mvvm;
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
        public List<FirstRegisterEntity> GetFirstRegisterList()
        {
            return this.GetEntitys<FirstRegisterEntity>();
        }
        public FirstRegisterEntity GetFirstRegisterEntityById(int RegisterId)
        {
            var firstEntity = this.FindById<FirstRegisterEntity>(new FirstRegisterEntity()
            {
                RegisterId = RegisterId
            });
            return firstEntity;
        }
        public SecondRegisterEntity GetSecondRegisterEntityById(int RegisterId)
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
        public int SaveRegisterEntity(SecondRegisterEntity entity)
        {
            return UseTran((tran) =>
            {
                var dbregisterentity = new T_Register();
                dbregisterentity.CopyValueFrom(entity);
                if (entity.RegisterId <= 0)
                {
                    dbregisterentity.LastModifyId = dbregisterentity.CreateId = (int)Session.Current.SessionClientId;
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
                    dbregisterentity.LastModifyId = (int)Session.Current.SessionClientId;
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
                    if (!base.DeleteEntity<RegisterEntity>(entity, tran))
                    {
                        throw new BusinessException(Properties.Resources.Error_DeleteFailed);
                    }
                    base.DeleteRelationEntitys<int, int>(entity.RegisterId, tran);
                }
            });
        }
        #endregion
        #region P_CRTemp产品检验报告
        #endregion
    }
}
