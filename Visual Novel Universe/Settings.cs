using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe
{
    [Serializable]
    public class Settings
    {
        private static Settings _Instance;
        public static Settings Instance
        {
            get
            {
                if (_Instance != null)
                {
                    return _Instance;
                }

                _Instance = Load(ConfigurationManager.AppSettings["ConfigFilepath"]);
                _Instance.TagsToClearFromFolderNames = _Instance.TagsToClearFromFolderNames.Distinct().ToList();
                if (_Instance != null)
                {
                    return _Instance;
                }

                _Instance = new Settings();
                _Instance.Save();

                return _Instance;
            }

            private set { _Instance = value; }
        }

        public List<string> WorkingDirectories { get; set; } = new List<string>();
        public List<SearchEntry> SearchOptions { get; set; } = new List<SearchEntry>();
        
        public double SpreaderDistance { get; set; } = 375d;
        
        public bool AutoGoToNextOverwrites { get; set; } = false;

        public bool ShowEnglishNameColumn { get; set; } = true;
        public bool ShowJapaneseNameColumn { get; set; } = false;
        public bool ShowDeveloperColumn { get; set; } = true;
        public bool ShowLengthColumn { get; set; } = false;
        public bool ShowAgeRatingColumn { get; set; } = false;

        public string VndbOwnedVnTitleColor = "limegreen";

        public string OwnedStatusLabelOwnedColor = "#90EE90";
        public string OwnedStatusLabelNotOwnedColor = "#FF4500";
        public string OwnedStatusLabelNoSelectionColor = "#FFFF00";

        public bool HighlightTitles = true;
        public bool HighlightDeveloper = true;
        public bool HighlightDescription = true;
        public bool HighlightTags = true;

        public string HighlightDefaultColor = "#FFFF00";
        public string HighlightTitleMatchColor = "#269900";
        public string HighlightDeveloperMatchColor = "#b300b3";
        public string HighlightDescriptionMatchColor = "#00b3b3";
        public string HighlightTagsMatchColor = "#cc5200";

        public List<string> TagsToClearFromFolderNames = new List<string> { "DL版", "パッケージ版", "初回", "認証回避", "+", "※", " Ver.", "DVD-ROM版" };

        #region IO

        public static Settings Load(string Filename)
        {
            if (!File.Exists(Filename))
            {
                return null;
            }

            try
            {
                using (var sw = new StreamReader(Filename))
                {
                    var xmls = new XmlSerializer(typeof(Settings));
                    return xmls.Deserialize(sw) as Settings;
                }
            }
            catch (InvalidOperationException Ioe)
            {
                Logger.Instance.LogError(Ioe.Message);
                Logger.Instance.LogWarning("Bad settings file.");

                var Result =
                    MessageBox.Show(
                        "An error has occurred reading the settings file. Press OK to create a new one or cancel to exit.",
                        "Config File Read Error", MessageBoxButton.OKCancel);

                switch (Result)
                {
                    case MessageBoxResult.None:
                        Environment.Exit(1);
                        break;
                    case MessageBoxResult.OK:
                        Logger.Instance.LogWarning("Bad settings file: Okay clicked.");
                        return null;
                    case MessageBoxResult.Cancel:
                        Environment.Exit(1);
                        break;
                    case MessageBoxResult.Yes:
                        Environment.Exit(1);
                        break;
                    case MessageBoxResult.No:
                        Environment.Exit(1);
                        break;
                    default:
                        Environment.Exit(1);
                        break;
                }
            }
            return null;
        }

        public void Save()
        {
            using (var sw = new StreamWriter(ConfigurationManager.AppSettings["ConfigFilepath"]))
            {
                var xmls = new XmlSerializer(typeof(Settings));
                xmls.Serialize(sw, this);
            }
        }

        internal void ResetToDefault()
        {
            Logger.Instance.Log("Resetting Settings.");

            var OldWorkingDirectories = WorkingDirectories;
            var OldSearchOptions = SearchOptions;

            Instance = new Settings
            {
                WorkingDirectories = OldWorkingDirectories,
                SearchOptions = OldSearchOptions
            };
            Instance.Save();
        }

        #endregion
    }
}
