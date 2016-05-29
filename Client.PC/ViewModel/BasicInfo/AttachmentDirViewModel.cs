using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class AttachmentDirViewModel : BaseNotificationObject, IAttachmentDirViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public AttachmentDirViewModel(AttachmentDirEditMessage EditMessage)
        {

            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            Entity = FirstAttachmentDirEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.Add:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "AttachmentDirId"
                        });
                    }
                    break;
                case EntityEditMode.CopyAdd:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "AttachmentDirId"
                        });
                    }
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstAttachmentDirEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstAttachmentDirEntity.CreateEntity();
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
        private FirstAttachmentDirEntity _Entity;

        public FirstAttachmentDirEntity Entity
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
            (this.Parameter as AttachmentDirEditMessage).IsContinue = true;
            this.SaveCore();
        }
        public void Save()
        {
            (this.Parameter as AttachmentDirEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }
        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as AttachmentDirEditMessage;
                if (string.IsNullOrWhiteSpace(para.TreeParentNo))
                {
                    ShowError(Properties.Resources.Error_FatherCanNotEmpty);
                    return false;
                }
                this.Entity.TreeParentNo = para.TreeParentNo;
                if (!string.IsNullOrWhiteSpace(this.Entity.Filter))
                {
                    string[] formats = this.Entity.Filter.Split(';');
                    if (formats == null)
                    {
                        ShowError(Properties.Resources.Error_Filter);
                        return false;
                    }
                    foreach (var item in formats)
                    {
                        if (!item.StartsWith("*."))
                        {
                            ShowError(Properties.Resources.Error_Filter);
                            return false;
                        }
                        if (System.Text.RegularExpressions.Regex.IsMatch("^[a-zA-Z]", item.Substring(2)))
                        {
                            ShowError(Properties.Resources.Error_Filter);
                            return false;
                        }
                    }
                }
                para.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveAttachmentDirEntity(this.Entity);
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
