using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserViewModel : BaseNotificationObject, IUserViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public UserViewModel(UserEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            Entity = FirstUserInfoEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.Add:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "UserId"
                        });
                    }
                    break;
                case EntityEditMode.CopyAdd:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "UserId"
                        });
                    }
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IRBACService>().GetFirstUserInfoEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstUserInfoEntity.CreateEntity();
            IsSee = EditMessage.EntityEditMode == EntityEditMode.See;

            var list = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntitys().OrderBy(t => t.UserGroupNo).ThenBy(m => m.UserGroupName);
            UserGroupItems = new ObservableCollection<FirstUserGroupEntity>(list);
        }

        #region propertys
        bool _IsSee = false;
        public bool IsSee
        {
            get { return _IsSee; }
            set
            {
                _IsSee = value;
                RaisePropertyChanged("IsSee");
            }
        }
        private FirstUserInfoEntity _Entity;

        public FirstUserInfoEntity Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
        public ObservableCollection<FirstUserGroupEntity> UserGroupItems { get; private set; }
        #endregion
        #region methods
        public void SaveAndNew()
        {
            (this.Parameter as UserEditMessage).IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            (this.Parameter as UserEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }
        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as UserEditMessage;
                para.Key = ServiceProxyFactory.Create<IRBACService>().SaveUserEntity(this.Entity);
                if (para.Key == null || para.Key.Length != 36)
                {
                    ShowMessage(Properties.Resources.Error_SaveFiled);
                    return false;
                }
                ShowMessage(Properties.Resources.Info_SaveSuccess);
                OnEntityViewEdited?.Invoke(this, para);
                return true;
            }
            catch (Exception ex)
            {
                ShowException(ex);
                return false;
            }
        }

        #endregion
    }
}
