using System;
using System.Security.Permissions;
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

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, args) =>
            {
                var ex = (Exception)args.ExceptionObject;
                Logger.Instance.LogError($"Uncaught exception: {ex.Message}\n{ex.StackTrace}");
            };

            DisplayRootViewFor<MainViewModel>();
        }
    }
}
