﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Core
{
    internal class PCConfig
    {
        internal static IEnumerable<string> CreateAndModifyInfoColNames = new System.Collections.Generic.List<string>()
        {
            "CreateId","Creater","CreateDate","LastModifyId","LastModifyer","LastModifyDate"
        };
    }
}
