using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{


    public interface ICommodityCollectionView : IView
    {
    }
    public interface ICommodityView : IView
    {
    }
    public interface ICommodityCollectionViewModel : IViewModel
    {
    }
    public interface ICommodityCollectionSelectViewModel : IViewModel
    {
        event OnSelectedItems<BusinessEntity.BasicInfo.P_CRTempEntity> OnSelectedItems;
    }
    public interface ICommodityViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
}
