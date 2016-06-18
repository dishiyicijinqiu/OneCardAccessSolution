using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.ViewModel;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.RBAC
{
    public class PermissionSetViewModel : BaseNotificationObject, IPermissionSetViewModel
    {
        public event OnEntityViewEdited<string> OnPermissionSeted;
        public ICommand SaveCommand { get; private set; }
        public PermissionSetViewModel(UserGroupEntity userGroupEntity)
        {
            this.UserGroupEntity = userGroupEntity;
            UserGroup_PermissionEntity = ServiceProxyFactory.Create<IRBACService>().GetUserGroupPermissionByUserGroupId(userGroupEntity.UserGroupId);
            if (UserGroup_PermissionEntity == null)
                UserGroup_PermissionEntity = new UserGroup_PermissionEntity();
            var permissionCateEntitys = ServiceProxyFactory.Create<IRBACService>().GetPermissionCateEntitys();
            var permissionEntitys = ServiceProxyFactory.Create<IRBACService>().GetPermissionEntitys();
            this.Items = new ObservableCollection<V_PermissionCateEntity>();
            foreach (var permissionCateEntity in permissionCateEntitys)
            {
                if (string.IsNullOrWhiteSpace(permissionCateEntity.TreeParentNo))
                    continue;
                var v_permissionCateEntity = new V_PermissionCateEntity();
                v_permissionCateEntity.CopyValueFrom(permissionCateEntity);
                var temppermissionEntitys = permissionEntitys.Where(t => t.PermissionCateId == v_permissionCateEntity.PermissionCateId).ToList();
                int linecount = (temppermissionEntitys.Count / PermissionCountPerLine) + (temppermissionEntitys.Count % PermissionCountPerLine == 0 ? 0 : 1);
                v_permissionCateEntity.V_PermissionLineHolderEntitys = new ObservableCollection<V_PermissionLineHolderEntity>();
                for (int i = 0; i < linecount - 1; i++)
                {
                    var v_permissionLineHolderEntity = new V_PermissionLineHolderEntity();
                    v_permissionLineHolderEntity.V_PermissionEntitys = new ObservableCollection<V_PermissionEntity>();
                    for (int j = 0; j < PermissionCountPerLine - 1; j++)
                    {
                        int permissionIndex = i * 5 + j;
                        if (permissionIndex >= temppermissionEntitys.Count)
                            break;
                        var v_PermissionEntity = new V_PermissionEntity();
                        v_PermissionEntity.CopyValueFrom(temppermissionEntitys[permissionIndex]);
                        v_PermissionEntity.HasPermission = UserGroup_PermissionEntity.HasPermission(v_PermissionEntity);
                        v_permissionLineHolderEntity.V_PermissionEntitys.Add(v_PermissionEntity);
                    }
                    v_permissionCateEntity.V_PermissionLineHolderEntitys.Add(v_permissionLineHolderEntity);
                }
                this.Items.Add(v_permissionCateEntity);
            }
            SaveCommand = new DelegateCommand(Save);
        }

        public void Save()
        {
            try
            {
                this.UserGroup_PermissionEntity.PermissionId1 = 0;
                this.UserGroup_PermissionEntity.PermissionId2 = 0;
                this.UserGroup_PermissionEntity.PermissionId3 = 0;
                this.UserGroup_PermissionEntity.PermissionId4 = 0;
                this.UserGroup_PermissionEntity.PermissionId5 = 0;
                foreach (V_PermissionCateEntity v_permissioncateentity in Items)
                {
                    foreach (var v_permissionlineholderentity in v_permissioncateentity.V_PermissionLineHolderEntitys)
                    {
                        foreach (var v_permissionentity in v_permissionlineholderentity.V_PermissionEntitys)
                        {
                            if (v_permissionentity.HasPermission)
                            {
                                this.UserGroup_PermissionEntity.PermissionId1 = this.UserGroup_PermissionEntity.PermissionId1 | v_permissionentity.PermissionId1;
                                this.UserGroup_PermissionEntity.PermissionId2 = this.UserGroup_PermissionEntity.PermissionId2 | v_permissionentity.PermissionId2;
                                this.UserGroup_PermissionEntity.PermissionId3 = this.UserGroup_PermissionEntity.PermissionId3 | v_permissionentity.PermissionId3;
                                this.UserGroup_PermissionEntity.PermissionId4 = this.UserGroup_PermissionEntity.PermissionId4 | v_permissionentity.PermissionId4;
                                this.UserGroup_PermissionEntity.PermissionId5 = this.UserGroup_PermissionEntity.PermissionId5 | v_permissionentity.PermissionId5;
                            }
                        }
                    }
                }
                this.UserGroup_PermissionEntity.UserGroupId = UserGroupEntity.UserGroupId;
                ServiceProxyFactory.Create<IRBACService>().SaveUserGroup_PermissionEntity(this.UserGroup_PermissionEntity);
                ShowMessage(Properties.Resources.Info_SaveSuccess);
                OnPermissionSeted?.Invoke(this, null);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
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

        private UserGroup_PermissionEntity _UserGroup_PermissionEntity;

        public UserGroup_PermissionEntity UserGroup_PermissionEntity
        {
            get { return _UserGroup_PermissionEntity; }
            set
            {
                _UserGroup_PermissionEntity = value;
                RaisePropertyChanged("UserGroup_PermissionEntity");
            }
        }


        public ObservableCollection<V_PermissionCateEntity> Items { get; set; }


        private int _PermissionCountPerLine = 5;

        public int PermissionCountPerLine
        {
            get { return _PermissionCountPerLine; }
            set
            {
                _PermissionCountPerLine = value;
                RaisePropertyChanged("PermissionCountPerLine");
            }
        }
    }

    public class V_PermissionCateEntity : PermissionCateEntity
    {
        public ObservableCollection<V_PermissionLineHolderEntity> V_PermissionLineHolderEntitys { get; set; }
    }
    public class V_PermissionLineHolderEntity : NotificationObject
    {
        public ObservableCollection<V_PermissionEntity> V_PermissionEntitys { get; set; }
    }
    public class V_PermissionEntity : PermissionEntity
    {
        private bool _HasPermission;

        public bool HasPermission
        {
            get { return _HasPermission; }
            set
            {
                _HasPermission = value;
                RaisePropertyChanged("HasPermission");
            }
        }
    }
}
