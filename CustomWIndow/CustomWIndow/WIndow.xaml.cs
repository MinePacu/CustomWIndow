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
using System.Diagnostics;
using Microsoft.UI.Xaml.Media.Animation;
using CustomWIndow.Pages;
using Microsoft.UI.Xaml.Navigation;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WIndow : Window
    {
        public AppWindow app { get; set; }
        MicaHelper micaHelper;

        List<(string Tag, Type page)> _pages = new(5)
        {
            ("Program", typeof(Pages.SettingsPage)),
            ("Color", typeof(Pages.ColorConfigPage)),
            ("Advanced", typeof(Pages.AdvancedSettings)),
            ("Developer", typeof(Pages.DeveloperPage)),
            ("Config", typeof(Pages.ConfIgPage)),
        };

        public WIndow()
        {
            InitializeComponent();
            app = WinUIFunction.GetAppWIndowForWIndow(this);
            app.Title = "커스텀 윈도우";
            app.SetIcon(Path.Combine(Environment.CurrentDirectory, "asset/window-system.ico"));

            if (AppColorFunction.GetAppColor() == AppColor.Dark)
                Dwm.DwmSetWindowAttribute_(WindowNative.GetWindowHandle(this), DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);

            app.Resize(new(ConfIg.Instance.WindowConfig.MainWindowGaro, ConfIg.Instance.WindowConfig.MainWindowSero));

            //Navigation.SelectedItem = Navigation.MenuItems[0];

            micaHelper = new(this);
            Debug.WriteLine("micaHelper : " + micaHelper.TrySetMica(true, false, true));

            app.Changed += App_Changed;
            SysFunction.FirstWindow = this;
        }

        private void App_Changed(AppWindow sender, AppWindowChangedEventArgs args)
        {
            if (args.DidSizeChange && sender.TitleBar.ExtendsContentIntoTitleBar)
                SetDragRegionForCustomtitleBar(sender);
        }

        private void Navigation_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigated += On_Navigated;
            Navigation.SelectedItem = Navigation.MenuItems[0];

            Navigation_Navigate(typeof(SettingsPage), new EntranceNavigationTransitionInfo());
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            Navigation.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType == typeof(ConfIgPage))
            {
                  Navigation.SelectedItem = (NavigationViewItem) Navigation.SettingsItem;
                  Navigation.Header = "설정";
            }

            else if (ContentFrame.SourcePageType == null == false)
            {
                Navigation.SelectedItem = Navigation.MenuItems
                                            .OfType<NavigationViewItem>()
                                            .First(i => i.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));

                Navigation.Header = ((NavigationViewItem)Navigation.SelectedItem)?.Content?.ToString();
            }
        }

        private void Navigation_Navigate(Type navPageType, NavigationTransitionInfo transitionInfo)
        {
            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (navPageType is not null && Type.Equals(preNavPageType, navPageType) == false)
                ContentFrame.Navigate(navPageType, null, transitionInfo);
        }

        private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Navigation_Navigate(typeof(ConfIgPage), args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer == null == false)
            {
                var navPageType = Type.GetType(args.SelectedItemContainer.Tag.ToString());
                Navigation_Navigate(navPageType, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void Navigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                Navigation_Navigate(typeof(ConfIgPage), args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer == null == false)
            {
                Type navPageType = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                Navigation_Navigate(navPageType, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void n_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) => TryMoveBackPage();

        private bool TryMoveBackPage()
        {
            if (ContentFrame.CanGoBack == false)
                return false;

            if (Navigation.IsPaneOpen && (Navigation.DisplayMode == NavigationViewDisplayMode.Compact || Navigation.DisplayMode == NavigationViewDisplayMode.Minimal))
                return false;

            ContentFrame.GoBack();
            return true;
        }

        void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            var wIndow = (OverlappedPresenter)app.Presenter;

            if (wIndow.State == OverlappedPresenterState.Minimized == false)
            {
                ConfIg.Instance.WindowConfig.MainWindowGaro = app.Size.Width;
                ConfIg.Instance.WindowConfig.MainWindowSero = app.Size.Height;
            }
        }
        private void SetDragRegionForCustomtitleBar(AppWindow appWindow)
        {
            int titleBarHeight = appWindow.TitleBar.Height;
            AppTitleBar.Height = titleBarHeight;

            int CaptionButtonOcclusionWidth = appWindow.TitleBar.RightInset;

            // int windowIconWidthAndPadding = (int) AppWindowIcon.ActualWidth + (int) AppWindowIcon.Margin.Right;
            int dragRegionWidth = appWindow.Size.Width - CaptionButtonOcclusionWidth; // + windowIconWidthAndPadding

            RectInt32[] dragRects = Array.Empty<RectInt32>();
            RectInt32 dragRect;

            dragRect.X = 0; // windowIconWidthAndPadding;
            dragRect.Y = 0;
            dragRect.Width = dragRegionWidth;
            dragRect.Height = titleBarHeight;

            appWindow.TitleBar.SetDragRectangles(dragRects.Append(dragRect).ToArray());
        }
        private async void Window_Closed(object sender, WindowEventArgs args)
        {
            ConfIg.Save();

            if (ConfIg.Instance.EtcConfIg.IsTray)
            {
                App.t_window = new();
                App.t_window.Activate();
                App.t_window.Hide(false);

                args.Handled = true;
                UtIl.WindowFunction.HwndControl.ShowWindow(WinRT.Interop.WindowNative.GetWindowHandle(this), 0);
            }

            else
            {
                if (ConfIg.Instance.EtcConfIg.IsRestoreDefaultWindowSetting)
                {
                    if (ConfIg.Instance.ProcessCheckermode == 1)
                    {
                        if (HwndCheckerWithWrapper.BackgroundTask == null == false && HwndCheckerWithWrapper.BackgroundTask.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation == false)
                        {
                            HwndCheckerWithWrapper.cts.Cancel();
                            await HwndCheckerWithWrapper.BackgroundTask;
                        }
                    }

                    else
                    {
                        if (SpecificHwndCheckerWithWrapper.BackgroundTask == null == false && SpecificHwndCheckerWithWrapper.BackgroundTask.Status == System.Threading.Tasks.TaskStatus.WaitingForActivation == false)
                        {
                            SpecificHwndCheckerWithWrapper.cts.Cancel();
                            await SpecificHwndCheckerWithWrapper.BackgroundTask;
                        }
                    }
                }
            }

            micaHelper.DisposeMicaController();
        }
    }
}
