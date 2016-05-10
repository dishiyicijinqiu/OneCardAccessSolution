using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.Core
{
    public class TransferHelper
    {
        const string UpLoadKey = "9D36AAB8-7AFB-4EC8-B3B7-A675CCD0C54C";
        static string DefaultUpLoadHandlerURL = "http://localhost/OneCardAccessServer/UploadHandler.ashx";
        static string ConfigUpLoadHandlerURL = System.Configuration.ConfigurationManager.AppSettings["UpLoadHandlerURL"];
        static string _UpLoadHandlerURL;
        static string UpLoadHandlerURL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_UpLoadHandlerURL))
                {
                    if (string.IsNullOrWhiteSpace(ConfigUpLoadHandlerURL))
                    {
                        _UpLoadHandlerURL = DefaultUpLoadHandlerURL;
                    }
                    else
                    {
                        _UpLoadHandlerURL = ConfigUpLoadHandlerURL;
                    }
                }
                return _UpLoadHandlerURL;
            }
        }



        static string DefaultBaseFileAttachMentURL = "http://localhost/OneCardAccessServer/FileAttachMent";
        static string ConfigBaseFileAttachMentURL = System.Configuration.ConfigurationManager.AppSettings["BaseFileAttachMentURL"];
        static string _BaseFileAttachMentURL;
        static string BaseFileAttachMentURL
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_BaseFileAttachMentURL))
                {
                    if (string.IsNullOrWhiteSpace(ConfigBaseFileAttachMentURL))
                    {
                        _BaseFileAttachMentURL = DefaultBaseFileAttachMentURL;
                    }
                    else
                    {
                        _BaseFileAttachMentURL = ConfigBaseFileAttachMentURL;
                    }
                }
                return _BaseFileAttachMentURL;
            }
        }



        public static string UploadFile(string filetype, string filepath, bool isrondomname = false, string savename = null)
        {
            System.Net.WebClient webclient = new System.Net.WebClient();
            webclient.Headers["key"] = UpLoadKey;
            webclient.Headers["FileType"] = filetype;
            if (!string.IsNullOrWhiteSpace(savename))
            {
                webclient.Headers["SaveName"] = savename;
            }
            else
            {
                webclient.Headers["IsRondomName"] = isrondomname ? bool.TrueString : bool.FalseString;
            }
            byte[] responseArray = webclient.UploadFile(UpLoadHandlerURL, "Post", filepath);
            return System.Text.Encoding.GetEncoding("UTF-8").GetString(responseArray);
        }
        public static string DownloadFile(string filetype, string filepath, string savename = null)
        {
            System.Net.WebClient webclient = new System.Net.WebClient();
            if (string.IsNullOrWhiteSpace(savename))
            {
                string extension = Path.GetExtension(filepath);
                savename = Path.Combine(PCConfig.TempDir, Guid.NewGuid().ToString() + extension);
            }
            webclient.DownloadFile(string.Format("{0}/{1}/{2}", BaseFileAttachMentURL, filetype, filepath), savename);
            return savename;
        }
    }
}
