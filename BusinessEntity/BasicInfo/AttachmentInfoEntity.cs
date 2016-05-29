using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 附件信息表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class AttachmentInfoEntity : NotificationObject
    {
        public AttachmentInfoEntity()
        {
        }

        private string _AttachmentName;
        /// <summary>
        /// 附件名称
        /// </summary>
        [DataMember]
        public string AttachmentName
        {
            get { return _AttachmentName; }
            set
            {
                if (_AttachmentName == value)
                    return;
                _AttachmentName = value;
                RaisePropertyChanged("AttachmentName");
            }
        }
        private string _AttachmentDirId;
        /// <summary>
        /// 附件目录Id
        /// </summary>
        [DataMember]
        public string AttachmentDirId
        {
            get { return _AttachmentDirId; }
            set
            {
                if (_AttachmentDirId == value)
                    return;
                _AttachmentDirId = value;
                RaisePropertyChanged("AttachmentDirId");
            }
        }
    }
    [KnownType(typeof(FirstAttachmentInfoEntity))]
    [KnownType(typeof(UpLoadAttachmentInfoEntity))]
    public partial class AttachmentInfoEntity
    {
        public static AttachmentInfoEntity CreateEntity()
        {
            return new AttachmentInfoEntity()
            {
                AttachmentDirId = string.Empty,
                AttachmentName = string.Empty
            };
        }
    }

    public class FirstAttachmentInfoEntity : AttachmentInfoEntity
    {
        public new static FirstAttachmentInfoEntity CreateEntity()
        {
            return new FirstAttachmentInfoEntity()
            {
                AttachmentDirId = string.Empty,
                AttachmentName = string.Empty,
                AttachmentPath = string.Empty
            };
        }
        private string _AttachmentPath;
        /// <summary>
        /// 上传路径
        /// </summary>
        [DataMember]
        public string AttachmentPath
        {
            get { return _AttachmentPath; }
            set
            {
                if (_AttachmentPath == value)
                    return;
                _AttachmentPath = value;
                RaisePropertyChanged("AttachmentPath");
            }
        }
    }
    public class UpLoadAttachmentInfoEntity : AttachmentInfoEntity
    {
        public new static UpLoadAttachmentInfoEntity CreateEntity()
        {
            return new UpLoadAttachmentInfoEntity()
            {
                AttachmentDirId = string.Empty,
                AttachmentName = string.Empty,
                AttachmentPath = string.Empty,
                LocalPath = string.Empty
            };
        }
        private string _AttachmentPath;
        /// <summary>
        /// 上传路径
        /// </summary>
        [DataMember]
        public string AttachmentPath
        {
            get { return _AttachmentPath; }
            set
            {
                if (_AttachmentPath == value)
                    return;
                _AttachmentPath = value;
                RaisePropertyChanged("AttachmentPath");
            }
        }
        private string _LocalPath;
        /// <summary>
        /// 本地文件路径
        /// </summary>
        [DataMember]
        public string LocalPath
        {
            get { return _LocalPath; }
            set
            {
                if (_LocalPath == value)
                    return;
                _LocalPath = value;
                RaisePropertyChanged("LocalPath");
            }
        }
        private string _Message;
        /// <summary>
        /// 结果
        /// </summary>
        [DataMember]
        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message == value)
                    return;
                _Message = value;
                RaisePropertyChanged("Message");
            }
        }
    }
}