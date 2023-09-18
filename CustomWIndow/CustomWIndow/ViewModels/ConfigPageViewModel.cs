using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CustomWIndow.UtIl;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Input;

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

        public ICommand RestartAppWithAdminCommand { get; }

        public ConfigPageViewModel()
        {
            IsAutoAdmin = ConfIg.Instance.AutoAdmin;
            IsTray = ConfIg.Instance.EtcConfIg.IsTray;
            IsRestoreDefaultWindowSetting = ConfIg.Instance.EtcConfIg.IsRestoreDefaultWindowSetting;
            IsTurnOnProgramOnboot = ConfIg.Instance.EtcConfIg.IsTurnOninBoot;

            RestartAppWithAdminCommand = new RelayCommand(RestartAppWithAdmin);
        }

        public void RestartAppWithAdmin()
        {
            ConfIg.Save();
            ProcessStartInfo psi = new(AppDomain.CurrentDomain.BaseDirectory + "\\CustomWIndow.exe")
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


    }
}
