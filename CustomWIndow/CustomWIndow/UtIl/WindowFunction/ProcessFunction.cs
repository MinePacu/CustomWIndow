using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static class ProcessFunction
    {
        /// <summary>
        /// - 기능
        /// <br/>ㅤ지정된 창을 만든 스레드의 식별자와 창을 만든 프로세스의 식별자를 로드합니다.
        /// </summary>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);


        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenProcess(uint dwDesiredToken, bool bInheritHandle, int dwProcessId);

        [DllImport("Kernel32.dll", EntryPoint = "QueryFullProcessImageName", CharSet = CharSet.Unicode)]
        public static extern bool QueryFullProcessImageTItle(IntPtr hProcess, int dwFlags, StringBuilder lpExe, ref uint lpdwSize);

        [DllImport("Kernel32.dll", EntryPoint = "GetPackageFullName", CharSet = CharSet.Unicode)]
        public static extern bool GetPackageFullTItle(IntPtr hProcess, ref uint packageFullTItleLength, StringBuilder packageFullTItle);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ열려 있는 개체 핸들을 닫습니다.
        /// </summary>
        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(IntPtr hObject);


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
    }
}
