﻿using Wirehome.Contracts.Logging;
using System;
using Windows.Foundation.Diagnostics;

namespace Wirehome.Extensions
{
    public class EtwLoggingService : ILogAdapter
    {
        LoggingChannel _loggingChannel;

        public EtwLoggingService()
        {
            _loggingChannel = new LoggingChannel("Wirehome", null, new Guid("4bd2826e-54a1-4ba9-bf63-92b73ea1ac4a"));
        }
        
        public void ProcessLogEntry(LogEntry logEntry)
        {
            var fields = new LoggingFields();

            fields.AddString("Source", logEntry.Source ?? "");
            //fields.AddString("Exception", logEntry.Exception ?? "");
            //fields.AddString("Source", logEntry.Source ?? "");

            _loggingChannel.LogEvent(logEntry.Message, fields, MapSeverity(logEntry.Severity));
        }

        private static LoggingLevel MapSeverity(LogEntrySeverity severity)
        {
            var level = LoggingLevel.Information;
            if (severity == LogEntrySeverity.Error)
            {
                level = LoggingLevel.Error;
            }
            else if (severity == LogEntrySeverity.Verbose)
            {
                level = LoggingLevel.Verbose;
            }
            else if (severity == LogEntrySeverity.Warning)
            {
                level = LoggingLevel.Warning;
            }

            return level;
        }
    }
}