using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IProductCollectionView : IView
    {

    }
    public interface IProductView : IView
    {
    }
    public interface IProductCollectionViewModel : IViewModel
    {

    }
    public interface IProductCollectionSelectViewModel : IViewModel
    {
        event OnSelectedItems<BusinessEntity.BasicInfo.P_CRTempEntity> OnSelectedItems;
    }
    public interface IProductViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
}
