using Sitecore.CookieConsent.Services;
using Sitecore.Pipelines.RenderLayout;

namespace Sitecore.CookieConsent.Pipelines.RenderLayout
{
    public class InsertCookieConsent : RenderLayoutProcessor
    {
        protected virtual ICookieConsentService Service { get; set; } = new CookieConsentService();

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