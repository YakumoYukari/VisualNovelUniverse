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
        }

        public void VnBrowserNavigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            _ViewModel?.OnWebBrowserNavigate(sender, e);
        }

        public void VnBrowserLoadCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            _ViewModel?.OnWebBrowserLoadCompleted(sender, e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _ViewModel = (MainViewModel)DataContext;
        }

        private void VnListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _ViewModel?.VnListMouseDoubleClick();
        }
    }
}
