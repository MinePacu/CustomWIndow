using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CustomWIndow.UtIl;
using CustomWIndow.UtIl.WindowFunction;

using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

using Windows.Graphics;
using Windows.UI;

using WinRT.Interop;

namespace CustomWIndow.ViewModels
{
    public class ConfigPageViewModel : ObservableObject
    {
        private bool isTray;

        public bool IsTray
        {
            get => isTray;
            set
            {
                SetProperty(ref isTray, value);
                ConfIg.Instance.EtcConfIg.IsTray = value;
            }
        }

        private bool isRestoreDefaultWindowSetting;

        public bool IsRestoreDefaultWindowSetting
        {
            get => isRestoreDefaultWindowSetting;
            set
            {
                SetProperty(ref isRestoreDefaultWindowSetting, value);
                ConfIg.Instance.EtcConfIg.IsRestoreDefaultWindowSetting = value;
            }
        }

        private bool isTurnOnProgramOnboot;

        public bool IsTurnOnProgramOnboot
        {
            get => isTurnOnProgramOnboot;
            set
            {
                SetProperty(ref isTurnOnProgramOnboot, value);
                ConfIg.Instance.EtcConfIg.IsTurnOninBoot = value;
            }
        }

        private bool isRestartWithAdminEnable;

        public bool IsRestartWithAdminEnable
        {
            get => isRestartWithAdminEnable;
            set
            {
                SetProperty(ref isRestartWithAdminEnable, value);
            }
        }

        private bool isAutoAdmin;

        public bool IsAutoAdmin
        {
            get => isAutoAdmin;
            set
            {
                SetProperty(ref isAutoAdmin, value);
                ConfIg.Instance.AutoAdmin = value;
            }
        }

        private bool isUseCustomTitleBar;

        public bool IsUseCustomTitleBar
        {
            get => isUseCustomTitleBar;
            set
            {
                SetProperty(ref isUseCustomTitleBar, value);
                ConfIg.Instance.EtcConfIg.IsUseCustomTitleBar = value;

                SetCustomTitleBar(value);
            }
        }

        public ICommand RestartAppWithAdminCommand { get; }

        public ConfigPageViewModel()
        {
            IsAutoAdmin = ConfIg.Instance.AutoAdmin;
            IsTray = ConfIg.Instance.EtcConfIg.IsTray;
            IsRestoreDefaultWindowSetting = ConfIg.Instance.EtcConfIg.IsRestoreDefaultWindowSetting;
            IsTurnOnProgramOnboot = ConfIg.Instance.EtcConfIg.IsTurnOninBoot;
            IsUseCustomTitleBar = ConfIg.Instance.EtcConfIg.IsUseCustomTitleBar;
            IsRestartWithAdminEnable = !SysFunction.IsAdmin();

            RestartAppWithAdminCommand = new RelayCommand(RestartAppWithAdmin);
        }

        public void RestartAppWithAdmin()
        {
            ConfIg.Save();
            Process psi = new()
            {
                StartInfo =
                    {
                        FileName = AppDomain.CurrentDomain.BaseDirectory + @"\CustomWIndow.exe",
                        UseShellExecute = true,
                        Verb = "runas"
                    }
            };

            try
            {
                psi.Start();
                Environment.Exit(0);
            }

            catch (Exception)
            {

            }
        }

        public void SetStartUp(bool IsSet)
        {
            if (SysFunction.IsAdmin())
            {
                var rk2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (IsSet)
                    rk2.SetValue("CustomWIndow", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Process.GetCurrentProcess().ProcessName + ".exe"));
                else
                    rk2.DeleteValue("CustomWIndow", false);

                rk2.Close();
            }

            else
            {
                var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (IsSet)
                    rk.SetValue("CustomWIndow", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Process.GetCurrentProcess().ProcessName + ".exe"));
                else
                    rk.DeleteValue("CustomWIndow", false);

                rk.Close();
            }
        }

        private void SetCustomTitleBar(bool IsOn)
        {
            Debug.WriteLine("ExtendsCotentIntoTitleBar : " + SysFunction.FirstWindow.app.TitleBar.ExtendsContentIntoTitleBar);

            if (IsOn == true)
            {
                SysFunction.FirstWindow.app.TitleBar.ExtendsContentIntoTitleBar = true;
                SysFunction.FirstWindow.AppTitleBar.Visibility = Visibility.Visible;
                SysFunction.FirstWindow.SetTitleBar(SysFunction.FirstWindow.AppTitleBar);
                ((Grid)SysFunction.FirstWindow.Content).RowDefinitions[0].Height = new GridLength(30);

                //SysFunction.FirstWindow.AppWindow.SetIcon(null);
                //SysFunction.FirstWindow.AppWindow.Title = "";

                //SetDragRegionForCustomtitleBar(SysFunction.FirstWindow.AppWindow);

                var TransclcentColor = new Color()
                {
                    A = 0,
                    R = 0,
                    G = 0,
                    B = 0
                };

                SysFunction.FirstWindow.app.TitleBar.ButtonBackgroundColor = TransclcentColor;
                SysFunction.FirstWindow.app.TitleBar.ButtonInactiveBackgroundColor = TransclcentColor;

                Debug.WriteLine("true - ExtendsCotentIntoTitleBar : " + SysFunction.FirstWindow.ExtendsContentIntoTitleBar);
            }

            else
            {
                SysFunction.FirstWindow.app.TitleBar.ExtendsContentIntoTitleBar = false;
                SysFunction.FirstWindow.AppTitleBar.Visibility = Visibility.Collapsed;
                ((Grid)SysFunction.FirstWindow.Content).RowDefinitions[0].Height = new GridLength(0);
                SysFunction.FirstWindow.SetTitleBar(null);

                Dwm.DwmSetWindowAttribute_(WindowNative.GetWindowHandle(SysFunction.FirstWindow), UtIl.Enum.DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);

                Debug.WriteLine("false - ExtendsCotentIntoTitleBar : " + SysFunction.FirstWindow.ExtendsContentIntoTitleBar);
            }

        }
    }
}
