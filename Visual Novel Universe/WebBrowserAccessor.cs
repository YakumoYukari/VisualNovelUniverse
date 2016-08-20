using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mshtml;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    public static class WebBrowserAccessor
    {
        public static WebBrowser WebBrowser;

        public static bool IsOnVndbPage()
        {
            return WebUtils.IsVndbPage(WebBrowser.Url.AbsoluteUri);
        }

        public static string Html
        {
            get
            {
                return WebBrowser.DocumentText;
            }
            set
            {
                WebBrowser.DocumentText = value;
            }
        }

        public static void Navigate(string Url)
        {
            WebBrowser.Navigate(Url);
        }

        public static void SearchVndb(string SearchTerm)
        {
            string SearchQuery = string.Format(ConfigurationManager.AppSettings["Links_VndbSearchRoot"], SearchTerm);
            WebBrowser.Navigate(SearchQuery.GetUri());
        }

        public static void InjectCss(string css)
        {
            if (WebBrowser.Document == null) return;
            var doc = (IHTMLDocument2)(WebBrowser.Document.DomDocument);
            var ss = doc.createStyleSheet("", 0);

            ss.cssText = css;
        }

        public static void SetVndbTitleColor(string Color, int FontSize)
        {
            if (!WebUtils.IsVndbPage(WebBrowser.Url.AbsoluteUri))
            {
                Logger.Instance.LogError($"SetVndbTitleColor attempted to set color to {Color} while not on VNDB page.");
                return;
            }

            if (WebBrowser.Document == null)
            {
                Logger.Instance.LogError("SetVndbTitleColor: Browser document is null.");
                return;
            }

            var HeaderElements = WebBrowser.Document.GetElementsByTagName("h1");

            if (HeaderElements.Count < 2) return;
            foreach (HtmlElement Element in HeaderElements)
                Element.Style = string.Format(Properties.Settings.Default.VndbTitleOwnedStyle, Color.ToLower(), FontSize);
        }

        public static void HighlightOwnedVnsOnPage(IEnumerable<VisualNovel> LookupSource, string Color)
        {
            if (!WebBrowser.Url.AbsoluteUri.Contains("vndb.org")) return;

            if (WebBrowser.Document == null)
            {
                Logger.Instance.LogError("HighlightOwnedVnsOnPage: Browser document is null.");
                return;
            }

            var HeaderElements = WebBrowser.Document.GetElementsByTagName("a");

            foreach (var Element in HeaderElements.Cast<HtmlElement>()
                .Where(he =>
                    {
                        string Href = he.GetAttribute("href");
                        return Regex.IsMatch(Href, @"/v\d+") && LookupSource.Select(v => v.VndbLink).Contains(Href);
                    })
                .Select(e => new { Elem = e, Href = e.GetAttribute("href") }))
            {
                Element.Elem.Style = string.Format(Properties.Settings.Default.VndbLinkOwnedStyle, Color.ToLower());
            }
        }

        public static void SetScrollBarCss()
        {
            InjectCss("body\r\n{\r\nscrollbar-face-color: #C9C9C9;\r\nscrollbar-shadow-color: #2D2C4D;\r\nscrollbar-highlight-color:#000000;\r\nscrollbar-3dlight-color: #7D7E94;\r\nscrollbar-darkshadow-color: #000000;\r\nscrollbar-track-color: #2E2E2E;\r\nscrollbar-arrow-color: #B1B1C1;\r\n}");
        }

        public static void ShowConfirmationNeededMessage()
        {
            if (!WebBrowser.Url.AbsoluteUri.Contains("vndb.org")) return;

            if (WebBrowser.Document == null)
            {
                Logger.Instance.LogError("HighlightOwnedVnsOnPage: Browser document is null.");
                return;
            }
            
            var HeaderElements = WebBrowser.Document.GetElementsByTagName("h1");

            foreach (var Element in HeaderElements.Cast<HtmlElement>()
                .Where(he => he.InnerText == "Browse visual novels"))
            {
                Element.Style = @"background-color:#000000;color:#FF0000";
                Element.InnerText = " An exact match has not been found. Select the correct VN below and go to Data -> Confirm VN. ";
            }
        }
    }
}
