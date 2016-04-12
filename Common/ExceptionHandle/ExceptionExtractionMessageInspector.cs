using System;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace FengSharp.OneCardAccess.Common
{
    public class ExceptionExtractionMessageInspector : IClientMessageInspector
    {
        public IClientFaultFormatter FaultFormatter
        { get; private set; }

        public ExceptionExtractionMessageInspector(IClientFaultFormatter faultFormatter)
        {
            if (faultFormatter == null)
            {
                throw new ArgumentNullException("faultFormatter");
            }

            this.FaultFormatter = faultFormatter;
        }

        private static Exception ExtractException(ServiceExceptionDetail detail)
        {
            Exception innerException = null;
            if (detail.InnerException != null)
            {
                innerException = ExtractException(detail.InnerException);
            }
            return CreateExceptionInstance(Type.GetType(detail.ExceptionAssemblyQualifiedName), detail, innerException);
        }

        private static Exception CreateExceptionInstance(Type type, ExceptionDetail detail, Exception innerException)
        {
            Exception exception = null;
            if (type != null)
            {
                try
                {
                    exception = Activator.CreateInstance(type, true) as Exception;
                    if (exception == null)
                    {
                        exception = new Exception();
                    }
                    SetExceptionField(exception, "_message", detail.Message);
                    SetExceptionField(exception, "_innerException", innerException);
                    SetExceptionField(exception, "_stackTraceString", detail.StackTrace);
                    SetExceptionField(exception, "_helpURL", detail.HelpLink);
                }
                catch (Exception ex)
                {
                    Exception inner = new Exception();
                    SetExceptionField(inner, "_message", detail.Message);
                    SetExceptionField(inner, "_innerException", innerException);
                    SetExceptionField(inner, "_stackTraceString", detail.StackTrace);
                    SetExceptionField(inner, "_helpURL", detail.HelpLink);
                    exception = new Exception(ex.Message, inner);
                }
            }

            return exception;
        }

        private static void SetExceptionField(Exception exception, string field, object value)
        {
            typeof(Exception).GetField(field, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(exception, value);
        }


        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (!reply.IsFault)
            {
                return;
            }

            FaultException faultException = this.FaultFormatter.Deserialize(MessageFault.CreateFault(reply, int.MaxValue), reply.Headers.Action);
            FaultException<ServiceExceptionDetail> genericFaultException = faultException as FaultException<ServiceExceptionDetail>;
            if (genericFaultException == null)
            {
                throw faultException;
            }

            throw ExtractException(genericFaultException.Detail);
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }

        #endregion
    }
}