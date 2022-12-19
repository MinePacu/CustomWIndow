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
        /// - ���
        /// <br/>�Կ��� �ִ� ��ü �ڵ��� �ݽ��ϴ�.
        /// </summary>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// - ���
        /// <br/>�������� â�� ���� �������� �ĺ��ڿ� â�� ���� ���μ����� �ĺ��ڸ� �ε��մϴ�.
        /// </summary>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("Kernel32.dll", EntryPoint = "GetPackageFullName", CharSet = CharSet.Unicode)]
        public static extern bool GetPackageFullTItle(IntPtr hProcess, ref uint packageFullTItleLength, StringBuilder packageFullTItle);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// - ���
        /// <br/>���ش� â�� WS_POPUP �Ӽ��� �������� �ε��մϴ�.
        /// </summary>
        public static bool IsWIndowPopup(IntPtr hwnd) => (GetWindowLong(hwnd, -16) & 0x80000000L) == 0 == false;

        /// <summary>
        /// - ���
        /// <br/>�������� â�� ����� �� �ִ����� �ε��մϴ�.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowEnabled(IntPtr hwnd);
    }
}
