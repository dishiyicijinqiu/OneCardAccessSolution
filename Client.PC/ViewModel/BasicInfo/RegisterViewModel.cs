using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : NotificationObject
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        RegisterEditMessage Parameter;
        public RegisterViewModel(RegisterEditMessage parameter)
        {
            try
            {
                Parameter = parameter;
                Entity = SecondRegisterEntity.CreateEntity();
                if (Parameter == null)
                    throw new Exception(Properties.Resources.Error_ParameterIsError);
                switch (Parameter.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        break;
                    case EntityEditMode.CopyAdd:
                        var copyEntity = basicinfoservice.GetSecondRegisterEntityById(Parameter.CopyKey);
                        Entity = SecondRegisterEntity.CreateEntity();
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "RegisterId","Register_FileEntitys"
                        });
                        break;
                    case EntityEditMode.Edit:
                        Entity = basicinfoservice.GetSecondRegisterEntityById(Parameter.Key);
                        break;
                    default:
                        throw new Exception(Properties.Resources.Error_ParameterIsError);
                }
                if (Entity == null)
                    Entity = SecondRegisterEntity.CreateEntity();
                EntityEditMode = Parameter.EntityEditMode;
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<RegisterViewModel>>().
             Publish(this, new ExceptionEventArgs(ex));
                DefaultEventAggregator.Current.GetEvent<CloseEvent<RegisterViewModel>>().
                    Publish(this, new NullEventArgs());
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
            //try
            //{
            //    RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
            //    paramsg.Key = basicinfoservice.SaveRegisterEntity(this.Entity);
            //    if (paramsg.Key <= 0)
            //    {
            //        MessageBoxService.ShowMessage(Properties.Resources.Error_SaveFiled, Properties.Resources.Error_Title, MessageButton.OK, MessageIcon.Error);
            //        return;
            //    }
            //    MessageBoxService.ShowMessage(Properties.Resources.Info_SaveSuccess, Properties.Resources.Info_Title, MessageButton.OK, MessageIcon.Information);
            //    this.Close();
            //    Messenger.Default.Send<RegisterEditMessage>(paramsg, (this as ISupportParentViewModel).ParentViewModel);
            //}
            //catch (Exception ex)
            //{
            //    this.MessageBoxService.HandleException(ex);
            //}
        }
        #endregion
    }
}
