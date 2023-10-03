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
        public int TaskbarBorderCornermode 
        {
            get
            {
                if (ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode == 0)
                    return 0;
                else if (ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode == UtIl.Enum.Taskbar_Corner.Round)
                    return 1;
                else
                    return 2;
            }
            set
            {
                if (value == 0)
                    ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode = 0;
                else if (value == 1)
                    ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode = UtIl.Enum.Taskbar_Corner.Round;
                else
                    ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode = UtIl.Enum.Taskbar_Corner.RoundSmall;
            }
        }

        public bool IsSetEmptyTitleToCaptionTitle
        {
            get
            {
                return ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitle;
            }
            set
            {
                ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitle = value;
            }
        }

        public bool IsSetEmptyTitleToCaptionTitleConstantly
        {
            get
            {
                return ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitleConstantly;
            }
            set
            {
                ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitleConstantly = value;
            }
        }

        public AdvancedSettings()
        {
            this.InitializeComponent();
            Extraborder1.IsOn = ConfIg.Instance.TaskBarConfig.IsTaskbarborder;

            if (ConfIg.Instance.TaskBarConfig.IsTaskbarborder == false)
            {
                TaskbarBorderCornermode = 0;
                TaskbarCorner.IsEnabled = false;
            }
        }

        void SettingsCard_Click(object sender, RoutedEventArgs e)
        {
            Dwm.RestartDWMwithProgram();
        }

        void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.TaskBarConfig.IsTaskbarborder = ((ToggleSwitch) sender).IsOn;
            TaskbarCorner.IsEnabled = true;
        }

        void IsSetEmptyTitle_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitle = ((ToggleSwitch)sender).IsOn;
        }

        void IsSetEmptyTitleConstantly_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitleConstantly = ((ToggleSwitch)sender).IsOn;
        }
    }
}
 