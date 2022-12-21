using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleTables;

namespace GetWIndowStyle.UtIl
{
    public static class HwndChecker
    {
        public static Task task;
        public static CancellationTokenSource cts = new();
        public static List<HwndClass> HwndLIst { get; } = new(10);

        static async Task GetHwnd(string process_string)
        {
            HwndLIst.Clear();
            Process[] Processes = Process.GetProcessesByName(process_string.Replace(".exe", ""));

            foreach (var process in Processes)
            {
                foreach (ProcessThread Thread in process.Threads)
                {
                    WIndowFunctIon.EnumThreadWindows(Thread.Id,
                        (hwnd, lP) =>
                        {
                            if (WIndowFunctIon.IsWindowVisible(hwnd))
                                HwndLIst.Add(new(WIndowFunctIon.GetWindowText_(hwnd), hwnd));
                            return true;
                        }, IntPtr.Zero);
                }
            }

            Console.Clear();
            var table = new ConsoleTable("캡션", "IntPtr");
            foreach (HwndClass hwnd in HwndLIst)
            {
                table.AddRow(hwnd.HwndCaptIon, hwnd.hwnd.ToString("X"));
            }
            table.Write();
            Console.WriteLine();

            await Task.Delay(1245);
        }


        public static async Task ConsumeTask(CancellationToken cancel, string process_string)
        {
            foreach (var task in ProduceTask(cancel, process_string))
            {
                //Debug.WriteLine("task - ");
                await task;
            }
        }

        static IEnumerable<Task> ProduceTask(CancellationToken cancel, string process_string)
        {
            while (cancel.IsCancellationRequested == false)
            {
                yield return GetHwnd(process_string);
            }
        }
    }
}
