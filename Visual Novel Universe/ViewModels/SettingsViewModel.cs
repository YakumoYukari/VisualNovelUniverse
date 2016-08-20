using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Caliburn.Micro;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed class SettingsViewModel : Screen
    {
        public ObservableCollection<string> FolderCleanups { get; set; } = new ObservableCollection<string>();

        private bool _ShowEnglishNameChecked;
        public bool ShowEnglishNameChecked
        {
            get { return _ShowEnglishNameChecked; }
            set
            {
                _ShowEnglishNameChecked = value;
                NotifyOfPropertyChange(() => ShowEnglishNameChecked);
            }
        }

        private bool _ShowJapaneseNameChecked;
        public bool ShowJapaneseNameChecked
        {
            get { return _ShowJapaneseNameChecked; }
            set
            {
                _ShowJapaneseNameChecked = value;
                NotifyOfPropertyChange(() => ShowJapaneseNameChecked);
            }
        }

        private bool _ShowDeveloperChecked;
        public bool ShowDeveloperChecked
        {
            get { return _ShowDeveloperChecked; }
            set
            {
                _ShowDeveloperChecked = value;
                NotifyOfPropertyChange(() => ShowDeveloperChecked);
            }
        }

        private bool _ShowLengthChecked;
        public bool ShowLengthChecked
        {
            get { return _ShowLengthChecked; }
            set
            {
                _ShowLengthChecked = value;
                NotifyOfPropertyChange(() => ShowLengthChecked);
            }
        }

        private bool _ShowAgeRatingChecked;
        public bool ShowAgeRatingChecked
        {
            get { return _ShowAgeRatingChecked; }
            set
            {
                _ShowAgeRatingChecked = value;
                NotifyOfPropertyChange(() => ShowAgeRatingChecked);
            }
        }


        private bool _SearchTitlesChecked;
        public bool SearchTitlesChecked
        {
            get { return _SearchTitlesChecked; }
            set
            {
                _SearchTitlesChecked = value;
                NotifyOfPropertyChange(() => SearchTitlesChecked);
            }
        }

        private bool _SearchDevelopersChecked;
        public bool SearchDevelopersChecked
        {
            get { return _SearchDevelopersChecked; }
            set
            {
                _SearchDevelopersChecked = value;
                NotifyOfPropertyChange(() => SearchDevelopersChecked);
            }
        }

        private bool _SearchDescriptionChecked;
        public bool SearchDescriptionChecked
        {
            get { return _SearchDescriptionChecked; }
            set
            {
                _SearchDescriptionChecked = value;
                NotifyOfPropertyChange(() => SearchDescriptionChecked);
            }
        }

        private bool _SearchTagsChecked;
        public bool SearchTagsChecked
        {
            get { return _SearchTagsChecked; }
            set
            {
                _SearchTagsChecked = value;
                NotifyOfPropertyChange(() => SearchTagsChecked);
            }
        }


        private string _HighlightDefault;
        public string HighlightDefault
        {
            get { return _HighlightDefault; }
            set
            {
                _HighlightDefault = value;
                NotifyOfPropertyChange(() => HighlightDefault);
            }
        }

        private string _HighlightTitle;
        public string HighlightTitle
        {
            get { return _HighlightTitle; }
            set
            {
                _HighlightTitle = value;
                NotifyOfPropertyChange(() => HighlightTitle);
            }
        }

        private string _HighlightDeveloper;
        public string HighlightDeveloper
        {
            get { return _HighlightDeveloper; }
            set
            {
                _HighlightDeveloper = value;
                NotifyOfPropertyChange(() => HighlightDeveloper);
            }
        }

        private string _HighlightDescription;
        public string HighlightDescription
        {
            get { return _HighlightDescription; }
            set
            {
                _HighlightDescription = value;
                NotifyOfPropertyChange(() => HighlightDescription);
            }
        }

        private string _HighlightTags;
        public string HighlightTags
        {
            get { return _HighlightTags; }
            set
            {
                _HighlightTags = value;
                NotifyOfPropertyChange(() => HighlightTags);
            }
        }


        private string _HighlightDefaultText;
        public string HighlightDefaultText
        {
            get { return _HighlightDefaultText; }
            set
            {
                if (Regex.IsMatch(value, @"#?[0-9a-fA-F]{6}"))
                    HighlightDefault = value;
                _HighlightDefaultText = value;
                NotifyOfPropertyChange(() => HighlightDefaultText);
            }
        }

        private string _HighlightTitleText;
        public string HighlightTitleText
        {
            get { return _HighlightTitleText; }
            set
            {
                if (Regex.IsMatch(value, @"#?[0-9a-fA-F]{6}"))
                    HighlightTitle = value;
                _HighlightTitleText = value;
                NotifyOfPropertyChange(() => HighlightTitleText);
            }
        }

        private string _HighlightDeveloperText;
        public string HighlightDeveloperText
        {
            get { return _HighlightDeveloperText; }
            set
            {
                if (Regex.IsMatch(value, @"#?[0-9a-fA-F]{6}"))
                    HighlightDeveloper = value;
                _HighlightDeveloperText = value;
                NotifyOfPropertyChange(() => HighlightDeveloperText);
            }
        }

        private string _HighlightDescriptionText;
        public string HighlightDescriptionText
        {
            get { return _HighlightDescriptionText; }
            set
            {
                if (Regex.IsMatch(value, @"#?[0-9a-fA-F]{6}"))
                    HighlightDescription = value;
                _HighlightDescriptionText = value;
                NotifyOfPropertyChange(() => HighlightDescriptionText);
            }
        }

        private string _HighlightTagsText;
        public string HighlightTagsText
        {
            get { return _HighlightTagsText; }
            set
            {
                if (Regex.IsMatch(value, @"#?[0-9a-fA-F]{6}"))
                    HighlightTags = value;
                _HighlightTagsText = value;
                NotifyOfPropertyChange(() => HighlightTagsText);
            }
        }


        private bool _GoToNextOverwritesChecked;
        public bool GoToNextOverwritesChecked
        {
            get { return _GoToNextOverwritesChecked; }
            set
            {
                _GoToNextOverwritesChecked = value;
                NotifyOfPropertyChange(() => GoToNextOverwritesChecked);
            }
        }

        public SettingsViewModel()
        {
            DisplayName = "Visual Novel Universe Settings";

            LoadSettings();
        }

        private void LoadSettings()
        {
            Settings.Instance.TagsToClearFromFolderNames.ForEach(FolderCleanups.Add);

            ShowEnglishNameChecked = Settings.Instance.ShowEnglishNameColumn;
            ShowJapaneseNameChecked = Settings.Instance.ShowJapaneseNameColumn;
            ShowDeveloperChecked = Settings.Instance.ShowDeveloperColumn;
            ShowLengthChecked = Settings.Instance.ShowLengthColumn;
            ShowAgeRatingChecked = Settings.Instance.ShowAgeRatingColumn;

            HighlightDefaultText = Settings.Instance.HighlightDefaultColor;
            HighlightTitleText = Settings.Instance.HighlightTitleMatchColor;
            HighlightDeveloperText = Settings.Instance.HighlightDeveloperMatchColor;
            HighlightDescriptionText = Settings.Instance.HighlightDescriptionMatchColor;
            HighlightTagsText = Settings.Instance.HighlightTagsMatchColor;

            SearchTitlesChecked = Settings.Instance.HighlightTitles;
            SearchDevelopersChecked = Settings.Instance.HighlightDeveloper;
            SearchDescriptionChecked = Settings.Instance.HighlightDescription;
            SearchTagsChecked = Settings.Instance.HighlightTags;

            GoToNextOverwritesChecked = Settings.Instance.AutoGoToNextOverwrites;
        }

        private void ApplySettings()
        {
            Settings.Instance.TagsToClearFromFolderNames = FolderCleanups.ToList();

            Settings.Instance.ShowEnglishNameColumn = ShowEnglishNameChecked;
            Settings.Instance.ShowJapaneseNameColumn = ShowJapaneseNameChecked;
            Settings.Instance.ShowDeveloperColumn = ShowDeveloperChecked;
            Settings.Instance.ShowLengthColumn = ShowLengthChecked;
            Settings.Instance.ShowAgeRatingColumn = ShowAgeRatingChecked;

            Settings.Instance.HighlightDefaultColor = HighlightDefaultText;
            Settings.Instance.HighlightTitleMatchColor = HighlightTitleText;
            Settings.Instance.HighlightDeveloperMatchColor = HighlightDeveloperText;
            Settings.Instance.HighlightDescriptionMatchColor = HighlightDescriptionText;
            Settings.Instance.HighlightTagsMatchColor = HighlightTagsText;

            Settings.Instance.HighlightTitles = SearchTitlesChecked;
            Settings.Instance.HighlightDeveloper = SearchDevelopersChecked;
            Settings.Instance.HighlightDescription = SearchDescriptionChecked;
            Settings.Instance.HighlightTags = SearchTagsChecked;

            Settings.Instance.AutoGoToNextOverwrites = GoToNextOverwritesChecked;

            Settings.Instance.Save();
        }

        public void OkayButton()
        {
            ApplySettings();
            TryClose();
        }

        public void CancelButton()
        {
            TryClose();
        }

        public void ResetButton()
        {
            var Result = MessageBox.Show("Are you sure you want to reset your configuration?", "Confirm Reset", MessageBoxButton.YesNo);

            switch (Result)
            {
                case MessageBoxResult.None:
                    return;
                case MessageBoxResult.OK:
                    return;
                case MessageBoxResult.Cancel:
                    return;
                case MessageBoxResult.Yes:
                    Settings.Instance.ResetToDefault();
                    break;
                case MessageBoxResult.No:
                    return;
                default:
                    return;
            }
        }
    }
}
