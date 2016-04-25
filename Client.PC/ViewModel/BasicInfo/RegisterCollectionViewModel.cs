﻿using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : NotificationObject
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterCollectionViewModel()
        {
        }
        #region propertys

        ObservableCollection<FirstRegisterEntity> _Items;
        public ObservableCollection<FirstRegisterEntity> Items
        {
            get
            {
                return _Items;
            }
            protected set
            {
                _Items = value;
                RaisePropertyChanged("Items");
            }
        }

        #endregion
        #region commandmethods
        public void Add()
        {
            try
            {
                IDocument document = GetDialogWindowService().CreateDocument("RegisterView", new RegisterEditMessage(), this);
                document.Show();
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void CopyAdd(FirstRegisterEntity SelectedEntity)
        {
            try
            {
                IDocument document = GetDialogWindowService().CreateDocument("RegisterView",
                    new RegisterEditMessage(_CopyKey: SelectedEntity.RegisterId, _EntityEditMode: EntityEditMode.CopyAdd)
                    , this);
                document.Show();
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void Edit(FirstRegisterEntity SelectedEntity)
        {
            try
            {
                if (SelectedEntity == null)
                {
                    MessageBoxService.ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                IDocument document = GetDialogWindowService().CreateDocument("RegisterView", new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit), this);
                document.Show();
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void Delete(System.Collections.IList SelectedEntitys)
        {
            try
            {
                if (SelectedEntitys == null && SelectedEntitys.Count <= 0)
                {
                    MessageBoxService.ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                var listToDelete = SelectedEntitys.Cast<RegisterEntity>().ToList();
                basicinfoservice.DeleteRegisterEntitys(listToDelete);
                MessageBoxService.ShowMessage(Properties.Resources.Info_DeleteSuccess);
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        private bool IsLoaded = false;
        public void OnLoaded()
        {
            if (!ViewModelBase.IsInDesignMode)
            {
                if (IsLoaded) return;
                try
                {
                    var list = basicinfoservice.GetFirstRegisterEntitys();
                    Items = new ObservableCollection<FirstRegisterEntity>(list);
                }
                catch (Exception ex)
                {
                    this.Close();
                    MessageBoxService.HandleException(ex);
                }
                finally
                {
                    IsLoaded = true;
                }
            }
        }
        #endregion
        #region methods
        public void OnEdited(RegisterEditMessage message)
        {
            try
            {
                switch (message.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = basicinfoservice.GetFirstRegisterEntityById(message.Key);
                            Items.Add(newItem);
                            if (message.IsContinue)
                                Add();
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = basicinfoservice.GetFirstRegisterEntityById(message.Key);
                            Items.Add(newItem);
                            if (message.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.RegisterId == message.CopyKey);
                                CopyAdd(copyItem);
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.RegisterId == message.Key);
                            if (oldItem == null) return;
                            var newItem = basicinfoservice.GetFirstRegisterEntityById(message.Key);
                            var itemIndex = Items.IndexOf(oldItem);
                            Items[itemIndex] = newItem;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        #endregion
        #region Events
        public override void OnClose(CancelEventArgs e)
        {
            Messenger.Default.Unregister<RegisterEditMessage>(this);
            base.OnClose(e);
        }
        #endregion
    }
    #region message
    public class RegisterEditMessage : EditMessage<int>
    {
        public RegisterEditMessage(int _Key = 0, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, int _CopyKey = 0)
        {
            Key = _Key;
            EntityEditMode = _EntityEditMode;
            IsContinue = _IsContinue;
            CopyKey = _CopyKey;
        }
        public int CopyKey { get; set; }
    }
    #endregion
}
