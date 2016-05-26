using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity
{

    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public enum MoveTree
    {
        [EnumMember]
        IntoNode,
        [EnumMember]
        After,
    }
}
