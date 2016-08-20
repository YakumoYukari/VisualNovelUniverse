using Caliburn.Micro;

namespace Visual_Novel_Universe.Models
{
    public class Release : PropertyChangedBase
    {
        private string _Publisher;
        public string Publisher
        {
            get { return _Publisher; }
            set
            {
                _Publisher = value;
                NotifyOfPropertyChange(() => Publisher);
            }
        }

        private string _Link;
        public string Link
        {
            get { return _Link; }
            set
            {
                _Link = value;
                NotifyOfPropertyChange(() => Link);
            }
        }
    }
}
