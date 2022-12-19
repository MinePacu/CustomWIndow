using System;
using System.Runtime.InteropServices;

namespace CustomWIndow.UtIl.WindowFunction
{
    /// <summary>
    /// - 클래스
    /// <br/>ㅤ윈도우 관련 클래스 중에서 창의 핸들를 로드하는 함수들을 모아놓은 클래스
    /// </summary>
    public static class HwndLoaderFunction
    {
        /// <summary>
        /// - 기능
        /// <br/>ㅤ클래스 또는 캡션 문자열과 일치하는 최상위 창에 대한 핸들을 검색하여 로드합니다. 이 함수는 하위 창을 검색하지 않으며 대소문자를 구분하지 않습니다.
        /// <br/>ㅤ지정된 하위 창을 로드하려면 <see cref="FindWindowEx(nint, nint, string, string)"/> 함수를 사용하세요.
        /// </summary>
        /// <param name="lpClass">로드하려는 창의 클래스 문자열입니다. 이 인스턴스가 null이면 캡션이 <paramref name="lpTItle"/>인 창을 로드합니다.</param>
        /// <param name="lpTItle">로드하려는 창의 캡션 문자열입니다. 이 인스턴스가 null이면 창의 클래스가 <paramref name="lpClass"/>인 창을 로드합니다.</param>
        /// <returns>지정한 클래스 또는 캡션 문자열에 해당하는 창의 핸들</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClass, string lpTItle);

        /// <summary>
        /// - 기능
        /// <br/>ㅤ클래스와 창이 지정된 문자열에 해당하는 창에 대한 핸들을 호드합니다. 이 함수는 지정된 하위 창 다음에 있는 하위 창부터 하위 창을 로드합니다.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();

    }
}
