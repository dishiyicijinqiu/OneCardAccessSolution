using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System;

namespace FengSharp.OneCardAccess.Common
{
    public class PolicyInjectionStrategy : IBuilderStrategy
    {
        private readonly bool applyPolicies;

        public PolicyInjectionStrategy(bool applyPolicies)
        {
            this.applyPolicies = applyPolicies;
        }

        public void PreBuildUp(IBuilderContext context)
        {
            if (context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey) == null)
            {
                context.Policies.Set<IPolicyInjectionPolicy>(new PolicyInjectionPolicy(this.applyPolicies), context.BuildKey);
            }
        }

        public void PostBuildUp(IBuilderContext context)
        {
            IPolicyInjectionPolicy policy = context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey);
            if (policy != null)
            {
                policy.SetPolicyConfigurationSource(GetConfigurationSource(context));
                context.Existing = policy.ApplyProxy(context.Existing, context.OriginalBuildKey.Type);
            }
        }

        public void PreTearDown(IBuilderContext context)
        {
        }

        public void PostTearDown(IBuilderContext context)
        {
        }
        protected static IConfigurationSource GetConfigurationSource(IBuilderContext context)
        {
            IConfigurationObjectPolicy policy
                = context.Policies.Get<IConfigurationObjectPolicy>(typeof(IConfigurationSource));

            if (policy == null)
                return new SystemConfigurationSource();
            else
                return policy.ConfigurationSource;
        }
        //public override void PreBuildUp(IBuilderContext context)
        //{
        //    base.PreBuildUp(context);
        //    if (context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey) == null)
        //    {
        //        context.Policies.Set<IPolicyInjectionPolicy>(new PolicyInjectionPolicy(this.Container), context.BuildKey);
        //    }
        //}

        //public override void PostBuildUp(IBuilderContext context)
        //{
        //    base.PostBuildUp(context);
        //    IPolicyInjectionPolicy policy = context.Policies.Get<IPolicyInjectionPolicy>(context.BuildKey);
        //    if (policy != null)
        //    {
        //        context.Existing = policy.ApplyProxy(context.Existing, context.OriginalBuildKey.Type);
        //    }
        //}
    }
}