using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Common;
using System.Data;

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
            var list = new Register_FileEntity[dbRegister_FileList.Count].ToList();
            ClassValueCopier.CopyArray(list, dbRegister_FileList);
            secondentity.Register_FileEntitys = new System.Collections.ObjectModel.ObservableCollection<Register_FileEntity>(list);

            var dbRegister_P_CRTempList = this.GetForeignEntitys<T_Register, FirstP_CRTemp_To_RegisterEntity>(new T_Register() { RegisterId = secondentity.RegisterId });
            var list1 = dbRegister_P_CRTempList.ToList();
            ClassValueCopier.CopyArray(list1, dbRegister_P_CRTempList);
            secondentity.P_CRTemp_To_RegisterEntitys = new System.Collections.ObjectModel.ObservableCollection<FirstP_CRTemp_To_RegisterEntity>(list1);
            return secondentity;
        }
        public string SaveRegisterEntity(SecondRegisterEntity entity)
        {
            return UseTran((tran) =>
            {
                var dbregisterentity = new T_Register();
                dbregisterentity.CopyValueFrom(entity);
                if (string.IsNullOrWhiteSpace(entity.RegisterId) || entity.RegisterId.Length != 36)
                {
                    #region 服务端验证
                    if (string.IsNullOrWhiteSpace(entity.RegisterNo))
                    {
                        throw new BusinessException(Properties.Resources.Error_NoCanNotEmpty);
                    }
                    if (string.IsNullOrWhiteSpace(entity.RegisterName))
                    {
                        throw new BusinessException(Properties.Resources.Error_RegisterNameCanNotEmpty);
                    }

                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_Register");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "CreateEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, null);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.RegisterNo);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result == 1)
                    {
                        throw new BusinessException(Properties.Resources.Error_NoIsExist);
                    }
                    #endregion
                    dbregisterentity.LastModifyId = dbregisterentity.CreateId = (string)Session.Current.SessionClientId;
                    dbregisterentity = CreateEntity(dbregisterentity, tran, CreateEntityUnCreateFileds);
                    if (entity.Register_FileEntitys != null)
                        foreach (var item in entity.Register_FileEntitys)
                        {
                            var dbregisterfileentity = new T_Register_File();
                            dbregisterfileentity.CopyValueFrom(item);
                            dbregisterfileentity.RegisterId = dbregisterentity.RegisterId;
                            CreateEntity(dbregisterfileentity, tran);
                        }
                    if (entity.P_CRTemp_To_RegisterEntitys != null)
                        foreach (var item in entity.P_CRTemp_To_RegisterEntitys)
                        {
                            var dbp_crtempentity = new T_P_CRTemp_To_Register();
                            dbp_crtempentity.CopyValueFrom(item);
                            dbp_crtempentity.RegisterId = dbregisterentity.RegisterId;
                            CreateEntity(dbp_crtempentity, tran);
                        }
                }
                else
                {
                    #region 服务端验证
                    if (string.IsNullOrWhiteSpace(entity.RegisterNo))
                    {
                        throw new BusinessException(Properties.Resources.Error_NoCanNotEmpty);
                    }
                    if (string.IsNullOrWhiteSpace(entity.RegisterName))
                    {
                        throw new BusinessException(Properties.Resources.Error_RegisterNameCanNotEmpty);
                    }

                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_Register");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "ModifyEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, entity.RegisterId);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.RegisterNo);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result == 1)
                    {
                        throw new BusinessException(Properties.Resources.Error_NoIsExist);
                    }
                    #endregion
                    dbregisterentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbregisterentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbregisterentity, tran, ModifyEntityUnChangedFileds))
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
                    DeleteRelationEntitys<T_Register, T_P_CRTemp_To_Register>(dbregisterentity, tran);
                    if (entity.P_CRTemp_To_RegisterEntitys != null)
                        foreach (var item in entity.P_CRTemp_To_RegisterEntitys)
                        {
                            var dbp_crtempentity = new T_P_CRTemp_To_Register();
                            dbp_crtempentity.CopyValueFrom(item);
                            dbp_crtempentity.RegisterId = dbregisterentity.RegisterId;
                            CreateEntity(dbp_crtempentity, tran);
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
                    int ReturnValue = 0;
                    base.DeleteEntity(dbentity, ref ReturnValue, tran);
                    switch (ReturnValue)
                    {
                        default:
                            throw new Exception(Properties.Resources.Error_UnHandleException);
                        case 0:
                            break;
                        case -1:
                            throw new BusinessException(string.Format(Properties.Resources.Error_ObjIsNotExist,
                                string.Format("{0},{1}", entity.RegisterNo, entity.RegisterName)));
                    }
                }
            });
        }
        #endregion
        #region P_CRTemp产品检验报告模板
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
            //return UseTran((tran) =>
            //{
            var dbregisterentity = new T_P_CRTemp();
            dbregisterentity.CopyValueFrom(entity);
            if (string.IsNullOrWhiteSpace(entity.P_CRTempId) || entity.P_CRTempId.Length != 36)
            {
                #region 服务端验证
                if (string.IsNullOrWhiteSpace(entity.CRTempName))
                    throw new BusinessException(Properties.Resources.Error_CRTempNameCanNotEmpty);
                DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_P_CRTemp");
                Database.AddInParameter(cmd, "cMode", DbType.String, "CreateEntity");
                Database.AddInParameter(cmd, "EntityId", DbType.String, null);
                Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.CRTempName);
                Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                this.Database.ExecuteNonQuery(cmd);
                int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                if (result == 1)
                {
                    throw new BusinessException(Properties.Resources.Error_NameIsExist);
                }
                #endregion
                dbregisterentity.LastModifyId = dbregisterentity.CreateId = (string)Session.Current.SessionClientId;
                dbregisterentity = CreateEntity(dbregisterentity, uncreatefileds: CreateEntityUnCreateFileds);
            }
            else
            {
                #region 服务端验证
                if (string.IsNullOrWhiteSpace(entity.CRTempName))
                    throw new BusinessException(Properties.Resources.Error_CRTempNameCanNotEmpty);
                DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_P_CRTemp");
                Database.AddInParameter(cmd, "cMode", DbType.String, "ModifyEntity");
                Database.AddInParameter(cmd, "EntityId", DbType.String, entity.P_CRTempId);
                Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.CRTempName);
                Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                this.Database.ExecuteNonQuery(cmd);
                int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                if (result == 1)
                {
                    throw new BusinessException(Properties.Resources.Error_NameIsExist);
                }
                #endregion
                dbregisterentity.LastModifyId = (string)Session.Current.SessionClientId;
                dbregisterentity.LastModifyDate = System.DateTime.Now;
                if (!ModifyEntity(dbregisterentity, uncreatefileds: ModifyEntityUnChangedFileds))
                {
                    throw new BusinessException(FengSharp.OneCardAccess.Services.Properties.Resources.Error_SaveFailed);
                }
            }
            return dbregisterentity.P_CRTempId;
            //});
        }

        public void DeleteP_CRTempEntitys(List<P_CRTempEntity> P_CRTempEntitys)
        {
            UseTran((tran) =>
            {
                foreach (var entity in P_CRTempEntitys)
                {
                    int ReturnValue = 0;
                    base.DeleteEntity<P_CRTempEntity>(entity, ref ReturnValue, tran);
                    switch (ReturnValue)
                    {
                        default:
                            throw new Exception(Properties.Resources.Error_UnHandleException);
                        case 0:
                            break;
                        case -1:
                            throw new BusinessException(string.Format(Properties.Resources.Error_ObjIsNotExist, entity.CRTempName));
                    }
                }
            });
        }
        #endregion
    }
}
