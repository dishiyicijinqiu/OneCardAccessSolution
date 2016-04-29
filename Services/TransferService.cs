using FengSharp.OneCardAccess.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FengSharp.OneCardAccess.BusinessEntity;

namespace FengSharp.OneCardAccess.Services
{
    public class TransferService : ITransferService
    {
        static string DefaultAttachBaseDir = Path.Combine(Path.GetFullPath(System.Web.Hosting.HostingEnvironment.MapPath("~")), "FileAttachMent");
        static string ConfigAttachBaseDir = Path.Combine(Path.GetFullPath(System.Web.Hosting.HostingEnvironment.MapPath("~")), "FileAttachMent");
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
                        _AttachBaseDir = ConfigAttachBaseDir;
                    }
                }
                if (string.IsNullOrWhiteSpace(_AttachBaseDir))
                {
                    throw new Exception(Properties.Resources.Error_FileAttachDir);
                }
                return _AttachBaseDir;
            }
        }
        string GetFileDirByFileType(FileType filetype)
        {
            switch (filetype)
            {
                case FileType.P_CRTemp:
                    return filetype.ToString();
                default:
                    throw new Exception(Properties.Resources.Error_FileSaveDir);
            }
        }
        public Stream GetFile(FileType filetype, string fileName)
        {
            //FileType filetype = downloadinfo.FileType;
            //string fileName = downloadinfo.FileName;
            string fileFullPath = Path.Combine(AttachBaseDir, GetFileDirByFileType(filetype), fileName);//合并路径生成文件存放路径
            if (!File.Exists(fileFullPath))//判断文件是否存在
            {
                throw new Common.BusinessException(Properties.Resources.Error_FileNotFound);
            }
            return File.OpenRead(fileFullPath);
        }

        public List<FileEntity> GetFiles(FileType filetype, string filter)
        {
            if (!Directory.Exists(AttachBaseDir))
            {
                Directory.CreateDirectory(AttachBaseDir);
            }
            DirectoryInfo myDirInfo = new DirectoryInfo(Path.Combine(AttachBaseDir, GetFileDirByFileType(filetype)));
            return myDirInfo.GetFiles(filter).
                Select(t => new FileEntity()
                {
                    FileName = t.Name
                }
                ).
                ToList();
        }

        public void Save(UpLoadInfo uploadinfo)
        {
            if (!Directory.Exists(AttachBaseDir))
            {
                Directory.CreateDirectory(AttachBaseDir);
            }
            Stream sourceStream = uploadinfo.Stream;
            if (sourceStream == null)
            {
                throw new Exception(Properties.Resources.Error_StreamIsEmpty);
            }
            if (!sourceStream.CanRead)
            {
                throw new Exception(Properties.Resources.Error_StreamCanNotRead);
            }
            FileType filetype = uploadinfo.FileType;
            string saveName = uploadinfo.SaveName;
            string fileFullPath = Path.Combine(AttachBaseDir, GetFileDirByFileType(filetype), saveName);//合并路径生成文件存放路径
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                const int bufferLength = 4096;
                byte[] myBuffer = new byte[bufferLength];//数据缓冲区
                int count;
                while ((count = sourceStream.Read(myBuffer, 0, bufferLength)) > 0)
                {
                    fs.Write(myBuffer, 0, count);
                }
                fs.Close();
                sourceStream.Close();
            }
        }
    }
}
