using System;
using System.Collections.Generic;
using System.IO;
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
        static string _TempDir = Path.Combine(Environment.CurrentDirectory, "TempData");
        internal static string TempDir
        {
            get
            {
                if (!Directory.Exists(_TempDir))
                {
                    Directory.CreateDirectory(_TempDir);
                }
                return _TempDir;
            }
        }
    }
}
