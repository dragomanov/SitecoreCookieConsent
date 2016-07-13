using System;
using System.Web.UI;
using Sitecore.CookieConsent.Models;

namespace Sitecore.CookieConsent.sitecore_modules.Web.CookieConsent
{
    public partial class CookieConsentConstrol : UserControl
    {
        public CookieConsentSettings Model { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}