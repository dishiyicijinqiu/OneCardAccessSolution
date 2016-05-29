using FengSharp.OneCardAccess.Common;
using FengSharp.OneCardAccess.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FengSharp.OneCardAccess.Server
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {
        const string ServerKey = "9D36AAB8-7AFB-4EC8-B3B7-A675CCD0C54C";
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                var key = HttpUtility.UrlDecode(context.Request.Headers["key"], Encoding.UTF8);
                if (key != ServerKey)
                {
                    throw new Exception(Properties.Resources.Error_InvalidUpload);
                }
                var filepath = HttpUtility.UrlDecode(context.Request.Headers["FilePath"], Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(filepath))
                {
                    throw new Exception(Properties.Resources.Error_FileType);
                }
                HttpPostedFile file = context.Request.Files[0];
                var saveName = HttpUtility.UrlDecode(context.Request.Headers["SaveName"], Encoding.UTF8);
                string extension = Path.GetExtension(file.FileName);
                if (string.IsNullOrWhiteSpace(saveName))
                {
                    var isRondomName = HttpUtility.UrlDecode(context.Request.Headers["IsRondomName"], Encoding.UTF8);
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
                string dir = Path.Combine(SystemServiceConfig.AttachBaseDir, filepath);
                if (!Directory.Exists(dir))
                {
                    throw new BusinessException(Properties.Resources.Error_FileAttachDirNotFound);
                }
                file.SaveAs(Path.Combine(dir, saveName));
                context.Response.Write(saveName);
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