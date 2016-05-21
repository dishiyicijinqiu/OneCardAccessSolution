using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class UserGroupViewModel : BaseNotificationObject, IUserGroupViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public UserGroupViewModel(UserGroupEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            Entity = FirstUserGroupEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.Add:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "UserGroupId"
                        });
                    }
                    break;
                case EntityEditMode.CopyAdd:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "UserGroupId"
                        });
                    }
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IRBACService>().GetFirstUserGroupEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstUserGroupEntity.CreateEntity();
            IsSee = EditMessage.EntityEditMode == EntityEditMode.See;
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
        private FirstUserGroupEntity _Entity;

        public FirstUserGroupEntity Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
        #endregion
        #region methods
        public void SaveAndNew()
        {
            (this.Parameter as UserGroupEditMessage).IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            (this.Parameter as UserGroupEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }
        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as UserGroupEditMessage;
                if (string.IsNullOrWhiteSpace(para.TreeParentNo))
                {
                    ShowError(Properties.Resources.Error_FatherCanNotEmpty);
                    return false;
                }
                this.Entity.TreeParentNo = para.TreeParentNo;
                para.Key = ServiceProxyFactory.Create<IRBACService>().SaveUserGroupEntity(this.Entity);
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
