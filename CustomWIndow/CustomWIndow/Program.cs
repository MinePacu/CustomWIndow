using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.UI.Dispatching;
using Microsoft.Windows.AppLifecycle;

namespace CustomWIndow
{
    class Program
    {
        [STAThread]
        static async Task<int> Main(string[] args)
        {
            WinRT.ComWrappersSupport.InitializeComWrappers();
            bool IsDIr = await DecideRedIr();
            if (IsDIr == false)
            {
                Microsoft.UI.Xaml.Application.Start((p) =>
                {
                    var Context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
                    SynchronizationContext.SetSynchronizationContext(Context);
                    _ = new App();
                });
            }

            return 0;
        }

        static async Task<bool> DecideRedIr()
        {
            bool IsRedIr = false;
            AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
            ExtendedActivationKind kInd = args.Kind;
            AppInstance keyInstance = AppInstance.FindOrRegisterForKey("randomkey");

            if (keyInstance.IsCurrent)
                keyInstance.Activated += OnActivated;

            else
            {
                IsRedIr = true;
                await keyInstance.RedirectActivationToAsync(args);
            }

            return IsRedIr;
        }

        static void OnActivated(object sender, AppActivationArguments e)
        {
            ExtendedActivationKind kInd = e.Kind;
        }
    }
}
