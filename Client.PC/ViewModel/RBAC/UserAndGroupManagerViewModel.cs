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
    public class UserAndGroupManagerViewModel : BaseNotificationObject, IUserAndGroupManagerViewModel
    {
        public UserAndGroupManagerViewModel()
        {
            
            this.UserGroupCollectionViewModel = new UserGroupCollectionViewModel();
            this.UserCollectionViewModel = new UserCollectionViewModel();
            this.UserGroupCollectionViewModel.MenuTitle = this.UserCollectionViewModel.MenuTitle = Properties.Resources.View_UserAndGroupManagerView_Title;
            UserGroupCollectionViewModel.PropertyChanged += UserGroupCollectionViewModel_PropertyChanged;
        }

        private void UserGroupCollectionViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                if (e.PropertyName == "SelectedEntity")
                {
                    var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntitysByUserGroupId(UserGroupCollectionViewModel.SelectedEntity.UserGroupId).
                         OrderBy(t => t.UserNo).ThenBy(m => m.UserNo);
                    UserCollectionViewModel.Items.Clear();
                    foreach (var item in list)
                    {
                        UserCollectionViewModel.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        public UserGroupCollectionViewModel UserGroupCollectionViewModel { get; private set; }
        public UserCollectionViewModel UserCollectionViewModel { get; private set; }
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
