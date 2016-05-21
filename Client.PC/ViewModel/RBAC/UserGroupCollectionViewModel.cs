using FengSharp.OneCardAccess.BusinessEntity.RBAC;
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
using System.Text;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserGroupCollectionViewModel : BaseNotificationObject, IUserGroupCollectionViewModel
    {
        public ICommand AddCommand { get; private set; }
        public ICommand CopyAddCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ClickToEditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public UserGroupCollectionViewModel() : this(ViewStyle.View) { }
        public UserGroupCollectionViewModel(ViewStyle ViewStyle)
        {
            this.MenuTitle = Properties.Resources.Menu_UserGroupCollectionView;
            this.ViewStyle = ViewStyle;
            AddCommand = new DelegateCommand<FirstUserGroupEntity>(Add);
            CopyAddCommand = new DelegateCommand<FirstUserGroupEntity>(CopyAdd, CanCopyAdd);
            EditCommand = new DelegateCommand<FirstUserGroupEntity>(Edit, CanEdit);
            DeleteCommand = new DelegateCommand<IList>(Delete, CanDelete);
            ClickToEditCommand = new DelegateCommand<FirstUserGroupEntity>(Edit);
            var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntitys().
                OrderBy(t => t.UserGroupNo).ThenBy(m => m.UserGroupName);
            Items = new ObservableCollection<FirstUserGroupEntity>(list);
        }

        public bool CanDelete(IList entitys)
        {
            if (entitys == null) return false;
            foreach (var item in entitys)
            {
                var entity = item as FirstUserGroupEntity;
                if (entity.IsSuper) return false;
                if (entity.TreeNo == "0000000000") return false;
            }
            return true;
        }

        public void Delete(IList entitys)
        {
            try
            {
                var deleteArgs = new MessageBoxEventArgs(Properties.Resources.Info_ConfirmToDelete, Properties.Resources.Info_Title, MsgButton.YesNo, MsgImage.Information);
                if (ShowMessage(deleteArgs) != MsgResult.Yes)
                    return;
                ServiceProxyFactory.Create<IRBACService>().DeleteUserGroupEntitys(entitys.Cast<UserGroupEntity>().ToList());
                ShowMessage(Properties.Resources.Info_DeleteSuccess);
                for (int i = entitys.Count - 1; i >= 0; i--)
                {
                    this.Items.Remove(entitys[i] as FirstUserGroupEntity);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void CopyAdd(FirstUserGroupEntity entity)
        {
            try
            {
                var vm = ServiceLoader.LoadService<IUserGroupViewModel>(new ParameterOverride("EditMessage",
                    new UserGroupEditMessage(entity.TreeParentNo, _CopyKey: entity.UserGroupId, _EntityEditMode: EntityEditMode.CopyAdd)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserGroupView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public bool CanCopyAdd(FirstUserGroupEntity entity)
        {
            if (entity == null) return false;
            if (entity.IsSuper) return false;
            if (entity.TreeNo == "0000000000") return false;
            return true;
        }

        public bool CanEdit(FirstUserGroupEntity entity)
        {
            if (entity == null) return false;
            if (entity.IsSuper) return false;
            if (entity.TreeNo == "0000000000") return false;
            return true;
        }

        public void Edit(FirstUserGroupEntity entity)
        {
            try
            {
                if (!this.CanEdit(entity))
                    return;
                var vm = ServiceLoader.LoadService<IUserGroupViewModel>(new ParameterOverride("EditMessage",
                    new UserGroupEditMessage(entity.TreeParentNo, entity.UserGroupId, EntityEditMode.Edit)));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserGroupView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public void Add(FirstUserGroupEntity entity)
        {
            try
            {
                string TreeParentNo = null;
                string CopyId = null;
                if (entity != null)
                {
                    TreeParentNo = entity.TreeNo;
                    CopyId = entity.UserGroupId;
                }
                var editmessage = new UserGroupEditMessage(TreeParentNo, _EntityEditMode: EntityEditMode.Add);
                var vm = ServiceLoader.LoadService<IUserGroupViewModel>(new ParameterOverride("EditMessage", editmessage));
                vm.OnEntityViewEdited += OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserGroupView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void OnEntityViewEdited(IViewModel vm, EditMessage<string> EditMessage)
        {
            try
            {
                switch (EditMessage.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntityById(EditMessage.Key);
                            var father = Items.First(t => t.TreeNo == newItem.TreeParentNo);

                            int newindex = Items.IndexOf(father) + Items.Count(t => t.TreeParentNo == father.TreeNo) + 1;
                            Items.Insert(newindex, newItem);
                            if (EditMessage.IsContinue)
                            {
                                var newvm = new UserGroupViewModel(new UserGroupEditMessage(newItem.TreeParentNo, _EntityEditMode: EntityEditMode.Add));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntityById(EditMessage.Key);
                            var father = Items.First(t => t.TreeNo == newItem.TreeParentNo);
                            int newindex = Items.IndexOf(father) + Items.Count(t => t.TreeParentNo == father.TreeNo) + 1;
                            Items.Insert(newindex, newItem);
                            if (EditMessage.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.UserGroupId == (EditMessage as UserGroupEditMessage).CopyKey);
                                SelectedEntity = copyItem;

                                var newvm = new UserGroupViewModel(new UserGroupEditMessage(newItem.TreeParentNo, _CopyKey: copyItem.UserGroupId, _EntityEditMode: EntityEditMode.CopyAdd));
                                newvm.OnEntityViewEdited += OnEntityViewEdited;
                                DefaultEventAggregator.Current.GetEvent<ChangeDataContextEvent>().
                                    Publish(vm.ChangeDataContextEventToken, new ChangeDataContextEventArgs(newvm));
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.UserGroupId == EditMessage.Key);
                            if (oldItem == null) return;
                            var newItem = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntityById(EditMessage.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                            if (EditMessage.IsContinue)
                            {
                                int nextIndex = itemIndex + 1;
                                if (Items.Count > nextIndex)
                                {
                                    var nextItem = Items[nextIndex];
                                    SelectedEntity = nextItem;
                                    var newvm = new UserGroupViewModel(new UserGroupEditMessage(newItem.TreeParentNo, nextItem.UserGroupId, EntityEditMode.Edit));
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
        public ObservableCollection<FirstUserGroupEntity> Items { get; private set; }

        private UserGroupEntity _SelectedEntity;
        public UserGroupEntity SelectedEntity
        {
            get { return _SelectedEntity; }
            set
            {
                _SelectedEntity = value;
                RaisePropertyChanged("SelectedEntity");
                (DeleteCommand as DelegateCommand<IList>).RaiseCanExecuteChanged();
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

    public class UserGroupEditMessage : TreeEditMessage<string>
    {
        public UserGroupEditMessage(string _TreeParentNo = "", string _Key = null, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, string _CopyKey = null)
        {
            TreeParentNo = _TreeParentNo;
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public string CopyKey { get; set; }
    }
}
