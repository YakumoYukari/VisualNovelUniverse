using System;
using System.Linq;
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

        public RelayCommand<string> BackCommand { get; set; } = new RelayCommand<string>(s =>
        {
            if (WebBrowserAccessor.WebBrowser.CanGoBack) WebBrowserAccessor.WebBrowser.GoBack();
        });
        public RelayCommand<string> ForwardCommand { get; set; } = new RelayCommand<string>(s =>
        {
            if (WebBrowserAccessor.WebBrowser.CanGoForward) WebBrowserAccessor.WebBrowser.GoForward();
        });
        public RelayCommand<string> RefreshCommand { get; set; } = new RelayCommand<string>(s =>
        {
            WebBrowserAccessor.WebBrowser.Refresh();
        });
        public RelayCommand<string> GoCommand { get; set; } = new RelayCommand<string>(s =>
        {
            if (string.IsNullOrWhiteSpace(s)) return;
            WebBrowserAccessor.Navigate(s);
        });

        public void NavigationBarKeyDown(KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            WebBrowserAccessor.Navigate(BrowserAddressBarText.GetUri());
        }

        public void OnWebBrowserNavigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.AbsoluteUri.StartsWith("vnu://")) return;

            e.Cancel = true;
            ProcessDirective(e.Url.AbsoluteUri);
        }

        public void ProcessDirective(string DirectiveUrl)
        {
            if (!DirectiveUrl.StartsWith("vnu://")) return;

            if (DirectiveUrl.StartsWith("vnu://confirmvn"))
            {
                SetVnInfo();
            }
        }

        public void OnWebBrowserNavigate(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (e.Url == null) return;

            Logger.Instance.Log($"OnWebBrowserNavigate URL: {e.Url}");
            BrowserAddressBarText = e.Url.AbsoluteUri;
        }

        public void OnWebBrowserLoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (WebBrowserAccessor.IsOnVndbPage())
            {
                Logger.Instance.Log($"Load completed on VNDB page: {e.Url}");
                ArrivedAtVndbPage(WebBrowserAccessor.WebBrowser.Url.AbsoluteUri);
            }
            else
            {
                Logger.Instance.Log($"Load completed on non-VNDB page: {e.Url}");
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
            VndbPageNovel.Owned = VisualNovels.Any(v => v.VndbLink == VndbPageNovel.VndbLink);

            if (VndbPageNovel.Owned)
            {
                WebBrowserAccessor.SetVndbTitleColor(Settings.Instance.VndbOwnedVnTitleColor, 14);
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
                        SelectedVisualNovel = ShownVisualNovels.First(vn => vn.VndbLink == Temp.VndbLink);
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
