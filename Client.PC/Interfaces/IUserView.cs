using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IUserCollectionView : IView
    {
    }
    public interface IUserView : IView
    {
    }
    public interface IUserCollectionViewModel : IViewModel
    {
    }
    public interface IUserViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
    public interface IUserCollectionSelectViewModel : IViewModel
    {
        event OnSelectedItems<BusinessEntity.RBAC.UserInfoEntity> OnSelectedItems;
    }
}
