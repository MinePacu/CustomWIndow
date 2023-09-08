using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CustomWIndow.UtIl;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Win32;

using System;
using System.Diagnostics;
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

        public void SetStartUp(bool IsSet)
        {
            var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var rk2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (IsSet)
            {
                rk.SetValue("CustomWIndow", Environment.ProcessPath);
                rk2.SetValue("CustomWIndow", Environment.ProcessPath);
            }
            else
            {
                rk.DeleteValue("CustomWIndow", false);
                rk2.DeleteValue("CustomWIndow", false);
            }
        }


    }
}
