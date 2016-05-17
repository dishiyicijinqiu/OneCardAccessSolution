using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class P_CRTempCollectionViewModel : BaseNotificationObject, IP_CRTempCollectionView, IP_CRTempCollectionSelect
    {
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand CloseWindowCommand { get; private set; }
        public ICommand ConfirmCommand { get; private set; }

        public P_CRTempCollectionViewModel() : this(CollectionViewStyle.CollectionView) { }
        public P_CRTempCollectionViewModel(CollectionViewStyle style)
        {
            this.CollectionViewStyle = style;
            AddCommand = new DelegateCommand(Add);
            CopyAddCommand = new DelegateCommand<FirstP_CRTempEntity>(CopyAdd);
            EditCommand = new DelegateCommand<FirstP_CRTempEntity>(Edit);
            DeleteCommand = new DelegateCommand<IList>(Delete);
            CloseWindowCommand = new DelegateCommand(CloseWindow);
            ConfirmCommand = new DelegateCommand<IList>(Confirm);
            var list = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntitys().OrderBy(t => t.CateNo).ThenBy(m => m.CRTempName);
            Items = new ObservableCollection<FirstP_CRTempEntity>(list);
        }

        private void OnEntityViewEdited(IViewModel vm, P_CRTempEditMessage EditMessage)
        {
            try
            {
                switch (EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.Key);
                            Items.Add(newItem);
                            if (EditMessage.IsContinue)
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                Publish(vm.ChangeDataContextEventToken,
                                    new ChangeDataContextEventArgs(
                                        new P_CRTempViewModel(new P_CRTempEditMessage())
                                    )
                                );
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.Key);
                            Items.Add(newItem);
                            if (EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.P_CRTempId == EditMessage.CopyKey);
                                SelectedEntity = copyItem;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                Publish(vm.ChangeDataContextEventToken,
                                    new ChangeDataContextEventArgs(
                                        new P_CRTempViewModel(new P_CRTempEditMessage(_CopyKey: copyItem.P_CRTempId, _EntityEditMode: EntityEditMode.CopyAdd))
                                    )
                                );
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.P_CRTempId == EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstP_CRTempEntityById(EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken,
                                        new ChangeDataContextEventArgs(
                                            new P_CRTempViewModel(new P_CRTempEditMessage(nextItem.P_CRTempId, EntityEditMode.Edit))
                                        )
                                    );
                                }
                                else
                                {
                                    SelectedEntity = Items[itemIndex];
                                    ShowMessage(Properties.Resources.Info_NoNext);
                                    DefaultEventAggregator.Current.GetEvent<CloseEvent>().
                                        Publish(vm.CloseEventToken, new CloseEventArgs(CloseStyle.NullClose));
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Add()
        {
            try
            {
                var vm = ServiceLoader.LoadService<IP_CRTempEdit>(
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("ParentViewModel",this),
                    new Microsoft.Practices.Unity.ParameterOverride("EditMessage",new P_CRTempEditMessage())
                });
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IView>("P_CRTempView",
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("vm",vm),
                });
                this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_P_CRTempView_Title));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void CopyAdd(FirstP_CRTempEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var vm = ServiceLoader.LoadService<IP_CRTempEdit>(
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("ParentViewModel",this),
                    new Microsoft.Practices.Unity.ParameterOverride("EditMessage",new P_CRTempEditMessage(_CopyKey: entity.P_CRTempId, _EntityEditMode: EntityEditMode.CopyAdd))
                });
                var view = ServiceLoader.LoadService<IView>("P_CRTempView",
                    new Microsoft.Practices.Unity.ResolverOverride[] {
                    new Microsoft.Practices.Unity.ParameterOverride("vm",vm),
                });
                this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_P_CRTempView_Title));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Edit(FirstP_CRTempEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var vm = ServiceLoader.LoadService<IP_CRTempEdit>(
                    new ParameterOverride("EditMessage", new P_CRTempEditMessage(entity.P_CRTempId, EntityEditMode.Edit))
               );
                var view = ServiceLoader.LoadService<IView>("P_CRTempView", new ParameterOverride("VM", vm));
                //var view = new View.BasicInfo.P_CRTempView(vm as P_CRTempViewModel);

                this.CreateView(new CreateViewEventArgs(view, Properties.Resources.View_P_CRTempView_Title));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Delete(IList entitys)
        {
            try
            {
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                ServiceProxyFactory.Create<IBasicInfoService>().DeleteP_CRTempEntitys(entitys.Cast<P_CRTempEntity>().ToList());
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                foreach (var item in entitys)
                {
                    this.Items.Remove(item as FirstP_CRTempEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void Confirm(IList entitys)
        {
            try
            {
                if (entitys.Count <= 0)
                {
                    ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                SelectItems = entitys.Cast<P_CRTempEntity>().ToList();
                this.CloseWindow();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }
        private void CloseWindow()
        {
            switch (CollectionViewStyle)
            {
                case CollectionViewStyle.CollectionView:
                    this.CloseDocument();
                    break;
                case CollectionViewStyle.CollectionOneSelect:
                case CollectionViewStyle.CollectionMulSelect:
                    this.Close();
                    break;
                default:
                    break;
            }
        }
        #region propertys
        public ObservableCollection<FirstP_CRTempEntity> Items { get; private set; }

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

        private CollectionViewStyle _CollectionViewStyle;
        public CollectionViewStyle CollectionViewStyle
        {
            get { return _CollectionViewStyle; }
            set
            {
                _CollectionViewStyle = value;
                RaisePropertyChanged("CollectionViewStyle");
            }
        }
        public List<P_CRTempEntity> SelectItems { get; set; }
        #endregion
    }
    #region message
    public class P_CRTempEditMessage : EditMessage<string>
    {
        public P_CRTempEditMessage(string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
        {
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public string CopyKey { get; set; }
    }
    #endregion
}
