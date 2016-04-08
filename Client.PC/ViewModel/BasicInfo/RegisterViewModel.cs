using FengSharp.OneCardAccess.BusinessEntity.BasicInfo;
using FengSharp.OneCardAccess.Client.PC.Core;
using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo
{
    public class RegisterViewModel : DefaultViewModelBase
    {
        IBasicInfoService basicinfoservice = ServiceProxyFactory.Create<IBasicInfoService>();
        public RegisterViewModel(RegisterEditMessage paramsg)
        {
            if (!IsInDesignMode)
            {
                this.Parameter = paramsg;
                if (paramsg == null)
                    throw new Exception("传入参数为空");
                switch (paramsg.EntityEditMode)
                {
                    case EntityEditMode.Add:
                        break;
                    case EntityEditMode.CopyAdd:
                    case EntityEditMode.Edit:
                        _Entity = basicinfoservice.GetRegisterEntityById(paramsg.Key);
                        break;
                    default:
                        throw new Exception("传入参数错误");
                }
                if (_Entity == null)
                    _Entity = RegisterEntity.CreateEntity();
            }
        }
        #region propertys
        RegisterEntity _Entity;
        public RegisterEntity Entity
        {
            get
            {
                return _Entity;
            }
            set
            {
                if (_Entity != value)
                {
                    _Entity = value;
                    RaisePropertyChanged("Entity");
                }
            }
        }
        #endregion
    }
}
