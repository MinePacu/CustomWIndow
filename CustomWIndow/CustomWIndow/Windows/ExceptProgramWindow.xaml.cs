using System;
using System.IO;

using CustomWIndow.UtIl.Enum;
using CustomWIndow.UtIl;
using CustomWIndow.UtIl.WindowFunction;

using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;

using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExceptProgramWindow : Window
    {
        AppWindow WIndow;

        public ExceptProgramWindow()
        {
            this.InitializeComponent();

            WIndow = WinUIFunction.GetAppWIndowForWIndow(this);

            WIndow.Title = "제외 프로그램 관리";
            WIndow.Resize(new(750, 400));
            WIndow.SetIcon(Path.Combine(Environment.CurrentDirectory, "asset/window-system.ico"));

            if (AppthFunction.GetAppTh() == Appth.Dark)
                Dwm.DwmSetWindowAttribute_(WindowNative.GetWindowHandle(this), DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);

            var apppresenter = (OverlappedPresenter) WIndow.Presenter;

            apppresenter.IsMaximizable = false;
            apppresenter.IsResizable = false;
        }
    }
}
