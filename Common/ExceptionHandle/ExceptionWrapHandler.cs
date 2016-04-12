using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
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
            //if (error is FaultException)
            //{
            //    return;
            //}
            //if (error.GetType().GetConstructor(new Type[] { typeof(string), typeof(Exception) }) == null)
            //{
            //    error = new ServiceInvocationException(error.Message);
            //}
            //ServiceExceptionDetail exceptionDetail = new ServiceExceptionDetail(error);
            //exceptionDetail.ExceptionAssemblyQualifiedName = error.GetType().AssemblyQualifiedName;
            //FaultException<ServiceExceptionDetail> faultException = new FaultException<ServiceExceptionDetail>(exceptionDetail, new FaultReason(error.Message));
            //fault = Message.CreateMessage(version, faultException.CreateMessageFault(), GetFaultAction());
            if (error is FaultException)
            {
                return;
            }
            if (error is System.Data.SqlClient.SqlException)
            {
                var sqlerror = error as System.Data.SqlClient.SqlException;
                if (sqlerror.Class != 11 || sqlerror.State != 1)
                {
                    error = new BusinessException("数据库操作失败");
                    Logger.Write(
                        string.Format("Class:{0},State:{1},LineNumber:{2},Procedure:{3},Message:{4}", sqlerror.Class, sqlerror.State, sqlerror.LineNumber, sqlerror.Procedure, sqlerror.Message),
                        "WCFMethodLog",
                         3,
                         -1,
                         System.Diagnostics.TraceEventType.Error, "数据库操作失败"
                        );
                }
            }
            else if (error is BusinessException)
            {

            }
            else if (error is LoginTimeOutException)
            {

            }
            else
            {
                Logger.Write(error.Message, "MethodException", 3, -1, System.Diagnostics.TraceEventType.Error, "数据库操作失败");
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
    [ConfigurationElementType(typeof(CustomHandlerData))]
    public class CustomerExceptionHandler : IExceptionHandler
    {
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
    public class CustomHandlerData : ExceptionHandlerData
    {
        public CustomHandlerData() : base(typeof(CustomerExceptionHandler))
        {
        }
        public CustomHandlerData(string name) :
            base(name, typeof(CustomerExceptionHandler))
        {
        }
        public override IEnumerable<TypeRegistration> GetRegistrations(string namePrefix)
        {
            yield return
                new TypeRegistration<IExceptionHandler>(
                    () => new CustomerExceptionHandler())
                {
                    Name = BuildName(namePrefix),
                    Lifetime = TypeRegistrationLifetime.Transient
                };
        }
    }
}