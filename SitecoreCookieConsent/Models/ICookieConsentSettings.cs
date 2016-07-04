namespace Sitecore.CookieConsent.Models
{
    public interface ICookieConsentSettings
    {
        bool Enabled { get; set; }

        string DismissButton { get; set; }

        string LearnMore { get; set; }

        string Message { get; set; }

        string PolicyLink { get; set; }

        string Theme { get; set; }
    }
}
