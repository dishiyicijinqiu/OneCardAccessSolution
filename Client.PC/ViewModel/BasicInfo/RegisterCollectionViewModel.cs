using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : NotificationObject
    {
        public ICommand AddCommand { get; private set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public RegisterCollectionViewModel()
        {
            try
            {
                AddCommand = new DelegateCommand(Add);
                CopyAddCommand = new DelegateCommand<FirstRegisterEntity>(CopyAdd);
                EditCommand = new DelegateCommand<FirstRegisterEntity>(Edit);
                DeleteCommand = new DelegateCommand<System.Collections.IList>(Delete);
                CloseCommand = new DelegateCommand(Close);
                var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntitys();
                Items = new ObservableCollection<FirstRegisterEntity>(list);
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
            }
        }
        #region propertys

        ObservableCollection<FirstRegisterEntity> _Items;
        public ObservableCollection<FirstRegisterEntity> Items
        {
            get
            {
                return _Items;
            }
            protected set
            {
                _Items = value;
                RaisePropertyChanged("Items");
            }
        }

        #endregion
        #region commandmethods
        public void Add()
        {
            try
            {
                DefaultEventAggregator.Current.GetEvent<ViewRegisterEvent<RegisterCollectionViewModel>>().Publish(this, new ViewRegisterEventArgs(new RegisterEditMessage()));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        public void CopyAdd(FirstRegisterEntity SelectedEntity)
        {
            try
            {
                DefaultEventAggregator.Current.GetEvent<ViewRegisterEvent<RegisterCollectionViewModel>>().
                    Publish(this, new ViewRegisterEventArgs(new RegisterEditMessage(_CopyKey: SelectedEntity.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd)));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        public void Edit(FirstRegisterEntity SelectedEntity)
        {
            try
            {
                if (SelectedEntity == null)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                    return;
                }
                DefaultEventAggregator.Current.GetEvent<ViewRegisterEvent<RegisterCollectionViewModel>>().
                    Publish(this, new ViewRegisterEventArgs(new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit)));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        public void Delete(System.Collections.IList SelectedEntitys)
        {
            try
            {
                if (SelectedEntitys == null && SelectedEntitys.Count <= 0)
                {
                    DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().
                        Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_SelectAtLeastOne));
                    return;
                }
                var listToDelete = SelectedEntitys.Cast<RegisterEntity>().ToList();
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteRegisterEntitys(listToDelete);
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().
                    Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_DeleteSuccess));
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        public void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
        }
        #endregion
        #region methods
        public void OnEdited(RegisterEditMessage message)
        {
            try
            {
                switch (message.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(message.Key);
                            Items.Add(newItem);
                            if (message.IsContinue)
                                Add();
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(message.Key);
                            Items.Add(newItem);
                            if (message.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.RegisterId == message.CopyKey);
                                CopyAdd(copyItem);
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.RegisterId == message.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(message.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }
        #endregion
    }
    #region message
    public class RegisterEditMessage : EditMessage<int>
    {
        public RegisterEditMessage(int _Key = 0, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, int _CopyKey = 0)
        {
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public int CopyKey { get; set; }
    }

    public class ViewRegisterEvent<Sender> : BaseSenderEvent<Sender, ViewRegisterEventArgs> { }
    public class ViewRegisterEventArgs
    {
        public ViewRegisterEventArgs(RegisterEditMessage RegisterEditMessage)
        {
            this.RegisterEditMessage = RegisterEditMessage;
        }
        public RegisterEditMessage RegisterEditMessage { get; set; }
    }
    #endregion
}
