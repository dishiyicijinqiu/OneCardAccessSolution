using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Core;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : DefaultViewModel
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterCollectionViewModel()
        {
        }
        #region Services
        IDocumentManagerService GetDialogWindowService()
        {
            return this.GetService<IDocumentManagerService>("DialogWindowService", ServiceSearchMode.PreferParents);
        }
        #endregion
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
            IDocument document = GetDialogWindowService().CreateDocument("RegisterView", new RegisterEditMessage(), this);
            document.Show();
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

                //IDocumentManagerService service = this.GetService<IDocumentManagerService>("SignleObjectDocumentManagerService");
                IDocument document = GetDialogWindowService().CreateDocument("RegisterView", new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit), this);

                //IDocument document = GetDocumentManagerService().CreateDocument("RegisterView", new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit), this);
                document.Show();


                //DialogWindowService.Show("RegisterView",
                //    new RegisterViewModel(new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit))
                //    , null, this);
                //DialogWindowService.Show("RegisterView", new RegisterEditMessage(SelectedEntity.RegisterId, EntityEditMode.Edit), this);
            }
            catch (Exception ex)
            {
                MessageBoxService.HandleException(ex);
            }
        }
        public void Delete()
        {

        }
        public void OnLoaded()
        {
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
                    this.Close();
                    MessageBoxService.HandleException(ex);
                }
            }
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
