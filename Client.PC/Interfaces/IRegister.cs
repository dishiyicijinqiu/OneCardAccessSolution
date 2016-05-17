using FengSharp.OneCardAccess.Client.PC.ViewModel.BasicInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IRegisterCollectionView : IViewModel
    {
    }
    public interface IRegisterEdit : IViewModel
    {
        event OnEntityViewEdited<string> OnEntityViewEdited;
    }
    public interface IRegisterCollectionSelect : IViewModel
    {
    }
}
