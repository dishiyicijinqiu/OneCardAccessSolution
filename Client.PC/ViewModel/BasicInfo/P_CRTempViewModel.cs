using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class P_CRTempViewModel: NotificationObject
    {
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        P_CRTempEditMessage Parameter;
        object ParentViewModel;
        public P_CRTempViewModel(object ParentViewModel, P_CRTempEditMessage parameter)
        {
            this.ParentViewModel = ParentViewModel;
            Parameter = parameter;
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            CloseCommand = new DelegateCommand(Close);
            DefaultEventAggregator.Current.GetEvent<P_CRTempViewDataContextChangeEvent<object>>().Subscribe(OnP_CRTempViewDataContextChange);
        }

        private void OnP_CRTempViewDataContextChange(object sender, P_CRTempViewDataContextChangeEventArgs args)
        {
            if (sender == this.ParentViewModel)
            {
                if (args == null || args.P_CRTempEditMessage == null)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_NoNext));
                    DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
                    return;
                }
                this.ParentViewModel = sender;
                this.Parameter = args.P_CRTempEditMessage;
                this.Init();
            }
        }

        public void Init()
        {
            try
            {
                Entity = P_CRTempEntity.CreateEntity();
                if (Parameter == null)
                    throw new Exception(Properties.Resources.Error_ParameterIsError);
                switch (Parameter.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newEntity = P_CRTempEntity.CreateEntity();
                            Entity.CopyValueFrom(newEntity);
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(Parameter.CopyKey);
                        Entity = P_CRTempEntity.CreateEntity();
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "P_CRTempId"
                        });
                        break;
                    case EntityEditMode.Edit:
                        {
                            var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(Parameter.Key);
                            Entity.CopyValueFrom(newEntity);
                        }
                        break;
                    default:
                        throw new Exception(Properties.Resources.Error_ParameterIsError);
                }
                if (Entity == null)
                    Entity = P_CRTempEntity.CreateEntity();
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().
             Publish(this, new ExceptionEventArgs(ex));
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
            }
        }

        #region propertys
        private P_CRTempEntity _Entity;

        public P_CRTempEntity Entity
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
            P_CRTempEditMessage paramsg = this.Parameter as P_CRTempEditMessage;
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
                P_CRTempEditMessage paramsg = this.Parameter as P_CRTempEditMessage;
                paramsg.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveP_CRTempEntity(this.Entity);
                if (paramsg.Key <= 0)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Error_SaveFiled));
                    return;
                }
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SaveSuccess));
                DefaultEventAggregator.Current.GetEvent<P_CRTempViewEditedEvent<object>>().Publish(this.ParentViewModel, new P_CRTempViewEditedEventArgs(this.Parameter));
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
