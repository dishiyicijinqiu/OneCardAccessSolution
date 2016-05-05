using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System;
namespace FengSharp.OneCardAccess.Common
{
    public class ExceptionHandlingMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (!reply.IsFault)
            {
                return;
            }
            if (reply.Headers.Action == ServiceExceptionDetail.FaultAction)
            {
                MessageFault fault = MessageFault.CreateFault(reply, int.MaxValue);
                if(fault.Code.SubCode.Name == ServiceExceptionDetail.FaultSubCodeName &&
                    fault.Code.SubCode.Namespace == ServiceExceptionDetail.FaultSubCodeNamespace)
                {
                    FaultException<ServiceExceptionDetail> exception = (FaultException<ServiceExceptionDetail>)FaultException.CreateFault(fault, typeof(ServiceExceptionDetail));
                    throw GetException(exception.Detail);
                }
            }
        }

        private Exception GetException(ServiceExceptionDetail exceptionDetail)
        {
            if (null == exceptionDetail.InnerException)
            {
                return (Exception)Activator.CreateInstance(Type.GetType(exceptionDetail.AssemblyQualifiedName), exceptionDetail.Message);
            }

            Exception innerException = GetException(exceptionDetail.InnerException);
            return (Exception)Activator.CreateInstance(Type.GetType(exceptionDetail.AssemblyQualifiedName), exceptionDetail.Message, innerException);
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }
    }
}