using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IP_CRTempCollectionView : IView
    {
    }
    public interface IP_CRTempView : IView
    {
    }
    public interface IP_CRTempCollectionViewModel : IViewModel
    {
    }
    public interface IP_CRTempCollectionSelectViewModel : IViewModel
    {
        event OnSelectedItems<BusinessEntity.BasicInfo.P_CRTempEntity> OnSelectedItems;
    }
    public interface IP_CRTempViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
}
