using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : NotificationObject
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public RegisterCollectionViewModel()
        {
            try
            {
                DeleteCommand = new DelegateCommand<System.Collections.IList>(Delete);
                CloseCommand = new DelegateCommand(Close);
                var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntitys();
                Items = new ObservableCollection<FirstRegisterEntity>(list);
                DefaultEventAggregator.Current.GetEvent<RegisterViewEditedEvent<object>>().Subscribe(OnRegisterViewEdited);
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
                DefaultEventAggregator.Current.GetEvent<CloseEvent<object>>().Publish(this);
            }
        }
        public void Delete(System.Collections.IList SelectedEntitys)
        {
            try
            {
                if (SelectedEntitys == null || SelectedEntitys.Count <= 0)
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
        public void Close()
        {
            DefaultEventAggregator.Current.GetEvent<CloseDocumentEvent<object>>().Publish(this);
        }
        #endregion
        #region methods

        private void OnRegisterViewEdited(object sender, RegisterViewEditedEventArgs args)
        {
            try
            {
                if (sender != this)
                    return;
                switch (args.RegisterEditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(args.RegisterEditMessage.Key);
                            Items.Add(newItem);
                            if (args.RegisterEditMessage.IsContinue)
                                DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().
                                    Publish(this, new RegisterViewDataContextChangeEventArgs(new RegisterEditMessage()));
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(args.RegisterEditMessage.Key);
                            Items.Add(newItem);
                            if (args.RegisterEditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.RegisterId == args.RegisterEditMessage.CopyKey);
                                DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().
                                    Publish(this, new RegisterViewDataContextChangeEventArgs(new RegisterEditMessage(_CopyKey: copyItem.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd)));

                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.RegisterId == args.RegisterEditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstRegisterEntityById(args.RegisterEditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            int nextIndex = itemIndex + 1;
                            if (Items.Count > nextIndex)
                            {
                                var nextItem = Items[nextIndex];
                                DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().
                                    Publish(this, new RegisterViewDataContextChangeEventArgs(new RegisterEditMessage(nextItem.RegisterId, EntityEditMode.Edit)));
                            }
                            else
                            {
                                DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().
                                    Publish(this, new RegisterViewDataContextChangeEventArgs(null));
                            }
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

    public class CreateRegisterViewEvent<Sender> : BaseSenderEvent<Sender, CreateRegisterViewEventArgs> { }
    public class CreateRegisterViewEventArgs
    {
        public CreateRegisterViewEventArgs(RegisterEditMessage RegisterEditMessage)
        {
            this.RegisterEditMessage = RegisterEditMessage;
        }
        public RegisterEditMessage RegisterEditMessage { get; set; }
    }


    public class RegisterViewEditedEvent<Sender> : BaseSenderEvent<Sender, RegisterViewEditedEventArgs> { }
    public class RegisterViewEditedEventArgs
    {
        public RegisterViewEditedEventArgs(RegisterEditMessage RegisterEditMessage)
        {
            this.RegisterEditMessage = RegisterEditMessage;
        }
        public RegisterEditMessage RegisterEditMessage { get; set; }
    }


    public class RegisterViewDataContextChangeEvent<Sender> : BaseSenderEvent<Sender, RegisterViewDataContextChangeEventArgs> { }
    public class RegisterViewDataContextChangeEventArgs
    {
        public RegisterViewDataContextChangeEventArgs(RegisterEditMessage RegisterEditMessage)
        {
            this.RegisterEditMessage = RegisterEditMessage;
        }
        public RegisterEditMessage RegisterEditMessage { get; set; }
    }
    #endregion
}
