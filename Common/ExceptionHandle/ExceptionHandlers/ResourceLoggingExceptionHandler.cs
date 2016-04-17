////using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
////using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
////using Microsoft.Practices.EnterpriseLibrary.Logging;
////using System;
////using System.Diagnostics;
////using System.Linq;
////using System.Text;
////using System.Collections;
////using System.IO;
////using System.Globalization;
////using System.Reflection;


//using System;
//using System.Collections;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.Properties;
//using Microsoft.Practices.EnterpriseLibrary.Logging;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
//using System.ComponentModel;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration;
//using System.Collections.Generic;
//using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;

//namespace FengSharp.OneCardAccess.Common.ExceptionHandle.ExceptionHandlers
//{
//    //[ConfigurationElementType(typeof(LoggingExceptionHandlerData))]
//    public class ResourceLoggingExceptionHandler : IExceptionHandler
//    {

//        private readonly string logCategory;
//        private readonly int eventId;
//        private readonly TraceEventType severity;
//        private readonly string defaultTitle;
//        private readonly Type formatterType;
//        private readonly int minimumPriority;
//        private readonly LogWriter logWriter;
//        public ResourceLoggingExceptionHandler(string logCategory, int eventId, TraceEventType severity, string title, int priority, Type formatterType, LogWriter writer)
//        {
//            this.logCategory = logCategory;
//            this.eventId = eventId;
//            this.severity = severity;
//            this.defaultTitle = title;
//            this.minimumPriority = priority;
//            this.formatterType = formatterType;
//            this.logWriter = writer;
//        }

//        public Exception HandleException(Exception exception, Guid handlingInstanceId)
//        {
//            WriteToLog(CreateMessage(exception, handlingInstanceId), exception.Data);
//            return exception;
//        }
//        protected virtual void WriteToLog(string logMessage, IDictionary exceptionData)
//        {
//            LogEntry entry = new LogEntry(logMessage, logCategory, minimumPriority, eventId, severity, defaultTitle, null);
//            foreach (DictionaryEntry dataEntry in exceptionData)
//            {
//                if (dataEntry.Key is string)
//                {
//                    entry.ExtendedProperties.Add(dataEntry.Key as string, dataEntry.Value);
//                }
//            }
//            this.logWriter.Write(entry);
//        }
//        protected virtual StringWriter CreateStringWriter()
//        {
//            return new StringWriter(CultureInfo.InvariantCulture);
//        }
//        protected virtual Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionFormatter CreateFormatter(StringWriter writer, Exception exception, Guid handlingInstanceID)
//        {
//            ConstructorInfo constructor = GetFormatterConstructor();
//            return (Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionFormatter)constructor.Invoke(new object[] { writer, exception, handlingInstanceID });
//        }

//        private ConstructorInfo GetFormatterConstructor()
//        {
//            Type[] types = new Type[] { typeof(TextWriter), typeof(Exception), typeof(Guid) };
//            ConstructorInfo constructor = formatterType.GetConstructor(types);
//            if (constructor == null)
//            {
//                throw new ExceptionHandlingException(string.Format(CultureInfo.CurrentCulture, "MissingConstructor", formatterType.AssemblyQualifiedName));
//            }
//            return constructor;
//        }

//        private string CreateMessage(Exception exception, Guid handlingInstanceID)
//        {
//            StringWriter writer = null;
//            StringBuilder stringBuilder = null;
//            try
//            {
//                writer = CreateStringWriter();
//                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionFormatter formatter = CreateFormatter(writer, exception, handlingInstanceID);
//                formatter.Format();
//                stringBuilder = writer.GetStringBuilder();

//            }
//            finally
//            {
//                if (writer != null)
//                {
//                    writer.Close();
//                }
//            }
//            return stringBuilder.ToString();
//        }
//    }

//    [Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design.AddSateliteProviderCommand
//        (Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings.SectionName,
//        typeof(Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings),
//        "DefaultCategory", "LogCategory")]
//    [Description("ResourceLoggingExceptionHandlerDataDescription")]
//    [DisplayName("ResourceLoggingExceptionHandlerDataDisplayName")]
//    public class ResourceLoggingExceptionHandlerData : ExceptionHandlerData
//    {
//        private static readonly AssemblyQualifiedTypeNameConverter typeConverter
//            = new AssemblyQualifiedTypeNameConverter();

//        private const string logCategory = "logCategory";
//        private const string eventId = "eventId";
//        private const string severity = "severity";
//        private const string title = "title";
//        private const string formatterType = "formatterType";
//        private const string priority = "priority";
//        private const string useDefaultLogger = "useDefaultLogger";
//        public ResourceLoggingExceptionHandlerData() : base(typeof(LoggingExceptionHandler))
//        {
//        }
//        public ResourceLoggingExceptionHandlerData(string name, string logCategory, int eventId, TraceEventType severity, string title, Type formatterType, int priority)
//            : this(name, logCategory, eventId, severity, title, typeConverter.ConvertToString(formatterType), priority)
//        {
//        }
//        public ResourceLoggingExceptionHandlerData(string name, string logCategory, int eventId, TraceEventType severity, string title, string formatterTypeName, int priority)
//            : base(name, typeof(LoggingExceptionHandler))
//        {
//            LogCategory = logCategory;
//            EventId = eventId;
//            Severity = severity;
//            Title = title;
//            FormatterTypeName = formatterTypeName;
//            Priority = priority;
//        }
//        public string LogCategory
//        {
//            get { return (string)this[logCategory]; }
//            set { this[logCategory] = value; }
//        }
//        public int EventId
//        {
//            get { return (int)this[eventId]; }
//            set { this[eventId] = value; }
//        }
//        public TraceEventType Severity
//        {
//            get { return (TraceEventType)this[severity]; }
//            set { this[severity] = value; }
//        }
//        public string Title
//        {
//            get { return (string)this[title]; }
//            set { this[title] = value; }
//        }
//        public Type FormatterType
//        {
//            get { return (Type)typeConverter.ConvertFrom(FormatterTypeName); }
//            set { FormatterTypeName = typeConverter.ConvertToString(value); }
//        }
//        public string FormatterTypeName
//        {
//            get { return (string)this[formatterType]; }
//            set { this[formatterType] = value; }
//        }
//        public int Priority
//        {
//            get { return (int)this[priority]; }
//            set { this[priority] = value; }
//        }
//        public bool UseDefaultLogger
//        {
//            get { return (bool)this[useDefaultLogger]; }
//            set { this[useDefaultLogger] = value; }
//        }
//        public override IEnumerable<TypeRegistration> GetRegistrations(string namePrefix)
//        {
//            yield return new TypeRegistration<IExceptionHandler>(
//                () =>
//                new LoggingExceptionHandler(LogCategory, EventId, Severity, Title, Priority, FormatterType,
//                                            Common.Configuration.ContainerModel.Container.Resolved<LogWriter>()))
//            {
//                Name = BuildName(namePrefix),
//                Lifetime = TypeRegistrationLifetime.Transient
//            };
//        }
//    }
//}
