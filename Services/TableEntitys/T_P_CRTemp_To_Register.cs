﻿using FengSharp.OneCardAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FengSharp.OneCardAccess.TEntity
{
    /// <summary>
    /// 注册证产品检验模板对应表
    /// </summary>
    public class T_P_CRTemp_To_Register
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataBaseKey(DataBaseKeyType.Guid)]
        public string Id { get; set; }
        /// <summary>
        /// 注册证Id
        /// </summary>
        public string RegisterId { get; set; }
        /// <summary>
        /// 产品检验报告模板Id
        /// </summary>
        public string P_CRTempId { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int SortNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
