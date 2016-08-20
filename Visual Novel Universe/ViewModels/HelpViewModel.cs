using Caliburn.Micro;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed class HelpViewModel : Screen
    {
        private string _HelpText;
        public string HelpText
        {
            get { return _HelpText; }
            set
            {
                _HelpText = value;
                NotifyOfPropertyChange(() => HelpText);
            }
        }

        public HelpViewModel()
        {
            DisplayName = "Help";
            HelpText = Properties.Resources.HelpText.Replace("\\n", "\n");
        }
    }
}
