using Caliburn.Micro;

namespace Visual_Novel_Universe.Models
{
    public class SearchEntry : PropertyChangedBase
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set {
                _Name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private string _Url;
        public string Url
        {
            get { return _Url; }
            set
            {
                _Url = value;
                NotifyOfPropertyChange(() => Url);
            }
        }

        public RelayCommand<string> SearchForVnCommand { get; set; }

        public string EnglishCommandString => $"{Name.Replace("|", "")}|English|{Url}";
        public string JapaneseCommandString => $"{Name.Replace("|", "")}|Japanese|{Url}";
    }
}
