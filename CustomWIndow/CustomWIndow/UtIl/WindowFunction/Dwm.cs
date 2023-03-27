using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using CustomWIndow.UtIl.Enum;

using Microsoft.Windows.AppLifecycle;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static partial class Dwm
    {
        /// <summary>
        /// - 기능
        /// <br/>ㅤ창에 대한 dwm 비 클라이언트 렌더링 특성을 설정합니다. 특성은 attr과 attr_ref로 설정합니다.
        /// </summary>
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWIndowAttrIbute attr, ref int attr_ref, int attrSIze);


        /// <summary>
        /// - 기능
        /// <br/>ㅤ창에 대한 dwm 비 클라이언트 렌더링 특성을 설정합니다. 특성은 attr과 attr_ref로 설정합니다. attr 목록과 attr에 따른 attr_ref의 타입은 <see cref="DwmWIndowAttrIbute"/>를 참조하세요.
        /// </summary>
        /// <param name="hwnd">특성을 설정할 창에 대한 핸들</param>
        /// <param name="dwAttribute">설정할 특성을 지정하는 열거형</param>
        /// <param name="attr_ref">설정할 특성의 값입니다. 이에 대한 포인터는 함수 내에서 자동으로 변환합니다.</param>
        public static int DwmSetWindowAttribute_(IntPtr hwnd, DwmWIndowAttrIbute dwAttribute, int attr_ref)
        {
            if (dwAttribute == DwmWIndowAttrIbute.DWMWA_BORDER_COLOR || dwAttribute == DwmWIndowAttrIbute.DWMWA_CAPTION_COLOR || dwAttribute == DwmWIndowAttrIbute.DWMWA_TEXT_COLOR)
            {
                int attrSIze = sizeof(int);
                return DwmSetWindowAttribute(hwnd, dwAttribute, ref attr_ref, attrSIze);
            }
            else
                return -1;
        }

        /// <summary>
        /// - 기능
        /// <br/>ㅤ창에 대한 dwm 비 클라이언트 렌더링 특성을 설정합니다. 특성은 attr과 attr_ref로 설정합니다. attr 목록과 attr에 따른 attr_ref의 타입은 <see cref="DwmWIndowAttrIbute"/>를 참조하세요.
        /// </summary>
        /// <param name="hwnd">특성을 설정할 창에 대한 핸들</param>
        /// <param name="dwAttribute">설정할 특성을 지정하는 열거형</param>
        /// <param name="attr_ref">설정할 특성의 값입니다. 이에 대한 포인터는 함수 내에서 자동으로 변환합니다.</param>
        public static int DwmSetWindowAttribute_(IntPtr hwnd, DwmWIndowAttrIbute dwAttribute, bool attr_ref)
        {
            if (dwAttribute == DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE)
            {
                int attrSIze = sizeof(int);
                int _attr_ref = Convert.ToInt32(attr_ref);
                return DwmSetWindowAttribute(hwnd, dwAttribute, ref _attr_ref, attrSIze);
            }

            else
                return -1;
        }

        /// <summary>
        /// - 기능
        /// <br/>ㅤ창에 대한 dwm 비 클라이언트 렌더링 특성을 설정합니다. 특성은 attr과 attr_ref로 설정합니다. attr 목록과 attr에 따른 attr_ref의 타입은 <see cref="DwmWIndowAttrIbute"/>를 참조하세요.
        /// </summary>
        /// <param name="hwnd">특성을 설정할 창에 대한 핸들</param>
        /// <param name="dwAttribute">설정할 특성을 지정하는 열거형</param>
        /// <param name="attr_ref">설정할 특성의 값입니다. 이에 대한 포인터는 함수 내에서 자동으로 변환합니다.</param>
        public static int DwmSetWindowAttribute_(IntPtr hwnd, DwmWIndowAttrIbute dwAttribute, DWM_WINDOW_CORNER_PREFERENCE attr_ref)
        {
            if (dwAttribute == DwmWIndowAttrIbute.DWMWA_WINDOW_CORNER_PREFERENCE)
            {
                int attrSIze = sizeof(int);
                int _attr_ref = Convert.ToInt32(attr_ref);
                return DwmSetWindowAttribute(hwnd, dwAttribute, ref _attr_ref, attrSIze);
            }

            else
                return -1;
        }


        public static async void RestartDWMwithProgram()
        {
            Process proc = new();
            ProcessStartInfo procs = new()
            {
                FileName = "cmd.exe",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };
            
            proc.StartInfo = procs;
            proc.Start();

            using (proc.StandardInput)
            {
                proc.StandardInput.Write("taskkill -f -Im dwm.exe" + Environment.NewLine);
                proc.StandardInput.Write("starţ dwm.exe" + Environment.NewLine);
            }

            proc.Close();

            ConfIg.Save();
            await Task.Delay(1340);

            _ = AppInstance.Restart("");
        }
    }
}
