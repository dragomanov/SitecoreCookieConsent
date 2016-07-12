using Sitecore.CookieConsent.Renderers;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;

namespace Sitecore.CookieConsent.Pipelines.GetRenderer
{
    public class GetViewRenderer : Mvc.Pipelines.Response.GetRenderer.GetViewRenderer
    {
        protected override Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
        {
            string viewPath = GetViewPath(rendering, args);

            if (string.IsNullOrWhiteSpace(viewPath))
            {
                return null;
            }

            if (rendering.RenderingType == "Layout")
            {
                return new CookieConsentViewRenderer
                {
                    ViewPath = viewPath,
                    Rendering = rendering
                };
            }

            return new ViewRenderer
            {
                ViewPath = viewPath,
                Rendering = rendering
            };
        }
    }
}