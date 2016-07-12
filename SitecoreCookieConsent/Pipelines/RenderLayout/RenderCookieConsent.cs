using Sitecore.CookieConsent.Domains;
using Sitecore.CookieConsent.Services;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderLayout;

namespace Sitecore.CookieConsent.Pipelines.RenderLayout
{
    public class RenderCookieConsent : RenderLayoutProcessor
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

        public override void Process(RenderLayoutArgs args)
        {
            Render();
        }

        protected virtual void Render()
        {
            Service.RenderCookieConsent();
        }
    }
}