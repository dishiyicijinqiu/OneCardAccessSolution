using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using FengSharp.OneCardAccess.Core;
namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    [POCOViewModel]
    public class RegisterCollectionViewModel : BaseDocumentContent
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterCollectionViewModel()
        {
        }
        #region propertys
        IList<FirstRegisterEntity> _Items;
        public IList<FirstRegisterEntity> Items
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
        public FirstRegisterEntity SelectedEntity { get; set; }
        #endregion
        #region commandmethods
        public void Add()
        {
            DialogWindowService.Show("RegisterView", new RegisterEditMessage(), this);
        }
        public void CopyAdd()
        {

        }
        public void Edit()
        {
            try
            {
                if (SelectedEntity == null)
                {
                    MessageBoxService.ShowMessage(Properties.Resources.Info_SelectAtLeastOne);
                    return;
                }
                DialogWindowService.Show("RegisterView",
                    new RegisterViewModel(new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit))
                    , null, this);
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void Delete()
        {

        }
        public void RowDoubleClick()
        {
            Edit();
        }
        public void OnLoaded()
        {
            if (this.IsLoaded)
                return;
            if (!ViewModelBase.IsInDesignMode)
            {
                try
                {
                    var list = basicinfoservice.GetFirstRegisterList();
                    //Items = new ObservableCollection<RegisterEntity>(list);
                    Items = list;
                }
                catch (Exception ex)
                {
                    Close();
                    MessageBoxService.HandleException(ex);
                }
            }
            this.IsLoaded = true;
        }
        #endregion
    }
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
}
