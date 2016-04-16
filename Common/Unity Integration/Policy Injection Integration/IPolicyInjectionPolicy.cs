using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace FengSharp.OneCardAccess.Common
{
    public interface IPolicyInjectionPolicy : IBuilderPolicy
    {
        bool ApplyPolicies { get; }
        void SetPolicyConfigurationSource(IConfigurationSource configSource);
        object ApplyProxy(object instanceToProxy, Type typeToProxy);
    }
    public interface IConfigurationObjectPolicy : IBuilderPolicy
    {
        IConfigurationSource ConfigurationSource { get; }
    }
}
