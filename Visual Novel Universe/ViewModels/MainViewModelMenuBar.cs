﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
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
            ChooseDirectoryCommand = new RelayCommand<string>(S => ChooseDirectory());
            ExportVnListCommand = new RelayCommand<string>(S => ExportVnList());
            OpenContainingFolderCommand = new RelayCommand<string>(S => OpenContainingFolder());

            //Edit
            CopyVnLinkCommand = new RelayCommand<string>(S => CopyVnLink());
            CopyVnEnglishNameCommand = new RelayCommand<string>(S => CopyVnEnglishName());
            CopyVnJapaneseNameCommand = new RelayCommand<string>(S => CopyVnJapaneseName());

            //View
            OpenCoverImageViewCommand = new RelayCommand<string>(S => OpenCoverImageView());

            //Highlight
            ClearHighlightingCommand = new RelayCommand<string>(S => ClearHighlighting());
            HighlightEnglishAvailableCommand = new RelayCommand<string>(S => HighlightEnglishAvailable());
            HighlightFavoritesCommand = new RelayCommand<string>(S => HighlightFavorites());
            HighlightMissingInfoCommand = new RelayCommand<string>(S => Highlight.MissingVnInfo(VisualNovels));
            HighlightMissingImageCommand = new RelayCommand<string>(S => Highlight.MissingCoverImages(VisualNovels));

            //Custom Search
            CustomizeSearchesCommand = new RelayCommand<string>(S => CustomizeSearches());

            //Data
            RefreshVnListCommand = new RelayCommand<string>(S => LoadVnList());
            SetVnInfoCommand = new RelayCommand<string>(S => SetVnInfo());
            SaveCoverImageCommand = new RelayCommand<string>(S => SaveCoverImage());
            FavoriteCommand = new RelayCommand<string>(S => Favorite());

            //Tools
            ImportVnsCommand = new RelayCommand<string>(S => ImportVns());
            MassRenameToEnglishCommand = new RelayCommand<string>(S => MassRenameToEnglish());
            MassRenameToJapaneseCommand = new RelayCommand<string>(S => MassRenameToJapanese());
            LookupJapaneseTitleCommand = new RelayCommand<string>(S => LookupJapaneseTitle());
            AutoGoToNextCommand = new RelayCommand<string>(S => AutoGoToNext());
            OpenSettingsMenuCommand = new RelayCommand<string>(S => OpenSettingsMenu());

            //Help
            GetHelpCommand = new RelayCommand<string>(S => GetHelp());
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

            VnListSearchBoxText = "";

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
            try
            {
                var Manager = new WindowManager();
                var CoverImageViewModel = new CoverImageGridViewModel {MaxDisplayableRows = 16};

                int CoverImagesAdded = 0;
                foreach (var Vn in VisualNovels.Where(V => V.HasCoverImage))
                {
                    CoverImageViewModel.AddCover(Vn);
                    CoverImagesAdded++;
                }
                Logger.Instance.Log($"Opening cover grid with {CoverImagesAdded} VNs");

                Manager.ShowWindow(CoverImageViewModel);
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error opening cover image view: {e.Message}\n{e.StackTrace}");
            }
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
        public RelayCommand<string> HighlightMissingInfoCommand { get; set; }
        public RelayCommand<string> HighlightMissingImageCommand { get; set; }

        private void ClearHighlighting()
        {
            Highlight.ClearHighlighting(VisualNovels);
            HighlightEnglishAvailableChecked = false;
            HighlightFavoritesChecked = false;
            VnListSearchBoxText = "";
        }
        
        private void HighlightEnglishAvailableMenu()
        {
            HighlightEnglishAvailableChecked = true;
            Highlight.OnCriteria(VisualNovels, VN => VN.EnglishReleases?.Any() ?? false);
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
            Searches.ForEach(S => S.SearchForVnCommand = new RelayCommand<string>(SearchForVn));

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

            try
            {
                var Temp = SelectedVisualNovel;
                LoadVnList();
                SelectedVisualNovel = ShownVisualNovels.First(VN => VN.VndbLink == Temp.VndbLink);
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error in SetVnInfo: {e.Message}\n{e.StackTrace}");
                SelectedVisualNovel = ShownVisualNovels.FirstOrDefault();
            }
        }
        private void SaveCoverImage()
        {
            if (SelectedVisualNovel == null) return;
            if (!WebBrowserAccessor.IsOnVndbPage()) return;
            Logger.Instance.Log($"Saving cover image from {WebBrowserAccessor.WebBrowser.Url}.");

            try
            {
                var Image = VndbExtractor.ExtractCoverImage(WebBrowserAccessor.Html);
                Image.Save(SelectedVisualNovel.CoverImagePath);
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error saving cover image: {e.Message}\n{e.StackTrace}");
            }
            
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
            try
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
            catch (Exception e)
            {
                Logger.Instance.LogError($"Import error: {e.Message}\n{e.StackTrace}");
            }
        }
        private void MassRenameToEnglish()
        {
            try
            {
                Logger.Instance.Log("Mass renaming to English.");
                FolderTools.RenameAllFolders(VisualNovels, Language.English);
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error renaming all to English: {e.Message}\n{e.StackTrace}");
            }
        }
        private void MassRenameToJapanese()
        {
            try
            {
                Logger.Instance.Log("Mass renaming to Japanese.");
                FolderTools.RenameAllFolders(VisualNovels, Language.Japanese);
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error renaming all to Japanese: {e.Message}\n{e.StackTrace}");
            }
        }
        private void LookupJapaneseTitle()
        {
            if (string.IsNullOrWhiteSpace(VndbPageNovel?.JapaneseName)) return;
            WebBrowserAccessor.Navigate($@"http://jisho.org/search/{HttpUtility.UrlEncode(VndbPageNovel.JapaneseName)}".GetUri());
        }
        private void AutoGoToNext()
        {
            AutoGoToNextOption = !AutoGoToNextOption;
            if (AutoGoToNextOption)
            {
                Logger.Instance.Log("Starting AutoGoToNext");

                VnListSearchBoxText = "";

                ShownVisualNovels.Clear();
                VisualNovels.ToList().ForEach(ShownVisualNovels.Add);

                AutoGoToNextOption = true;

                if (SelectedVisualNovel != null && SelectedVisualNovel.HasVnInfo)
                {
                    WebBrowserAccessor.Navigate(SelectedVisualNovel.VndbLink);
                }
                else
                {
                    SelectedVisualNovel = ShownVisualNovels.First();
                }
            }
            else
            {
                Logger.Instance.Log("Stopping AutoGoToNext (command)");
            }
        }
        private void OpenSettingsMenu()
        {
            try
            {
                Logger.Instance.Log("Opening settings.");
                var ViewModel = new SettingsViewModel();

                var Manager = new WindowManager();
                Manager.ShowDialog(ViewModel);

                ReloadVnListColumns();
                RefreshWindowSizes();
                VnListSearchBoxText = VnListSearchBoxText;
            }
            catch (Exception e)
            {
                Logger.Instance.LogError($"Error in OpenSettingsMenu: {e.Message}\n{e.StackTrace}");
            }
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
