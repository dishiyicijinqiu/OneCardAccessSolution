namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IAttachmentDirCollectionView : IView
    {
    }
    public interface IAttachmentDirView : IView
    {
    }
    public interface IAttachmentDirCollectionViewModel : IViewModel
    {
    }
    public interface IAttachmentDirViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
}
