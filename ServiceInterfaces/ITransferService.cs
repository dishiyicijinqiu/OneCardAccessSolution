using FengSharp.OneCardAccess.BusinessEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace FengSharp.OneCardAccess.ServiceInterfaces
{
    [ServiceContract]
    public interface ITransferService
    {
        //[OperationContract]
        //Stream GetFile(DownLoadInfo downloadinfo);
        [OperationContract]
        Stream GetFile(FileType filetype, string fileName);
        [OperationContract]
        List<FileEntity> GetFiles(FileType filetype, string filter);
        [OperationContract(IsOneWay = true)]
        void Save(UpLoadInfo uploadinfo);
    }
    [MessageContract]
    public class DownLoadInfo
    {
        public DownLoadInfo(FileType FileType, string FileName)
        {
            this.FileType = FileType;
            this.FileName = FileName;
        }
        [MessageHeader]
        public FileType FileType { get; set; }
        [MessageHeader]
        public string FileName { get; set; }
    }
    [MessageContract]
    public class UpLoadInfo : IDisposable
    {
        public UpLoadInfo(Stream Stream, FileType FileType, string SaveName)
        {
            this.Stream = Stream;
            this.FileType = FileType;
            this.SaveName = SaveName;
        }
        [MessageBodyMember]
        public Stream Stream { get; set; }
        [MessageHeader]
        public FileType FileType { get; set; }
        [MessageHeader]
        public string SaveName { get; set; }
        public void Dispose()
        {
            if (Stream != null)
            {
                Stream.Close();
                Stream = null;
            }
        }
    }
}
