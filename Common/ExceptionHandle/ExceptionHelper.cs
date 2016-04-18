using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace FengSharp.OneCardAccess.Common
{
   public static class ExceptionHelper
   {
       public static bool HandleException(Exception ex,string exceptionPolicy)
       {
           FaultException<ServiceExceptionDetail> faultException = ex as FaultException<ServiceExceptionDetail>;
           if (faultException != null)
           {
               ex = GetException(faultException.Detail);               
           }

           return ExceptionPolicy.HandleException(ex, exceptionPolicy);
       }

       public static Exception GetException(ServiceExceptionDetail exceptionDetail)
       {
           if (null == exceptionDetail.InnerException)
           {
               return (Exception)Activator.CreateInstance(Type.GetType(exceptionDetail.AssemblyQualifiedName),exceptionDetail.Message);
           }

           Exception innerException = GetException(exceptionDetail.InnerException);
           return (Exception)Activator.CreateInstance(Type.GetType(exceptionDetail.AssemblyQualifiedName), exceptionDetail.Message, innerException);
       }
    }
}