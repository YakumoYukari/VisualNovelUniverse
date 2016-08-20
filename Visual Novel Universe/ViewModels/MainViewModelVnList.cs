using System;
using System.Diagnostics;
using System.Linq;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed partial class MainViewModel
    {
        private bool _HighlightFavoritesChecked;
        public bool HighlightFavoritesChecked
        {
            get { return _HighlightFavoritesChecked; }
            set
            {
                _HighlightFavoritesChecked = value;
                if (_HighlightFavoritesChecked)
                {
                    HighlightEnglishAvailableChecked = false;
                }
                NotifyOfPropertyChange(() => HighlightFavoritesChecked);
            }
        }

        private bool _HighlightEnglishAvailableChecked;
        public bool HighlightEnglishAvailableChecked
        {
            get { return _HighlightEnglishAvailableChecked; }
            set
            {
                _HighlightEnglishAvailableChecked = value;
                if (_HighlightEnglishAvailableChecked)
                {
                    HighlightFavoritesChecked = false;
                }
                NotifyOfPropertyChange(() => HighlightEnglishAvailableChecked);
            }
        }

        private string _VnListSearchBoxText;

        public string VnListSearchBoxText
        {
            get { return _VnListSearchBoxText; }
            set
            {
                _VnListSearchBoxText = value;
                DoHighlighting();
                NotifyOfPropertyChange(() => VnListSearchBoxText);
            }
        }

        private bool _ShowEnglishNameColumn;
        public bool ShowEnglishNameColumn
        {
            get { return _ShowEnglishNameColumn; }
            set
            {
                _ShowEnglishNameColumn = value;
                NotifyOfPropertyChange(() => ShowEnglishNameColumn);
            }
        }

        private bool _ShowJapaneseNameColumn;
        public bool ShowJapaneseNameColumn
        {
            get { return _ShowJapaneseNameColumn; }
            set
            {
                _ShowJapaneseNameColumn = value;
                NotifyOfPropertyChange(() => ShowJapaneseNameColumn);
            }
        }

        private bool _ShowDeveloperColumn;
        public bool ShowDeveloperColumn
        {
            get { return _ShowDeveloperColumn; }
            set
            {
                _ShowDeveloperColumn = value;
                NotifyOfPropertyChange(() => ShowDeveloperColumn);
            }
        }

        private bool _ShowLengthColumn;
        public bool ShowLengthColumn
        {
            get { return _ShowLengthColumn; }
            set
            {
                _ShowLengthColumn = value;
                NotifyOfPropertyChange(() => ShowLengthColumn);
            }
        }

        private bool _ShowAgeRatingColumn;
        public bool ShowAgeRatingColumn
        {
            get { return _ShowAgeRatingColumn; }
            set
            {
                _ShowAgeRatingColumn = value;
                NotifyOfPropertyChange(() => ShowAgeRatingColumn);
            }
        }

        public void VnListMouseDoubleClick()
        {
            if (!string.IsNullOrWhiteSpace(SelectedVisualNovel?.VnFolderPath))
                Process.Start(SelectedVisualNovel.VnFolderPath);
        }

        private void DoHighlighting()
        {
            if (string.IsNullOrWhiteSpace(VnListSearchBoxText))
            {
                ShownVisualNovels.Clear();
                VisualNovels.ToList().ForEach(ShownVisualNovels.Add);
                Highlight.ClearHighlighting(ShownVisualNovels);
                Highlight.ClearHighlighting(VisualNovels);
            }
            else
            {
                var Added = Highlight.SearchTerms(VisualNovels, VnListSearchBoxText);
                ShownVisualNovels.Clear();
                Added.ToList().ForEach(ShownVisualNovels.Add);
            }
        }

        public RelayCommand<string> SelectRandomCommand { get; set; }

        public void InitVnList()
        {
            SelectRandomCommand = new RelayCommand<string>(s => SelectRandom());
        }

        public void SelectRandom()
        {
            int SelectedIndex = new Random().Next(0, VisualNovels.Count);
            SelectedVisualNovel = VisualNovels.ElementAt(SelectedIndex);
        }

        public void SelectNextVn()
        {
            if (!ShownVisualNovels.Any()) return;

            int idx = ShownVisualNovels.IndexOf(SelectedVisualNovel);
            if (idx < ShownVisualNovels.Count - 1)
            {
                SelectedVisualNovel = ShownVisualNovels.ElementAt(idx + 1);
            }
            else
            {
                AutoGoToNextOption = false;
            }
        }

        public void ClearVnSearchButton()
        {
            VnListSearchBoxText = "";
        }

        public void ReloadVnListColumns()
        {
            ShowEnglishNameColumn = Settings.Instance.ShowEnglishNameColumn;
            ShowJapaneseNameColumn = Settings.Instance.ShowJapaneseNameColumn;
            ShowDeveloperColumn = Settings.Instance.ShowDeveloperColumn;
            ShowLengthColumn = Settings.Instance.ShowLengthColumn;
            ShowAgeRatingColumn = Settings.Instance.ShowAgeRatingColumn;
        }
    }
}
