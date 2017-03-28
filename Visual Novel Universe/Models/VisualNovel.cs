using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Caliburn.Micro;

namespace Visual_Novel_Universe.Models
{
    [Serializable]
    public class VisualNovel : PropertyChangedBase
    {
        private string _VnFolderPath = "";
        public string VnFolderPath
        {
            get { return _VnFolderPath; }
            set {
                _VnFolderPath = value;
                NotifyOfPropertyChange(() => VnFolderPath);
                _HasCoverImage = File.Exists(CoverImagePath);
            }
        }

        public string BaseFolderPath => string.IsNullOrWhiteSpace(VnFolderPath) ? "" : PathUtils.OneLevelUp(VnFolderPath);
        public string FolderName => string.IsNullOrWhiteSpace(VnFolderPath) ? "" : PathUtils.LastElementOfPath(VnFolderPath);

        private bool _HasVnInfo;
        public bool HasVnInfo
        {
            get { return _HasVnInfo; // && File.Exists(VnInfoFilepath);
            }
            set {
                _HasVnInfo = value;
                NotifyOfPropertyChange(() => HasVnInfo);
            }
        }

        public string VnInfoFilepath => Path.Combine(VnFolderPath, ConfigurationManager.AppSettings["VnInfoFilename"]);
        public string CoverImagePath => Path.Combine(VnFolderPath, ConfigurationManager.AppSettings["VnCoverImageFilename"]);

        private bool _HasCoverImage;
        public bool HasCoverImage => _HasCoverImage;

        private string _EnglishName = "";
        public string EnglishName
        {
            get { return _EnglishName; }
            set {
                _EnglishName = value;
                NotifyOfPropertyChange(() => EnglishName);
            }
        }

        private string _JapaneseName = "";
        public string JapaneseName
        {
            get { return _JapaneseName; }
            set {
                _JapaneseName = value;
                NotifyOfPropertyChange(() => JapaneseName);
            }
        }

        private string _Description = "";
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private List<string> _Developers;
        public List<string> Developers
        {
            get { return _Developers; }
            set {
                _Developers = value;
                NotifyOfPropertyChange(() => Developers);
                NotifyOfPropertyChange(() => DisplayDeveloper);
            }
        }

        public string DisplayDeveloper
        {
            get
            {
                if (Developers == null || Developers.Count == 0)
                {
                    return !HasVnInfo ? Properties.Resources.UnconfirmedVN : Properties.Resources.MissingDeveloperInfo;
                }
                return Developers[0] ?? Properties.Resources.MissingDeveloperInfo;
            }
        }

        private List<Publisher> _Publishers;
        public List<Publisher> Publishers
        {
            get { return _Publishers; }
            set {
                _Publishers = value;
                NotifyOfPropertyChange(() => Publishers);
            }
        }

        private List<Release> _EnglishReleases;
        public List<Release> EnglishReleases
        {
            get { return _EnglishReleases; }
            set
            {
                _EnglishReleases = value;
                NotifyOfPropertyChange(() => EnglishReleases);
            }
        }

        private List<string> _Tags;
        public List<string> Tags
        {
            get { return _Tags; }
            set {
                _Tags = value;
                NotifyOfPropertyChange(() => Tags);
            }
        }

        private Length _NovelLength;
        public Length NovelLength
        {
            get { return _NovelLength; }
            set
            {
                _NovelLength = value;
                NotifyOfPropertyChange(() => NovelLength);
            }
        }

        public string LengthString => NovelLength.ToString();

        public AgeRating SafeForWork
        {
            get
            {
                if (Tags == null) return AgeRating.AllAges;
                return Tags.Contains("Nukige", StringComparer.CurrentCultureIgnoreCase)
                    ? AgeRating.Nukige
                    : Tags.Contains("Eroge", StringComparer.CurrentCultureIgnoreCase)
                        ? AgeRating.Eroge
                        : Tags.Any(
                            S =>
                                (S.ToLowerInvariant().Contains(" sex") ||
                                S.ToLowerInvariant().Contains("sex ") ||
                                S.ToLowerInvariant().Contains("rape") ||
                                S.ToLowerInvariant().Contains("masturbat") ||
                                S.ToLowerInvariant().Contains("incest") ||
                                S.ToLowerInvariant().Contains("yaoi") ||
                                S.ToLowerInvariant().Contains("yuri") ||
                                S.ToLowerInvariant().Contains("sexual content")) &&
                                S.ToLowerInvariant() != "no sexual content")
                            ? AgeRating.SexualContent
                            : AgeRating.AllAges;
            }
        }

        public string RatingString => SafeForWork.ToString();

        private string _VndbLink = "";
        public string VndbLink
        {
            get { return _VndbLink; }
            set {
                _VndbLink = value;
                NotifyOfPropertyChange(() => VndbLink);
            }
        }

        private bool _Favorited;
        public bool Favorited
        {
            get { return _Favorited; }
            set {
                _Favorited = value;
                NotifyOfPropertyChange(() => Favorited);
                NotifyOfPropertyChange(() => FavoritedString);
                NotifyOfPropertyChange(() => FavoritedColor);
            }
        }

        public string FavoritedString => Favorited ? "♥" : "";
        public string FavoritedColor => Favorited ? "DarkGoldenrod" : "GhostWhite";

        private bool _Owned;
        public bool Owned
        {
            get { return _Owned; }
            set
            {
                _Owned = value;
                NotifyOfPropertyChange(() => Owned);
            }
        }

        [NonSerialized]
        private bool _Highlighted;
        public bool Highlighted
        {
            get { return _Highlighted; }
            set
            {
                _Highlighted = value;
                NotifyOfPropertyChange(() => Highlighted);
            }
        }

        [NonSerialized]
        private string _HighlightColor = "";
        public string HighlightColor
        {
            get { return _HighlightColor; }
            set
            {
                _HighlightColor = value;
                NotifyOfPropertyChange(() => HighlightColor);
            }
        }
    }
}
