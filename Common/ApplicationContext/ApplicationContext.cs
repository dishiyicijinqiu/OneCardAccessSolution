using System;
using System.Collections.Generic;

namespace FengSharp.OneCardAccess.Common
{
    [Serializable]
    public class ApplicationContext : Dictionary<string, object>
    {
        private const string CallContextKey = "__ApplicationContext";
        internal const string ContextHeaderLocalName = "__ApplicationContext";
        internal const string ContextHeaderNamespace = "www.fengsharp.com";
        private void EnsureSerializable(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (!value.GetType().IsSerializable)
            {
                throw new ArgumentException(string.Format("The argument of the type \"{0}\" is not serializable!", value.GetType().FullName));
            }
        }
        public new object this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                this.EnsureSerializable(value);
                base[key] = value;
            }
        }
        [ThreadStatic]
        static ApplicationContext _Current;
        public static ApplicationContext Current
        {
            get
            {
                if (_Current == null)
                    _Current = new ApplicationContext();
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }
    }
}
