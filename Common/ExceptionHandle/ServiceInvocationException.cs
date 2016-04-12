
using System.Runtime.Serialization;
namespace FengSharp.OneCardAccess.Common
{
    public class ServiceInvocationException : global::System.Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ServiceInvocationException() { }
        public ServiceInvocationException(string message) : base(message) { }
        public ServiceInvocationException(string message, global::System.Exception inner) : base(message, inner) { }
        protected ServiceInvocationException(
          global::System.Runtime.Serialization.SerializationInfo info,
          global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}