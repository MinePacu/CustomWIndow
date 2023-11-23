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

        public bool IsSetWindowBorderColorConstantly
        {
            get
            {
                return ConfIg.Instance.EtcConfIg.IsSetWindowBorderColorConstantly;
            }

            set
            {
                ConfIg.Instance.EtcConfIg.IsSetWindowBorderColorConstantly = value;
            }
        }

        public string WindowDelay_String
        {
            get => ConfIg.Instance.EtcConfIg.WindowDelay.ToString();
            set
            {
                if (int.Parse(value) >= 100)
                {
                    ConfIg.Instance.EtcConfIg.WindowDelay = int.Parse(value);
                    if (ConfIg.Instance.ProcessCheckermode == 0)
                    {
                        if (SpecificHwndCheckerWithWrapper.wrapper != null)
                            SpecificHwndCheckerWithWrapper.IsSettingChanged = true;
                    }

                    else
                    {
                        if (HwndCheckerWithWrapper.wrapper != null)
                            HwndCheckerWithWrapper.IsSettingChanged = true;
                    }
                }
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

        void IsSetWindowBorderColorConstantly_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.EtcConfIg.IsSetWindowBorderColorConstantly = ((ToggleSwitch)sender).IsOn;
        }

        private void WindowDelay_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((TextBox)sender).Text != "")
                ConfIg.Instance.EtcConfIg.WindowDelay = int.Parse(((TextBox)sender).Text);
        }
    }
}
 