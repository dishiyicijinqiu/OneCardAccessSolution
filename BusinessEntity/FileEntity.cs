using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace FengSharp.OneCardAccess.BusinessEntity
{
    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public class FileEntity
    {
        public string FileName { get; set; }
    }

    [DataContract(Namespace = "http://www.fengsharp.com/onecardaccess/")]
    public enum FileType
    {
        /// <summary>
        /// 产品检验报告模板
        /// </summary>
        [EnumMember]
        P_CRTemp = 0,
    }
}
