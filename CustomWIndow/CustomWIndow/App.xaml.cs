// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;

using CustomWIndow.UtIl;
using CustomWIndow.Windows;

using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            ConfIg.Load();

            if (ConfIg.Instance.AutoAdmin && (SysFunction.IsAdmin() == false))
            {
                ProcessStartInfo psi = new(AppDomain.CurrentDomain.BaseDirectory + "\\CustomWIndow.exe")
                {
                    Verb = "Runas",
                    UseShellExecute = true,
                };

                Process.Start(psi);
                Environment.Exit(0);
            }

            //uint color = 0x0;
            //bool ColorizationOpaqueBlend = false;
            //WIndowFunctIon.DwmGetColorizationColor(out color, out ColorizationOpaqueBlend);
            //Debug.WriteLine("DwmGetColorizationColor - " + color.ToString("X"));
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new WIndow();
            m_window.Activate();
        }

        public static Window m_window { get; set; }
        public static TrayWindow t_window { get; set; }
    }
}
