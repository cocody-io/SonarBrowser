using log4net;
using log4net.Core;
using SonarBrowser.Infrastructure.ContextProvider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SonarBrowser.Infrastructure.Logging
{
    public class Log4NetLoggingService : ILoggingService
    {
        private readonly IContextService _contextService;
        private string _log4netConfigFileName;

        public Log4NetLoggingService( IContextService contextService)
        {
            _contextService = contextService ?? throw new ArgumentNullException("ContextService");
            _log4netConfigFileName = "log4net.config";
            SetupLogger();
        }

        private void SetupLogger()
        {
            FileInfo log4netSettingsFileInfo = new FileInfo(HttpContext.Current.Server.MapPath(string.Concat("~/", _log4netConfigFileName)));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(log4netSettingsFileInfo);
        }

        public void LogInfo(object logSource, string message, Exception exception = null)
        {
            LogMessageWithProperties(logSource, message, Level.Info, exception);
        }

        public void LogWarning(object logSource, string message, Exception exception = null)
        {
            LogMessageWithProperties(logSource, message, Level.Warn, exception);
        }

        public void LogError(object logSource, string message, Exception exception = null)
        {
            LogMessageWithProperties(logSource, message, Level.Error, exception);
        }

        public void LogFatal(object logSource, string message, Exception exception = null)
        {
            LogMessageWithProperties(logSource, message, Level.Fatal, exception);
        }

        private void LogMessageWithProperties(object logSource, string message, Level level, Exception exception)
        {
            var logger = LogManager.GetLogger(logSource.GetType());

            var loggingEvent = new LoggingEvent(logSource.GetType(), logger.Logger.Repository, logger.Logger.Name, level, message, exception);
            AddProperties(logSource, exception, loggingEvent);
            try
            {
                logger.Logger.Log(loggingEvent);
            }
            catch (AggregateException ae)
            {
                ae.Handle(x => { return true; });
            }
            catch (Exception) { }
        }

        private string GetUserName()
        {
            return _contextService.GetUserName();
        }

        private void AddProperties(object logSource, Exception exception, LoggingEvent loggingEvent)
        {
            loggingEvent.Properties["UserName"] = GetUserName();
            try
            {
                ContextProperties contextProperties = _contextService.GetContextProperties();
                if (contextProperties != null)
                {
                    try
                    {
                        loggingEvent.Properties["UserAgent"] = contextProperties.UserAgent;
                        loggingEvent.Properties["RemoteHost"] = contextProperties.RemoteHost;
                        loggingEvent.Properties["Path"] = contextProperties.Path;
                        loggingEvent.Properties["Query"] = contextProperties.Query;
                        loggingEvent.Properties["RefererUrl"] = contextProperties.Referrer;
                        loggingEvent.Properties["RequestId"] = contextProperties.RequestId;
                        loggingEvent.Properties["SessionId"] = contextProperties.SessionId;
                    }
                    catch (Exception)
                    {
                    }
                }

                loggingEvent.Properties["ExceptionType"] = exception == null ? "" : exception.GetType().ToString();
                loggingEvent.Properties["ExceptionMessage"] = exception == null ? "" : exception.Message;
                loggingEvent.Properties["ExceptionStackTrace"] = exception == null ? "" : exception.StackTrace;
                loggingEvent.Properties["LogSource"] = logSource.GetType().ToString();
            }
            catch (Exception ex)
            {
                var type = typeof(Log4NetLoggingService);
                var logger = LogManager.GetLogger(type);
                logger.Logger.Log(type, Level.Fatal, "Exception when extracting properties: " + ex.Message, ex);
            }
        }
    }
}
