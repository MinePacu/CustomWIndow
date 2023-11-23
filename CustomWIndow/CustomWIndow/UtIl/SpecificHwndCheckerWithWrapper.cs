using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using CustomWIndow.UtIl.WindowFunction;

namespace CustomWIndow.UtIl
{
    /// <summary>
    /// 특정 윈도우만 찾아 설정을 적용하는 체커
    /// </summary>
    public static class SpecificHwndCheckerWithWrapper
    {
        public static WinAPIWrapper.WindowmoduleWrapper wrapper;
        private static List<IntPtr> tmpIntptrList = new(10);
        static StringBuilder fileString { get; set; } = new(1000);

        public static bool IsSettingChanged { get; set; } = false;
        private static List<IntPtr> VisualStudio_HWND { get; set; } = new(5);
        private static List<IntPtr> VisualStudio_RemoveHWND { get; set; } = new(5);

        public static CancellationTokenSource cts { get; set; }
        public static Task BackgroundTask { get; set; }

        public static int WindowDelay { get; set; } = 1000;

        static async Task GetSpecificHwndWithEnumWindows()
        {
            _ = WindowFunction.EnumHwndFunction.EnumWindows((hwnd, lp) =>
            {
                WindowFunction.StringUtIl.GetWindowModuleFileName(hwnd, fileString, 1000);
                if (ConfIg.Instance.AppLIst.Contains(Path.GetFileName(fileString.ToString())))
                {
                    if (!HwndControl.IsWindowVisible(hwnd))
                        return true;

                    if (!ConfIg.Instance.DeveloperConfig.UseDwm)
                    {
                        if (wrapper.FindHwnd(hwnd))
                            return true;
                        else
                            wrapper.AddHwnd(hwnd);
                    }

                    else
                    {
                        if (wrapper.HwndList != null && !wrapper.HwndList.Contains(hwnd))
                        {
                            if (ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfBorderWindow)
                                wrapper.SetWindowBorderColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);

                            if (ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfCaptionWindow)
                                wrapper.SetWindowCaptionColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionColorTransparency, ConfIg.Instance.ColorConfIg.CaptionColormode);

                            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 1)
                                wrapper.SetWindowCaptionColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionTextColorTransparency, ConfIg.Instance.ColorConfIg.CaptionColormode);

                            if (ConfIg.Instance.ColorConfIg.CaptionColormode == 1)
                                wrapper.SetWindowCaptionColormode(hwnd, true);

                            else if (ConfIg.Instance.ColorConfIg.CaptionColormode == 0)
                                wrapper.SetWindowCaptionColormode(hwnd, false);

                            wrapper.SetWindowCornerPropertyWithDwm(hwnd, (int) ConfIg.Instance.WindowConfig.WindowCornerOption);

                            if (ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitle)
                                wrapper.SetWindowTitleToEmptyText(hwnd);

                            wrapper.HwndList.Add(hwnd);

                        }
                    }

                }
                return true;
            }, IntPtr.Zero);

            if (IsSettingChanged)
            {
                foreach (var hwnd in wrapper.HwndList)
                {
                    if (ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfBorderWindow)
                        wrapper.SetWindowBorderColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);

                    if (ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfCaptionWindow)
                        wrapper.SetWindowCaptionColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionColorTransparency, ConfIg.Instance.ColorConfIg.CaptionColormode);

                    if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 1)
                        wrapper.SetWindowCaptionTextColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionTextColorTransparency);

                    if (ConfIg.Instance.ColorConfIg.CaptionColormode == 1)
                        wrapper.SetWindowCaptionColormode(hwnd, true);

                    else if (ConfIg.Instance.ColorConfIg.CaptionColormode == 0)
                        wrapper.SetWindowCaptionColormode(hwnd, false);

                    wrapper.SetWindowCornerPropertyWithDwm(hwnd, (int)ConfIg.Instance.WindowConfig.WindowCornerOption);

                    WindowDelay = ConfIg.Instance.EtcConfIg.WindowDelay;
                }
                IsSettingChanged = false;
            }

            foreach (var HWND in wrapper.HwndList)
            {
                if (ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitleConstantly)
                {
                    if (wrapper.GetWindowTitleLength(HWND) > 1)
                        wrapper.SetWindowTitleToEmptyText(HWND);
                }

                if (!HwndControl.IsWindowEnabled(HWND) || !HwndControl.IsWindowVisible(HWND))
                    tmpIntptrList.Add(HWND);
            }

            foreach (var HWND in tmpIntptrList)
            {
                wrapper.HwndList.Remove(HWND);
            }

            if (ConfIg.Instance.EtcConfIg.IsSetWindowBorderColorConstantly)
            {
                if (VisualStudio_HWND.Count < 1)
                {
                    var array = Process.GetProcessesByName("devenv");
                    if (array.Length > 0)
                    {
                        foreach (var window in array)
                        {
                            VisualStudio_HWND.Add(window.MainWindowHandle);
                            wrapper.SetWindowBorderColorWithDwm(window.MainWindowHandle, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);
                        }
                    }
                }

                else
                {
                    foreach (var Hwnd in VisualStudio_HWND)
                    {
                        if (Hwnd != IntPtr.Zero && wrapper.HwndList.Contains(Hwnd))
                            wrapper.SetWindowBorderColorWithDwm(Hwnd, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);

                        else if (Hwnd != IntPtr.Zero && wrapper.HwndList.Contains(Hwnd) == false)
                            VisualStudio_RemoveHWND.Add(Hwnd);

                        else
                        {
                            var array = Process.GetProcessesByName("devenv");
                            if (array.Length > 0)
                            {
                                foreach (var window in array)
                                {
                                    VisualStudio_HWND.Add(window.MainWindowHandle);
                                    wrapper.SetWindowBorderColorWithDwm(window.MainWindowHandle, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);
                                }
                            }
                        }
                    }

                    foreach (var Hwnd in VisualStudio_RemoveHWND)
                    {
                        VisualStudio_HWND.Remove(Hwnd);
                    }
                }
            }

            tmpIntptrList.Clear();

            await Task.Delay(WindowDelay);
        }

        public static async Task StartBackgroundtask(byte r, byte g, byte b, CancellationToken cancel)
        {
            wrapper = new(r, g, b, ConfIg.Instance.ColorConfIg.CaptIonColor_.R, ConfIg.Instance.ColorConfIg.CaptIonColor_.G, ConfIg.Instance.ColorConfIg.CaptIonColor_.B, (int)ConfIg.Instance.WindowConfig.WindowCornerOption, ConfIg.Instance.DeveloperConfig.UseDwm);
            WindowDelay = ConfIg.Instance.EtcConfIg.WindowDelay;

            if (!ConfIg.Instance.DeveloperConfig.UseDwm)
            {
                wrapper.SetBuildVer((int)SysFunction.WinBuildVersion);
                wrapper.SetCornerPre((uint)ConfIg.Instance.WindowConfig.WindowCornerOption);

                if (ConfIg.Instance.TaskBarConfig.IsTaskbarborder)
                    wrapper.SetTaskbarRoundedCornerandBorderColor((int)ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode, r, g, b);

                foreach (var task in ProduceTask(cancel))
                    await task;
            }
        }

        static IEnumerable<Task> ProduceTask(CancellationToken cancel)
        {
            while (cancel.IsCancellationRequested == false)
                yield return GetSpecificHwndWithEnumWindows();

            if (cancel.IsCancellationRequested)
            {
                if (ConfIg.Instance.DeveloperConfig.UseDwm)
                    wrapper.SetDefaultWindowOptionWithDWM();
                wrapper.SetTaskbarDefaultSetting();
                wrapper.Dispose();
            }

            GC.Collect();
        }

    }
}
