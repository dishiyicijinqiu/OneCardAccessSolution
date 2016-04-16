using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class CustomerExceptionHandler : IExceptionHandler
    {
        public CustomerExceptionHandler()
        {

        }
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


    public class ExceptionWrapHandler : IErrorHandler
    {
        public string ExceptionPolicyName
        { get; private set; }

        public ExceptionWrapHandler(string exceptionPolicyName)
        {
            if (string.IsNullOrEmpty(exceptionPolicyName))
            {
                throw new ArgumentNullException("exceptionPolicyName");
            }
            this.ExceptionPolicyName = exceptionPolicyName;
        }

        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            return false;
        }

        private void ProcessException(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
            {
                return;
            }
            if (error.GetType().GetConstructor(new Type[] { typeof(string), typeof(Exception) }) == null)
            {
                error = new ServiceInvocationException(error.Message);
            }
            ServiceExceptionDetail exceptionDetail = new ServiceExceptionDetail(error);
            exceptionDetail.ExceptionAssemblyQualifiedName = error.GetType().AssemblyQualifiedName;
            FaultException<ServiceExceptionDetail> faultException = new FaultException<ServiceExceptionDetail>(exceptionDetail, new FaultReason(error.Message));
            fault = Message.CreateMessage(version, faultException.CreateMessageFault(), GetFaultAction());
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            this.EnsureMessage(ref fault, version);
            try
            {
                ExceptionPolicy.HandleException(error, this.ExceptionPolicyName);
                this.ProcessException(error, version, ref fault);
            }
            catch (Exception ex)
            {
                this.ProcessException(ex, version, ref fault);
            }
        }

        #endregion

        private void EnsureMessage(ref Message message, MessageVersion defaultVersion)
        {
            if (message == null)
            {
                message = Message.CreateMessage(defaultVersion ?? MessageVersion.Default, "");
            }
        }

        private string GetFaultAction()
        {
            string operationAction = OperationContext.Current.RequestContext.RequestMessage.Headers.Action;

            foreach (DispatchOperation operation in OperationContext.Current.EndpointDispatcher.DispatchRuntime.Operations)
            {
                if (operation.Action.Equals(operationAction, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (FaultContractInfo fault in operation.FaultContractInfos)
                    {
                        if (fault.Detail == typeof(ServiceExceptionDetail))
                        {
                            return fault.Action;
                        }
                    }
                }
            }
            return operationAction;
        }
    }
}