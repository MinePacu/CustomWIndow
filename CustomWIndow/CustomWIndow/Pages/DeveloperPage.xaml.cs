using Microsoft.UI.Xaml.Controls;

using CustomWIndow.UtIl;
using CustomWIndow.Windows;
using CustomWIndow.UtIl.WindowFunction;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeveloperPage : Page
    {
        DebugWindow debugWindow { get; set; }

        public DeveloperPage()
        {
            this.InitializeComponent();

            FrameDrawerMode.SelectedIndex = (ConfIg.Instance.DeveloperConfig.UseDwm == true) ? 0 : 1;
            //FrameDrawerMode.SelectionChanged += FrameDrawerMode_SelectionChanged;
            if (HwndCheckerWithWrapper.wrapper != null || SpecificHwndCheckerWithWrapper.wrapper != null)
                HWND_Debug.IsClickEnabled = true;
            else
                HWND_Debug.IsClickEnabled = false;
        }

        private void FrameDrawerMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConfIg.Instance.DeveloperConfig.UseDwm = (((ComboBox)sender).SelectedIndex == 0) ? true : false;
        }

        private void HWND_Debug_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (ConfIg.Instance.ProcessCheckermode == 1)
            {
                if (HwndCheckerWithWrapper.wrapper != null)
                {
                    if (debugWindow != null)
                    {
                        if (debugWindow.AppWindow != null)
                        {
                            HwndControl.SetForegroundWindow(Win32Interop.GetWindowFromWindowId(debugWindow.AppWindow.Id));
                            return;
                        }
                        
                        debugWindow = null;
                        debugWindow = new(HwndCheckerWithWrapper.wrapper.HwndList);
                    }

                    else
                    {
                        debugWindow = new(HwndCheckerWithWrapper.wrapper.HwndList);
                    }
                    debugWindow.Activate();
                }
            }
            else
            {
                if (SpecificHwndCheckerWithWrapper.wrapper != null)
                {
                    if (debugWindow != null)
                    {
                        if (debugWindow.AppWindow != null)
                        {
                            HwndControl.SetForegroundWindow(Win32Interop.GetWindowFromWindowId(debugWindow.AppWindow.Id));
                            return;
                        }

                        debugWindow = null;
                        debugWindow = new(SpecificHwndCheckerWithWrapper.wrapper.HwndList);
                    }

                    else
                    {
                        debugWindow = new(SpecificHwndCheckerWithWrapper.wrapper.HwndList);
                    }
                    debugWindow.Activate();
                }
            }
        }
    }
}
