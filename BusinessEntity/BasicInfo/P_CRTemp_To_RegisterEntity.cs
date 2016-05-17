using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity.BasicInfo
{
    public partial class P_CRTemp_To_RegisterEntity
    {
        public static P_CRTemp_To_RegisterEntity CreateEntity()
        {
            return new P_CRTemp_To_RegisterEntity();
        }
    }
    public class FirstP_CRTemp_To_RegisterEntity : P_CRTemp_To_RegisterEntity
    {
        public new static FirstP_CRTemp_To_RegisterEntity CreateEntity()
        {
            return new FirstP_CRTemp_To_RegisterEntity();
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
                if (_CRTempName == value)
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
                if (_CRTempPath == value)
                    return;
                _CRTempPath = value;
                RaisePropertyChanged("CRTempPath");
            }
        }
    }
}
