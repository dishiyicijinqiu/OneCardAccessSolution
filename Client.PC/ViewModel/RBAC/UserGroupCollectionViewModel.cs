using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserGroupCollectionViewModel : BaseNotificationObject, IUserGroupCollectionViewModel
    {
        public UserGroupCollectionViewModel() : this(ViewStyle.View) { }
        public UserGroupCollectionViewModel(ViewStyle ViewStyle)
        {
            this.MenuTitle = Properties.Resources.Menu_UserGroupCollectionView;
            this.ViewStyle = ViewStyle;
            AddCommand = new DelegateCommand(Add);
            var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntitys().
                OrderBy(t => t.UserGroupNo).ThenBy(m => m.UserGroupName);
            Items = new ObservableCollection<FirstUserGroupEntity>(list);
        }

        public void Add()
        {
            try
            {
                string TreeParentNo = string.Empty;
                if (SelectedEntity != null)
                {
                    TreeParentNo = SelectedEntity.TreeNo;
                }
                var vm = ServiceLoader.LoadService<IUserGroupViewModel>(new ParameterOverride("EditMessage", new UserGroupEditMessage(TreeParentNo)));
                vm.OnEntityViewEdited += vm_OnEntityViewEdited;
                var view = ServiceLoader.LoadService<IUserGroupView>(new ParameterOverride("VM", vm));
                this.CreateView(new CreateViewEventArgs(view, "DialogWindowStyle"));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void vm_OnEntityViewEdited(IViewModel vm, EditMessage<string> EditMessage)
        {
           
        }

        public ICommand AddCommand { get; private set; }
        public ObservableCollection<FirstUserGroupEntity> Items { get; private set; }

        private UserGroupEntity _SelectedEntity;
        public UserGroupEntity SelectedEntity
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
