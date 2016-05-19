using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserGroupCollectionViewModel : BaseNotificationObject, IUserGroupCollectionViewModel
    {
        public UserGroupCollectionViewModel() : this(ViewStyle.View) { }
        public UserGroupCollectionViewModel(ViewStyle ViewStyle)
        {
            this.MenuTitle = Properties.Resources.Menu_UserGroupCollectionView;
            this.ViewStyle = ViewStyle;
            var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntitys().
                OrderBy(t => t.UserGroupNo).ThenBy(m => m.UserGroupName);
            Items = new ObservableCollection<FirstUserGroupEntity>(list);
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
}
