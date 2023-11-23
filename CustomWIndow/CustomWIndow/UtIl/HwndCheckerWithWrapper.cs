using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using CustomWIndow.UtIl.WindowFunction;

using Microsoft.UI.Xaml;

namespace CustomWIndow.UtIl
{
    /// <summary>
    /// 일부 시스템 윈도우를 뺀 나머지 대부분의 윈도우를 찾아 설정을 적용하는 체커
    /// </summary>
    public static class HwndCheckerWithWrapper
    {
        public static WinAPIWrapper.WindowmoduleWrapper wrapper { get; set; }
        private static List<IntPtr> tmpIntptrList = new(10);
        //static GCHandle gc;
        public static bool IsSettingChanged { get; set; } = false;
        private static List<IntPtr> VisualStudio_HWND { get; set; } = new(5);
        private static List<IntPtr> VisualStudio_RemoveHWND { get; set; } = new(5);

        public static CancellationTokenSource cts { get; set; }
        public static Task BackgroundTask { get; set; }

        public static int WindowDelay { get; set; } = 1000;

        static async Task GetHwndsWithEnumWindows()
        {
            _ = WindowFunction.EnumHwndFunction.EnumWindows((hwnd, lp) =>
            {
                if (!HwndControl.IsWindowVisible(hwnd))
                    return true;

                else
                {
                    uint ThreadId = ProcessFunction.GetWindowThreadProcessId(hwnd, out int ProcessID);
                    using var proc = Process.GetProcessById(ProcessID);
                    int NonappList_Index = ProcessChecker.ProcessColorChangeExceptLIst.FindIndex(p => p.ProcessStrIng == proc.ProcessName);

                    if (!ConfIg.Instance.DeveloperConfig.UseDwm)
                    {
                        if (wrapper.FindHwnd(hwnd) || (NonappList_Index != -1 && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsBorderChange == false && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonChange == false && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonTextChange == false))
                            return true;

                        else
                            wrapper.AddHwnd(hwnd);
                    }

                    else
                    {
                        if (NonappList_Index == -1)
                        {
                            if (wrapper.HwndList != null && !wrapper.HwndList.Contains(hwnd))
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

                                if (ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitle)
                                    wrapper.SetWindowTitleToEmptyText(hwnd);

                                wrapper.HwndList.Add(hwnd);
                            }
                        }

                        else
                        {
                            if (wrapper.HwndList != null && !wrapper.HwndList.Contains(hwnd))
                            {
                                if (ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfBorderWindow && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsBorderChange == true)
                                    wrapper.SetWindowBorderColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);

                                if (ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfCaptionWindow && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonChange == true)
                                    wrapper.SetWindowCaptionColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionColorTransparency, ConfIg.Instance.ColorConfIg.CaptionColormode);

                                if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 1 && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonTextChange == true)
                                    wrapper.SetWindowCaptionTextColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionTextColorTransparency);

                                if (ConfIg.Instance.ColorConfIg.CaptionColormode == 1 && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonChange == true)
                                    wrapper.SetWindowCaptionColormode(hwnd, true);

                                else if (ConfIg.Instance.ColorConfIg.CaptionColormode == 0 && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonChange == true)
                                    wrapper.SetWindowCaptionColormode(hwnd, false);

                                wrapper.SetWindowCornerPropertyWithDwm(hwnd, (int)ConfIg.Instance.WindowConfig.WindowCornerOption);

                                if (ConfIg.Instance.EtcConfIg.IsSetEmptyTextToCaptionTitle && ProcessChecker.ProcessColorChangeExceptLIst[NonappList_Index].IsCaptIonTextChange == true)
                                    wrapper.SetWindowTitleToEmptyText(hwnd);

                                wrapper.HwndList.Add(hwnd);
                            }
                        }
                    }
                    //Debug.Write("HWND Window Title - " + WindowFunction.StringUtIl.GetWindowTItle(hwnd) + "/ HWND Address - " + hwnd.ToString("X"));
                    //Debug.WriteLine(" / HWND Class Title - " + WindowFunction.StringUtIl.GetClassTItle(hwnd));
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

                /*
                if (ConfIg.Instance.EtcConfIg.IsSetWindowBorderColorConstantly)
                {
                    if (wrapper.CheckDevEnvFromHWND(HWND))
                        wrapper.SetWindowBorderColorWithDwm(HWND, ConfIg.Instance.ColorConfIg.IsBorderColorTransparency);
                }
                */
                if (!WindowFunction.HwndControl.IsWindowEnabled(HWND) || !WindowFunction.HwndControl.IsWindowVisible(HWND))
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
                //Debug.WriteLine("VisualStudio_HWND : " + VisualStudio_HWND.ToString("X"));
            }

            tmpIntptrList.Clear();

            await Task.Delay(WindowDelay);
        }

        public static async Task StartBackgroundTask(CancellationToken cancel, byte r, byte g, byte b)
        {
            wrapper = new(r, g, b, ConfIg.Instance.ColorConfIg.CaptIonColor_.R, ConfIg.Instance.ColorConfIg.CaptIonColor_.G, ConfIg.Instance.ColorConfIg.CaptIonColor_.B, (int)ConfIg.Instance.WindowConfig.WindowCornerOption ,ConfIg.Instance.DeveloperConfig.UseDwm);
            WindowDelay = ConfIg.Instance.EtcConfIg.WindowDelay;

            if (!ConfIg.Instance.DeveloperConfig.UseDwm)
            {
                wrapper.SetBuildVer((int)SysFunction.WinBuildVersion);
                //wrapper.SetCaptionColor(ConfIg.Instance.ColorConfIg.CaptIonColor_.R, ConfIg.Instance.ColorConfIg.CaptIonColor_.G, ConfIg.Instance.ColorConfIg.CaptIonColor_.B);
                wrapper.SetCornerPre((uint)ConfIg.Instance.WindowConfig.WindowCornerOption);
            }

            if (ConfIg.Instance.TaskBarConfig.IsTaskbarborder)
                wrapper.SetTaskbarRoundedCornerandBorderColor((int)ConfIg.Instance.TaskBarConfig.TaskbarBorderCornermode, r, g, b);
            //gc = GCHandle.Alloc(wrapper, GCHandleType.Pinned);

            foreach (var task in ProduceTask(cancel))
            {
                await task;
            }
        }

        static IEnumerable<Task> ProduceTask(CancellationToken cancel)
        {
            while (cancel.IsCancellationRequested == false)
            {
                yield return GetHwndsWithEnumWindows();
            }

            if (cancel.IsCancellationRequested)
            {
                //wrapper.RestoreDwmMica((int) SysFunction.WinBuildVersion);
                wrapper.SetDefaultWindowOptionWithDWM();
                wrapper.SetTaskbarDefaultSetting();
                wrapper.Dispose();
                //gc.Free();
                GC.Collect();

                IsSettingChanged = false;
            }
        }
    }
}
