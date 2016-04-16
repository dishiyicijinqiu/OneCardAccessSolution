using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace FengSharp.OneCardAccess.Common
{
    public class PolicyInjectionExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            base.Context.Strategies.Add(new PolicyInjectionStrategy(true), UnityBuildStage.Initialization);
        }
    }
}
