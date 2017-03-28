using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed partial class MainViewModel
    {
        private bool _AutoGoToNextOption;
        public bool AutoGoToNextOption
        {
            get { return _AutoGoToNextOption; }
            set
            {
                _AutoGoToNextOption = value;
                NotifyOfPropertyChange(() => AutoGoToNextOption);
            }
        }

        private string _BrowserAddressBarText;
        public string BrowserAddressBarText
        {
            get { return _BrowserAddressBarText; }
            set
            {
                _BrowserAddressBarText = value;
                NotifyOfPropertyChange(() => BrowserAddressBarText);
            }
        }

        private bool _LookingForVndbEntry;
        public bool LookingForVndbEntry
        {
            get { return _LookingForVndbEntry; }
            set
            {
                _LookingForVndbEntry = value;
                NotifyOfPropertyChange(() => LookingForVndbEntry);
            }
        }

        public RelayCommand<string> BackCommand { get; set; } = new RelayCommand<string>(S =>
        {
            if (WebBrowserAccessor.WebBrowser.CanGoBack) WebBrowserAccessor.WebBrowser.GoBack();
        });
        public RelayCommand<string> ForwardCommand { get; set; } = new RelayCommand<string>(S =>
        {
            if (WebBrowserAccessor.WebBrowser.CanGoForward) WebBrowserAccessor.WebBrowser.GoForward();
        });
        public RelayCommand<string> RefreshCommand { get; set; } = new RelayCommand<string>(S =>
        {
            WebBrowserAccessor.WebBrowser.Refresh();
        });
        public RelayCommand<string> GoCommand { get; set; } = new RelayCommand<string>(S =>
        {
            if (string.IsNullOrWhiteSpace(S)) return;
            WebBrowserAccessor.Navigate(S);
        });

        public void NavigationBarKeyDown(KeyEventArgs E)
        {
            if (E.Key != Key.Enter) return;
            WebBrowserAccessor.Navigate(BrowserAddressBarText.GetUri());
        }

        public void OnWebBrowserNavigating(object Sender, WebBrowserNavigatingEventArgs E)
        {
            if (E.Url == null) return;

            string Url = E.Url.AbsoluteUri;
            if (Url.Contains("nyaa.se/?page=download&tid="))
            {
                E.Cancel = true;

                const string TORRENT_FILE_NAME = "DownloadedTorrent.torrent";

                new WebClient().DownloadFile(Url, TORRENT_FILE_NAME);
                Process.Start(TORRENT_FILE_NAME);
            }
            if (Url.StartsWith("vnu://"))
            {
                E.Cancel = true;
                ProcessDirective(Url);
            }
        }

        public void ProcessDirective(string DirectiveUrl)
        {
            if (!DirectiveUrl.StartsWith("vnu://")) return;

            if (DirectiveUrl.StartsWith("vnu://confirmvn"))
            {
                SetVnInfo();
            }
        }

        public void OnWebBrowserNavigate(object Sender, WebBrowserNavigatedEventArgs EventArgs)
        {
            if (EventArgs.Url == null) return;
            
            Logger.Instance.Log($"OnWebBrowserNavigate URL: {EventArgs.Url.AbsoluteUri}");
            BrowserAddressBarText = EventArgs.Url.AbsoluteUri;
        }

        public void OnWebBrowserLoadCompleted(object Sender, WebBrowserDocumentCompletedEventArgs EventArgs)
        {
            if (WebBrowserAccessor.IsOnVndbPage())
            {
                Logger.Instance.Log($"Load completed on VNDB page: {EventArgs.Url}");
                ArrivedAtVndbPage(WebBrowserAccessor.WebBrowser.Url.AbsoluteUri);
            }
            else
            {
                Logger.Instance.Log($"Load completed on non-VNDB page: {EventArgs.Url}");
                if (LookingForVndbEntry)
                {
                    WebBrowserAccessor.ShowConfirmationNeededMessage();
                    LookingForVndbEntry = false;
                }
                if (AutoGoToNextOption)
                {
                    SelectedVisualNovelIndex++;
                }
            }

            WebBrowserAccessor.HighlightOwnedVnsOnPage(VisualNovels, "yellow");
            WebBrowserAccessor.SetScrollBarCss();
        }

        private void ArrivedAtVndbPage(string Url)
        {
            VndbPageNovel = VndbExtractor.ExtractVisualNovel(Url, WebBrowserAccessor.Html);
            VndbPageNovel.Owned = VisualNovels.Any(V => V.VndbLink == VndbPageNovel.VndbLink);

            WebBrowserAccessor.AppendTabsToVnPage();

            if (VndbPageNovel.Owned)
            {
                WebBrowserAccessor.SetVndbTitleColor(Settings.Instance.VndbOwnedVnTitleColor, 14);
                if (ShownVisualNovels.Any(Vn => Vn.VndbLink == VndbPageNovel.VndbLink))
                    SelectedVisualNovel = ShownVisualNovels.First(Vn => Vn.VndbLink == VndbPageNovel.VndbLink);
            }

            if (SelectedVisualNovel != null && !SelectedVisualNovel.HasVnInfo)
            {
                WebBrowserAccessor.AppendConfirmVnDirective();
            }

            if ((AutoGoToNextOption && Settings.Instance.AutoGoToNextOverwrites) || (SelectedVisualNovel != null && !SelectedVisualNovel.HasVnInfo && LookingForVndbEntry))
            {
                SelectedVisualNovel = VisualNovelMerger.MergeLocalAndWeb(SelectedVisualNovel, VndbPageNovel);
                VisualNovelLoader.Save(SelectedVisualNovel);
                SaveCoverImage();
                
                if (!AutoGoToNextOption)
                {
                    try
                    {
                        var Temp = SelectedVisualNovel;
                        LoadVnList();
                        SelectedVisualNovel = ShownVisualNovels.First(VN => VN.VndbLink == Temp.VndbLink);
                        WebBrowserAccessor.WebBrowser.Refresh();
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.LogError($"Error selecting newly saved VN: {e.Message}\n{e.StackTrace}");
                    }
                }
            }

            if(AutoGoToNextOption)
            {
                SelectedVisualNovelIndex++;
            }
        }
    }
}
