using System;
using System.Windows;

namespace Visual_Novel_Universe.ViewModels
{
    public sealed partial class MainViewModel
    {
        public void RefreshWindowSizes()
        {
            NotifyOfPropertyChange(() => SpreaderDistance);
        }

        #region Window Sizes
        private double _MainWindowWidth = 1024;
        public double MainWindowWidth
        {
            get { return _MainWindowWidth; }
            set
            {
                _MainWindowWidth = value;
                NotifyOfPropertyChange(() => MainWindowWidth);
                NotifyOfPropertyChange(() => SpreaderDistance);
            }
        }
        
        public GridLength SpreaderDistance
        {
            get { return new GridLength(Math.Min(Settings.Instance.SpreaderDistance, MainWindowWidth - 30.0d), GridUnitType.Pixel); }
            set
            {
                Settings.Instance.SpreaderDistance = value.Value;
                Settings.Instance.Save();
                NotifyOfPropertyChange(() => SpreaderDistance);
            }
        }
        #endregion
    }
}
