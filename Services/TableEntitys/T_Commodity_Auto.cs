﻿using System;
using FengSharp.OneCardAccess.Common;
namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 商品表
    /// </summary>
    public class T_Commodity
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        public string CommodityId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string CommodityNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CommodityName { get; set; }
        /// <summary>
        /// 产品大图
        /// </summary>
        public string CommodityImage { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 材料标识
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 包装数量
        /// </summary>
        public int PackQty { get; set; }
        /// <summary>
        /// 产品类型（成品，零部件）
        /// </summary>
        public short ProductType { get; set; }
        /// <summary>
        /// 货位号
        /// </summary>
        public string GoodCode { get; set; }
        /// <summary>
        /// 证件类型（a证，b证）
        /// </summary>
        public short CertType { get; set; }
        /// <summary>
        /// 产品注册证Id
        /// </summary>
        public string RegisterId { get; set; }
        /// <summary>
        /// 图纸Id
        /// </summary>
        public string DrawingId { get; set; }
        /// <summary>
        /// 数量模式（0，通用模式，1严格管理序列号，2严格管理批号）
        /// </summary>
        public short QtyMode { get; set; }
        /// <summary>
        /// 备注1
        /// </summary>
        public string Remark1 { get; set; }
        /// <summary>
        /// 备注2
        /// </summary>
        public string Remark2 { get; set; }
        /// <summary>
        /// 备注3
        /// </summary>
        public string Remark3 { get; set; }
        /// <summary>
        /// 备注4
        /// </summary>
        public string Remark4 { get; set; }
        /// <summary>
        /// 备注5
        /// </summary>
        public string Remark5 { get; set; }
        /// <summary>
        /// 备注6
        /// </summary>
        public string Remark6 { get; set; }
        /// <summary>
        /// 备注7
        /// </summary>
        public string Remark7 { get; set; }
        /// <summary>
        /// 备注8
        /// </summary>
        public string Remark8 { get; set; }
        /// <summary>
        /// 树编号
        /// </summary>
        public string TreeNo { get; set; }
        /// <summary>
        /// 父亲树编号
        /// </summary>
        public string TreeParentNo { get; set; }
        /// <summary>
        /// 树路径
        /// </summary>
        public string TreePath { get; set; }
        /// <summary>
        /// 儿子数量
        /// </summary>
        public int TreeSon { get; set; }
        /// <summary>
        /// Sku儿子数量
        /// </summary>
        public int SkuSon { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更改人Id
        /// </summary>
        public string LastModifyId { get; set; }
        /// <summary>
        /// 最后更改日期
        /// </summary>
        public DateTime LastModifyDate { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public bool Deleted { get; set; }
    }
}
