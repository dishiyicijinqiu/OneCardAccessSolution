using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System.Collections.ObjectModel;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterCollectionViewModel : ViewModelBase
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterCollectionViewModel()
        {
            if (!IsInDesignMode)
            {
                var list = basicinfoservice.GetRegisterList();
                Items = new ObservableCollection<RegisterEntity>(list);
            }
        }
        #region propertys
        private ObservableCollection<RegisterEntity> _Items;
        public ObservableCollection<RegisterEntity> Items
        {
            get
            {
                return _Items;
            }
            private set
            {
                _Items = value;
                RaisePropertyChanged("Items");
            }
        }
        #endregion
        #region commandmethods
        public void Add()
        {
            var iwindowservice = this.GetService<IWindowService>();
            iwindowservice.Show("RegisterView",
                new RegisterViewModel(
                    new RegisterEditMessage()
                    {
                        Key = 0,
                        EntityEditMode = EntityEditMode.Add,
                        IsContinue = false,
                        CopyKey = 0,
                    }
                    ), null, this);

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
        #endregion
    }
    public class RegisterEditMessage : EditMessage<int>
    {
        public int CopyKey { get; set; }
    }
}
