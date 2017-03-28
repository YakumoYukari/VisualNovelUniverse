using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Core.Utils;
using mshtml;
using Visual_Novel_Universe.Models;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

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

        public static void InjectCss(string Css)
        {
            if (WebBrowser.Document == null) return;
            var doc = (IHTMLDocument2)(WebBrowser.Document.DomDocument);
            var ss = doc.createStyleSheet("", 0);

            ss.cssText = Css;
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
                .Where(HtmlElem =>
                    {
                        string Href = HtmlElem.GetAttribute("href");
                        return Regex.IsMatch(Href, @"/v\d+") && LookupSource.Select(V => V.VndbLink).Contains(Href);
                    }))
            {
                Element.Style = string.Format(Properties.Settings.Default.VndbLinkOwnedStyle, Color.ToLower());

                foreach (var Child in Element.Children.Cast<HtmlElement>())
                {
                    Child.Style = $"border-style:solid;border-width:2px;border-color:{Color.ToLower()};";
                }
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
                Logger.Instance.LogError("ShowConfirmationNeededMessage: Browser document is null.");
                return;
            }
            
            var HeaderElements = WebBrowser.Document.GetElementsByTagName("h1");

            foreach (var Element in HeaderElements.Cast<HtmlElement>()
                .Where(HtmlElem => HtmlElem.InnerText == "Browse visual novels"))
            {
                Element.Style = @"background-color:#000000;color:#FF0000";
                Element.InnerText = " Select the correct VN below (or change your search) and go to Data -> Confirm VN. ";
            }
        }

        public static void AppendConfirmVnDirective()
        {
            if (!IsOnVndbPage()) return;

            if (WebBrowser.Document == null)
            {
                Logger.Instance.LogError("AppendConfirmVnDirective: Browser document is null.");
                return;
            }

            var HeaderElements = WebBrowser.Document.GetElementsByTagName("h1");

            int Index = 0;
            foreach (var Element in HeaderElements.Cast<HtmlElement>())
            {
                //I hate mshtml
                Index++;
                if (Index != 2) continue;
                Element.InnerHtml += @"  <a href=""vnu://confirmvn"">Confirm Selected VN is This Entry</a>";
                break;
            }
        }

        public static void AppendTabsToVnPage()
        {
            if (!IsOnVndbPage()) return;

            if (WebBrowser.Document == null)
            {
                Logger.Instance.LogError("AppendConfirmVnDirective: Browser document is null.");
                return;
            }

            var Doc = new HtmlDocument();
            Doc.LoadHtml(Html);
            
            var MainBox = Doc.DocumentNode.SelectSingleNode("//div[@class=\"mainbox\"]");

            string EnglishTitle = MainBox.SelectSingleNode("h1")?.InnerText;
            string EnglishName = StringUtils.FixUrlCharacters(EnglishTitle);

            string JapaneseTitle = MainBox.SelectSingleNode("h2")?.InnerText;
            string JapaneseName = StringUtils.FixUrlCharacters(JapaneseTitle);

            var HeaderElements = WebBrowser.Document.GetElementsByTagName("ul");

            const string SUKEBEI_NYAA = @"http://sukebei.nyaa.se/?page=search&cats=7_27&term={0}&sort=2";

            foreach (var Element in HeaderElements.Cast<HtmlElement>())
            {
                Element.InnerHtml +=
                    $@"<li><a href=""{string.Format(SUKEBEI_NYAA, EnglishName).GetUri()}"">sukebei eng</a></li>" +
                    (!string.IsNullOrEmpty(JapaneseName) ? $@"<li><a href=""{string.Format(SUKEBEI_NYAA, JapaneseName).GetUri()}"">sukebei jpn</a></li>" : "");
                break;
            }
        }
    }
}
