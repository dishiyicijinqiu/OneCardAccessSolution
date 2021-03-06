﻿using FengSharp.OneCardAccess.Common;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    /// <summary>
    /// 注册证产品检验模板对应表
    /// </summary>
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public partial class P_CRTemp_To_RegisterEntity : NotificationObject
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public P_CRTemp_To_RegisterEntity()
        {
            Id = string.Empty;
            RegisterId = string.Empty;
            P_CRTempId = string.Empty;
            Remark = string.Empty;
        }
        private string _Id;
        /// <summary>
        /// Id
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
        private string _RegisterId;
        /// <summary>
        /// 注册证Id
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
        private string _P_CRTempId;
        /// <summary>
        /// 产品检验报告模板Id
        /// </summary>
        [DataMember]
        public string P_CRTempId
        {
            get { return _P_CRTempId; }
            set
            {
                if (_P_CRTempId == value)
                    return;
                _P_CRTempId = value;
                RaisePropertyChanged("P_CRTempId");
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
    }
}
