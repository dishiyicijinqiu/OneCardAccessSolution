using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using FengSharp.OneCardAccess.TEntity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Common;
using System.Data;
using System.IO;

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
                    if (result == -1)
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
                    if (result == -1)
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
                if (result == -1)
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
                if (result == -1)
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
        #region Attachment
        public List<FirstAttachmentDirEntity> GetFirstAttachmentDirEntitys()
        {
            return this.GetEntitys<FirstAttachmentDirEntity>();
        }

        public FirstAttachmentDirEntity GetFirstAttachmentDirEntityById(string AttachmentDirId)
        {
            return this.FindById<FirstAttachmentDirEntity>(new FirstAttachmentDirEntity()
            {
                AttachmentDirId = AttachmentDirId
            });
        }
        public string SaveAttachmentDirEntity(AttachmentDirEntity entity)
        {
            if (entity.AttachmentDirName == Properties.Resources.Filed_RootDir)
            {
                throw new BusinessException(string.Format(Properties.Resources.Error_CanNotNameRootDir, Properties.Resources.Filed_RootDir));
            }
            return UseTran((tran) =>
            {
                var dbattachmentdirentity = new T_AttachmentDir();
                dbattachmentdirentity.CopyValueFrom(entity);
                if (string.IsNullOrWhiteSpace(entity.AttachmentDirId) || entity.AttachmentDirId.Length != 36)
                {
                    #region 服务端验证
                    VerifyAttachmentDir(entity);
                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_AttachmentDir");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "CreateEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, null);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.AttachmentDirNo);
                    Database.AddInParameter(cmd, "TreeParentNo", DbType.String, entity.TreeParentNo);
                    Database.AddOutParameter(cmd, "Filter", DbType.String, 100);
                    Database.AddOutParameter(cmd, "FullPath", DbType.String, 3000);
                    Database.AddParameter(cmd, "AttachmentDirName", DbType.String, 100, ParameterDirection.InputOutput, true, 0, 0, String.Empty, DataRowVersion.Default, entity.AttachmentDirName);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result != 1)
                    {
                        switch (result)
                        {
                            default:
                                throw new BusinessException(Properties.Resources.Error_UnHandleException);
                            case -1:
                                throw new BusinessException(Properties.Resources.Error_NoIsExist);
                            case -2:
                                throw new BusinessException(Properties.Resources.Error_NameIsExist);
                        }
                    }
                    #endregion
                    #region 新建文件夹
                    string path = Database.GetParameterValue(cmd, "FullPath").ToString();
                    string fullpath = Path.Combine(SystemServiceConfig.AttachBaseDir, path);
                    if (!Directory.Exists(fullpath))
                    {
                        throw new BusinessException(string.Format(Properties.Resources.Error_PathNotFound, path));
                    }
                    fullpath = Path.Combine(fullpath, entity.AttachmentDirName);
                    Directory.CreateDirectory(fullpath);
                    #endregion
                    dbattachmentdirentity.LastModifyId = dbattachmentdirentity.CreateId = (string)Session.Current.SessionClientId;
                    dbattachmentdirentity = CreateEntity(dbattachmentdirentity, tran, CreateTreeEntityUnCreateFileds);
                    result = this.SaveEntityFlow(dbattachmentdirentity, "CreateAttachmentDirEntity", tran);
                    if (result != 1)
                    {
                        switch (result)
                        {
                            default:
                                throw new BusinessException(Properties.Resources.Error_UnHandleException);
                            case -1:
                                throw new BusinessException(Properties.Resources.Error_FatherNotExist);
                            case -2:
                                throw new BusinessException(Properties.Resources.Error_FatherIsUsingCanNotChild);
                        }
                    }
                }
                else
                {
                    #region 服务端验证
                    VerifyAttachmentDir(entity);
                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_AttachmentDir");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "ModifyEntity");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, entity.AttachmentDirId);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.AttachmentDirNo);
                    Database.AddInParameter(cmd, "TreeParentNo", DbType.String, entity.TreeParentNo);
                    Database.AddOutParameter(cmd, "Filter", DbType.String, 100);
                    Database.AddOutParameter(cmd, "FullPath", DbType.String, 3000);
                    Database.AddParameter(cmd, "AttachmentDirName", DbType.String, 100, ParameterDirection.InputOutput, true, 0, 0, String.Empty, DataRowVersion.Default, entity.AttachmentDirName);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result != 1)
                    {
                        switch (result)
                        {
                            default:
                                throw new BusinessException(Properties.Resources.Error_UnHandleException);
                            case -1:
                                throw new BusinessException(Properties.Resources.Error_NoIsExist);
                            case -2:
                                throw new BusinessException(Properties.Resources.Error_NameIsExist);
                        }
                    }
                    #endregion

                    string attachmentdirname = Database.GetParameterValue(cmd, "AttachmentDirName").ToString();
                    if (string.Compare(attachmentdirname, entity.AttachmentDirName, true) != 0)
                    {
                        #region 更改目录名称
                        string path = Database.GetParameterValue(cmd, "FullPath").ToString();
                        string fullpath = Path.Combine(SystemServiceConfig.AttachBaseDir, path);
                        string oldfullpath = Path.Combine(fullpath, attachmentdirname);
                        string newfullpath = Path.Combine(fullpath, entity.AttachmentDirName);
                        System.IO.Directory.Move(oldfullpath, newfullpath);
                        #endregion
                    }
                    dbattachmentdirentity.LastModifyId = (string)Session.Current.SessionClientId;
                    dbattachmentdirentity.LastModifyDate = System.DateTime.Now;
                    if (!ModifyEntity(dbattachmentdirentity, tran, ModifyTreeEntityUnChangedFileds))
                    {
                        throw new BusinessException(Properties.Resources.Error_SaveFailed);
                    }
                }
                return dbattachmentdirentity.AttachmentDirId;
            });
        }
        private void VerifyAttachmentDir(AttachmentDirEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.AttachmentDirNo))
            {
                throw new BusinessException(Properties.Resources.Error_NoCanNotEmpty);
            }
            if (string.IsNullOrWhiteSpace(entity.AttachmentDirName))
            {
                throw new BusinessException(Properties.Resources.Error_AttachmentDirNameCanNotEmpty);
            }

            if (!string.IsNullOrWhiteSpace(entity.Filter))
            {
                string[] formats = entity.Filter.Split(';');
                if (formats == null)
                {
                    throw new BusinessException(Properties.Resources.Error_Filter);
                }
                foreach (var item in formats)
                {
                    if (!item.StartsWith("*."))
                    {
                        throw new BusinessException(Properties.Resources.Error_Filter);
                    }
                    if (System.Text.RegularExpressions.Regex.IsMatch("^[a-zA-Z]", item.Substring(2)))
                    {
                        throw new BusinessException(Properties.Resources.Error_Filter);
                    }
                }
            }
        }

        public void DeleteAttachmentDirEntitys(List<AttachmentDirEntity> entitys)
        {
            UseTran((tran) =>
            {
                foreach (var entity in entitys)
                {
                    var dbentity = new T_AttachmentDir();
                    dbentity.CopyValueFrom(entity);

                    #region 服务端验证

                    DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_AttachmentDir");
                    Database.AddInParameter(cmd, "cMode", DbType.String, "getfullpath");
                    Database.AddInParameter(cmd, "EntityId", DbType.String, entity.AttachmentDirId);
                    Database.AddInParameter(cmd, "EntityNo", DbType.String, entity.AttachmentDirNo);
                    Database.AddInParameter(cmd, "TreeParentNo", DbType.String, entity.TreeParentNo);
                    Database.AddParameter(cmd, "AttachmentDirName", DbType.String, 100, ParameterDirection.InputOutput, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
                    Database.AddOutParameter(cmd, "Filter", DbType.String, 100);
                    Database.AddOutParameter(cmd, "FullPath", DbType.String, 3000);
                    Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
                    this.Database.ExecuteNonQuery(cmd);
                    int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
                    if (result == -1)
                    {
                        throw new BusinessException(Properties.Resources.Error_NoIsExist);
                    }
                    #endregion
                    #region 检查文件夹下是否有文件或文件夹
                    if (!string.IsNullOrWhiteSpace(entity.AttachmentDirName))
                    {
                        string path = Database.GetParameterValue(cmd, "FullPath").ToString();
                        string fullpath = Path.Combine(SystemServiceConfig.AttachBaseDir, path);
                        fullpath = Path.Combine(fullpath, entity.AttachmentDirName);
                        if (Directory.Exists(fullpath))
                        {
                            var filecount = Directory.GetFiles(fullpath, entity.Filter).Length;
                            if (filecount > 0)
                            {
                                throw new BusinessException(Properties.Resources.Error_DirHasChild);
                            }
                            var dircount = Directory.GetDirectories(fullpath).Length;
                            if (dircount > 0)
                            {
                                throw new BusinessException(Properties.Resources.Error_DirHasChild);
                            }
                            Directory.Delete(fullpath);
                        }
                    }
                    #endregion
                    int ReturnValue = 0;
                    base.DeleteEntity(dbentity, ref ReturnValue, tran);
                    if (ReturnValue != 1)
                    {
                        switch (ReturnValue)
                        {
                            default:
                                throw new BusinessException(Properties.Resources.Error_UnHandleException);
                            case -1:
                                throw new BusinessException(string.Format(Properties.Resources.Error_ObjIsNotExist,
                                    string.Format("{0},{1}", entity.AttachmentDirNo, entity.AttachmentDirName)));
                            case -2:
                                throw new BusinessException(string.Format(Properties.Resources.Error_IsUsing,
                                    string.Format("{0},{1}", entity.AttachmentDirNo, entity.AttachmentDirName)));
                        }
                    }
                }
            });
        }

        public List<FirstAttachmentInfoEntity> GetFirstAttachmentInfoEntitysByAttachmentDirId(string attachmentdirid)
        {
            DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_AttachmentDir");
            Database.AddInParameter(cmd, "cMode", DbType.String, "GetFullPath");
            Database.AddInParameter(cmd, "EntityId", DbType.String, attachmentdirid);
            Database.AddInParameter(cmd, "EntityNo", DbType.String);
            Database.AddInParameter(cmd, "TreeParentNo", DbType.String);
            Database.AddOutParameter(cmd, "FullPath", DbType.String, 3000);
            Database.AddOutParameter(cmd, "Filter", DbType.String, 100);
            Database.AddParameter(cmd, "AttachmentDirName", DbType.String, 100, ParameterDirection.InputOutput, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
            Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
            Database.ExecuteNonQuery(cmd);
            int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
            if (result == 2)
                return new List<FirstAttachmentInfoEntity>();
            if (result != 1)
            {
                switch (result)
                {
                    default:
                        throw new BusinessException(Properties.Resources.Error_UnHandleException);
                }
            }
            string path = Database.GetParameterValue(cmd, "FullPath").ToString();
            string fullpath = Path.Combine(SystemServiceConfig.AttachBaseDir, path);
            string attachmentdirname = Database.GetParameterValue(cmd, "AttachmentDirName").ToString();
            fullpath = Path.Combine(fullpath, attachmentdirname);
            if (!Directory.Exists(fullpath))
            {
                throw new BusinessException(Properties.Resources.Error_PathNotFound);
            }
            string filter = Database.GetParameterValue(cmd, "Filter").ToString();
            if (string.IsNullOrWhiteSpace(filter))
                return Directory.GetFiles(fullpath, string.Empty, SearchOption.AllDirectories).
                        Select(t => new FirstAttachmentInfoEntity()
                        {
                            AttachmentDirId = attachmentdirid,
                            AttachmentPath = fullpath,
                            AttachmentName = Path.GetFileName(t)
                        }).ToList();



            string[] formats = filter.Split(';');
            if (formats == null)
            {
                throw new BusinessException(Properties.Resources.Error_Filter);
            }
            foreach (var item in formats)
            {
                if (!item.StartsWith("*."))
                {
                    throw new BusinessException(Properties.Resources.Error_Filter);
                }
                if (System.Text.RegularExpressions.Regex.IsMatch("^[a-zA-Z]", item.Substring(2)))
                {
                    throw new BusinessException(Properties.Resources.Error_Filter);
                }
            }
            var results = new List<FirstAttachmentInfoEntity>();
            foreach (var item in formats)
            {
                var templist = Directory.GetFiles(fullpath, item, SearchOption.AllDirectories).
                                Select(t => new FirstAttachmentInfoEntity()
                                {
                                    AttachmentDirId = attachmentdirid,
                                    AttachmentPath = fullpath,
                                    AttachmentName = Path.GetFileName(t)
                                });
                results.AddRange(templist);
            }
            return results;
        }
        private static string[] GetFiles(string sourceFolder, string filters, SearchOption searchOption)
        {
            return filters.Split('|').SelectMany(filter => Directory.GetFiles(sourceFolder, filter, searchOption)).ToArray();
        }

        public void DeleteAttachment(AttachmentInfoEntity attachmentinfoentity)
        {
            if (string.IsNullOrWhiteSpace(attachmentinfoentity.AttachmentName))
                throw new BusinessException(Properties.Resources.Error_AttachmentNameCanNotEmpty);
            DbCommand cmd = this.Database.GetStoredProcCommand("P_Verify_AttachmentDir");
            Database.AddInParameter(cmd, "cMode", DbType.String, "GetFullPath");
            Database.AddInParameter(cmd, "EntityId", DbType.String, attachmentinfoentity.AttachmentDirId);
            Database.AddInParameter(cmd, "EntityNo", DbType.String);
            Database.AddInParameter(cmd, "TreeParentNo", DbType.String);
            Database.AddOutParameter(cmd, "FullPath", DbType.String, 3000);
            Database.AddOutParameter(cmd, "Filter", DbType.String, 100);
            Database.AddParameter(cmd, "AttachmentDirName", DbType.String, 100, ParameterDirection.InputOutput, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
            Database.AddReturnParameter(cmd, "ReturnValue", DbType.Int32);
            Database.ExecuteNonQuery(cmd);
            int result = (int)Database.GetParameterValue(cmd, "ReturnValue");
            if (result != 1)
            {
                switch (result)
                {
                    default:
                        throw new BusinessException(Properties.Resources.Error_UnHandleException);
                }
            }
            string path = Database.GetParameterValue(cmd, "FullPath").ToString();
            string fullpath = Path.Combine(SystemServiceConfig.AttachBaseDir, path);
            string attachmentdirname = Database.GetParameterValue(cmd, "AttachmentDirName").ToString();
            fullpath = Path.Combine(fullpath, attachmentdirname);
            if (!Directory.Exists(fullpath))
            {
                throw new BusinessException(Properties.Resources.Error_PathNotFound);
            }
            string filename = Path.Combine(fullpath, attachmentinfoentity.AttachmentName);
            if (File.Exists(filename))
                File.Delete(filename);
        }
        #endregion
        #region newmethods
        #endregion
    }
}
