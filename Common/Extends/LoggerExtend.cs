using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Practices.EnterpriseLibrary.Logging
{
    public static class LoggerExtend
    {
        private const int DefaultPriority = -1;
        private const int DefaultEventId = 1;
        private static readonly ICollection<string> emptyCategoriesList = new string[0];
        public static void Write(this LogWriter logwriter, object message, TraceEventType severity = TraceEventType.Information)
        {
            logwriter.Write(message, emptyCategoriesList, DefaultPriority, DefaultEventId, severity);
        }
    }
}
