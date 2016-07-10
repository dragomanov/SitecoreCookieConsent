using System.Web.UI;
using Sitecore.CookieConsent.layouts.Modules.CookieConsent;
using Sitecore.CookieConsent.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        private const string SettingsItemPath = "/sitecore/system/Modules/Cookie Consent/Settings/{0}";

        public void RenderControl()
        {
            Item settingsItem = GetSettingsItem();

            if (settingsItem == null)
            {
                return;
            }

            CookieConsentSettings settings = GetSettingsModel(settingsItem);

            if (!settings.Enabled)
            {
                return;
            }

            RenderAscxControl(settings);
        }

        private static Item GetSettingsItem()
        {
            string settingsItemPath = string.Format(SettingsItemPath, Context.GetSiteName());

            return Context.Database != null ? Context.Database.GetItem(settingsItemPath) : null;
        }

        private static CookieConsentSettings GetSettingsModel(Item settingItem)
        {
            var policyLink = (LinkField)settingItem.Fields["PolicyLink"];

            return new CookieConsentSettings
            {
                Enabled = settingItem["Enabled"] == "1",
                Message = settingItem["Message"],
                DismissButton = settingItem["DismissButton"],
                LearnMore = settingItem["LearnMore"],
                PolicyLink = policyLink != null ? policyLink.GetFriendlyUrl() : string.Empty,
                Theme = settingItem["Theme"]
            };
        }

        private static void RenderAscxControl(CookieConsentSettings settings)
        {
            Page page = Context.Page != null ? Context.Page.Page : null;

            if (page == null)
            {
                return;
            }

            var control = (ScriptJs)page.LoadControl("~/layouts/Modules/CookieConsent/ScriptJs.ascx");

            if (control == null)
            {
                return;
            }

            control.Model = settings;
            page.Controls.Add(control);
        }
    }
}