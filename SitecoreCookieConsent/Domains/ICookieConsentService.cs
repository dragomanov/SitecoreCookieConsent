using Sitecore.Pipelines;

namespace Sitecore.CookieConsent.Domains
{
    public interface ICookieConsentService
    {
        void RenderCookieConsent();

        string GetCookieConsent();
    }
}
