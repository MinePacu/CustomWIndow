using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CustomWIndow.UtIl;

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
    }
}
