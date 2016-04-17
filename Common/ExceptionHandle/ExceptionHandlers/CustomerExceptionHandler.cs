using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Specialized;

namespace FengSharp.OneCardAccess.Common.ExceptionHandle.ExceptionHandlers
{

    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class CustomerExceptionHandler : IExceptionHandler
    {
        public CustomerExceptionHandler(NameValueCollection namevalue)
        {

        }

        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            if (exception is System.Data.SqlClient.SqlException)
            {
                var sqlerror = exception as System.Data.SqlClient.SqlException;
                if (sqlerror.Class != 11 || sqlerror.State != 1)
                {
                    object[] args = new object[] { ExceptionUtility.FormatExceptionMessage("服务调用出错", handlingInstanceId) };
                    return (Exception)Activator.CreateInstance(typeof(Exception), args);
                }
            }
            return exception;
        }
    }
}
