using Microsoft.UI.Xaml.Controls;

using CustomWIndow.UtIl;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeveloperPage : Page
    {
        public DeveloperPage()
        {
            this.InitializeComponent();

            FrameDrawerMode.SelectedIndex = (ConfIg.Instance.DeveloperConfig.UseDwm == true) ? 0 : 1;
            //FrameDrawerMode.SelectionChanged += FrameDrawerMode_SelectionChanged;
        }

        private void FrameDrawerMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConfIg.Instance.DeveloperConfig.UseDwm = (((ComboBox)sender).SelectedIndex == 0) ? true : false;
        }
    }
}