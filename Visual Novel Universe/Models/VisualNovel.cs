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
            }
        }

        public string BaseFolderPath => string.IsNullOrWhiteSpace(VnFolderPath) ? "" : PathUtils.OneLevelUp(VnFolderPath);
        public string FolderName => string.IsNullOrWhiteSpace(VnFolderPath) ? "" : PathUtils.LastElementOfPath(VnFolderPath);

        private bool _HasVnInfo;
        public bool HasVnInfo
        {
            get
            {
                return File.Exists(VnInfoFilepath) && _HasVnInfo;
            }
            set {
                _HasVnInfo = value;
                NotifyOfPropertyChange(() => HasVnInfo);
            }
        }

        public string VnInfoFilepath => Path.Combine(VnFolderPath, ConfigurationManager.AppSettings["VnInfoFilename"]);
        public string CoverImagePath => Path.Combine(VnFolderPath, ConfigurationManager.AppSettings["VnCoverImageFilename"]);
        public bool HasCoverImage => File.Exists(CoverImagePath);

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
            =>
                Developers == null || Developers.Count == 0
                    ? Properties.Resources.MissingDeveloperInfo
                    : Developers[0] ?? Properties.Resources.MissingDeveloperInfo;

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
                            s =>
                                (s.ToLowerInvariant().Contains(" sex") ||
                                s.ToLowerInvariant().Contains("sex ") ||
                                s.ToLowerInvariant().Contains("rape") ||
                                s.ToLowerInvariant().Contains("masturbat") ||
                                s.ToLowerInvariant().Contains("incest") ||
                                s.ToLowerInvariant().Contains("yaoi") ||
                                s.ToLowerInvariant().Contains("yuri") ||
                                s.ToLowerInvariant().Contains("sexual content")) &&
                                s.ToLowerInvariant() != "no sexual content")
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
            }
        }

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
