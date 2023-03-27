using System;
using System.Runtime.InteropServices;
using System.Text;

using CustomWIndow.UtIl.Enum;

namespace CustomWIndow.UtIl
{
    public static partial class WIndowFunctIon
    {
        [DllImport("user32")]
        static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        public const long SYNCHRONIZE = (0x00100000L);
        public const int STANDARD_RIGHTS_REQUIRED = (0x000F0000);

        public const int PROCESS_VM_READ = (0x0010);
        public const int PROCESS_VM_WRITE = (0x0020);
        public const int PROCESS_DUP_HANDLE = (0x0040);
        public const int PROCESS_CREATE_PROCESS = (0x0080);
        public const int PROCESS_SET_QUOTA = (0x0100);
        public const int PROCESS_SET_INFORMATION = (0x0200);
        public const int PROCESS_QUERY_INFORMATION = (0x0400);
        public const int PROCESS_SUSPEND_RESUME = (0x0800);
        public const int PROCESS_QUERY_LIMITED_INFORMATION = (0x1000);
        public const int PROCESS_SET_LIMITED_INFORMATION = (0x2000);
        public const long PROCESS_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFFF);

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenProcess(uint dwDesiredToken, bool bInheritHandle, int dwProcessId);

        [DllImport("Kernel32.dll", EntryPoint = "QueryFullProcessImageName", CharSet = CharSet.Unicode)]
        public static extern bool QueryFullProcessImageTItle(IntPtr hProcess, int dwFlags, StringBuilder lpExe, ref uint lpdwSize);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ열려 있는 개체 핸들을 닫습니다.
        /// </summary>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ지정된 창을 만든 스레드의 식별자와 창을 만든 프로세스의 식별자를 로드합니다.
        /// </summary>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("Kernel32.dll", EntryPoint = "GetPackageFullName", CharSet = CharSet.Unicode)]
        public static extern bool GetPackageFullTItle(IntPtr hProcess, ref uint packageFullTItleLength, StringBuilder packageFullTItle);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ해당 창이 WS_POPUP 속성을 가지는지 로드합니다.
        /// </summary>
        public static bool IsWIndowPopup(IntPtr hwnd) => (GetWindowLong(hwnd, -16) & 0x80000000L) == 0 == false;

        /// <summary>
        /// - 기능
        /// <br/>ㅤ지정한 창을 사용할 수 있는지를 로드합니다.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        [DllImport("user32")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out uint ColorizationColor, [MarshalAs(UnmanagedType.Bool)] out bool ColorizationOpaqueBlend);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);


        [DllImport("user32.dll")]
        public static extern IntPtr CreateWindowEx(uint dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool KillTimer(IntPtr hWnd, IntPtr uIDEvent);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);
        public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

        // or alternatively
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, IntPtr lpTimerFunc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(HandleRef windowHandle, uint message, IntPtr wordParameter, IntPtr longParameter);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);
    }
}
