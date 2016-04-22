using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using DevExpress.Mvvm;
namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
	/// <summary>
	/// 产品检验报告模板
	/// </summary>
	[DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
	public partial class P_CRTempEntity: BindableBase
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public P_CRTempEntity()
		{
			CRTempName=string.Empty;
			CRTempPath=string.Empty;
			MaterIden=string.Empty;
			CateNo=string.Empty;
			Remark=string.Empty;
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
				if(_Id == value)
					return;
                _Id = value;
                RaisePropertyChanged("Id");
            }
        }
		private string _CRTempName;
		/// <summary>
		/// 模板名称
		/// </summary>
		[DataMember]
		public string CRTempName
		{
            get { return _CRTempName; }
            set
            {
				if(_CRTempName == value)
					return;
                _CRTempName = value;
                RaisePropertyChanged("CRTempName");
            }
        }
		private string _CRTempPath;
		/// <summary>
		/// 路径
		/// </summary>
		[DataMember]
		public string CRTempPath
		{
            get { return _CRTempPath; }
            set
            {
				if(_CRTempPath == value)
					return;
                _CRTempPath = value;
                RaisePropertyChanged("CRTempPath");
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
				if(_MaterIden == value)
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
				if(_IsLock == value)
					return;
                _IsLock = value;
                RaisePropertyChanged("IsLock");
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
				if(_CateNo == value)
					return;
                _CateNo = value;
                RaisePropertyChanged("CateNo");
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
				if(_Remark == value)
					return;
                _Remark = value;
                RaisePropertyChanged("Remark");
            }
        }
		private int _CreateId;
		/// <summary>
		/// 创建人Id
		/// </summary>
		[DataMember]
		public int CreateId
		{
            get { return _CreateId; }
            set
            {
				if(_CreateId == value)
					return;
                _CreateId = value;
                RaisePropertyChanged("CreateId");
            }
        }
		private DateTime _CreateDate;
		/// <summary>
		/// 创建日期
		/// </summary>
		[DataMember]
		public DateTime CreateDate
		{
            get { return _CreateDate; }
            set
            {
				if(_CreateDate == value)
					return;
                _CreateDate = value;
                RaisePropertyChanged("CreateDate");
            }
        }
		private int _LastModifyId;
		/// <summary>
		/// 最后更改人Id
		/// </summary>
		[DataMember]
		public int LastModifyId
		{
            get { return _LastModifyId; }
            set
            {
				if(_LastModifyId == value)
					return;
                _LastModifyId = value;
                RaisePropertyChanged("LastModifyId");
            }
        }
		private DateTime _LastModifyDate;
		/// <summary>
		/// 最后更改日期
		/// </summary>
		[DataMember]
		public DateTime LastModifyDate
		{
            get { return _LastModifyDate; }
            set
            {
				if(_LastModifyDate == value)
					return;
                _LastModifyDate = value;
                RaisePropertyChanged("LastModifyDate");
            }
        }
	}
}