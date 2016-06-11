using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class PermissionSetViewModel : BaseNotificationObject, IPermissionSetViewModel
    {
        public ICommand SaveCommand { get; private set; }
        public PermissionSetViewModel(UserGroupEntity userGroupEntity)
        {
            this.UserGroupEntity = userGroupEntity;
            SaveCommand = new DelegateCommand(Save);
        }

        public void Save()
        {
          
        }

        private UserGroupEntity _UserGroupEntity;

        public UserGroupEntity UserGroupEntity
        {
            get { return _UserGroupEntity; }
            set
            {
                _UserGroupEntity = value;
                RaisePropertyChanged("UserGroupEntity");
            }
        }

    }
}
