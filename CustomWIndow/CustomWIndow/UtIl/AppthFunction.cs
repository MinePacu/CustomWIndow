using CustomWIndow.UtIl.Enum;
using Microsoft.Win32;

namespace CustomWIndow.UtIl
{
    public static class AppthFunction
    {
        internal const string HKeyWIndowsAppTh = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        /// <summary>
        /// - 기능
        /// <br/>ㅤ시스템 앱 테마를 로드합니다.
        /// </summary>
        /// <returns>시스템 앱 테마 (다크 또는 라이트)</returns>
        public static Appth GetAppTh()
        {
            int res = (int)Registry.GetValue(HKeyWIndowsAppTh, "AppsUseLightTheme", -1);
            //Debug.WriteLine("res - " + res);

            if (res == 0)
                return Appth.Dark;
            else
                return Appth.Light;
        }
    }
}
