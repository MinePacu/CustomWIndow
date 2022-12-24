using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CustomWIndow.UtIl.Enum;
using CustomWIndow.UtIl.WindowFunction;
using CustomWIndow.UtIl;

using Microsoft.UI.Xaml;
using WinRT.Interop;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Controls;
using H.NotifyIcon;
using CustomWIndow.Windows;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WIndow : Window
    {
        AppWindow app;

        List<(string Tag, Type page)> _pages = new List<(string Tag, Type page)>(5)
        {
            ("Primary", typeof(Pages.SettingsPage)),
            ("Advanced", typeof(Pages.AdvancedSettings)),
            ("Config", typeof(Pages.ConfIgPage)),
        };

        public WIndow()
        {
            InitializeComponent();
            app = WinUIFunction.GetAppWIndowForWIndow(this);
            app.Title = "커스텀 윈도우";
            app.SetIcon(Path.Combine(Environment.CurrentDirectory, "asset/window-system.ico"));

            if (AppthFunction.GetAppTh() == Appth.Dark)
                Dwm.DwmSetWindowAttribute_(WindowNative.GetWindowHandle(this), DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);

            app.Resize(new(ConfIg.Instance.ProgramWIndowCon.WIndowGaro, ConfIg.Instance.ProgramWIndowCon.WIndowSero));

            n.SelectedItem = n.MenuItems[0];
        }

        void N_NavIgate(string navIteTag, Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo tranInfo)
        {
            Type _page = null;

            if (navIteTag == "Config")
            {
                _page = typeof(Pages.ConfIgPage);
            }

            else
            {
                var Ite = _pages.FirstOrDefault(p => p.Tag.Equals(navIteTag));
                _page = Ite.page;
            }

            var NaPageType = content.CurrentSourcePageType;

            if (_page == null == false)
            {
                if (Type.Equals(NaPageType, _page) == false)
                {
                    content.Navigate(_page, null, tranInfo);
                }
            }
        }

        void n_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                N_NavIgate("Config", args.RecommendedNavigationTransitionInfo);
            }

            else if (args.SelectedItemContainer == null == false)
            {
                var IteTag = args.SelectedItemContainer.Tag.ToString();

                N_NavIgate(IteTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            var wIndow = (OverlappedPresenter)app.Presenter;

            if (wIndow.State == OverlappedPresenterState.Minimized == false)
            {
                ConfIg.Instance.ProgramWIndowCon.WIndowGaro = app.Size.Width;
                ConfIg.Instance.ProgramWIndowCon.WIndowSero = app.Size.Height;
            }
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            ConfIg.Save();

            if (ConfIg.Instance.EtcConfIg.IsTray)
            {
                App.t_window = new();
                App.t_window.Activate();
                App.t_window.Hide(false);

                args.Handled = true;
                WIndowFunctIon.ShowWindow(WinRT.Interop.WindowNative.GetWindowHandle(this), 0);
            }
        }
    }
}
