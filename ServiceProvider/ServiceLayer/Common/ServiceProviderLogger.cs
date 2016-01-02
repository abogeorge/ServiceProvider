using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Common
{
    public class ServiceProviderLogger
    {
        private static ServiceProviderLogger instance;

        private ServiceProviderLogger()
        {
            Logger.SetLogWriter(new LogWriterFactory().Create());
        }

        public static ServiceProviderLogger GetInstance()
        {
            if (instance == null)
            {
                instance = new ServiceProviderLogger();
                return instance;
            }
            else
            {
                return instance;
            }
        }

        public void logInfo(String message)
        {
            if (Logger.IsLoggingEnabled())
            {
                LogEntry logEntry = new LogEntry()
                {
                    Message = message,
                    Severity = System.Diagnostics.TraceEventType.Information
                };

                logEntry.ExtendedProperties["location"] = this.GetType().ToString();

                Logger.Write(logEntry);
            }
        }

        public void logError(Exception exception)
        {
            if (Logger.IsLoggingEnabled())
            {
                LogEntry logEntry = new LogEntry()
                {
                    Message = exception.Message,
                    Severity = System.Diagnostics.TraceEventType.Error
                };

                logEntry.ExtendedProperties["location"] = this.GetType().ToString();
                logEntry.ExtendedProperties["Stack Trace"] = exception.StackTrace;

                Logger.Write(logEntry);
            }
        }
    }
}
