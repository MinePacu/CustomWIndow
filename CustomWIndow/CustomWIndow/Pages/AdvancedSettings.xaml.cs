using CustomWIndow.UtIl;
using CustomWIndow.UtIl.WindowFunction;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdvancedSettings : Page
    {
        public AdvancedSettings()
        {
            this.InitializeComponent();
            Extraborder1.IsOn = ConfIg.Instance.EtcConfIg.IsTaskbarborder;
        }

        void SettingsCard_Click(object sender, RoutedEventArgs e)
        {
            Dwm.RestartDWMwithProgram();
        }

        void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.EtcConfIg.IsTaskbarborder = ((ToggleSwitch) sender).IsOn;
        }
    }
}
