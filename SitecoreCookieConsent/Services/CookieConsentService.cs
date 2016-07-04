using System.Web.UI;
using Sitecore.CookieConsent.layouts.Modules.CookieConsent;
using Sitecore.CookieConsent.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        protected virtual ICookieConsentSettings SettingsModel { get; set; }

        public void RenderControl()
        {
            Item settingsItem = GetSettingsItem();

            if (settingsItem == null)
            {
                return;
            }

            SetSettingsModel(settingsItem);

            if (!SettingsModel.Enabled)
            {
                return;
            }

            RenderAscxControl();
        }

        private static Item GetSettingsItem()
        {
            string settingsItemPath = $"/sitecore/system/Modules/Cookie Consent/Settings/{Context.GetSiteName()}";

            return Context.Database?.GetItem(settingsItemPath);
        }

        private void SetSettingsModel(Item settingItem)
        {
            SettingsModel = new CookieConsentSettings
            {
                Enabled = settingItem["Enabled"] == "1",
                Message = settingItem["Message"],
                DismissButton = settingItem["DismissButton"],
                LearnMore = settingItem["LearnMore"],
                PolicyLink = ((LinkField)settingItem.Fields["PolicyLink"])?.GetFriendlyUrl(),
                Theme = settingItem["Theme"]
            };
        }

        private void RenderAscxControl()
        {
            Page page = Context.Page?.Page;
            ScriptJs control = (ScriptJs)page?.LoadControl("~/layouts/Modules/CookieConsent/ScriptJs.ascx");

            if (control == null)
            {
                return;
            }

            control.Model = SettingsModel;
            page.Controls.Add(control);
        }
    }
}