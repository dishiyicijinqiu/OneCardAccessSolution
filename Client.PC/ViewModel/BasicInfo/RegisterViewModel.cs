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
                        var copyEntity = basicinfoservice.GetSecondRegisterEntityById(paramsg.CopyKey);
                        Entity = SecondRegisterEntity.CreateEntity();
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "RegisterId","Register_FileEntitys"
                        });
                        break;
                    case EntityEditMode.Edit:
                        Entity = basicinfoservice.GetSecondRegisterEntityById(paramsg.Key);
                        break;
                    default:
                        throw new Exception(Properties.Resources.Error_ParameterIsError);
                }
                if (Entity == null)
                    Entity = SecondRegisterEntity.CreateEntity();
                EntityEditMode = paramsg.EntityEditMode;
            }
        }

        #region propertys
        private SecondRegisterEntity _Entity;

        public SecondRegisterEntity Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }


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
        #endregion
        #region methods
        public void SaveAndNew()
        {
            RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
            paramsg.IsContinue = true;
            this.Save();

        }
        public void Save()
        {
            try
            {
                RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
                paramsg.Key = basicinfoservice.SaveRegisterEntity(this.Entity);
                if (paramsg.Key <= 0)
                {
                    MessageBoxService.ShowMessage(Properties.Resources.Error_SaveFiled, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
                    return;
                }
                MessageBoxService.ShowMessage(Properties.Resources.Info_SaveSuccess, Properties.Resources.Info_Title, MessageButton.OK, MessageIcon.Information);
                this.Close();
                Messenger.Default.Send<RegisterEditMessage>(paramsg, (this as ISupportParentViewModel).ParentViewModel);
            }
            catch (Exception ex)
            {
                this.MessageBoxService.HandleException(ex);
            }
        }
        #endregion
    }
}
