using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using CustomWIndow.UtIl;
using Microsoft.Windows.AppLifecycle;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using WinRT;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConfIgPage : Page
    {
        public bool IsTray { get; set; }

        public ConfIgPage()
        {
            this.InitializeComponent();

            if (SysFunction.IsAdmin())
                RestartWithAdminbutton.IsEnabled = false;

            AutoAdminToggle.IsOn = ConfIg.Instance.AutoAdmin;
            IsTray = ConfIg.Instance.EtcConfIg.IsTray;
        }

        void AutoAdminToggle_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.AutoAdmin = ((ToggleSwitch) sender).IsOn;
        }

        void RestartWithAdminbutton_Click(object sender, RoutedEventArgs e)
        {
            ConfIg.Save();

            ProcessStartInfo psi = new(Environment.CurrentDirectory + "\\CustomWIndow.exe")
            {
                Verb = "Runas",
                UseShellExecute = true,
            };

            try
            {
                Process.Start(psi);
                Environment.Exit(0);
            }

            catch (Exception)
            {

            }
        }

        private void IsTrayToggle_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.EtcConfIg.IsTray = ((ToggleSwitch)sender).IsOn;
        }
    }
}
