using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CommunityToolkit.WinUI.Helpers;

namespace CustomWIndow.UtIl
{
    public static class HwndCheckerWithWrapper
    {
        static WinAPIWrapper.WindowmoduleWrapper wrapper;
        static GCHandle gc;

        public static CancellationTokenSource cts { get; set; }
        public static Task BackgroundTask { get; set; }

        static async Task GetHwndsWithEnumWindows()
        {
            _ = WindowFunction.EnumHwndFunction.EnumWindows((hwnd, lp) =>
            {
                if (!WIndowFunctIon.IsWindowVisible(hwnd))
                    return true;

                int WindowTitleLength = wrapper.GetWindowTitleLength(hwnd);
                if (WindowTitleLength > 0)
                {
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
                                wrapper.SetWindowCaptionColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionColorTransparency);
                            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 1)
                                wrapper.SetWindowCaptionTextColorWithDwm(hwnd, ConfIg.Instance.ColorConfIg.IsCaptionTextColorTransparency);
                            if (ConfIg.Instance.ColorConfIg.CaptionColormode == 1)
                                wrapper.SetWindowCaptionColormode(hwnd, true);
                            else if (ConfIg.Instance.ColorConfIg.CaptionColormode == 0)
                                wrapper.SetWindowCaptionColormode(hwnd, false);
                            wrapper.SetWindowCornerPropertyWithDwm(hwnd);
                            wrapper.HwndList.Add(hwnd);
                        }
                    }

                    //Debug.Write("HWND Window Title - " + WindowFunction.StringUtIl.GetWindowTItle(hwnd) + "/ HWND Address - " + hwnd.ToString("X"));
                    //Debug.WriteLine(" / HWND Class Title - " + WindowFunction.StringUtIl.GetClassTItle(hwnd));
                }
                return true;
            }, IntPtr.Zero);

            await Task.Delay(1000);
        }

        public static async Task StartBackgroundTask(CancellationToken cancel, byte r, byte g, byte b)
        {
            wrapper = new(r, g, b, ConfIg.Instance.ColorConfIg.CaptIonColor_.R, ConfIg.Instance.ColorConfIg.CaptIonColor_.G, ConfIg.Instance.ColorConfIg.CaptIonColor_.B, (int)ConfIg.Instance.WindowConfig.WindowCornerOption ,ConfIg.Instance.DeveloperConfig.UseDwm);

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
            }
        }
    }
}
