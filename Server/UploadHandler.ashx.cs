using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FengSharp.OneCardAccess.Server
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        static string DefaultAttachBaseDir = Path.Combine(Path.GetFullPath(System.Web.Hosting.HostingEnvironment.MapPath("~")), "FileAttachMent");
        static string ConfigAttachBaseDir = System.Configuration.ConfigurationManager.AppSettings["FileAttachMentPath"];
        static string _AttachBaseDir;
        static string AttachBaseDir
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

        //const string DefaultAttachBaseURL = "http://localhost/OneCardAccessServer/FileAttachMent";
        //static string ConfigAttachBaseURL = System.Configuration.ConfigurationManager.AppSettings["FileAttachMentURL"];
        //static string _AttachBaseURL;
        //static string AttachBaseURL
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(_AttachBaseURL))
        //        {
        //            if (string.IsNullOrWhiteSpace(ConfigAttachBaseURL))
        //            {
        //                _AttachBaseURL = DefaultAttachBaseURL;
        //            }
        //            else
        //            {
        //                _AttachBaseURL = ConfigAttachBaseURL;
        //            }
        //        }
        //        if (string.IsNullOrWhiteSpace(_AttachBaseURL))
        //        {
        //            throw new Exception(Properties.Resources.Error_FileAttachDir);
        //        }
        //        return _AttachBaseURL;
        //    }
        //}
        const string ServerKey = "9D36AAB8-7AFB-4EC8-B3B7-A675CCD0C54C";
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                var key = context.Request.Headers["key"];
                if (key != ServerKey)
                {
                    throw new Exception(Properties.Resources.Error_InvalidUpload);
                }
                var filetype = context.Request.Headers["FileType"];
                if (string.IsNullOrWhiteSpace(filetype))
                {
                    throw new Exception(Properties.Resources.Error_FileType);
                }
                HttpPostedFile file = context.Request.Files[0];
                var saveName = context.Request.Headers["SaveName"];
                string extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrWhiteSpace(saveName))
                {
                    var isRondomName = context.Request.Headers["IsRondomName"];
                    if (isRondomName == bool.TrueString)
                    {
                        saveName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    }
                    else
                    {
                        saveName = file.FileName;
                    }
                }
                if (string.Compare(extension, Path.GetExtension(saveName), true) != 0)
                {
                    throw new Exception(Properties.Resources.Error_DiffExtension);
                }
                string filedir = GetDirByFileType(filetype.ToLower());
                string dir = Path.Combine(AttachBaseDir, filedir);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                file.SaveAs(Path.Combine(dir, saveName));
                context.Response.Write(saveName);
            }
        }

        private string GetDirByFileType(string filetype)
        {
            switch (filetype)
            {
                case "p_crtemp":
                    return "P_CRTemp";
                case "register_file":
                    return "Register_File";
                default:
                    throw new Exception(Properties.Resources.Error_FileAttachDir);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}