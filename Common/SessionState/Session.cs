using System;
using System.Collections.Generic;

namespace FengSharp.OneCardAccess.Common
{

    [Serializable]
    public class Session
    {
        private const string CallContextKey = "__Session";
        internal const string ContextHeaderLocalName = "__Session";
        internal const string ContextHeaderNamespace = "www.fengsharp.com";
        public string Ticket { get; private set; }
        public object SessionClientId { get; private set; }
        public string SessionClientNo { get; private set; }
        public string SessionClientName { get; private set; }
        public Session(string ticket, object sessionClientId, string sessionClientNo, string sessionClientName)
        {
            Ticket = ticket;
            SessionClientId = sessionClientId;
            SessionClientNo = sessionClientNo;
            SessionClientName = sessionClientName;
        }
        [ThreadStatic]
        static Session _Current;
        public static Session Current
        {
            get
            {
                return _Current as Session;
            }

            set
            {
                _Current = value;
            }
        }
    }
}
