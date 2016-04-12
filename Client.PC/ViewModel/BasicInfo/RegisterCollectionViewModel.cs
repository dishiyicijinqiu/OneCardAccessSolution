using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    [POCOViewModel]
    public class RegisterCollectionViewModel : BaseDocumentContent
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterCollectionViewModel()
        {
            if (!ViewModelBase.IsInDesignMode)
            {
                var list = basicinfoservice.GetRegisterList();
                Items = new ObservableCollection<RegisterEntity>(list);
            }
        }
        #region propertys
        public IList<RegisterEntity> Items
        {
            get; protected set;
        }
        #endregion
        #region commandmethods
        public void Add()
        {
            DialogWindowService.Show("RegisterView", new RegisterEditMessage()
            {
                Key = 0,
                EntityEditMode = EntityEditMode.Add,
                IsContinue = false,
                CopyKey = 0,
            }, this);
        }
        public void CopyAdd()
        {

        }
        public void Edit()
        {

        }
        public void Delete()
        {

        }
        public void MouseDoubleClick(RegisterEntity entity)
        {

        }
        
        #endregion

        public void Close()
        {
            if (DocumentManagerService != null)
            {
                IDocument document = DocumentManagerService.FindDocument(this);
                if (document != null)
                    document.Close();
            };
        }
    }
    public class RegisterEditMessage : EditMessage<int>
    {
        public int CopyKey { get; set; }
    }
}
