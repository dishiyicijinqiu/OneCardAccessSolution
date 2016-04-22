using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class P_CRTempCollectionViewModel : DefaultViewModel
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public P_CRTempCollectionViewModel()
        {
            Messenger.Default.Register<P_CRTempEditMessage>(this, this, OnEdited);
        }
        #region Services
        IDocumentManagerService GetDialogWindowService()
        {
            return this.GetService<IDocumentManagerService>("DialogWindowService", ServiceSearchMode.PreferParents);
        }
        #endregion
        #region propertys

        ObservableCollection<P_CRTempEntity> _Items;
        public ObservableCollection<P_CRTempEntity> Items
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
                IDocument document = GetDialogWindowService().CreateDocument("P_CRTempView", new RegisterEditMessage(), this);
                document.Show();
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void CopyAdd(P_CRTempEntity SelectedEntity)
        {
            try
            {
                IDocument document = GetDialogWindowService().CreateDocument("P_CRTempView",
                    new RegisterEditMessage(_CopyKey: SelectedEntity.P_CRTempId, _EntityEditMode: EntityEditMode.CopyAdd)
                    , this);
                document.Show();
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void Edit(P_CRTempEntity SelectedEntity)
        {
            try
            {
                if (SelectedEntity == null)
                {
                    MessageBoxService.ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                IDocument document = GetDialogWindowService().CreateDocument("P_CRTempView", new RegisterEditMessage(SelectedEntity.P_CRTempId, EntityEditMode.Edit), this);
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
                    var list = basicinfoservice.GetP_CRTempEntitys();
                    Items = new ObservableCollection<P_CRTempEntity>(list);
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
        public void OnEdited(P_CRTempEditMessage message)
        {
            try
            {
                switch (message.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        {
                            var newItem = basicinfoservice.GetP_CRTempEntityById(message.Key);
                            Items.Add(newItem);
                            if (message.IsContinue)
                                Add();
                        }
                        break;
                    case EntityEditMode.CopyAdd:
                        {
                            var newItem = basicinfoservice.GetP_CRTempEntityById(message.Key);
                            Items.Add(newItem);
                            if (message.IsContinue)
                            {
                                var copyItem = Items.FirstOrDefault(t => t.P_CRTempId == message.CopyKey);
                                CopyAdd(copyItem);
                            }
                        }
                        break;
                    case EntityEditMode.Edit:
                        {
                            var oldItem = Items.FirstOrDefault(t => t.P_CRTempId == message.Key);
                            if (oldItem == null) return;
                            var newItem = basicinfoservice.GetP_CRTempEntityById(message.Key);
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
            Messenger.Default.Unregister<P_CRTempEditMessage>(this);
            base.OnClose(e);
        }
        #endregion
    }
    #region message
    public class P_CRTempEditMessage : EditMessage<int>
    {
        public P_CRTempEditMessage(int _Key = 0, EntityEditMode _EntityEditMode = EntityEditMode.Add, bool _IsContinue = false, int _CopyKey = 0)
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
