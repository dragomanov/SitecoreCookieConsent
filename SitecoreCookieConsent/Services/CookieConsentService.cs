using System.Web.UI;
using Sitecore.CookieConsent.layouts.Modules.CookieConsent;
using Sitecore.CookieConsent.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace Sitecore.CookieConsent.Services
{
    public class CookieConsentService : ICookieConsentService
    {
        public void RenderControl()
        {
            string settingsItemPath = $"/sitecore/system/Modules/Cookie Consent/Settings/{Context.GetSiteName()}";
            Item settingItem = Context.Database?.GetItem(settingsItemPath);

            if (settingItem == null)
            {
                return;
            }

            CookieConsentSettings model = new CookieConsentSettings
            {
                Enabled = settingItem["Enabled"] == "1",
                Message = settingItem["Message"],
                DismissButton = settingItem["DismissButton"],
                LearnMore = settingItem["LearnMore"],
                PolicyLink = ((LinkField)settingItem.Fields["PolicyLink"])?.GetFriendlyUrl(),
                Theme = settingItem["Theme"]
            };

            if (!model.Enabled)
            {
                return;
            }

            Page page = Context.Page?.Page;
            ScriptJs control = (ScriptJs)page?.LoadControl("~/layouts/Modules/CookieConsent/ScriptJs.ascx");

            if (control == null)
            {
                return;
            }

            control.Model = model;
            page.Controls.Add(control);
        }
    }
}