using System.Collections.Generic;
using System.IO;
using System.Linq;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    public static class LegacyVnLoader
    {
        /// <summary>
        /// Loads a new VisualNovel object from an old VN Info.txt file from the last generation of VNU.
        /// </summary>
        /// <param name="Folderpath">The VN Info.txt file to convert.</param>
        /// <returns></returns>
        public static VisualNovel Load(string Folderpath)
        {
            string Filename = Path.Combine(Folderpath, "VN Info.txt");
            var Lines = File.ReadAllLines(Filename);

            var Vn = new VisualNovel
            {
                VnFolderPath = Path.GetDirectoryName(Filename),
                Description = GetValue(Lines, "VNDB Description"),
                Developers = string.IsNullOrWhiteSpace(GetValue(Lines, "Developer")) ? Properties.Resources.MissingDeveloperInfo.ToSingleElementList() : GetValue(Lines, "Developer").ToSingleElementList(),
                EnglishName = GetValue(Lines, "English Name"),
                Favorited = GetValue(Lines, "Favorite") == "Yes",
                HasVnInfo = true,
                JapaneseName = GetValue(Lines, "Japanese Name"),
                Publishers = new List<Publisher>(),
                VndbLink = GetValue(Lines, "VNDB Link"),
                Tags = GetValue(Lines, "VNDB Tags").Split(',').Select(t => t.Trim()).ToList(),
                Owned = true,
                EnglishReleases = GetValue(Lines, "English Release").Split(',')
                    .Where(s => !string.IsNullOrWhiteSpace(s) && s.Contains('-'))
                    .Select(s =>
                    {
                        var Parts = s.Split('-');
                        return new Release {Publisher = Parts[0].Trim(), Link = Parts[1].Trim()};
                    }).ToList()
            };

            return Vn;
        }

        private static string GetValue(IEnumerable<string> Lines, string Value)
        {
            string Match = Lines.SingleOrDefault(s => s.StartsWith(Value));
            return string.IsNullOrEmpty(Match) ? string.Empty : Match.Split(new[] { ':' }, 2)[1].Trim();
        }

        private static List<string> ToSingleElementList(this string Element)
        {
            return new List<string>(new[] { Element });
        }
    }
}
