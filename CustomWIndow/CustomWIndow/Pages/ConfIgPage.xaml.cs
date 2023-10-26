using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using CustomWIndow.UtIl;

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

        public bool IsRestoreDefaultWindowSetting { get; set; }

        public ConfIgPage()
        {
            this.InitializeComponent();

            if (SysFunction.IsAdmin())
                ConfigPageViewmodel.IsRestartWithAdminEnable = false;
        }

        private void IsTurnOnProgramOnBoot_Toggle_Toggled(object sender, RoutedEventArgs e)
        {
            var toggleSwitch = sender as ToggleSwitch;
            ConfigPageViewmodel.SetStartUp(toggleSwitch.IsOn);
        }
    }
}
