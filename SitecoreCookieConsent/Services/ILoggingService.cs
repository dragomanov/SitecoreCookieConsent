using System;

namespace Sitecore.CookieConsent.Services
{
    public interface ILoggingService
    {
        void LogDebug(string message);

        void LogInfo(string message);

        void LogWarn(string message);

        void LogError(string message);

        void LogError(string message, Exception ex);

        void LogFatal(string message);

        void LogFatal(string message, Exception ex);
    }
}
