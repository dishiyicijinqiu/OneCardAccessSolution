using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.CodeBuilder.Templates.RunTime
{
    public partial class RuntimeTableEntity
    {
        public string BuildPath { get; set; }
        public static void GeneCodeToPath(string buildpath, string tablename, string _namespace)
        {
            var obj = new RuntimeTableEntity();
            obj.BuildPath = buildpath;
            obj.TableName = tablename;
            obj.NameSpace = _namespace;
            string content = obj.TransformText();
            File.WriteAllText(buildpath, content);
        }
    }
}
