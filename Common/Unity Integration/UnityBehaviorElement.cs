using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace FengSharp.OneCardAccess.Common
{
    public class UnityBehaviorElement: BehaviorExtensionElement
    {
        [ConfigurationProperty("containerName", IsRequired = false, DefaultValue = "")]
        public string ContainerName
        {
            get
            {
                return this["containerName"] as string;
            }
            set
            {
                this["containerName"] = value;
            }
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(UnityBehaviorAttribute);
            }
        }

        protected override object CreateBehavior()
        {
            return new UnityBehaviorAttribute(this.ContainerName);
        }
    }
}
