using System;
using System.Web.UI;
using Sitecore.CookieConsent.Models;

namespace Sitecore.CookieConsent.layouts.Modules.CookieConsent
{
    public partial class ScriptJs : UserControl
    {
        public ICookieConsentSettings Model { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}