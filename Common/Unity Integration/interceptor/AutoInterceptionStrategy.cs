using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ObjectBuilder2;

namespace FengSharp.OneCardAccess.Common
{
    public abstract class AutoInterceptionStrategy: BuilderStrategy
    {
        public bool CanIntercept(IBuilderContext context)
        {
            return !context.BuildKey.Type.FullName.StartsWith("Microsoft.Practices");
        }
    }
}
