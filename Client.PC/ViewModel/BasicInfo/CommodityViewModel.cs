using FengSharp.OneCardAccess.Client.PC.Interfaces;
using FengSharp.OneCardAccess.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class CommodityViewModel : BaseNotificationObject, ICommodityViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public CommodityViewModel(CommodityEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            Entity = FirstCommodityEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.CopyAdd:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstCommodityEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "CommodityId"
                        });
                    }
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstCommodityEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstCommodityEntity.CreateEntity();
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
        private FirstCommodityEntity _Entity;

        public FirstCommodityEntity Entity
        {
            get { return _Entity; }
            set
            {
                _Entity = value;
                RaisePropertyChanged("Entity");
            }
        }
        #endregion
        public void Save()
        {
            (this.Parameter as CommodityEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }

        public void SaveAndNew()
        {
            (this.Parameter as CommodityEditMessage).IsContinue = true;
            this.SaveCore();
        }

        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as CommodityEditMessage;
                if (string.IsNullOrWhiteSpace(para.TreeParentNo))
                {
                    ShowError(Properties.Resources.Error_FatherCanNotEmpty);
                    return false;
                }
                this.Entity.TreeParentNo = para.TreeParentNo;
                para.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveCommodityEntity(this.Entity);
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
    }
}
