using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Visual_Novel_Universe.Models;
using System.IO;
using System.Windows.Markup;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed partial class MainViewModel : Screen, IHandle<CoverImageClickedMessage>
    {
        public ObservableCollection<VisualNovel> ShownVisualNovels { get; set; } = new ObservableCollection<VisualNovel>();
        public ObservableCollection<VisualNovel> VisualNovels { get; set; } = new ObservableCollection<VisualNovel>();
        public ObservableCollection<SearchEntry> EnglishSearchEntries { get; set; } = new ObservableCollection<SearchEntry>();
        public ObservableCollection<SearchEntry> JapaneseSearchEntries { get; set; } = new ObservableCollection<SearchEntry>();

        private VisualNovel _SelectedVisualNovel;
        public VisualNovel SelectedVisualNovel
        {
            get { return _SelectedVisualNovel; }
            set
            {
                SelectedVisualNovelChanged(value);
                _SelectedVisualNovel = value;
                NotifyOfPropertyChange(() => SelectedVisualNovel);
            }
        }

        private int _SelectedVisualNovelIndex;
        public int SelectedVisualNovelIndex
        {
            get { return _SelectedVisualNovelIndex; }
            set
            {
                if (value > ShownVisualNovels.Count - 1)
                {
                    _SelectedVisualNovelIndex = ShownVisualNovels.Count - 1;
                    if (AutoGoToNextOption)
                    {
                        AutoGoToNextOption = false;
                        LoadVnList();
                    }
                }
                else if (value < 0)
                {
                    _SelectedVisualNovelIndex = -1;
                }
                else
                {
                    _SelectedVisualNovelIndex = value;
                }
                NotifyOfPropertyChange(() => SelectedVisualNovelIndex);
            }
        }

        private VisualNovel _VndbPageNovel;
        public VisualNovel VndbPageNovel
        {
            get { return _VndbPageNovel; }
            set
            {
                _VndbPageNovel = value;
                _VndbPageNovel.Owned = VisualNovels.Any(v => v.VndbLink == _VndbPageNovel.VndbLink);
                if (_VndbPageNovel.Owned)
                {
                    OwnedStatusText = Properties.Resources.StatusLabel_Owned;
                    OwnedStatusLabelColor = Settings.Instance.OwnedStatusLabelOwnedColor;
                }
                else
                {
                    OwnedStatusText = Properties.Resources.StatusLabel_NotOwned;
                    OwnedStatusLabelColor = Settings.Instance.OwnedStatusLabelNotOwnedColor;
                }
                CurrentVnLabel = $"{_VndbPageNovel.EnglishName} / {_VndbPageNovel.JapaneseName}";
                NotifyOfPropertyChange(() => VndbPageNovel);
            }
        }

        public MainViewModel()
        {
            DisplayName = "Visual Novel Universe";

            Events.Aggregator.Subscribe(this);
            Settings.Instance.SearchOptions.ForEach((s) =>
            {
                s.SearchForVnCommand = new RelayCommand<string>(SearchForVn);
            });
            Settings.Instance.SearchOptions.ForEach(EnglishSearchEntries.Add);
            Settings.Instance.SearchOptions.ForEach(JapaneseSearchEntries.Add);

            try
            {
                Logger.Instance.Log("Starting InitVnList");
                InitVnList();
                Logger.Instance.Log("Starting InitMenuBar");
                InitMenuBar();
                Logger.Instance.Log("Starting ReloadVnListColumns");
                ReloadVnListColumns();
                Logger.Instance.Log("Starting LoadVnList");
                LoadVnList();
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error initializing MainViewModel: {e.Message}\n{e.StackTrace}");
            }
        }

        private void SelectedVisualNovelChanged(VisualNovel NewSelection)
        {
            if (SelectedVisualNovel == null) return;
            if (SelectedVisualNovel == NewSelection) return;
            Logger.Instance.Log($"Selected visual novel changed to: {SelectedVisualNovel.FolderName}");

            if (SelectedVisualNovel.HasVnInfo && !string.IsNullOrWhiteSpace(SelectedVisualNovel.VndbLink))
            {
                Logger.Instance.Log($"Going to known address: {SelectedVisualNovel.VndbLink}");
                WebBrowserAccessor.Navigate(SelectedVisualNovel.VndbLink);
            }
            else
            {
                Logger.Instance.Log($"Searching VNDB for {SelectedVisualNovel.FolderName}");
                LookingForVndbEntry = true;
                WebBrowserAccessor.SearchVndb(SelectedVisualNovel.FolderName);
            }
        }

        private void LoadVnList()
        {
            Logger.Instance.Log("Starting LoadVnList.");

            ShownVisualNovels.Clear();
            VisualNovels.Clear();

            var SortingList = new List<VisualNovel>();

            try
            {
                foreach (string BaseDirectory in Settings.Instance.WorkingDirectories.Where(Directory.Exists))
                {
                    foreach (string FolderName in Directory.GetDirectories(BaseDirectory))
                    {
                        if (File.Exists(Path.Combine(FolderName, "VN Info.xml")))
                        {
                            SortingList.Add(LoadVisualNovel(FolderName, false));
                        }
                        else if (File.Exists(Path.Combine(FolderName, "VN Info.txt")))
                        {
                            SortingList.Add(LoadOldVisualNovel(FolderName));
                        }
                        else
                        {
                            SortingList.Add(LoadVnWithNoInfo(FolderName));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error in LoadVnList: {e.Message}\n{e.StackTrace}");
            }


            SortingList = SortingList.OrderBy(v => v.EnglishName).ToList();
            SortingList.ForEach(VisualNovels.Add);
            SortingList.ForEach(ShownVisualNovels.Add);

            DisplayName = $"Visual Novel Universe :: {VisualNovels.Count} VNs";
            VnListSearchBoxText = VnListSearchBoxText;

            Logger.Instance.Log("Ending LoadVnList.");
        }

        private static VisualNovel LoadVisualNovel(string FolderName, bool ForceSave)
        {
            var Vn = VisualNovelLoader.Load(FolderName);
            Vn.HasVnInfo = true;
            if (ForceSave)
            {
                VisualNovelLoader.Save(Vn, FolderName);
            }
            return Vn;
        }
        private static VisualNovel LoadOldVisualNovel(string FolderName)
        {
            var Vn = LegacyVnLoader.Load(FolderName);
            VisualNovelLoader.Save(Vn, FolderName);
            return Vn;
        }
        private static VisualNovel LoadVnWithNoInfo(string FolderName)
        {
            return new VisualNovel
            {
                EnglishName = PathUtils.LastElementOfPath(FolderName),
                JapaneseName = PathUtils.LastElementOfPath(FolderName),
                VnFolderPath = FolderName,
                Developers = new List<string>(),
                HasVnInfo = false,
                Owned = true
            };
        }
    }
}
