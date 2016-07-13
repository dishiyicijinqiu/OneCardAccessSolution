using FengSharp.OneCardAccess.BusinessEntity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Client.PC.Interfaces
{
    public interface IPermissionSetView : IView
    {
    }
    public interface IPermissionSetViewModel : IViewModel
    {
        event OnEntityViewEdited<string> OnPermissionSeted;
        UserGroupEntity UserGroupEntity { get; set; }
    }
}
