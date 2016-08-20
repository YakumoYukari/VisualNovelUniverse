using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using Visual_Novel_Universe.Models;
using HtmlAgilityPack;

namespace Visual_Novel_Universe
{
    public static class VndbExtractor
    {
        public static VisualNovel ExtractVisualNovel(string Url, string Html)
        {
            var Vn = new VisualNovel();

            var Doc = new HtmlDocument();
            Doc.LoadHtml(Html);

            Vn.VndbLink = Url;

            var MainBox = Doc.DocumentNode.SelectSingleNode("//div[@class=\"mainbox\"]");
            var VnDescription = Doc.DocumentNode.SelectSingleNode("//td[@class=\"vndesc\"]");

            string EnglishTitle = MainBox.SelectSingleNode("h1")?.InnerText;
            Vn.EnglishName = StringUtils.FixUrlCharacters(EnglishTitle);

            string JapaneseTitle = MainBox.SelectSingleNode("h2")?.InnerText;
            Vn.JapaneseName = StringUtils.FixUrlCharacters(JapaneseTitle);

            string Description =
                VnDescription.SelectSingleNode("p")?.InnerHtml
                    .Replace("<br>", Environment.NewLine)
                    .Replace("<br/>", Environment.NewLine)
                    .Replace("<br />", Environment.NewLine);
            Vn.Description = StringUtils.FixUrlCharacters(StringUtils.ConvertHrefToText(Description));

            Vn.NovelLength = GetLength(Doc.DocumentNode);
            Vn.Developers = GetDevelopers(Doc.DocumentNode);
            Vn.Publishers = GetPublishers(Doc.DocumentNode);
            Vn.Tags = GetTags(Doc.DocumentNode);

            Vn.EnglishReleases = new List<Release>(Vn.Publishers.Where(p => p.Language == "English").Select(p => new Release{ Link = p.Link, Publisher = p.PublisherName }));

            Vn.Favorited = false;
            Vn.HasVnInfo = false;
            Vn.VnFolderPath = "";
            Vn.Highlighted = false;
            Vn.HighlightColor = "#FFFFFF";

            return Vn;
        }

        public static Bitmap ExtractCoverImage(string Html)
        {
            var Doc = new HtmlDocument();
            Doc.LoadHtml(Html);

            string Url = "https:" + Doc.DocumentNode.SelectSingleNode("//img[1]").GetAttributeValue("src", "");

            if (string.IsNullOrWhiteSpace(Url))
                return null;

            var wc = new WebClient();
            var bytes = wc.DownloadData(Url);
            return new Bitmap(new MemoryStream(bytes));
        }

        private static Length GetLength(HtmlNode Root)
        {
            string Len = Root.SelectSingleNode("/html/body/div[4]/div/div/table/tr[./*[contains(., 'Length')]]/td[2]")?.InnerText;
            if (Len == null) return Length.Unknown;

            Len = StringUtils.ClearAllBracketGroups(Len).Trim().ToLowerInvariant();

            switch (Len)
            {
                case "very long":
                    return Length.VeryLong;
                case "long":
                    return Length.Long;
                case "medium":
                    return Length.Medium;
                case "short":
                    return Length.Short;
                case "very short":
                    return Length.VeryShort;
                default:
                    return Length.Unknown;
            }
        }

        private static List<string> GetDevelopers(HtmlNode Node)
        {
            string Dev = Node.SelectSingleNode("/html/body/div[4]/div/div/table/tr[./*[contains(., 'Developer')]]/td[2]")?.InnerText;

            if (Dev == null) return new List<string>();

            var Devs = StringUtils.FixUrlCharacters(Dev).Split('&');
            StringUtils.TrimAll(ref Devs);

            return Devs.ToList();
        }

        private static List<Publisher> GetPublishers(HtmlNode Node)
        {
            var Pubs = Node.SelectNodes("/html/body/div[4]/div/div/table/tr[./*[contains(., 'Publishers')]]/td[2]/*");
            if (Pubs == null) return new List<Publisher>();

            var Found = new List<Publisher>();

            foreach (var ChildNode in Pubs)
            {
                if (!ChildNode.GetAttributeValue("class", "").StartsWith("icons")) continue;
                
                var CurNode = ChildNode.NextSibling;
                while (CurNode.HasAttributes || CurNode.Name == "#text")
                {
                    if (CurNode.Name == "#text")
                    {
                        CurNode = CurNode.NextSibling;
                        continue;
                    }
                    
                    var NewPub = new Publisher
                    {
                        Language = StringUtils.FixUrlCharacters(ChildNode.GetAttributeValue("title", "")),
                        PublisherName = StringUtils.FixUrlCharacters(CurNode.InnerText),
                        Link = "https://www.vndb.org" + StringUtils.FixUrlCharacters(CurNode.GetAttributeValue("href", ""))
                    };

                    Found.Add(NewPub);
                    CurNode = CurNode.NextSibling;
                }
            }

            return Found;
        }

        private static List<string> GetTags(HtmlNode Node)
        {
            var TagsNode = Node.SelectSingleNode("//div[@id=\"vntags\"]");
            return TagsNode == null ? new List<string>() : new List<string>(TagsNode.ChildNodes.Elements().Where(e => e.Name == "a").Select(e => StringUtils.FixUrlCharacters(e.InnerText)));
        }
    }
}
