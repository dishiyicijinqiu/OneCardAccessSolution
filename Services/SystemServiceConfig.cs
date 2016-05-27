using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Services
{
    public class SystemServiceConfig
    {
        static string DefaultAttachBaseDir = Path.Combine(Path.GetFullPath(System.Web.Hosting.HostingEnvironment.MapPath("~")), "FileAttachMent");
        static string ConfigAttachBaseDir = System.Configuration.ConfigurationManager.AppSettings["FileAttachMentPath"];
        static string _AttachBaseDir;
        public static string AttachBaseDir
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_AttachBaseDir))
                {
                    if (string.IsNullOrWhiteSpace(ConfigAttachBaseDir))
                    {
                        _AttachBaseDir = DefaultAttachBaseDir;
                    }
                    else
                    {
                        _AttachBaseDir = Path.GetFullPath(ConfigAttachBaseDir);
                    }
                }
                if (string.IsNullOrWhiteSpace(_AttachBaseDir))
                {
                    throw new Exception(Properties.Resources.Error_FileAttachDir);
                }
                return _AttachBaseDir;
            }
        }
    }
}
