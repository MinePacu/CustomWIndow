using System;

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using WinRT.Interop;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static class WinUIFunction
    {
        public static AppWindow GetAppWIndowForWIndow(Window _window)
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(_window);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);

            return AppWindow.GetFromWindowId(wndId);
        }
    }
}
