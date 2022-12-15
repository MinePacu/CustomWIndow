using System;
using System.Runtime.InteropServices;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static partial class EnumHwndFunction
    {
        /// <summary>
        /// - 기능
        /// <br/>ㅤ각 창의 핸들을 차례로 <see cref="EnumThreadDelegate">애플리케이션 콜백 함수</see>에 전달하여 스레드와 연결된 모든 최상위 창을 열거합니다. 이 함수는 스레드와 연결된 모든 최상위 창을 열거했거나 콜백 함수가 false를 반환하면 중단합니다.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lP);

        public delegate bool EnumThreadDelegate(IntPtr hwnd, IntPtr lP);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ각 창의 핸들을 차례로 <see cref="EnumWindowsProc">애플리케이션 콜백 함수</see>에 전달하여 모든 최상위 창을 열거합니다. 이 함수는 모든 최상위 창을 열거했거나 콜백 함수가 false를 반환하면 중단합니다.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lP);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumWindowsProc lpEnumFunc, ref IntPtr lParam);

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    }
}
