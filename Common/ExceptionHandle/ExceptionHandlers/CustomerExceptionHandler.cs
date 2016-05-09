using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.PolicyInjection;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Specialized;
using System.Reflection;

namespace FengSharp.OneCardAccess.Common.ExceptionHandle.ExceptionHandlers
{

    public class CustomerExceptionHandler : ICallHandler
    {
        public CustomerExceptionHandler(string exceptionPolicyName) : this(exceptionPolicyName, 0)
        {
        }
        public CustomerExceptionHandler(string exceptionPolicyName, int order)
        {
            this.Order = order;
            this.exceptionPolicy = EnterpriseLibraryContainer.Current.GetInstance<ExceptionPolicyImpl>(exceptionPolicyName);
        }
        private ExceptionPolicyImpl exceptionPolicy;
        public int Order { get; set; }
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            if (input == null) throw new ArgumentNullException("input");
            if (getNext == null) throw new ArgumentNullException("getNext");

            IMethodReturn result = getNext()(input, getNext);
            if (result.Exception != null)
            {
                try
                {
                    bool rethrow = exceptionPolicy.HandleException(result.Exception);
                    if (!rethrow)
                    {
                        // Exception is being swallowed
                        result.ReturnValue = null;
                        result.Exception = null;

                        if (input.MethodBase.MemberType == MemberTypes.Method)
                        {
                            MethodInfo method = (MethodInfo)input.MethodBase;
                            if (method.ReturnType != typeof(void))
                            {
                                result.Exception =
                                    new InvalidOperationException(
                                        Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Properties.Resources.CantSwallowNonVoidReturnMessage);
                            }
                        }
                    }
                    // Otherwise the original exception will be returned to the previous handler
                }
                catch (Exception ex)
                {
                    // New exception was returned
                    result.Exception = ex;
                }
            }
            return result;
        }

    }
}
