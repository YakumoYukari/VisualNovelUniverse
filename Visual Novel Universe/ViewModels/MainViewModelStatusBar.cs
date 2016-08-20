using System.Windows.Input;
using Caliburn.Micro;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed partial class MainViewModel
    {
        private string _VndbSearchText;
        public string VndbSearchText
        {
            get { return _VndbSearchText; }
            set
            {
                _VndbSearchText = value;
                NotifyOfPropertyChange(() => VndbSearchText);
            }
        }

        private string _OwnedStatusText = Properties.Resources.StatusLabel_NoSelection;
        public string OwnedStatusText
        {
            get { return _OwnedStatusText; }
            set
            {
                _OwnedStatusText = value;
                NotifyOfPropertyChange(() => OwnedStatusText);
            }
        }

        private string _OwnedStatusLabelColor = "#FFFFFF00";
        public string OwnedStatusLabelColor
        {
            get { return _OwnedStatusLabelColor; }
            set
            {
                _OwnedStatusLabelColor = value;
                NotifyOfPropertyChange(() => OwnedStatusLabelColor);
            }
        }

        private string _CurrentVnLabel;
        public string CurrentVnLabel
        {
            get { return _CurrentVnLabel; }
            set
            {
                _CurrentVnLabel = value;
                NotifyOfPropertyChange(() => CurrentVnLabel);
            }
        }

        public void DoVndbSearch(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter && !string.IsNullOrWhiteSpace(VndbSearchText))
            {
                WebBrowserAccessor.SearchVndb(VndbSearchText);
            }
        }

        public void ClearVndbSearchButton()
        {
            VndbSearchText = "";
        }
    }
}
