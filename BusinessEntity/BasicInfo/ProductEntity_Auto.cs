using System;
using System.Runtime.Serialization;
using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 产品表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class ProductEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProductEntity()
        {
            ProductId = string.Empty;
            ProductNo = string.Empty;
            ProductName = string.Empty;
            ProductName1 = string.Empty;
            Spec = string.Empty;
            Spec1 = string.Empty;
            ProductImage = string.Empty;
            Unit = string.Empty;
            Material = string.Empty;
            RegisterId = string.Empty;
            Remark1 = string.Empty;
            Remark2 = string.Empty;
            Remark3 = string.Empty;
            Remark4 = string.Empty;
            TreeNo = string.Empty;
            TreeParentNo = string.Empty;
            TreePath = string.Empty;
            CreateId = string.Empty;
            LastModifyId = string.Empty;
        }
        private string _ProductId;
        /// <summary>
        /// 
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        [DataMember]
        public string ProductId
        {
            get { return _ProductId; }
            set
            {
                if (_ProductId == value)
                    return;
                _ProductId = value;
                RaisePropertyChanged("ProductId");
            }
        }
        private string _ProductNo;
        /// <summary>
        /// 产品编号
        /// </summary>
        [DataMember]
        public string ProductNo
        {
            get { return _ProductNo; }
            set
            {
                if (_ProductNo == value)
                    return;
                _ProductNo = value;
                RaisePropertyChanged("ProductNo");
            }
        }
        private string _ProductName;
        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        public string ProductName
        {
            get { return _ProductName; }
            set
            {
                if (_ProductName == value)
                    return;
                _ProductName = value;
                RaisePropertyChanged("ProductName");
            }
        }
        private string _ProductName1;
        /// <summary>
        /// 商品名称(英文)
        /// </summary>
        [DataMember]
        public string ProductName1
        {
            get { return _ProductName1; }
            set
            {
                if (_ProductName1 == value)
                    return;
                _ProductName1 = value;
                RaisePropertyChanged("ProductName1");
            }
        }
        private string _Spec;
        /// <summary>
        /// 规格型号
        /// </summary>
        [DataMember]
        public string Spec
        {
            get { return _Spec; }
            set
            {
                if (_Spec == value)
                    return;
                _Spec = value;
                RaisePropertyChanged("Spec");
            }
        }
        private string _Spec1;
        /// <summary>
        /// 规格型号(英文)
        /// </summary>
        [DataMember]
        public string Spec1
        {
            get { return _Spec1; }
            set
            {
                if (_Spec1 == value)
                    return;
                _Spec1 = value;
                RaisePropertyChanged("Spec1");
            }
        }
        private string _ProductImage;
        /// <summary>
        /// 产品大图
        /// </summary>
        [DataMember]
        public string ProductImage
        {
            get { return _ProductImage; }
            set
            {
                if (_ProductImage == value)
                    return;
                _ProductImage = value;
                RaisePropertyChanged("ProductImage");
            }
        }
        private string _Unit;
        /// <summary>
        /// 计量单位
        /// </summary>
        [DataMember]
        public string Unit
        {
            get { return _Unit; }
            set
            {
                if (_Unit == value)
                    return;
                _Unit = value;
                RaisePropertyChanged("Unit");
            }
        }
        private string _Material;
        /// <summary>
        /// 材料标识
        /// </summary>
        [DataMember]
        public string Material
        {
            get { return _Material; }
            set
            {
                if (_Material == value)
                    return;
                _Material = value;
                RaisePropertyChanged("Material");
            }
        }
        private int _PackQty;
        /// <summary>
        /// 包装数量
        /// </summary>
        [DataMember]
        public int PackQty
        {
            get { return _PackQty; }
            set
            {
                if (_PackQty == value)
                    return;
                _PackQty = value;
                RaisePropertyChanged("PackQty");
            }
        }
        private short _ProductType;
        /// <summary>
        /// 产品类型（成品，零部件）
        /// </summary>
        [DataMember]
        public short ProductType
        {
            get { return _ProductType; }
            set
            {
                if (_ProductType == value)
                    return;
                _ProductType = value;
                RaisePropertyChanged("ProductType");
            }
        }
        private short _CertType;
        /// <summary>
        /// 证件类型（a证，b证）
        /// </summary>
        [DataMember]
        public short CertType
        {
            get { return _CertType; }
            set
            {
                if (_CertType == value)
                    return;
                _CertType = value;
                RaisePropertyChanged("CertType");
            }
        }
        private string _RegisterId;
        /// <summary>
        /// 产品注册证Id
        /// </summary>
        [DataMember]
        public string RegisterId
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
        private string _Remark1;
        /// <summary>
        /// 备注1
        /// </summary>
        [DataMember]
        public string Remark1
        {
            get { return _Remark1; }
            set
            {
                if (_Remark1 == value)
                    return;
                _Remark1 = value;
                RaisePropertyChanged("Remark1");
            }
        }
        private string _Remark2;
        /// <summary>
        /// 备注2
        /// </summary>
        [DataMember]
        public string Remark2
        {
            get { return _Remark2; }
            set
            {
                if (_Remark2 == value)
                    return;
                _Remark2 = value;
                RaisePropertyChanged("Remark2");
            }
        }
        private string _Remark3;
        /// <summary>
        /// 备注3
        /// </summary>
        [DataMember]
        public string Remark3
        {
            get { return _Remark3; }
            set
            {
                if (_Remark3 == value)
                    return;
                _Remark3 = value;
                RaisePropertyChanged("Remark3");
            }
        }
        private string _Remark4;
        /// <summary>
        /// 备注4
        /// </summary>
        [DataMember]
        public string Remark4
        {
            get { return _Remark4; }
            set
            {
                if (_Remark4 == value)
                    return;
                _Remark4 = value;
                RaisePropertyChanged("Remark4");
            }
        }
        private string _TreeNo;
        /// <summary>
        /// 树编号
        /// </summary>
        [DataMember]
        public string TreeNo
        {
            get { return _TreeNo; }
            set
            {
                if (_TreeNo == value)
                    return;
                _TreeNo = value;
                RaisePropertyChanged("TreeNo");
            }
        }
        private string _TreeParentNo;
        /// <summary>
        /// 父亲树编号
        /// </summary>
        [DataMember]
        public string TreeParentNo
        {
            get { return _TreeParentNo; }
            set
            {
                if (_TreeParentNo == value)
                    return;
                _TreeParentNo = value;
                RaisePropertyChanged("TreeParentNo");
            }
        }
        private string _TreePath;
        /// <summary>
        /// 树路径
        /// </summary>
        [DataMember]
        public string TreePath
        {
            get { return _TreePath; }
            set
            {
                if (_TreePath == value)
                    return;
                _TreePath = value;
                RaisePropertyChanged("TreePath");
            }
        }
        private int _TreeSon;
        /// <summary>
        /// 儿子数量
        /// </summary>
        [DataMember]
        public int TreeSon
        {
            get { return _TreeSon; }
            set
            {
                if (_TreeSon == value)
                    return;
                _TreeSon = value;
                RaisePropertyChanged("TreeSon");
            }
        }
        private int _SkuSon;
        /// <summary>
        /// Sku儿子数量
        /// </summary>
        [DataMember]
        public int SkuSon
        {
            get { return _SkuSon; }
            set
            {
                if (_SkuSon == value)
                    return;
                _SkuSon = value;
                RaisePropertyChanged("SkuSon");
            }
        }
        private string _CreateId;
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DataMember]
        public string CreateId
        {
            get { return _CreateId; }
            set
            {
                if (_CreateId == value)
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
                if (_CreateDate == value)
                    return;
                _CreateDate = value;
                RaisePropertyChanged("CreateDate");
            }
        }
        private string _LastModifyId;
        /// <summary>
        /// 最后更改人Id
        /// </summary>
        [DataMember]
        public string LastModifyId
        {
            get { return _LastModifyId; }
            set
            {
                if (_LastModifyId == value)
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
                if (_LastModifyDate == value)
                    return;
                _LastModifyDate = value;
                RaisePropertyChanged("LastModifyDate");
            }
        }
    }
}
