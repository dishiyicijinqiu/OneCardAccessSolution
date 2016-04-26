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
            DeleteCommand = new DelegateCommand<System.Collections.IList>(DeleteWithConfirm);
            CloseCommand = new DelegateCommand(Close);
        }
        public void Init()
        {
            try
            {
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

        private void DeleteCore(MsgResult msgResult, object[] param)
        {
            try
            {
                if (msgResult != MsgResult.Yes)
                    return;
                var SelectedEntitys = param[0] as System.Collections.IList;
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
                foreach (var item in listToDelete)
                {
                    this.Items.Remove(item as FirstRegisterEntity);
                }
            }
            catch (Exception ex)
            {
                DefaultEventAggregator.Current.GetEvent<ExceptionEvent<object>>().Publish(this, new ExceptionEventArgs(ex));
            }
        }

        public void DeleteWithConfirm(System.Collections.IList SelectedEntitys)
        {
            var deleteArgs =
                new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, null, MsgButton.YesNo, MsgImage.Information, DeleteCore, SelectedEntitys);
            DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().Publish(this, deleteArgs);
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
        //FirstRegisterEntity
        //SelectedEntity

        private FirstRegisterEntity _SelectedEntity;
        public FirstRegisterEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
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
                                SelectedEntity = copyItem;
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
                            if (args.RegisterEditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().
                                        Publish(this, new RegisterViewDataContextChangeEventArgs(new RegisterEditMessage(nextItem.RegisterId, EntityEditMode.Edit)));
                                }
                                else
                                {
                                    SelectedEntity = Items[itemIndex];
                                    DefaultEventAggregator.Current.GetEvent<RegisterViewDataContextChangeEvent<object>>().
                                        Publish(this, new RegisterViewDataContextChangeEventArgs(null));
                                }
                            }
                            SelectedEntity = Items[itemIndex];
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
