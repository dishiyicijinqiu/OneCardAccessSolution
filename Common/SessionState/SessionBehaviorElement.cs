using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace FengSharp.OneCardAccess.Common
{
    public class SessionBehaviorElement : BehaviorExtensionElement
    {
        /// <summary>
        /// 是否检测会话的有效性,此项对客户端配置无效
        /// </summary>
        [ConfigurationProperty("sessionCheck", DefaultValue = true)]
        public bool SessionCheck
        {
            get
            {
                return (bool)this["sessionCheck"];
            }
            set
            {
                this["sessionCheck"] = value;
            }
        }
        public override Type BehaviorType
        {
            get
            {
                return typeof(SessionBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new SessionBehavior(this.SessionCheck);
        }
    }
}
