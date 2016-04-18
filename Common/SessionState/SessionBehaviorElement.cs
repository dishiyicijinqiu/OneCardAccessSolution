using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace FengSharp.OneCardAccess.Common
{
    public class SessionBehaviorElement : BehaviorExtensionElement
    {
        ///// <summary>
        ///// 是否双向绑定
        ///// </summary>
        //[ConfigurationProperty("isBidirectional", DefaultValue = false)]
        //public bool IsBidirectional
        //{
        //    get
        //    {
        //        return (bool)this["isBidirectional"];
        //    }
        //    set
        //    {
        //        this["isBidirectional"] = value;
        //    }
        //}
        public override Type BehaviorType
        {
            get
            {
                return typeof(SessionBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new SessionBehavior();
        }
    }
}
