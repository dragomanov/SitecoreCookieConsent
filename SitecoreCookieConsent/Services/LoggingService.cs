using System;
using Sitecore.CookieConsent.Domains;
using Sitecore.Diagnostics;

namespace Sitecore.CookieConsent.Services
{
    public class LoggingService : ILoggingService
    {
        public void LogDebug(string message)
        {
            Log.Debug(message, typeof(LoggingService));
        }

        public void LogInfo(string message)
        {
            Log.Info(message, typeof(LoggingService));
        }

        public void LogWarn(string message)
        {
            Log.Warn(message, typeof(LoggingService));
        }

        public void LogError(string message)
        {
            LogError(message, null);
        }

        public void LogError(string message, Exception ex)
        {
            Log.Error(message, ex, typeof(LoggingService));
        }

        public void LogFatal(string message)
        {
            LogFatal(message, null);
        }

        public void LogFatal(string message, Exception ex)
        {
            Log.Fatal(message, ex, typeof(LoggingService));
        }
    }
}