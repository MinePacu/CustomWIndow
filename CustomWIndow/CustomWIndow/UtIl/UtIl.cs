using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Win32;

using CustomWIndow.UtIl.WindowFunction;
using static CustomWIndow.UtIl.WIndowFunctIon;

namespace CustomWIndow.UtIl
{
    public static class UtIl
    {
        public static CancellationTokenSource cts { get; set; } = new();
        public static Task task { get; set; }

        public static List<string> ProcessLIst { get; } = new();

        public static List<ProcessWIndow> Process_WIndow { get; } = new();

        internal const string HKeyWIndowsAppTh = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        public static int Bordercolor { get; set; }
        public static int CaptIonColor { get; set; }
        public static int CaptIonTextColor { get; set; }

        public static async Task GetApplyHwnd()
        {
            int tep = 0;
            foreach (string _process in ProcessLIst)
            { 
                Process[] process = Process.GetProcessesByName(_process);

                //Debug.WriteLine("tep");
                // 처음 적용
                if (Process_WIndow[tep].FIrst == true)
                {
                    // 프로세스가 열림
                    if (process.Length > 0)
                    {
                        foreach (Process process_ in process)
                        {
                            if (process_.MainWindowHandle == IntPtr.Zero == false)
                            {
                                Process_WIndow[tep].Subhwnd.Clear();
                                Process_WIndow[tep].SubApplyed.Clear();

                                //Debug.WriteLine("handle add - " + process_.MainWindowHandle);
                                //Process_WIndow[tep].FIrsthwnd = process_.MainWindowHandle;
                                foreach (ProcessThread thread in process_.Threads)
                                {
                                    EnumHwndFunction.EnumThreadWindows(thread.Id, 
                                        (hwnd, lP) => 
                                        {
                                            if (IsWindowVisible(hwnd) == true)
                                            {
                                                Process_WIndow[tep].Subhwnd.Add(hwnd);
                                                Process_WIndow[tep].SubApplyed.Add(false);

                                                Process_WIndow[tep].ThreadLength++;
                                            }
                                            //Debug.WriteLine("Style - " + IsWindowVisible(hwnd)); 
                                            return true; 
                                        }, IntPtr.Zero);
                                }

                                Process_WIndow[tep].Applyed = false;
                                Process_WIndow[tep].FIrst = false;
                            }
                        }
                    }
                }

                else
                {
                    // 프로세스가 닫힘
                    if (process.Length > 0 == false)
                    {
                        Process_WIndow[tep].FIrsthwnd = IntPtr.Zero;
                        Process_WIndow[tep].Applyed = false;
                    }

                    else
                    {
                        foreach (Process process_ in process)
                        {
                            Process_WIndow[tep].ThreadLength = process_.Threads.Count;

                            if (process_.MainWindowHandle == IntPtr.Zero == false)
                            {
                                int tep_ = 0;
                                //Debug.WriteLine("tep - subhwnd- " + tep + " - " + Process_WIndow[tep].Subhwnd.Count);
                                if (Process_WIndow[tep].ThreadLength == Process_WIndow[tep].Subhwnd.Count)
                                {
                                    //Process_WIndow[tep].FIrsthwnd = process_.MainWindowHandle;
                                    foreach (ProcessThread thread in process_.Threads)
                                    {
                                        EnumHwndFunction.EnumThreadWindows(thread.Id, 
                                            (hwnd, lP) =>
                                            {
                                                if (IsWindowVisible(hwnd) == true)
                                                {
                                                    //Debug.WriteLine("tep_ - " + tep_);
                                                    if (tep_ < Process_WIndow[tep_].Subhwnd.Count)
                                                    {
                                                        // 윈도우 핸들러 중복 걸러내기
                                                        if (hwnd == Process_WIndow[tep].Subhwnd[tep_] == false)
                                                        {
                                                            Process_WIndow[tep].Subhwnd[tep_] = hwnd;
                                                            Process_WIndow[tep].SubApplyed[tep_] = false;
                                                        }
                                                    }
                                                }

                                                return true;
                                            }, IntPtr.Zero);

                                        tep_++;
                                    }
                                }

                                else
                                {
                                    // 핸들러 목록 갱신
                                    Process_WIndow[tep].Subhwnd.Clear();
                                    Process_WIndow[tep].SubApplyed.Clear();
                                    Process_WIndow[tep].ThreadLength = 0;
                                    foreach (ProcessThread thread in process_.Threads)
                                    {
                                        EnumHwndFunction.EnumThreadWindows(thread.Id, 
                                            (hwnd, lP) =>
                                            {
                                                if (IsWindowVisible(hwnd) == true)
                                                {
                                                    Process_WIndow[tep].Subhwnd.Add(hwnd);
                                                    Process_WIndow[tep].SubApplyed.Add(false);

                                                    Process_WIndow[tep].ThreadLength++;
                                                }
                                                return true; 
                                            }, IntPtr.Zero);
                                    }

                                }

                                Process_WIndow[tep].Applyed = false;
                            }
                        }
                    }
                }
                tep++;
            }

            await ApplyTItlebar(Bordercolor, CaptIonColor, CaptIonTextColor);
            await Task.Delay(1240);
        }

        static async Task ApplyTItlebar(int bcolor, int ccolor, int ctcolor)
        {
            int tep = 0;
            foreach (ProcessWIndow process_wIndow in Process_WIndow)
            {
                process_wIndow.Applyed = true;

                int subtep = 0;
                foreach (IntPtr subhwnd in process_wIndow.Subhwnd)
                {
                    if (process_wIndow.SubApplyed[subtep] == false)
                    {
                        if (process_wIndow.IsBorderChange)
                            _ = Dwm.DwmSetWindowAttribute_(subhwnd, Enum.DwmWIndowAttrIbute.DWMWA_BORDER_COLOR, bcolor);
                        else
                        {
                            if (AppthFunction.GetAppTh() == Enum.Appth.Dark)
                                _ = Dwm.DwmSetWindowAttribute_(subhwnd, Enum.DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);
                        }

                        if (process_wIndow.IsCaptIonChange)
                            _ = Dwm.DwmSetWindowAttribute_(subhwnd, Enum.DwmWIndowAttrIbute.DWMWA_CAPTION_COLOR, ccolor);

                        if (process_wIndow.IsCaptIonTextChange)
                            _ = Dwm.DwmSetWindowAttribute_(subhwnd, Enum.DwmWIndowAttrIbute.DWMWA_TEXT_COLOR, ctcolor);

                        process_wIndow.SubApplyed[subtep] = true;
                    }
                    subtep++;
                }
                tep++;
            }
        }

        public static async Task ConsumeTask(CancellationToken cancel)
        {
            foreach (var task in ProduceTask(cancel))
            {
                //Debug.WriteLine("task - ");
                await task;
            }
        }

        static IEnumerable<Task> ProduceTask(CancellationToken cancel)
        {
            while (cancel.IsCancellationRequested == false)
            {
                yield return GetApplyHwnd();
            }
        }
    }
}
