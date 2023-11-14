using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using CustomWIndow.UtIl.WindowFunction;

namespace CustomWIndow.UtIl
{
    [Obsolete]
    public static class UWPHwndLoader
    {
        public static List<Hwndstruct> UWPHwndLIst { get; } = new();

        public static bool ListWindows(IntPtr hWnd, ref IntPtr lParam)
        {
            var ep = (Hwndstruct)Marshal.PtrToStructure(lParam, typeof(Hwndstruct));

            StringBuilder sbClassTItle = new StringBuilder(260);
            string sClassTItle = null;

            StringUtIl.GetClassTItle(hWnd);
            sClassTItle = sbClassTItle.ToString();

            // 최소화
            if (sClassTItle == "Windows.UI.Core.CoreWindow")
            {
                int nPID = 0;
                uint nThreadId = WindowFunction.ProcessFunction.GetWindowThreadProcessId(hWnd, out nPID);
                IntPtr hProcess = ProcessFunction.OpenProcess(ProcessFunction.PROCESS_QUERY_LIMITED_INFORMATION, false, nPID);
                string sPackage = string.Empty;
                if (hProcess != IntPtr.Zero)
                {
                    uint nSize = 260;
                    StringBuilder sPackageTItle = new StringBuilder((int)nSize);
                    ProcessFunction.GetPackageFullTItle(hProcess, ref nSize, sPackageTItle);

                    nSize = 260;
                    StringBuilder sProcessImageTItle = new StringBuilder((int)nSize);
                    ProcessFunction.QueryFullProcessImageTItle(hProcess, 0, sProcessImageTItle, ref nSize);

                    ep.hWnd = hWnd;
                    ep.sExe = sProcessImageTItle.ToString();
                    ep.nPID = nPID;
                    ep.nState = 1;
                    Marshal.StructureToPtr(ep, lParam, false);

                    UWPHwndLIst.Add(ep);

                    ProcessFunction.CloseHandle(hProcess);
                }
            }

            // 일반
            if (sClassTItle == "ApplicationFrameWindow")
            {
                IntPtr hWndFind = HwndLoaderFunction.FindWindowEx(hWnd, IntPtr.Zero, "Windows.UI.Core.CoreWindow", null);
                if (hWndFind != IntPtr.Zero)
                {
                    int nPID = 0;
                    uint nThreadId = ProcessFunction.GetWindowThreadProcessId(hWndFind, out nPID);
                    IntPtr hProcess = ProcessFunction.OpenProcess(ProcessFunction.PROCESS_QUERY_LIMITED_INFORMATION, false, nPID);
                    string sPackage = string.Empty;
                    if (hProcess != IntPtr.Zero)
                    {
                        uint nSize = 260;
                        StringBuilder sPackageFullTItle = new StringBuilder((int)nSize);
                        ProcessFunction.GetPackageFullTItle(hProcess, ref nSize, sPackageFullTItle);
                        nSize = 260;
                        StringBuilder sProcessImageTItle = new StringBuilder((int)nSize);
                        ProcessFunction.QueryFullProcessImageTItle(hProcess, 0, sProcessImageTItle, ref nSize);

                        ep.hWnd = hWnd;
                        ep.sExe = sProcessImageTItle.ToString();
                        ep.nPID = nPID;
                        Marshal.StructureToPtr(ep, lParam, false);

                        UWPHwndLIst.Add(ep);

                        ProcessFunction.CloseHandle(hProcess);
                    }
                }
            }
            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Hwndstruct
        {
            public IntPtr hWnd { get; set; }
            public int nPID { get; set; }
            public string sExe { get; set; }
            public int nState { get; set; }
        }
    }
} 
