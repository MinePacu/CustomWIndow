using System.Runtime.InteropServices;
using System.Text;

namespace GetWIndowStyle.UtIl
{
    public static class WIndowFunctIon
    {
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lP);

        public delegate bool EnumThreadDelegate(IntPtr hwnd, IntPtr lP);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClass, string lpTItle);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpstring, int max);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ각 창의 핸들을 차례로 애플리케이션 콜백 함수에 전달하여 모든 최상위 창을 열거합니다. 이 함수는 모든 최상위 창을 열거했거나 콜백 함수가 false를 반환하면 중단합니다.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, ref IntPtr lP);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpEnumFunc, ref IntPtr lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, ref IntPtr lParam);

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

        [DllImport("Kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ지정된 창을 만든 스레드의 식별자와 창을 만든 프로세스의 식별자를 로드합니다.
        /// </summary>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("User32.dll", EntryPoint = "GetClassName", CharSet = CharSet.Unicode)]
        public static extern int GetClassTItle(IntPtr hWnd, StringBuilder lpClassTItle, int nMaxCount);

        [DllImport("Kernel32.dll", EntryPoint = "GetPackageFullName", CharSet = CharSet.Unicode)]
        public static extern bool GetPackageFullTItle(IntPtr hProcess, ref uint packageFullTItleLength, StringBuilder packageFullTItle);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ클래스와 창이 지정된 문자열에 해당하는 창에 대한 핸들을 호드합니다. 이 함수는 지정된 하위 창 다음에 있는 하위 창부터 하위 창을 로드합니다.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attr_ref, int attrSIze);
    }
}
