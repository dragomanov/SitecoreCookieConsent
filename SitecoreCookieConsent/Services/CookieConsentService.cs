using System.Web.UI;
using Sitecore.CookieConsent.layouts.Modules.CookieConsent;
using Sitecore.CookieConsent.Models;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        private const string SettingsItemPath = "/sitecore/system/Modules/Cookie Consent/Settings/{0}";

        private static readonly ID EnabledFieldID = new ID("{48458386-3B70-414E-BC99-9545D41FA78F}");
        private static readonly ID MessageFieldID = new ID("{6B7B8503-E07F-43F1-B7E6-C0CBD2332274}");
        private static readonly ID DismissButtonFieldID = new ID("{13157F2A-E4AA-432E-A2A5-2403103AD1E2}");
        private static readonly ID LearnMoreFieldID = new ID("{8662EF97-1FBD-46DB-B70E-22826CC1C313}");
        private static readonly ID PolicyLinkFieldID = new ID("{14A55E2B-F047-4D11-AA71-F235C06CE341}");
        private static readonly ID ThemeFieldID = new ID("{3205FD36-5762-4F45-A0B1-C206110E4479}");

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
            var policyLink = (LinkField)settingItem.Fields[PolicyLinkFieldID];

            return new CookieConsentSettings
            {
                Enabled = settingItem[EnabledFieldID] == "1",
                Message = settingItem[MessageFieldID],
                DismissButton = settingItem[DismissButtonFieldID],
                LearnMore = settingItem[LearnMoreFieldID],
                PolicyLink = policyLink != null ? policyLink.GetFriendlyUrl() : string.Empty,
                Theme = settingItem[ThemeFieldID]
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