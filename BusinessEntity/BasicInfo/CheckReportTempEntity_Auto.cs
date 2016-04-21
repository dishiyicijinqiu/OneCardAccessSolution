using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using DevExpress.Mvvm;
namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 检验报告模板
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class CheckReportTempEntity : BindableBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckReportTempEntity()
        {
            TemplateName = string.Empty;
            TemplatePath = string.Empty;
            MaterIden = string.Empty;
            Remark = string.Empty;
            CateNo = string.Empty;
        }
        private int _Id;
        /// <summary>
        /// Id
        /// </summary>
        [DataBaseKey]
        [DataMember]
        public int Id
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
        private string _TemplateName;
        /// <summary>
        /// 模板名称
        /// </summary>
        [DataMember]
        public string TemplateName
        {
            get { return _TemplateName; }
            set
            {
                if (_TemplateName == value)
                    return;
                _TemplateName = value;
                RaisePropertyChanged("TemplateName");
            }
        }
        private string _TemplatePath;
        /// <summary>
        /// 路径
        /// </summary>
        [DataMember]
        public string TemplatePath
        {
            get { return _TemplatePath; }
            set
            {
                if (_TemplatePath == value)
                    return;
                _TemplatePath = value;
                RaisePropertyChanged("TemplatePath");
            }
        }
        private string _MaterIden;
        /// <summary>
        /// 材料标识
        /// </summary>
        [DataMember]
        public string MaterIden
        {
            get { return _MaterIden; }
            set
            {
                if (_MaterIden == value)
                    return;
                _MaterIden = value;
                RaisePropertyChanged("MaterIden");
            }
        }
        private bool _IsLock;
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DataMember]
        public bool IsLock
        {
            get { return _IsLock; }
            set
            {
                if (_IsLock == value)
                    return;
                _IsLock = value;
                RaisePropertyChanged("IsLock");
            }
        }
        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark
        {
            get { return _Remark; }
            set
            {
                if (_Remark == value)
                    return;
                _Remark = value;
                RaisePropertyChanged("Remark");
            }
        }
        private string _CateNo;
        /// <summary>
        /// 类别号
        /// </summary>
        [DataMember]
        public string CateNo
        {
            get { return _CateNo; }
            set
            {
                if (_CateNo == value)
                    return;
                _CateNo = value;
                RaisePropertyChanged("CateNo");
            }
        }
    }
}
