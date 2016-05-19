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
    public class UserCollectionViewModel : BaseNotificationObject, IUserConnectionViewModel, IUserCollectionSelectViewModel
    {
        public event OnSelectedItems<UserInfoEntity> OnSelectedItems;

        public UserCollectionViewModel() : this(ViewStyle.View) { }
        public UserCollectionViewModel(ViewStyle ViewStyle)
        {
            this.MenuTitle = Properties.Resources.Menu_UserCollectionView;
            this.ViewStyle = ViewStyle;
            //var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntitys().
            //    OrderBy(t => t.UserNo).ThenBy(m => m.UserNo);
            //Items = new ObservableCollection<FirstUserInfoEntity>(list);
            Items = new ObservableCollection<FirstUserInfoEntity>();
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
}
