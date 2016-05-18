using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IRegisterCollectionView : IView
    {
    }
    public interface IRegisterView : IView
    {
    }
    public interface IRegisterCollectionViewModel : IViewModel
    {
    }
    public interface IRegisterViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
    public interface IRegisterCollectionSelectViewModel : IViewModel
    {
    }
}
