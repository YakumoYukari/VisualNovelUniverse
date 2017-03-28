using System;
using System.Collections.Generic;
using System.Linq;
using Core.Utils;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    public static class Highlight
    {
        public static void ClearHighlighting(IEnumerable<VisualNovel> VisualNovels)
        {
            foreach (var Vn in VisualNovels)
                Vn.Highlighted = false;
        }

        public static IEnumerable<VisualNovel> SearchTerms(IEnumerable<VisualNovel> VisualNovels, string SearchTerm)
        {
            var HighlightedVns = new List<VisualNovel>();

            SearchTerm = SearchTerm.ToLower().Trim();
            var SearchTerms = StringUtils.SplitWithQuotes(SearchTerm);

            foreach (var Vn in VisualNovels.Where(V => V.HasVnInfo))
            {
                Vn.Highlighted = false;

                if (string.IsNullOrWhiteSpace(SearchTerm)) continue;

                if (Settings.Instance.HighlightTitles)
                {
                    if (SearchTerms.All(Vn.EnglishName.ToLower().Contains) || (Vn.JapaneseName != null && SearchTerms.All(Vn.JapaneseName.Contains)))
                    {
                        Vn.Highlighted = true;
                        Vn.HighlightColor = Settings.Instance.HighlightTitleMatchColor;
                        HighlightedVns.Add(Vn);
                        continue;
                    }
                }
                if (Settings.Instance.HighlightDeveloper)
                {
                    if (SearchTerms.All(Term => Vn.Developers.Any(D => D.ToLower().Contains(Term))))
                    {
                        Vn.Highlighted = true;
                        Vn.HighlightColor = Settings.Instance.HighlightDeveloperMatchColor;
                        HighlightedVns.Add(Vn);
                        continue;
                    }
                }
                if (Settings.Instance.HighlightDescription)
                {
                    if (Vn.Description == null) continue;
                    if (SearchTerms.All(StringUtils.ClearAllBracketGroups(Vn.Description).ToLower().Contains))
                    {
                        Vn.Highlighted = true;
                        Vn.HighlightColor = Settings.Instance.HighlightDescriptionMatchColor;
                        HighlightedVns.Add(Vn);
                        continue;
                    }
                }
                if (Settings.Instance.HighlightTags)
                {
                    if (SearchTerms.All(Term => Vn.Tags.Any(T => T.ToLower().Contains(Term))))
                    {
                        Vn.Highlighted = true;
                        Vn.HighlightColor = Settings.Instance.HighlightTagsMatchColor;
                        HighlightedVns.Add(Vn);
                    }
                }
            }

            return HighlightedVns;
        }

        public static void MissingVnInfo(IEnumerable<VisualNovel> VisualNovels)
        {
            foreach (var Vn in VisualNovels)
            {
                Vn.Highlighted = false;
                if (!Vn.HasVnInfo)
                {
                    Vn.Highlighted = true;
                    Vn.HighlightColor = Settings.Instance.HighlightDefaultColor;
                }
            }
        }

        public static void MissingCoverImages(IEnumerable<VisualNovel> VisualNovels)
        {
            foreach (var Vn in VisualNovels)
            {
                Vn.Highlighted = false;
                if (Vn.HasCoverImage) continue;

                Vn.Highlighted = true;
                Vn.HighlightColor = Settings.Instance.HighlightDefaultColor;
            }
        }                          

        public static void OnCriteria(IEnumerable<VisualNovel> VisualNovels, Predicate<VisualNovel> Conditions)
        {
            foreach (var Vn in VisualNovels)
            {
                Vn.Highlighted = false;
                if (Conditions(Vn))
                {
                    Vn.Highlighted = true;
                    Vn.HighlightColor = Settings.Instance.HighlightDefaultColor;
                }
            }
        }
    }
}
