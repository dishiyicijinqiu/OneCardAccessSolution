using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace FengSharp.OneCardAccess.Common
{
    public class ExceptionHandlingBehaviorElement: BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ExceptionHandlingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ExceptionHandlingBehavior(this.ExceptionPolicyName);
        }

        [ConfigurationProperty("exceptionPolicyName", IsRequired = false, DefaultValue = "")]
        public string ExceptionPolicyName
        {
            get
            {
                return (string)this["exceptionPolicyName"];
            }
        }
    }
}
