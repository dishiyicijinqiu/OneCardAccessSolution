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
using FengSharp.OneCardAccess.BusinessEntity.RBAC;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserCollectionViewModel : BaseNotificationObject, IUserCollectionViewModel, IUserCollectionSelectViewModel
    {
        public event OnSelectedItems<UserInfoEntity> OnSelectedItems;

        public ICommand AddCommand { get; private set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public UserCollectionViewModel() : this(ViewStyle.View) { }
        public UserCollectionViewModel(ViewStyle ViewStyle)
        {
            this.MenuTitle = Properties.Resources.Menu_UserCollectionView;
            this.ViewStyle = ViewStyle;
            AddCommand = new DelegateCommand<UserGroupEntity>(Add);
            CopyAddCommand = new DelegateCommand<FirstUserInfoEntity>(CopyAdd, CanCopyAdd);
            EditCommand = new DelegateCommand<FirstUserInfoEntity>(Edit, CanEdit);
            DeleteCommand = new DelegateCommand<IList>(Delete, CanDelete);
            //ClickToEditCommand = new DelegateCommand<FirstUserGroupEntity>(Edit);
            var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntitys().
                OrderBy(t => t.UserNo).ThenBy(m => m.UserNo);
            Items = new ObservableCollection<FirstUserInfoEntity>(list);
            //Items = new ObservableCollection<FirstUserInfoEntity>();
        }

        private bool CanDelete(IList entitys)
        {
            if (entitys == null) return false;
            return entitys.Count > 0;
        }

        private void Delete(IList entitys)
        {
            try
            {
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                ServiceProxyFactory.Create<IRBACService>().DeleteUserEntitys(entitys.Cast<UserInfoEntity>().ToList());
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Items.Remove(entitys[i] as FirstUserInfoEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private bool CanEdit(FirstUserInfoEntity entity)
        {
            return entity != null;
        }

        private void Edit(FirstUserInfoEntity entity)
        {
            try
            {
                if (!this.CanEdit(entity))
                    return;
                var vm = ServiceLoader.LoadService<IUserViewModel>(new ParameterOverride("EditMessage",
                    new UserEditMessage(entity.UserGroupId, entity.UserId, EntityEditMode.Edit)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanCopyAdd(FirstUserInfoEntity entity)
        {
            return entity != null;
        }

        public void CopyAdd(FirstUserInfoEntity entity)
        {
            try
            {
                var editmessage = new UserEditMessage(entity.UserGroupId, _EntityEditMode: EntityEditMode.CopyAdd, _CopyKey: entity.UserId);
                var vm = ServiceLoader.LoadService<IUserViewModel>(new ParameterOverride("EditMessage", editmessage));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void Add(UserGroupEntity usergroupentity)
        {
            try
            {
                string usergroupid = null;
                if (usergroupentity != null)
                {
                    usergroupid = usergroupentity.UserGroupId;
                }
                var editmessage = new UserEditMessage(usergroupid, _EntityEditMode: EntityEditMode.Add);
                var vm = ServiceLoader.LoadService<IUserViewModel>(new ParameterOverride("EditMessage", editmessage));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void OnEntityViewEdited(IViewModel vm, EditMessage<string> EditMessage)
        {
            try
            {
                switch (EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntityById(EditMessage.Key);
                            Items.Add(newItem);
                            if (EditMessage.IsContinue)
                            {
                                var args = EditMessage as UserEditMessage;
                                string usergroupid = null;
                                if (args != null)
                                {
                                    usergroupid = args.UserGroupId;
                                }
                                var newvm = new UserViewModel(new UserEditMessage(usergroupid, _EntityEditMode: EntityEditMode.Add));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntityById(EditMessage.Key);
                            Items.Add(newItem);
                            if (EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.UserId == (EditMessage as UserEditMessage).CopyKey);
                                SelectedEntity = copyItem;
                                var newvm = new UserGroupViewModel(new UserGroupEditMessage((EditMessage as UserEditMessage).UserGroupId, _CopyKey: copyItem.UserGroupId, _EntityEditMode: EntityEditMode.CopyAdd));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.UserId == EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntityById(EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    var newvm = new UserGroupViewModel(new UserGroupEditMessage((EditMessage as UserEditMessage).UserGroupId, nextItem.UserId, EntityEditMode.Edit));
                                    newvm.OnEntityViewEdited += OnEntityViewEdited;
                                    DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                        Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
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

        public ObservableCollection<FirstUserInfoEntity> Items { get; set; }

        private UserInfoEntity _SelectedEntity;
        public UserInfoEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
            }
        }
        private ViewStyle _ViewStyle;

        public ViewStyle ViewStyle
        {
            get { return _ViewStyle; }
            set
            {
                _ViewStyle = value;
                RaisePropertyChanged("ViewStyle");
            }
        }
        private string _MenuTitle;

        public string MenuTitle
        {
            get { return _MenuTitle; }
            set
            {
                _MenuTitle = value;
                RaisePropertyChanged("MenuTitle");
            }
        }
    }


    public class UserEditMessage : EditMessage<string>
    {
        public UserEditMessage(string _UserGroupId = null, string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
        {
            this.UserGroupId = UserGroupId;
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public string CopyKey { get; set; }
        public string UserGroupId { get; set; }
    }
}
