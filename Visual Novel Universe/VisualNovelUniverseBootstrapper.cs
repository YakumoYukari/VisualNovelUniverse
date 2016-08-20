using System.Windows;
using Caliburn.Micro;
using Visual_Novel_Universe.ViewModels;

namespace Visual_Novel_Universe
{
    public class VisualNovelUniverseBootstrapper : BootstrapperBase
    {
        public VisualNovelUniverseBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
