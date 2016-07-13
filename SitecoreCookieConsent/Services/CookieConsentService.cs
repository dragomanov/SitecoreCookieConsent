using System.Collections.Generic;
using System.Web.UI;
using Sitecore.CookieConsent.Domains;
using Sitecore.CookieConsent.Models;
using Sitecore.CookieConsent.sitecore_modules.Web.CookieConsent;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        private const string SettingsItemPath = "/sitecore/system/Modules/Cookie Consent/Settings/{0}";
        private const string ScriptFormat =
            @"  <!-- Begin Cookie Consent plugin by Silktide - http://silktide.com/cookieconsent -->
                <script type=""text/javascript"">
                    window.cookieconsent_options = {{
                        ""message"": ""{0}"",
                        ""dismiss"": ""{1}"",
                        ""learnMore"": ""{2}"",
                        ""link"": ""{3}"",
                        ""theme"": ""{4}""
                    }};
                </script>

                <script type=""text/javascript"" src=""//cdnjs.cloudflare.com/ajax/libs/cookieconsent2/1.0.10/cookieconsent.min.js""></script>
                <!-- End Cookie Consent plugin -->";

        private static readonly ID EnabledFieldID = new ID("{48458386-3B70-414E-BC99-9545D41FA78F}");
        private static readonly ID MessageFieldID = new ID("{6B7B8503-E07F-43F1-B7E6-C0CBD2332274}");
        private static readonly ID DismissButtonFieldID = new ID("{13157F2A-E4AA-432E-A2A5-2403103AD1E2}");
        private static readonly ID LearnMoreFieldID = new ID("{8662EF97-1FBD-46DB-B70E-22826CC1C313}");
        private static readonly ID PolicyLinkFieldID = new ID("{14A55E2B-F047-4D11-AA71-F235C06CE341}");
        private static readonly ID ThemeFieldID = new ID("{3205FD36-5762-4F45-A0B1-C206110E4479}");

        private static readonly ICollection<string> SitecoreSitesNames = new[]
        {
            "shell",
            "login",
            "admin",
            "service",
            "modules_shell",
            "modules_website",
            "scheduler",
            "system",
            "publisher"
        };

        private readonly Item _settingsItem;
        private readonly CookieConsentSettings _settings;

        public CookieConsentService()
        {
            if (IsSitecoreSite())
            {
                return;
            }

            _settingsItem = GetSettingsItem();

            if (_settingsItem == null)
            {
                return;
            }

            _settings = GetSettingsModel();
        }

        public void RenderCookieConsent()
        {
            if (_settings == null || !_settings.Enabled)
            {
                return;
            }
            
            RenderAscxControl();
        }

        public string GetCookieConsent()
        {
            if (_settings == null || !_settings.Enabled)
            {
                return string.Empty;
            }

            string message = _settings.Message;
            string dismiss = _settings.DismissButton;
            string learnMore = _settings.LearnMore;
            string link = _settings.PolicyLink;
            string theme = _settings.Theme;

            return string.Format(ScriptFormat, message, dismiss, learnMore, link, theme);
        }

        private bool IsSitecoreSite()
        {
            return SitecoreSitesNames.Contains(Context.GetSiteName());
        }

        private Item GetSettingsItem()
        {
            string settingsItemPath = string.Format(SettingsItemPath, Context.GetSiteName());

            return Context.Database != null ? Context.Database.GetItem(settingsItemPath) : null;
        }

        private CookieConsentSettings GetSettingsModel()
        {
            if (_settingsItem == null)
            {
                return null;
            }

            var policyLink = (LinkField)_settingsItem.Fields[PolicyLinkFieldID];

            return new CookieConsentSettings
            {
                Enabled = _settingsItem[EnabledFieldID] == "1",
                Message = _settingsItem[MessageFieldID],
                DismissButton = _settingsItem[DismissButtonFieldID],
                LearnMore = _settingsItem[LearnMoreFieldID],
                PolicyLink = policyLink != null ? policyLink.GetFriendlyUrl() : string.Empty,
                Theme = _settingsItem[ThemeFieldID]
            };
        }

        private void RenderAscxControl()
        {
            Page page = Context.Page != null ? Context.Page.Page : null;

            if (page == null)
            {
                return;
            }

            var control = (CookieConsentConstrol)page.LoadControl("~/layouts/Modules/CookieConsent/ScriptJs.ascx");

            if (control == null)
            {
                return;
            }

            control.Model = _settings;
            page.Controls.Add(control);
        }
    }
}