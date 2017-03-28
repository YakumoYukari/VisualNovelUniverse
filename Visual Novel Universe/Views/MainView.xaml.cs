using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Visual_Novel_Universe.ThirdParty;
using Visual_Novel_Universe.ViewModels;

namespace Visual_Novel_Universe.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private MainViewModel _ViewModel;

        public MainView()
        {
            BrowserConfiguration.SetBrowserFeatureControl();
            InitializeComponent();
            WebBrowserAccessor.WebBrowser = VnBrowser;
            VnBrowser.ScriptErrorsSuppressed = true;
        }

        public void VnBrowserNavigated(object Sender, WebBrowserNavigatedEventArgs EventArgs)
        {
            _ViewModel?.OnWebBrowserNavigate(Sender, EventArgs);
        }

        public void VnBrowserLoadCompleted(object Sender, WebBrowserDocumentCompletedEventArgs EventArgs)
        {
            _ViewModel?.OnWebBrowserLoadCompleted(Sender, EventArgs);
        }

        private void OnLoaded(object Sender, RoutedEventArgs E)
        {
            _ViewModel = (MainViewModel)DataContext;
        }

        private void VnListMouseDoubleClick(object Sender, MouseButtonEventArgs EventArgs)
        {
            _ViewModel?.VnListMouseDoubleClick();
        }

        private void VnBrowser_OnNavigating(object Sender, WebBrowserNavigatingEventArgs EventArgs)
        {
            _ViewModel?.OnWebBrowserNavigating(Sender, EventArgs);
        }
    }
}
