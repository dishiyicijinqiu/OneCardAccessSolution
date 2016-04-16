using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using System;

namespace FengSharp.OneCardAccess.Common
{
    public class PolicyInjectionPolicy : IPolicyInjectionPolicy
    {
        private readonly bool applyPolicies;
        private volatile IConfigurationSource configSource;
        private PolicyInjector injector;
        private object policiesLock = new object();
        public PolicyInjectionPolicy(bool applyPolicies)
        {
            this.applyPolicies = applyPolicies;
        }
        public bool ApplyPolicies
        {
            get { return applyPolicies; }
        }
        #region IPolicyInjectionPolicy Members
        public void SetPolicyConfigurationSource(IConfigurationSource configSource)
        {
            if (this.configSource == null || this.configSource != configSource)
            {
                lock (policiesLock)
                {
                    if (this.configSource == null || this.configSource != configSource)
                    {
                        this.configSource = configSource;
                        injector = new PolicyInjector(this.configSource);
                    }
                }
            }
        }
        public object ApplyProxy(object instanceToProxy, Type typeToProxy)
        {
            return injector.Wrap(typeToProxy, instanceToProxy);
        }

        #endregion
    }
}