using System.Collections.ObjectModel;
using Caliburn.Micro;
using Core.Utils;
using Visual_Novel_Universe.Models;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed class SearchCustomizerViewModel : Screen
    {
        public ObservableCollection<SearchEntry> SearchEntries { get; set; } = new ObservableCollection<SearchEntry>();

        private SearchEntry _SelectedItem;
        public SearchEntry SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                if (_SelectedItem != null)
                {
                    NameText = SelectedItem.Name;
                    UrlText = SelectedItem.Url;
                }
                else
                {
                    NameText = "";
                    UrlText = "";
                }
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        private string _NameText;
        public string NameText
        {
            get { return _NameText; }
            set
            {
                _NameText = StringUtils.TruncateString(value, 30);
                NotifyOfPropertyChange(() => NameText);
            }
        }

        private string _UrlText;
        public string UrlText
        {
            get { return _UrlText; }
            set
            {
                _UrlText = value;
                NotifyOfPropertyChange(() => UrlText);
            }
        }

        public SearchCustomizerViewModel()
        {
            DisplayName = "Search Customizer";
        }

        public void Add()
        {
            if (string.IsNullOrWhiteSpace(NameText) || string.IsNullOrWhiteSpace(UrlText)) return;

            SearchEntries.Add(new SearchEntry {Name = NameText, Url = UrlText});
            NameText = "";
            UrlText = "";
        }

        public void Update()
        {
            SelectedItem.Name = NameText;
            SelectedItem.Url = UrlText;
        }

        public void Remove()
        {
            if (SelectedItem == null) return;

            SearchEntries.RemoveAt(SearchEntries.IndexOf(SelectedItem));
        }

        public void Apply()
        {
            TryClose();
        }

    }
}
