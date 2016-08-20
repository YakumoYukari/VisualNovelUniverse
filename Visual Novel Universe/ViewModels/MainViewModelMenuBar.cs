using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed partial class MainViewModel
    {
        public void InitMenuBar()
        {
            //File
            ChooseDirectoryCommand = new RelayCommand<string>(s => ChooseDirectory());
            ExportVnListCommand = new RelayCommand<string>(s => ExportVnList());
            OpenContainingFolderCommand = new RelayCommand<string>(s => OpenContainingFolder());

            //Edit
            CopyVnLinkCommand = new RelayCommand<string>(s => CopyVnLink());
            CopyVnEnglishNameCommand = new RelayCommand<string>(s => CopyVnEnglishName());
            CopyVnJapaneseNameCommand = new RelayCommand<string>(s => CopyVnJapaneseName());

            //View
            OpenCoverImageViewCommand = new RelayCommand<string>(s => OpenCoverImageView());

            //Highlight
            ClearHighlightingCommand = new RelayCommand<string>(s => ClearHighlighting());
            HighlightEnglishAvailableCommand = new RelayCommand<string>(s => HighlightEnglishAvailable());
            HighlightEnglishAvailableMenuCommand = new RelayCommand<string>(s => HighlightEnglishAvailableMenu());
            HighlightFavoritesCommand = new RelayCommand<string>(s => HighlightFavorites());
            HighlightFavoritesMenuCommand = new RelayCommand<string>(s => HighlightFavoritesMenu());
            HighlightMissingInfoCommand = new RelayCommand<string>(s => Highlight.MissingVnInfo(VisualNovels));
            HighlightMissingImageCommand = new RelayCommand<string>(s => Highlight.MissingCoverImages(VisualNovels));

            //Custom Search
            CustomizeSearchesCommand = new RelayCommand<string>(s => CustomizeSearches());

            //Data
            RefreshVnListCommand = new RelayCommand<string>(s => LoadVnList());
            SetVnInfoCommand = new RelayCommand<string>(s => SetVnInfo());
            SaveCoverImageCommand = new RelayCommand<string>(s => SaveCoverImage());
            FavoriteCommand = new RelayCommand<string>(s => Favorite());

            //Tools
            ImportVnsCommand = new RelayCommand<string>(s => ImportVns());
            MassRenameToEnglishCommand = new RelayCommand<string>(s => MassRenameToEnglish());
            MassRenameToJapaneseCommand = new RelayCommand<string>(s => MassRenameToJapanese());
            LookupJapaneseTitleCommand = new RelayCommand<string>(s => LookupJapaneseTitle());
            AutoGoToNextCommand = new RelayCommand<string>(s => AutoGoToNext());
            OpenSettingsMenuCommand = new RelayCommand<string>(s => OpenSettingsMenu());

            //Help
            GetHelpCommand = new RelayCommand<string>(s => GetHelp());
        }

        #region File

        public RelayCommand<string> ChooseDirectoryCommand { get; set; }
        public RelayCommand<string> ExportVnListCommand { get; set; }
        public RelayCommand<string> OpenContainingFolderCommand { get; set; }

        private void ChooseDirectory()
        {
            Logger.Instance.Log("Opening directories window.");

            var WindowManager = new WindowManager();
            var ViewModel = new DirectoriesViewModel();
            Settings.Instance.WorkingDirectories.ForEach(ViewModel.ActiveDirectories.Add);

            WindowManager.ShowDialog(ViewModel);

            Settings.Instance.WorkingDirectories = ViewModel.ActiveDirectories.ToList();
            Settings.Instance.Save();

            LoadVnList();
        }
        private void ExportVnList()
        {
            var Dialog = new CommonSaveFileDialog
            {
                Title = "Save Visual Novel List",
                AddToMostRecentlyUsedList = false,
                DefaultDirectory = @"C:\",
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                ShowPlacesList = true
            };

            Dialog.Filters.Add(new CommonFileDialogFilter("Text Files", "*.txt"));
            Dialog.Filters.Add(new CommonFileDialogFilter("All Files", "*.*"));

            if (Dialog.ShowDialog() != CommonFileDialogResult.Ok) return;

            string ChosenFilename = Dialog.FileName;
            if (!PathUtils.LastElementOfPath(ChosenFilename).Contains("."))
                ChosenFilename += ".txt";

            string Output = VisualNovels
                .Where(Vn => Vn.Owned && Vn.HasVnInfo)
                .Aggregate("",
                    (Current, Vn) =>
                        Current + Vn.EnglishName +
                        (!string.IsNullOrWhiteSpace(Vn.JapaneseName) ? $" [{Vn.JapaneseName}]" : "") + $" [{Vn.VndbLink}]" + Environment.NewLine);

            Logger.Instance.Log($"Exporting list of {VisualNovels.Count} VNs");
            File.WriteAllText(ChosenFilename, Output);
        }
        private void OpenContainingFolder()
        {
            if (string.IsNullOrWhiteSpace(SelectedVisualNovel?.VnFolderPath)) return;

            try
            {
                Logger.Instance.Log(
                    $"Opening directory for {SelectedVisualNovel.EnglishName}: [{SelectedVisualNovel.VnFolderPath}]");
                Process.Start(SelectedVisualNovel.VnFolderPath);
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Process.Start error opening folder: {SelectedVisualNovel.VnFolderPath}; {e.Message}");
            }
        }

        #endregion

        #region Edit
        
        public RelayCommand<string> CopyVnLinkCommand { get; set; }
        public RelayCommand<string> CopyVnEnglishNameCommand { get; set; }
        public RelayCommand<string> CopyVnJapaneseNameCommand { get; set; }

        private void CopyVnLink()
        {
            Clipboard.SetText(VndbPageNovel.VndbLink);
        }
        private void CopyVnEnglishName()
        {
            Clipboard.SetText(VndbPageNovel.EnglishName);
        }
        private void CopyVnJapaneseName()
        {
            Clipboard.SetText(VndbPageNovel.JapaneseName);
        }

        #endregion

        #region View

        public RelayCommand<string> OpenCoverImageViewCommand { get; set; }

        private void OpenCoverImageView()
        {
            var Manager = new WindowManager();
            var CoverImageViewModel = new CoverImageGridViewModel {MaxDisplayableRows = 10};

            int CoverImagesAdded = 0;
            foreach (var Vn in VisualNovels.Where(v => v.HasCoverImage))
            {
                CoverImageViewModel.AddCover(Vn);
                CoverImagesAdded++;
            }
            Logger.Instance.Log($"Opening cover grid with {CoverImagesAdded} VNs");

            Manager.ShowWindow(CoverImageViewModel);
        }
        public void Handle(CoverImageClickedMessage Message)
        {
            Logger.Instance.Log($"Cover Image Clicked: {Message.ClickedVisualNovel.VnFolderPath}");
            SelectedVisualNovel = Message.ClickedVisualNovel;
        }

        #endregion

        #region Highlighting

        public RelayCommand<string> ClearHighlightingCommand { get; set; }
        public RelayCommand<string> HighlightEnglishAvailableCommand { get; set; }
        public RelayCommand<string> HighlightFavoritesCommand { get; set; }
        public RelayCommand<string> HighlightEnglishAvailableMenuCommand { get; set; }
        public RelayCommand<string> HighlightFavoritesMenuCommand { get; set; }
        public RelayCommand<string> HighlightMissingInfoCommand { get; set; }
        public RelayCommand<string> HighlightMissingImageCommand { get; set; }

        private void ClearHighlighting()
        {
            Highlight.ClearHighlighting(VisualNovels);
            HighlightEnglishAvailableChecked = false;
            HighlightFavoritesChecked = false;
            VnListSearchBoxText = "";
        }
        private void HighlightEnglishAvailable()
        {
            if (HighlightEnglishAvailableChecked)
            {
                Highlight.OnCriteria(VisualNovels, vn => vn.EnglishReleases.Any());
            }
            else
            {
                Highlight.ClearHighlighting(VisualNovels);
            }
        }
        private void HighlightEnglishAvailableMenu()
        {
            HighlightEnglishAvailableChecked = true;
            Highlight.OnCriteria(VisualNovels, vn => vn.EnglishReleases.Any());
        }
        private void HighlightFavorites()
        {
            if (HighlightFavoritesChecked)
            {
                Highlight.OnCriteria(VisualNovels, vn => vn.Favorited);
            }
            else
            {
                Highlight.ClearHighlighting(VisualNovels);
            }
        }
        private void HighlightFavoritesMenu()
        {
            HighlightFavoritesChecked = true;
            Highlight.OnCriteria(VisualNovels, vn => vn.Favorited);
        }

        #endregion

        #region Eng/Jpn Searches

        public RelayCommand<string> SearchForVnCommand { get; set; }

        public void SearchForVn(string SearchArgs)
        {
            if (VndbPageNovel == null) return;

            var Args = SearchArgs.Split('|');
            if (Args.Length != 3)
            {
                Logger.Instance.LogError($"SearchArgs not in 3 parts: [{SearchArgs}]");
                return;
            }

            switch (Args[1])
            {
                case "English":
                    WebBrowserAccessor.Navigate(string.Format(Args[2], VndbPageNovel.EnglishName).GetUri());
                    break;
                case "Japanese":
                    WebBrowserAccessor.Navigate(string.Format(Args[2], VndbPageNovel.JapaneseName ?? "").GetUri());
                    break;
                default:
                    Logger.Instance.LogError($"SearchArgs badly formatted: [{SearchArgs}]");
                    break;
            }
        }

        #endregion

        #region Custom Search

        public RelayCommand<string> CustomizeSearchesCommand { get; set; }

        private void CustomizeSearches()
        {
            var WindowManager = new WindowManager();
            var ViewModel = new SearchCustomizerViewModel();
            Settings.Instance.SearchOptions.ForEach(ViewModel.SearchEntries.Add);

            WindowManager.ShowDialog(ViewModel);

            var Searches = ViewModel.SearchEntries.ToList();
            Searches.ForEach(s => s.SearchForVnCommand = new RelayCommand<string>(SearchForVn));

            Settings.Instance.SearchOptions = Searches;
            Settings.Instance.Save();

            EnglishSearchEntries.Clear();
            JapaneseSearchEntries.Clear();
            Settings.Instance.SearchOptions.ForEach(EnglishSearchEntries.Add);
            Settings.Instance.SearchOptions.ForEach(JapaneseSearchEntries.Add);
        }

        #endregion

        #region Data

        public RelayCommand<string> RefreshVnListCommand { get; set; }
        public RelayCommand<string> SetVnInfoCommand { get; set; }
        public RelayCommand<string> SaveCoverImageCommand { get; set; }
        public RelayCommand<string> FavoriteCommand { get; set; }

        private void SetVnInfo()
        {
            if (SelectedVisualNovel == null) return;
            if (!WebBrowserAccessor.IsOnVndbPage()) return;
            if (VndbPageNovel == null)
            {
                Logger.Instance.LogError("Attempt to set VN info: On VNDB page but VndbPageNovel is null!");
                return;
            }

            Logger.Instance.Log($"Setting VN info for: {SelectedVisualNovel.VnFolderPath} to match {VndbPageNovel.EnglishName}.");
            if (SelectedVisualNovel.HasVnInfo)
            {
                Logger.Instance.Log("SetVnInfo confirmation box shown.");
                var Response =
                    MessageBox.Show("This VN already has a populated VN Info file. Would you like to overwrite?",
                        "Overwrite Existing VN Info", MessageBoxButton.YesNo);

                if (Response == MessageBoxResult.No)
                    return;
            }
            SelectedVisualNovel = VisualNovelMerger.MergeLocalAndWeb(SelectedVisualNovel, VndbPageNovel);
            VisualNovelLoader.Save(SelectedVisualNovel);

            SaveCoverImage();

            var Temp = SelectedVisualNovel;
            LoadVnList();
            SelectedVisualNovel = ShownVisualNovels.First(vn => vn.VndbLink == Temp.VndbLink);
        }
        private void SaveCoverImage()
        {
            if (SelectedVisualNovel == null) return;
            if (!WebBrowserAccessor.IsOnVndbPage()) return;
            Logger.Instance.Log($"Saving cover image from {WebBrowserAccessor.WebBrowser.Url}.");

            var Image = VndbExtractor.ExtractCoverImage(WebBrowserAccessor.Html);
            Image.Save(SelectedVisualNovel.CoverImagePath);
        }
        private void Favorite()
        {
            if (SelectedVisualNovel == null) return;
            Logger.Instance.Log($"Favoriting {SelectedVisualNovel.EnglishName}.");
            SelectedVisualNovel.Favorited = !SelectedVisualNovel.Favorited;
            VisualNovelLoader.Save(SelectedVisualNovel);
        }

        #endregion

        #region Tools

        public RelayCommand<string> ImportVnsCommand { get; set; }
        public RelayCommand<string> MassRenameToEnglishCommand { get; set; }
        public RelayCommand<string> MassRenameToJapaneseCommand { get; set; }
        public RelayCommand<string> LookupJapaneseTitleCommand { get; set; }
        public RelayCommand<string> AutoGoToNextCommand { get; set; }
        public RelayCommand<string> OpenSettingsMenuCommand { get; set; }

        private void ImportVns()
        {
            Logger.Instance.Log("Import started.");
            foreach (string Dir in Settings.Instance.WorkingDirectories)
            {
                FolderTools.EncapsulateAllFiles(Dir);
                FolderTools.CleanAllFolderNames(Dir);
            }
            LoadVnList();
            Logger.Instance.Log($"Import command ended. {VisualNovels.Count} VNs loaded.");
        }
        private void MassRenameToEnglish()
        {
            Logger.Instance.Log("Mass renaming to English.");
            FolderTools.RenameAllFolders(VisualNovels, Language.English);
        }
        private void MassRenameToJapanese()
        {
            Logger.Instance.Log("Mass renaming to Japanese.");
            FolderTools.RenameAllFolders(VisualNovels, Language.Japanese);
        }
        private void LookupJapaneseTitle()
        {
            if (string.IsNullOrWhiteSpace(VndbPageNovel?.JapaneseName)) return;
            WebBrowserAccessor.Navigate($@"http://jisho.org/search/{VndbPageNovel.JapaneseName.SafeForUrl()}".GetUri());
        }
        private void AutoGoToNext()
        {
            AutoGoToNextOption = !AutoGoToNextOption;
            if (AutoGoToNextOption)
            {
                Logger.Instance.Log("Starting AutoGoToNext");

                ShownVisualNovels.Clear();
                VisualNovels.ToList().ForEach(ShownVisualNovels.Add);

                AutoGoToNextOption = true;
                SelectedVisualNovel = ShownVisualNovels.First();
            }
            else
            {
                Logger.Instance.Log("Stopping AutoGoToNext (command)");
            }
        }
        private void OpenSettingsMenu()
        {
            Logger.Instance.Log("Opening settings.");
            var ViewModel = new SettingsViewModel();

            var Manager = new WindowManager();
            Manager.ShowDialog(ViewModel);

            ReloadVnListColumns();
            RefreshWindowSizes();
            VnListSearchBoxText = VnListSearchBoxText;
        }

        #endregion

        #region Help

        public RelayCommand<string> GetHelpCommand { get; set; }

        private static void GetHelp()
        {
            Logger.Instance.Log("Opening help.");
            var Manager = new WindowManager();
            Manager.ShowDialog(new HelpViewModel());
        }

        #endregion
    }
}
