using shared = MacroContext.Shared.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Configuration;
using Serilog;
using System.IO;
using MacroContext.ApplicationServices;
using Serilog.Context;

namespace MacroContext.Infrastructure.Abstractions.Loggers
{
    public class SerilogAdapter : shared.ILogger
    {
        private readonly Serilog.ILogger _seriLogger;
        private static readonly LoggerConfiguration _loggerConfig;
        static SerilogAdapter()
        {
            var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"my-logs\myapp_.txt");
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] <ThreadId: {ThreadId}> {Message:lj}{NewLine}{Properties}{NewLine}{Exception}";
            var config = new Serilog.LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .Enrich.WithProperty("App", "MacroContext")
                .MinimumLevel.Debug()
                .WriteTo.File(
                    outputTemplate: outputTemplate,
                    path: logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 5);
            _loggerConfig = config;
        }

        public SerilogAdapter()
        {
            this._seriLogger = _loggerConfig.CreateLogger();
        }

        public void Log(shared.LogEntry entry)
        {
            try
            {
                //var sessionContext = Bootstrapper.GetInstance<ISessionContext>();
                //var clientSession = sessionContext.ClientSessionInfo;
                //var userId = clientSession.UserId == Guid.Empty ? null : clientSession.UserId.ToString();
                //using (LogContext.PushProperty("UserId", userId))
                //using (LogContext.PushProperty("ClientRouteId", clientSession.ClientCommunicationId))
                {
                    if (entry.Severity == shared.LoggingEventType.Debug)
                        this._seriLogger.Debug(entry.Exception, entry.Message);
                    if (entry.Severity == shared.LoggingEventType.Information)
                        this._seriLogger.Information(entry.Exception, entry.Message);
                    else if (entry.Severity == shared.LoggingEventType.Warning)
                        this._seriLogger.Warning(entry.Exception, entry.Message);
                    else if (entry.Severity == shared.LoggingEventType.Error)
                        this._seriLogger.Error(entry.Exception, entry.Message);
                    else
                        this._seriLogger.Fatal(entry.Exception, entry.Message);
                }

            }
            catch (Exception e)
            {
                var msg = "Error during logging Process";
                this._seriLogger
                    .ForContext("originalEntry", entry, true)
                    .Error(e, msg);
            }
        }
    }
}
