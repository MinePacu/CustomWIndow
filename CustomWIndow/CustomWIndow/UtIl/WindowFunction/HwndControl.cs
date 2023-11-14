using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static class HwndControl
    {
        /// <summary>
        /// 창을 포그라운드로 전환합니다.
        /// </summary>
        /// <param name="hWnd">포그라운드로 전환할 창 Address</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ지정한 창을 사용할 수 있는지를 로드합니다.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowEnabled(IntPtr hwnd);

        /// <summary>
        /// 창이 보이는 상태인지 확인합니다.
        /// </summary>
        /// <param name="hWnd">창 상태를 확인할 창 Address</param>
        /// <returns></returns>
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsWindowVisible(IntPtr hWnd);


        [DllImport("user32")]
        static extern uint GetWindowLong(IntPtr hwnd, int nIndex);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ해당 창이 WS_POPUP 속성을 가지는지 로드합니다.
        /// </summary>
        public static bool IsWIndowPopup(IntPtr hwnd) => (GetWindowLong(hwnd, -16) & 0x80000000L) == 0 == false;
    }
}
