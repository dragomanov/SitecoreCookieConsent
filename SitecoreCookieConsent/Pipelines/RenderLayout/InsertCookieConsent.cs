using Sitecore.CookieConsent.Domains;
using Sitecore.CookieConsent.Services;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.RenderLayout;

namespace Sitecore.CookieConsent.Pipelines.RenderLayout
{
    public class InsertCookieConsent : RenderLayoutProcessor
    {
        protected ICookieConsentService Service { get; set; }

        public InsertCookieConsent()
            : this(new CookieConsentService())
        {
        }

        public InsertCookieConsent(ICookieConsentService service)
        {
            Assert.ArgumentNotNull(service, "service");
            Service = service;
        }

        public override void Process(RenderLayoutArgs args)
        {
            RenderControl();
        }

        protected virtual void RenderControl()
        {
            Service.RenderControl();
        }
    }
}