using System.Diagnostics;
using System.Web.UI;
using Sitecore.CookieConsent.layouts.Modules.CookieConsent;
using Sitecore.CookieConsent.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Pipelines.RenderLayout;

namespace Sitecore.CookieConsent.Pipelines.RenderLayout
{
    public class InsertCookieConsent : RenderLayoutProcessor
    {
        public override void Process(RenderLayoutArgs args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            InsertControl();
            sw.Stop();
            Diagnostics.Log.Info($"Cookie Consent took {sw.ElapsedMilliseconds}ms", this);
        }

        protected virtual void InsertControl()
        {
            string settingsItemPath = $"/sitecore/system/Modules/Cookie Consent/Settings/{Context.GetSiteName()}";
            Item settingItem = Context.Database?.GetItem(settingsItemPath);

            if (settingItem == null)
            {
                return;
            }

            Settings model = new Settings
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
            page?.Controls.Add(control);
        }
    }
}