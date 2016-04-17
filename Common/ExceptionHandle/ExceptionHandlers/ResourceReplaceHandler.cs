using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Specialized;
using System.Globalization;

namespace FengSharp.OneCardAccess.Common
{
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class ResourceReplaceHandler : IExceptionHandler
    {
        private readonly IStringResolver exceptionMessageResolver;
        private readonly Type replaceExceptionType;
        public ResourceReplaceHandler(NameValueCollection namevalue)
        : this(
              Type.GetType(namevalue["exceptionMessageType"]).
              GetProperty(namevalue["exceptionMessageKey"]).GetValue(null, null).ToString()
              ,
              Type.GetType(namevalue["replaceExceptionType"])
              )
        {
        }
        public ResourceReplaceHandler(string exceptionMessage, Type replaceExceptionType)
            : this(new ConstantStringResolver(exceptionMessage), replaceExceptionType)
        { }
        public ResourceReplaceHandler(IStringResolver exceptionMessageResolver, Type replaceExceptionType)
        {
            if (replaceExceptionType == null) throw new ArgumentNullException("replaceExceptionType");
            if (!typeof(Exception).IsAssignableFrom(replaceExceptionType))
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ExceptionTypeNotException", replaceExceptionType.Name), "replaceExceptionType");
            }

            this.exceptionMessageResolver = exceptionMessageResolver;
            this.replaceExceptionType = replaceExceptionType;
        }

        /// <summary>
        /// The type of exception to replace.
        /// </summary>
        public Type ReplaceExceptionType
        {
            get { return replaceExceptionType; }
        }

        /// <summary>
        /// Gets the message for the new exception.
        /// </summary>
        public string ExceptionMessage
        {
            get { return exceptionMessageResolver.GetString(); }
        }

        /// <summary>
        /// Replaces the exception with the configured type for the specified policy.
        /// </summary>
        /// <param name="exception">The original exception.</param>        
        /// <param name="handlingInstanceId">The unique identifier attached to the handling chain for this handling instance.</param>
        /// <returns>Modified exception to pass to the next exceptionHandlerData in the chain.</returns>
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            return ReplaceException(ExceptionUtility.FormatExceptionMessage(ExceptionMessage, handlingInstanceId));
        }
        private Exception ReplaceException(string replaceExceptionMessage)
        {

            object[] extraParameters = new object[] { replaceExceptionMessage };
            return (Exception)Activator.CreateInstance(replaceExceptionType, extraParameters);
        }
    }
}
