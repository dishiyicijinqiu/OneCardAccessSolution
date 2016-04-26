using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class P_CRTempCollectionViewModel : NotificationObject
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public P_CRTempCollectionViewModel()
        {
            DeleteCommand = new DelegateCommand<System.Collections.IList>(DeleteWithConfirm);
            CloseCommand = new DelegateCommand(Close);
        }
        public void Init()
        {
            try
            {
                var list = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntitys();
                Items = new ObservableCollection<P_CRTempEntity>(list);
                DefaultEventAggregator.Current.GetEvent<P_CRTempViewEditedEvent<object>>().Subscribe(OnP_CRTempViewEdited);
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
                var listToDelete = SelectedEntitys.Cast<P_CRTempEntity>().ToList();
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteP_CRTempEntitys(listToDelete);
                DefaultEventAggregator.Current.GetEvent<MessageBoxEvent<object>>().
                    Publish(this, new MessageBoxEventArgs(Properties.Resources.Info_DeleteSuccess));
                foreach (var item in listToDelete)
                {
                    this.Items.Remove(item as P_CRTempEntity);
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
        ObservableCollection<P_CRTempEntity> _Items;
        public ObservableCollection<P_CRTempEntity> Items
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

        private P_CRTempEntity _SelectedEntity;
        public P_CRTempEntity SelectedEntity
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

        private void OnP_CRTempViewEdited(object sender, P_CRTempViewEditedEventArgs args)
        {
            try
            {
                if (sender != this)
                    return;
                switch (args.P_CRTempEditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(args.P_CRTempEditMessage.Key);
                            Items.Add(newItem);
                            if (args.P_CRTempEditMessage.IsContinue)
                                DefaultEventAggregator.Current.GetEvent<P_CRTempViewDataContextChangeEvent<object>>().
                                    Publish(this, new P_CRTempViewDataContextChangeEventArgs(new P_CRTempEditMessage()));
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(args.P_CRTempEditMessage.Key);
                            Items.Add(newItem);
                            if (args.P_CRTempEditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.P_CRTempId == args.P_CRTempEditMessage.CopyKey);
                                SelectedEntity = copyItem;
                                DefaultEventAggregator.Current.GetEvent<P_CRTempViewDataContextChangeEvent<object>>().
                                    Publish(this, new P_CRTempViewDataContextChangeEventArgs(new P_CRTempEditMessage(_CopyKey: copyItem.P_CRTempId, _EntityEditMode: EntityEditMode.CopyAdd)));
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.P_CRTempId == args.P_CRTempEditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetP_CRTempEntityById(args.P_CRTempEditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (args.P_CRTempEditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    DefaultEventAggregator.Current.GetEvent<P_CRTempViewDataContextChangeEvent<object>>().
                                        Publish(this, new P_CRTempViewDataContextChangeEventArgs(new P_CRTempEditMessage(nextItem.P_CRTempId, EntityEditMode.Edit)));
                                }
                                else
                                {
                                    SelectedEntity = Items[itemIndex];
                                    DefaultEventAggregator.Current.GetEvent<P_CRTempViewDataContextChangeEvent<object>>().
                                        Publish(this, new P_CRTempViewDataContextChangeEventArgs(null));
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
    public class P_CRTempEditMessage : EditMessage<int>
    {
        public P_CRTempEditMessage(int _Key = 0, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, int _CopyKey = 0)
        {
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public int CopyKey { get; set; }
    }
    public class CreateP_CRTempViewEvent<Sender> : BaseSenderEvent<Sender, CreateP_CRTempViewEventArgs> { }
    public class CreateP_CRTempViewEventArgs
    {
        public CreateP_CRTempViewEventArgs(P_CRTempEditMessage P_CRTempEditMessage)
        {
            this.P_CRTempEditMessage = P_CRTempEditMessage;
        }
        public P_CRTempEditMessage P_CRTempEditMessage { get; set; }
    }


    public class P_CRTempViewEditedEvent<Sender> : BaseSenderEvent<Sender, P_CRTempViewEditedEventArgs> { }
    public class P_CRTempViewEditedEventArgs
    {
        public P_CRTempViewEditedEventArgs(P_CRTempEditMessage P_CRTempEditMessage)
        {
            this.P_CRTempEditMessage = P_CRTempEditMessage;
        }
        public P_CRTempEditMessage P_CRTempEditMessage { get; set; }
    }


    public class P_CRTempViewDataContextChangeEvent<Sender> : BaseSenderEvent<Sender, P_CRTempViewDataContextChangeEventArgs> { }
    public class P_CRTempViewDataContextChangeEventArgs
    {
        public P_CRTempViewDataContextChangeEventArgs(P_CRTempEditMessage P_CRTempEditMessage)
        {
            this.P_CRTempEditMessage = P_CRTempEditMessage;
        }
        public P_CRTempEditMessage P_CRTempEditMessage { get; set; }
    }
    #endregion
}
