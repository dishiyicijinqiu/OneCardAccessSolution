using DevExpress.Mvvm;
using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.ComponentModel;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : DefaultViewModel
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        #region propertys
        public RegisterEntity Entity { get; set; }
        #endregion
        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            RegisterEditMessage paramsg = parameter as RegisterEditMessage;
            if (!ViewModelBase.IsInDesignMode)
            {
                if (paramsg == null)
                    throw new Exception(Properties.Resources.ParaMeterIsError);
                switch (paramsg.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        break;
                    case EntityEditMode.CopyAdd:
                    case EntityEditMode.Edit:
                        Entity = basicinfoservice.GetRegisterEntityById(paramsg.Key);
                        break;
                    default:
                        throw new Exception(Properties.Resources.ParaMeterIsError);
                }
                if (Entity == null)
                    Entity = RegisterEntity.CreateEntity();
            }
        }
        public void Close()
        {
            if (CurrentWindowService != null)
                CurrentWindowService.Close();
        }
        EntityEditMode _EntityEditMode = EntityEditMode.Add;

        public EntityEditMode EntityEditMode
        {
            get
            {
                return _EntityEditMode;
            }
            private set
            {
                if (_EntityEditMode == value) return;
                _EntityEditMode = value;
                RaisePropertyChanged("EntityEditMode");
            }
        }

        protected virtual ICurrentWindowService CurrentWindowService { get { return null; } }
        public void Add()
        {
        }
    }
}
