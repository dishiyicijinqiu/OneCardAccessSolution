using System;
using System.Runtime.Serialization;

namespace FengSharp.OneCardAccess.Common
{
    [System.Serializable]
    public class LoginTimeOutException : Exception
    {
        public LoginTimeOutException()
        {
        }
        public LoginTimeOutException(string message)
            : base(message)
        {
        }
        public LoginTimeOutException(string message, Exception inner)
            : base(message, inner)
        {
        }
        protected LoginTimeOutException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
