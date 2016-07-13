using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Sitecore.Configuration;
using Sitecore.CookieConsent.Constants;
using Sitecore.CookieConsent.Domains;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Mvc.Presentation;

namespace Sitecore.CookieConsent.Renderers
{
    public class CookieConsentViewRenderer : ViewRenderer
    {
        private const string ClosingTag = "</body>";

        protected ICookieConsentService Service { get; set; }

        public CookieConsentViewRenderer()
            : this(Factory.CreateObject(Paths.CookieConsentServiceConfigPath, true) as ICookieConsentService)
        {
        } 

        public CookieConsentViewRenderer(ICookieConsentService service)
        {
            Assert.ArgumentNotNull(service, "service");
            Service = service;
        }

        public override void Render(TextWriter writer)
        {
            Assert.IsNotNull(writer, "writer");
            string path = GetPath(ViewPath, MvcSettings.DefaultViewExtension);
            HtmlHelper htmlHelper = GetHtmlHelper();
            MvcHtmlString mvcHtmlString;

            try
            {
                object model = Model;
                mvcHtmlString = model != null ? htmlHelper.Partial(path, model) : htmlHelper.Partial(path);
            }
            catch (Exception ex)
            {
                string str = string.Format("Error while rendering view: '{0}'", path);

                if (Model != null)
                {
                    Type type = Model.GetType();
                    str += string.Format(" (model: '{0}, {1}')", type.FullName, type.Assembly.GetName().Name);
                }

                throw new InvalidOperationException(str + string.Format(".{0}", Environment.NewLine), ex);
            }

            if (mvcHtmlString == null)
            {
                return;
            }

            string output = mvcHtmlString.ToString();
            output = Regex.Replace(output, ClosingTag, Service.GetCookieConsent() + ClosingTag, RegexOptions.IgnoreCase);
            writer.Write(output);
        }

        public string GetPath(string path, string defaultViewExtension)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string extension = Path.GetExtension(path);
            if (!MvcSettings.IsViewExtension(extension) && path.IsAbsoluteViewPath())
            {
                return path.WithPostfix(defaultViewExtension.WithPrefix('.'));
            }

            if (MvcSettings.IsViewExtension(extension) && !path.IsAbsoluteViewPath())
            {
                return path.WithoutPostfix(extension.WithPrefix('.'));
            }

            return path;
        }
    }
}