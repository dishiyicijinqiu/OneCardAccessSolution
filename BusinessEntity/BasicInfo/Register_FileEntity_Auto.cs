using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 注册证附件表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class Register_FileEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public Register_FileEntity()
        {
            Id = string.Empty;
            FileName = string.Empty;
            FilePath = string.Empty;
        }
        private string _Id;
        /// <summary>
        /// 注册证附件Id
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        [DataMember]
        public string Id
        {
            get { return _Id; }
            set
            {
                if (_Id == value)
                    return;
                _Id = value;
                RaisePropertyChanged("Id");
            }
        }
        private int _RegisterId;
        /// <summary>
        /// 注册证Id
        /// </summary>
        [DataMember]
        public int RegisterId
        {
            get { return _RegisterId; }
            set
            {
                if (_RegisterId == value)
                    return;
                _RegisterId = value;
                RaisePropertyChanged("RegisterId");
            }
        }
        private string _FileName;
        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (_FileName == value)
                    return;
                _FileName = value;
                RaisePropertyChanged("FileName");
            }
        }
        private string _FilePath;
        /// <summary>
        /// 保存路径
        /// </summary>
        [DataMember]
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                if (_FilePath == value)
                    return;
                _FilePath = value;
                RaisePropertyChanged("FilePath");
            }
        }
        private int _SortNo;
        /// <summary>
        /// 序号
        /// </summary>
        [DataMember]
        public int SortNo
        {
            get { return _SortNo; }
            set
            {
                if (_SortNo == value)
                    return;
                _SortNo = value;
                RaisePropertyChanged("SortNo");
            }
        }
    }
}
