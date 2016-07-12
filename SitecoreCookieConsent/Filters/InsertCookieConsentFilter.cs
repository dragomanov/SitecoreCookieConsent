using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Sitecore.Diagnostics;

namespace Sitecore.CookieConsent.Filters
{
    public class InsertCookieConsentFilter : MemoryStream
    {
        private const string ClosingTag = "</body>";

        private readonly Stream _output;
        private readonly string _script;

        public InsertCookieConsentFilter(Stream output, string script)
        {
            Assert.IsNotNull(output, "output");
            Assert.IsNotNull(output, "settings");
            _output = output;
            _script = script;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            string text = Encoding.UTF8.GetString(buffer);
            text = Regex.Replace(text, ClosingTag, _script + ClosingTag, RegexOptions.IgnoreCase);
            _output.Write(Encoding.UTF8.GetBytes(text), 0, Encoding.UTF8.GetByteCount(text));
        }
    }
}