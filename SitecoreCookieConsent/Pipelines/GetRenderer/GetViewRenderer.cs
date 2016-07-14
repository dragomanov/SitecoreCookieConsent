using Sitecore.CookieConsent.Renderers;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;

namespace Sitecore.CookieConsent.Pipelines.GetRenderer
{
    public class GetViewRenderer : Mvc.Pipelines.Response.GetRenderer.GetViewRenderer
    {
        public override void Process(GetRendererArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            args.Result = GetRenderer(args.Rendering, args);
        }

        protected override Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
        {
            string viewPath = GetViewPath(rendering, args);

            if (string.IsNullOrWhiteSpace(viewPath))
            {
                return null;
            }

            if (rendering.RenderingType != "Layout")
            {
                return args.Result;
            }

            return new CookieConsentViewRenderer
            {
                ViewPath = viewPath,
                Rendering = rendering
            };
        }
    }
}