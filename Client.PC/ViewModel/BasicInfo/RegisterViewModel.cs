using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows.Input;
using DevExpress.Xpf.Core.Commands;
using Microsoft.Practices.Prism.Commands;
using DevExpress.Xpf.Core;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : NotificationObject
    {

        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        RegisterEditMessage Parameter;
        object ParentViewModel;
        public RegisterViewModel(object ParentViewModel, RegisterEditMessage parameter)
        {
            this.ParentViewModel = ParentViewModel;
            Parameter = parameter;
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            CloseCommand = new DelegateCommand(Close);
            DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().Subscribe(OnRegisterViewDataContextChange);
        }

        private void OnRegisterViewDataContextChange(object sender, RegisterViewDataContextChangeEventArgs args)
        {
            if (sender == this.ParentViewModel)
            {
                if (args == null || args.RegisterEditMessage == null)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_NoNext));
                    DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
                    return;
                }
                this.ParentViewModel = sender;
                this.Parameter = args.RegisterEditMessage;
                this.Init();
            }
        }
        public void Init()
        {
            try
            {
                Entity = SecondRegisterEntity.CreateEntity();
                if (Parameter == null)
                    throw new Exception(Properties.Resources.Error_ParameterIsError);
                switch (Parameter.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newEntity = SecondRegisterEntity.CreateEntity();
                            Entity.CopyValueFrom(newEntity);
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetSecondRegisterEntityById(Parameter.CopyKey);
                        Entity = SecondRegisterEntity.CreateEntity();
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "RegisterId","Register_FileEntitys"
                        });
                        break;
                    case EntityEditMode.Edit:
                        {
                            var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetSecondRegisterEntityById(Parameter.Key);
                            Entity.CopyValueFrom(newEntity);
                        }
                        break;
                    default:
                        throw new Exception(Properties.Resources.Error_ParameterIsError);
                }
                if (Entity == null)
                    Entity = SecondRegisterEntity.CreateEntity();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().
             Publish(this, new ExceptionEventArgs(ex));
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
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
        #endregion
        #region methods
        public void SaveAndNew()
        {
            RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
            paramsg.IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            this.SaveCore();
            DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
        }
        void SaveCore()
        {
            try
            {
                RegisterEditMessage paramsg = this.Parameter as RegisterEditMessage;
                paramsg.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveRegisterEntity(this.Entity);
                if (paramsg.Key <= 0)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Error_SaveFiled));
                    return;
                }
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SaveSuccess));
                DefaultEventAggregator.Current.GetEvent<RegisterViewEditedEvent<object>>().Publish(this.ParentViewModel, new RegisterViewEditedEventArgs(this.Parameter));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().
             Publish(this, new ExceptionEventArgs(ex));
            }
        }
        public void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
        }
        #endregion
    }
}
