using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IP_CRTempEdit : IViewModel
    {
        event OnEntityViewEdited OnEntityViewEdited;
    }
    public delegate void OnEntityViewEdited(IViewModel vm, P_CRTempEditMessage EditMessage);

    public interface IP_CRTempCollectionView : IViewModel
    {
    }
    public interface IP_CRTempCollectionSelect : IViewModel
    {
        List<BusinessEntity.BasicInfo.P_CRTempEntity> SelectItems { get; set; }
    }
}
