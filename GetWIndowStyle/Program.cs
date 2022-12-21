﻿// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using ConsoleTables;

using GetWIndowStyle.UtIl;

using static GetWIndowStyle.UtIl.UWPHwndLoader;
using static GetWIndowStyle.UtIl.WIndowFunctIon;

if (args.Length > 0)
{
    if (args.Length == 1)
    {
        if (args[0] == "/GetUWPHwnd")
        {
            EnumWindowsProc Callback = new EnumWindowsProc(ListWindows);
            Hwndstruct ep = new Hwndstruct();
            IntPtr plParam = Marshal.AllocHGlobal(Marshal.SizeOf(ep));
            Marshal.StructureToPtr(ep, plParam, false);
            EnumWindows(Callback, ref plParam);
            Hwndstruct epret = (Hwndstruct)Marshal.PtrToStructure(plParam, typeof(Hwndstruct));
            Marshal.FreeHGlobal(plParam);
            //if (epret.hWnd != IntPtr.Zero)
            //{
            //    string sState = (epret.nState == 1) ? "\n(state = minimized)" : "";
             //   Console.WriteLine(string.Format("Window handle = {0}\nPID = {1}\nExecutable = {2}" + sState, epret.hWnd.ToString("X"), epret.nPID, epret.sExe));
            //}

            foreach (var UWPHwnd in UWPHwndLIst)
            {
                if (UWPHwnd.hWnd == IntPtr.Zero == false)
                {
                    string sState = (UWPHwnd.nState == 1) ? "\n(state = minimized)" : "";
                    Console.WriteLine(string.Format("Window handle = {0}\nPID = {1}\nExecutable = {2}" + sState, UWPHwnd.hWnd.ToString("X"), UWPHwnd.nPID, UWPHwnd.sExe));
                    Console.WriteLine();

                    if (UWPHwnd.nState == 1 == false)
                    {
                        int Color = 0x00009514;
                        DwmSetWindowAttribute(UWPHwnd.hWnd, 34, ref Color, sizeof(int));
                    }
                }
            }
        }
    }

    else if (args.Length >= 2)
    {
        if (args[0] == "/GetWIndowHwnd")
        {
            if (args[1] == "/t")
            {
                if (args[2] == null == false)
                {
                    while (true)
                    {
                        HwndChecker.HwndLIst.Clear();
                        Process[] Processes = Process.GetProcessesByName(args[2].Replace(".exe", ""));

                        foreach (var process in Processes)
                        {
                            foreach (ProcessThread Thread in process.Threads)
                            {
                                WIndowFunctIon.EnumThreadWindows(Thread.Id,
                                    (hwnd, lP) =>
                                    {
                                        if (WIndowFunctIon.IsWindowVisible(hwnd))
                                            HwndChecker.HwndLIst.Add(new(WIndowFunctIon.GetWindowText_(hwnd), hwnd));
                                        return true;
                                    }, IntPtr.Zero);
                            }
                        }

                        Console.Clear();
                        var table = new ConsoleTable("캡션", "IntPtr");
                        foreach (HwndClass hwnd in HwndChecker.HwndLIst)
                        {
                            table.AddRow(hwnd.HwndCaptIon, hwnd.hwnd.ToString("X"));
                        }
                        table.Write();
                        Console.WriteLine();

                        Thread.Sleep(1245);
                    }
                }
            }
            else
            {
                Process[] Processes = Process.GetProcessesByName(args[1].Replace(".exe", ""));

                foreach (var Process in Processes)
                {
                    if (Process.MainWindowHandle == IntPtr.Zero == false)
                    {
                        Console.WriteLine("JuHandle - " + Process.MainWindowHandle);
                        Console.WriteLine("JuTItle - " + Process.MainWindowTitle);
                    }

                    foreach (ProcessThread Thread in Process.Threads)
                    {
                        //Console.WriteLine("ThreadsId - " + Thread.Id);
                        EnumThreadWindows(Thread.Id,
                            (hwnd, lP) =>
                            {
                                //Console.WriteLine("WIndowHwnd - " + hwnd.ToString("X"));
                                if (GetWIndowStyle.UtIl.WIndowFunctIon.IsWindowVisible(hwnd))
                                    Console.WriteLine("WIndowHwnd - " + hwnd.ToString("X") + "(WIndowStyle - WS_V)");
                                else
                                    Console.WriteLine("WIndowHwnd - " + hwnd.ToString("X") + "(WIndowStyle - WS_NV)");
                                return true;
                            }, IntPtr.Zero);
                    }
                }
            }
        }
    }
}
