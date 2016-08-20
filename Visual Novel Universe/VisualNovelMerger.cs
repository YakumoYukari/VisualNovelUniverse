using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    public static class VisualNovelMerger
    {
        public static VisualNovel MergeLocalAndWeb(VisualNovel Local, VisualNovel Web)
        {
            return new VisualNovel
            {
                VnFolderPath = Local.VnFolderPath,
                Favorited = Local.Favorited,
                Highlighted = Local.Highlighted,
                HighlightColor = Local.HighlightColor,
                Owned = Local.Owned,

                Description = Web.Description,
                Developers = Web.Developers,
                EnglishName = Web.EnglishName,
                JapaneseName = Web.JapaneseName,
                EnglishReleases = Web.EnglishReleases,
                NovelLength = Web.NovelLength,
                Publishers = Web.Publishers,
                Tags = Web.Tags,
                VndbLink = Web.VndbLink,

                HasVnInfo = true
            };
        }
    }
}
