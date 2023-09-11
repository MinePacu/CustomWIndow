// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using H.NotifyIcon;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Controls
{
    public sealed partial class TrayIconUser : UserControl
    {
        public TrayIconUser()
        {
            this.InitializeComponent();
        }

#nullable enable
        public void ShowHideWindow_Requested(object? _, ExecuteRequestedEventArgs args)
        {
            var window = App.m_window;

            if (window == null)
                return;

            else
            {
                TrayIcon.Dispose();
                App.t_window.Close();
                window.Show();
            }
        }

        public void ExitApplication_Requested(object? _, ExecuteRequestedEventArgs args)
        {
            TrayIcon.Dispose();
            Environment.Exit(0);
        }
    }
}