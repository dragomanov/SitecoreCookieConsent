using Sitecore.CookieConsent.Domains;
using Sitecore.CookieConsent.Services;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Request.RequestEnd;

namespace Sitecore.CookieConsent.Pipelines.RequestEnd
{
    public class RenderCookieConsent : RequestEndProcessor
    {
        protected ICookieConsentService Service { get; set; }

        public RenderCookieConsent()
            : this(new CookieConsentService())
        {
        }

        public RenderCookieConsent(ICookieConsentService service)
        {
            Assert.ArgumentNotNull(service, "service");
            Service = service;
        }

        public override void Process(RequestEndArgs args)
        {
            Render(args);
        }

        protected virtual void Render(RequestEndArgs args)
        {
            Service.RenderCookieConsent(args);
        }
    }
}