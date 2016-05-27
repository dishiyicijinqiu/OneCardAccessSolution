namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IUserGroupCollectionView : IView
    {
    }
    public interface IUserGroupView : IView
    {
    }
    public interface IUserGroupCollectionViewModel : IViewModel
    {
    }
    public interface IUserGroupViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
    public interface IUserGroupCollectionSelectViewModel : IViewModel
    {
        event OnSelectedItems<BusinessEntity.RBAC.UserGroupEntity> OnSelectedItems;
    }


    public interface IUserAndGroupManagerView : IView
    {
    }
    public interface IUserAndGroupManagerViewModel : IViewModel
    {
    }
}
