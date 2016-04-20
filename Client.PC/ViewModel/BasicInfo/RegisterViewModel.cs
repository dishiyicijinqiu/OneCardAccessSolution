using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using DevExpress.Mvvm.DataAnnotations;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : DefaultViewModel
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterViewModel()
        {
            Entity = SecondRegisterEntity.CreateEntity();
        }
        public void Loaded()
        {
            if (!ViewModelBase.IsInDesignMode)
            {
                RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
                if (paramsg == null)
                    throw new Exception(Properties.Resources.Error_ParameterIsError);
                switch (paramsg.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        break;
                    case EntityEditMode.CopyAdd:
                    case EntityEditMode.Edit:
                        Entity = basicinfoservice.GetSecondRegisterEntityById(paramsg.Key);
                        break;
                    default:
                        throw new Exception(Properties.Resources.Error_ParameterIsError);
                }
                if (Entity == null)
                    Entity = SecondRegisterEntity.CreateEntity();
                this.RaisePropertiesChanged();
                RaisePropertyChanged("RegisterNo");
            }
        }

        #region propertys
        public SecondRegisterEntity Entity { get; set; }
        EntityEditMode _EntityEditMode = EntityEditMode.Add;
        public EntityEditMode EntityEditMode
        {
            get
            {
                return _EntityEditMode;
            }
            private set
            {
                if (_EntityEditMode == value) return;
                _EntityEditMode = value;
                RaisePropertyChanged("EntityEditMode");
            }
        }
        public List<Register_FileEntity> Register_FileEntitys
        {
            get
            {
                return Entity.Register_FileEntitys;
            }
            set
            {
                Entity.Register_FileEntitys = value;
            }
        }
        #region RegisterEntity
        public string RegisterNo
        {
            get
            {
                return Entity.RegisterNo;
            }
            set
            {
                if (Entity.RegisterNo != value)
                {
                    Entity.RegisterNo = value;
                    RaisePropertyChanged("RegisterNo");
                }
            }
        }
        public string RegisterProductName
        {
            get
            {
                return Entity.RegisterProductName;
            }
            set
            {
                if (Entity.RegisterProductName != value)
                {
                    Entity.RegisterProductName = value;
                    RaisePropertyChanged("RegisterProductName");
                }
            }
        }
        public string StandardCode
        {
            get
            {
                return Entity.StandardCode;
            }
            set
            {
                if (Entity.StandardCode != value)
                {
                    Entity.StandardCode = value;
                    RaisePropertyChanged("StandardCode");
                }
            }
        }
        public string RegisterNo1
        {
            get
            {
                return Entity.RegisterNo1;
            }
            set
            {
                if (Entity.RegisterNo1 != value)
                {
                    Entity.RegisterNo1 = value;
                    RaisePropertyChanged("RegisterNo1");
                }
            }
        }
        public string RegisterProductName1
        {
            get
            {
                return Entity.RegisterProductName1;
            }
            set
            {
                if (Entity.RegisterProductName1 != value)
                {
                    Entity.RegisterProductName1 = value;
                    RaisePropertyChanged("RegisterProductName1");
                }
            }
        }
        public string StandardCode1
        {
            get
            {
                return Entity.StandardCode1;
            }
            set
            {
                if (Entity.StandardCode1 != value)
                {
                    Entity.StandardCode1 = value;
                    RaisePropertyChanged("StandardCode1");
                }
            }
        }
        public string StartDate
        {
            get
            {
                return Entity.StartDate;
            }
            set
            {
                if (Entity.StartDate != value)
                {
                    Entity.StartDate = value;
                    RaisePropertyChanged("StartDate");
                }
            }
        }
        public string EndDate
        {
            get
            {
                return Entity.EndDate;
            }
            set
            {
                if (Entity.EndDate != value)
                {
                    Entity.EndDate = value;
                    RaisePropertyChanged("EndDate");
                }
            }
        }
        public string Creater
        {
            get
            {
                return Entity.Creater;
            }
            set
            {
                if (Entity.Creater != value)
                {
                    Entity.Creater = value;
                    RaisePropertyChanged("Creater");
                }
            }
        }
        public DateTime CreateDate
        {
            get
            {
                return Entity.CreateDate;
            }
            set
            {
                if (Entity.CreateDate != value)
                {
                    Entity.CreateDate = value;
                    RaisePropertyChanged("CreateDate");
                }
            }
        }
        public string LastModifyer
        {
            get
            {
                return Entity.LastModifyer;
            }
            set
            {
                if (Entity.LastModifyer != value)
                {
                    Entity.LastModifyer = value;
                    RaisePropertyChanged("LastModifyer");
                }
            }
        }
        public DateTime LastModifyDate
        {
            get
            {
                return Entity.LastModifyDate;
            }
            set
            {
                if (Entity.LastModifyDate != value)
                {
                    Entity.LastModifyDate = value;
                    RaisePropertyChanged("LastModifyDate");
                }
            }
        }
        public string Remark
        {
            get
            {
                return Entity.Remark;
            }
            set
            {
                if (Entity.Remark != value)
                {
                    Entity.Remark = value;
                    RaisePropertyChanged("Remark");
                }
            }
        }
        #endregion
        #endregion
        #region methods
        public void Save()
        {
            try
            {
                RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
                paramsg.Key = basicinfoservice.Save(this.Entity);
                if (paramsg.Key <= 0)
                {
                    MessageBoxService.ShowMessage(Properties.Resources.Error_SaveFiled, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                }
                MessageBoxService.ShowMessage(Properties.Resources.Info_SaveSuccess, Properties.Resources.Info_Title, MessageButton.OK, MessageIcon.Information);
                Messenger.Default.Send(paramsg);
            }
            catch (Exception ex)
            {
                this.MessageBoxService.HandleException(ex);
            }
        }
        #endregion
    }
}
