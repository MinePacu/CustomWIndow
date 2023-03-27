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
                    if (wrapper.FindHwnd(hwnd))
                        return true;

                    else
                        wrapper.AddHwnd(hwnd);

                    //Debug.Write("HWND Window Title - " + WindowFunction.StringUtIl.GetWindowTItle(hwnd) + "/ HWND Address - " + hwnd.ToString("X"));
                    //Debug.WriteLine(" / HWND Class Title - " + WindowFunction.StringUtIl.GetClassTItle(hwnd));
                }
                return true;
            }, IntPtr.Zero);

            await Task.Delay(1000);
        }

        public static async Task StartBackgroundTask(CancellationToken cancel)
        {
            wrapper = new();

            wrapper.SetBuildVer((int) SysFunction.WinBuildVersion);
            wrapper.SetCornerPre((uint)ConfIg.Instance.WIndowCornermode);

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
                wrapper.RestoreDwmMica((int) SysFunction.WinBuildVersion);
                wrapper.Dispose();
                //gc.Free();
                GC.Collect();
            }
        }
    }
}
