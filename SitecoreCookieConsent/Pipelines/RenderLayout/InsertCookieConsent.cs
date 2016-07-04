using System.Diagnostics;
using Sitecore.CookieConsent.Services;
using Sitecore.Pipelines.RenderLayout;

namespace Sitecore.CookieConsent.Pipelines.RenderLayout
{
    public class InsertCookieConsent : RenderLayoutProcessor
    {
        protected virtual ICookieConsentService Service { get; set; } = new CookieConsentService();

        public override void Process(RenderLayoutArgs args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RenderControl();
            sw.Stop();
        }

        protected virtual void RenderControl()
        {
            Service.RenderControl();
        }
    }
}