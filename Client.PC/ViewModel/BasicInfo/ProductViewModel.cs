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
    public class ProductViewModel : BaseNotificationObject, IProductViewModel
    {
        public event OnEntityViewEdited<string> OnEntityViewEdited;
        public ICommand SaveAndNewCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ProductViewModel(ProductEditMessage EditMessage)
        {
            this.Parameter = EditMessage;
            if (this.Parameter == null)
                throw new Exception(Properties.Resources.Error_ParameterIsError);
            SaveAndNewCommand = new DelegateCommand(SaveAndNew);
            SaveCommand = new DelegateCommand(Save);
            Entity = FirstProductEntity.CreateEntity();
            switch (EditMessage.EntityEditMode)
            {
                case EntityEditMode.CopyAdd:
                    {
                        var copyEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstProductEntityById(EditMessage.CopyKey);
                        Entity.CopyValueFrom(copyEntity,
                            new List<string>(PCConfig.CreateAndModifyInfoColNames)
                        {
                            "ProductId"
                        });
                    }
                    break;
                case EntityEditMode.See:
                case EntityEditMode.Edit:
                    {
                        var newEntity = ServiceProxyFactory.Create<IBasicInfoService>().GetFirstProductEntityById(EditMessage.Key);
                        Entity.CopyValueFrom(newEntity);
                    }
                    break;
            }
            if (Entity == null)
                Entity = FirstProductEntity.CreateEntity();
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
        private FirstProductEntity _Entity;

        public FirstProductEntity Entity
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
            (this.Parameter as ProductEditMessage).IsContinue = false;
            if (this.SaveCore())
                this.Close();
        }

        public void SaveAndNew()
        {
            (this.Parameter as ProductEditMessage).IsContinue = true;
            this.SaveCore();
        }

        bool SaveCore()
        {
            try
            {
                var para = this.Parameter as ProductEditMessage;
                if (string.IsNullOrWhiteSpace(para.TreeParentNo))
                {
                    ShowError(Properties.Resources.Error_FatherCanNotEmpty);
                    return false;
                }
                this.Entity.TreeParentNo = para.TreeParentNo;
                para.Key = ServiceProxyFactory.Create<IBasicInfoService>().SaveProductEntity(this.Entity);
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
