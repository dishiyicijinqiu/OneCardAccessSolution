using System;
namespace FengSharp.OneCardAccess.Common
{
    public static class ServiceProxyFactory
    {
        public static T Create<T>(string endpointName)
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            return (T)(new ServiceRealProxy<T>(endpointName).GetTransparentProxy());
        }
        static string Prefix = string.Empty;
        public static T Create<T>()
        {
            string endpointName = typeof(T).Name.Remove(0, 1);
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            endpointName = Prefix + endpointName;
            return (T)(new ServiceRealProxy<T>(endpointName).GetTransparentProxy());
        }
    }
}
