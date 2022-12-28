using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using CustomWIndow.UtIl.Enum;

using CustomWIndow.UtIl.WindowFunction;
using static CustomWIndow.UtIl.WIndowFunctIon;

namespace CustomWIndow.UtIl
{
    public static class ProcessChecker
    {
        public static CancellationTokenSource cts { get; set; } = new();
        public static Task task { get; set; }

        /// <summary>
        /// - 리스트
        /// <br/>ㅤ각 앱에 대한 핸들러을 저장하는 클래스의 리스트
        /// </summary>
        static List<ProcessHwnd> Process_WIndow_LIst { get; } = new(20);
        /// <summary>
        /// - 리스트
        /// <br/>ㅤ각 앱에 대한 색깔 설정 옵션을 저장하는 구조체의 리스트입니다. 각 요소의 <see cref="ProcessColorChangeExcept.IsBorderChange"/>, <see cref="ProcessColorChangeExcept.IsCaptIonChange"/>, <see cref="ProcessColorChangeExcept.IsCaptIonTextChange"/>가 모두 false이면 제외 프로그램으로 분류됩니다.
        /// </summary>
        public static List<ProcessColorChangeExcept> ProcessColorChangeExceptLIst { get; } = new();

        public static List<IntPtr> Process_tep { get; } = new();

        public static ProcessHwnd Taskbar { get; set; }

        public static bool IsFIrstLoad { get; set; } = true;
        public static bool IsTaskWork { get; set; } = false;
        static bool SkIp { get; set; } = false;
        public static int Bordercolor { get; set; }
        public static int CaptIonColor { get; set; }
        public static int CaptIonTextColor { get; set; }
        static int RefreshStack { get; set; } = 0;

        public static DWM_WINDOW_CORNER_PREFERENCE Corner_ConfIg { get; set; }

        public static IntPtr DesktopHwnd { get; } = HwndLoaderFunction.FindWindow(null, "Program Manager");

        /// <summary>
        /// - 기능 
        /// <br/>ㅤ각 프로세스에 대한 창 핸들러를 저장하고 창 모서리, 캡션, 캡션 텍스트 색을 지정합니다. 이 함수는 <see cref="ConsumeTask(CancellationToken)"/>에 의해 취소 토큰이 true가 아니면 반복합니다.
        /// </summary>
        /// <returns></returns>
        static async Task GetAndApplyHwnd()
        {
            // 프로세스 로드
            // 첫 Task 로드인지 체크 (IsFIrstLoad)
            // 첫 Task 로드임
            // 각 프로세스에 대하여
            // 제외 앱 체크
            // 제외 앱이면(모든 컬러 바꾸기가 false) 스킵 켜기
            // 스킵이 아니고 주 윈도우 핸들이 있는지 체크
            // 주 윈도우가 있으면 각 스레드의 윈도우 핸들러를 체크
            // 윈도우 핸들러가 WS_VISIBLE임
            // 커스텀 설정 리스트에 등록되지 않음
            // 리스트(Process_WIndow_LIst)에 등록
            // 등록됨
            // 커스텀 옵션 반영하여 등록

            // 첫 Task 로드가 아님
            // 각 프로세스에 대하여
            // 제외 앱 체크
            // 제외 앱이면 스킵 켜기
            // 스킵이 아니고 주 윈도우 핸들이 있는지 체크
            // 주 윈도우가 있으면 각 스레드의 윈도우 핸들러를 체크
            // 윈도우 핸들러가 WS_VISIBLE임
            // 리스트(Process_WIndow_LIst) 중복 확인
            // 리스트가 같은 Thread.Id이 이미 있음
            // 스킵
            // 같은 Thread.Id가 없음
            // 리스트에 추가

            // 리스트에 이미 닫힌 프로세스가 있는지 체크

            // 코드
            Process[] processes = Process.GetProcesses();

            if (IsFIrstLoad)
            {
                //IntPtr DesktopHwnd = GetDesktopWindow();
                //Process_WIndow_LIst.Add(new("바탕화면", 0, DesktopHwnd, false, true, false, false, false));
                foreach (Process process in processes)
                {
                    foreach (var nonprocess in ProcessColorChangeExceptLIst)
                    {
                        if (nonprocess.ProcessStrIng == process.ProcessName && nonprocess.IsBorderChange == false && nonprocess.IsCaptIonChange == false && nonprocess.IsCaptIonTextChange == false)
                            SkIp = true;
                    }

                    if (SkIp == false)
                    {
                        if (process.MainWindowHandle == IntPtr.Zero == false)
                        {
                            foreach (ProcessThread thread in process.Threads)
                            {
                                _ = EnumHwndFunction.EnumThreadWindows(thread.Id,
                                    (hwnd, lP) =>
                                    {
                                        // 바탕화면 넘기기
                                        if (hwnd == DesktopHwnd)
                                        {
                                            Process_WIndow_LIst.Add(new(process.ProcessName, thread.Id, hwnd, false, true, false, false, false));
                                            return true;
                                        }

                                        int Index = ProcessColorChangeExceptLIst.FindIndex(x => x.ProcessStrIng == process.ProcessName);

                                        if (IsWindowVisible(hwnd) == true)
                                        {
                                            if (process.ProcessName == "explorer")
                                            {
                                                string classTItle = StringUtIl.GetClassTItle(hwnd);
                                                if (classTItle == "Shell_TrayWnd")
                                                    Taskbar = new(process.ProcessName, thread.Id, hwnd, false, true, true, false, false);

                                                else if (classTItle == "TaskListThumbnailWnd")
                                                    Process_WIndow_LIst.Add(new(process.ProcessName, thread.Id, hwnd, false, true, true, false, false));

                                                if (IsWIndowPopup(hwnd))
                                                    return true;
                                            }

                                            if (Index == -1)
                                                Process_WIndow_LIst.Add(new(process.ProcessName, thread.Id, hwnd, false, true));
                                            else
                                                Process_WIndow_LIst.Add(new(process.ProcessName, thread.Id, hwnd, false, true, ProcessColorChangeExceptLIst[Index].IsBorderChange, ProcessColorChangeExceptLIst[Index].IsCaptIonChange, ProcessColorChangeExceptLIst[Index].IsCaptIonTextChange));
                                        }
                                        return true;
                                    }, IntPtr.Zero);
                            }
                        }
                    }
                    SkIp = false;
                }

                await ApplyBorderCaptIonColor(Bordercolor, CaptIonTextColor, CaptIonTextColor);

                IsFIrstLoad = false;
                //WrIteDebug("GetHwnd", Process_WIndow_LIst);
            }

            else
            {
                foreach (Process process in processes)
                {
                    foreach (var nonprocess in ProcessColorChangeExceptLIst)
                    {
                        if (nonprocess.ProcessStrIng == process.ProcessName && nonprocess.IsBorderChange == false && nonprocess.IsCaptIonChange == false && nonprocess.IsCaptIonTextChange == false)
                            SkIp = true;
                    }

                    if (SkIp == false)
                    {
                        if (process.MainWindowHandle == IntPtr.Zero == false)
                        {
                            foreach (ProcessThread thread in process.Threads)
                            {
                                _ = EnumHwndFunction.EnumThreadWindows(thread.Id,
                                    (hwnd, lP) =>
                                    {
                                        if (hwnd == DesktopHwnd)
                                            return true;

                                        if (IsWindowVisible(hwnd) == true)
                                        {
                                            if (process.ProcessName == "explorer" && IsWIndowPopup(hwnd))
                                                return true;

                                            else
                                            {
                                                //ProcessStrIngComparer ec = new();
                                                if (Process_WIndow_LIst.FindIndex(x => x.Hwnd == hwnd) == -1)
                                                {
                                                    var Index = ProcessColorChangeExceptLIst.FindIndex(x => x.ProcessStrIng == process.ProcessName);

                                                    if (Index == -1)
                                                    {
                                                        ProcessHwnd tep = new(process.ProcessName, thread.Id, hwnd, false, true);
                                                        ApplyBorderCaptIonColor(tep, Bordercolor, CaptIonColor, CaptIonTextColor);
                                                        Process_WIndow_LIst.Add(tep);
                                                    }
                                                    else
                                                    {
                                                        ProcessHwnd tep = new(process.ProcessName, thread.Id, hwnd, false, true, ProcessColorChangeExceptLIst[Index].IsBorderChange, ProcessColorChangeExceptLIst[Index].IsCaptIonChange, ProcessColorChangeExceptLIst[Index].IsCaptIonTextChange);
                                                        ApplyBorderCaptIonColor(tep, Bordercolor, CaptIonColor, CaptIonTextColor);
                                                        Process_WIndow_LIst.Add(tep);
                                                    }
                                                }
                                            }
                                            SkIp = false;
                                        }

                                        return true;
                                    }, IntPtr.Zero);
                            }
                        }
                    }
                }

                if (RefreshStack == 20)
                {
                    foreach (var hwnd in Process_WIndow_LIst)
                    {
                        if (IsWindowEnabled(hwnd.Hwnd) == false)
                            hwnd.used = false;
                    }

                    int tep = 0;
                    while (tep < Process_WIndow_LIst.Count)
                    {
                        if (Process_WIndow_LIst[tep].used == false)
                            _ = Process_WIndow_LIst.Remove(Process_WIndow_LIst[tep]);
                        else
                            tep++;
                    }
                    RefreshStack = 0;
                }
                RefreshStack++;
            }

            //WrIteDebug("GetHwnd ", Process_WIndow_LIst);
            //tep++;
            await Task.Delay(1770);
        }

        static async Task GetAndApplyHwndWIthEnumWindow()
        {
            _ = EnumHwndFunction.EnumWindows(
                (hwnd, lp) =>
                {
                    if (IsWindowVisible(hwnd) == false)
                        return true;

                    uint ThreadId = GetWindowThreadProcessId(hwnd, out int ProcessID);

                    if (IsFIrstLoad)
                    {
                        // 바탕화면 넘기기
                        if (hwnd == DesktopHwnd)
                        {
                            Process_WIndow_LIst.Add(new("Desktop", 1, hwnd, false, true, false, false, false));
                            return true;
                        }

                        using (var process = Process.GetProcessById(ProcessID))
                        {
                            foreach (var nonprocess in ProcessColorChangeExceptLIst)
                            {
                                if (nonprocess.ProcessStrIng == process.ProcessName && nonprocess.IsBorderChange == false && nonprocess.IsCaptIonChange == false && nonprocess.IsCaptIonTextChange == false)
                                    SkIp = true;
                            }
                            if (SkIp == false)
                            {
                                int Index = ProcessColorChangeExceptLIst.FindIndex(x => x.ProcessStrIng == process.ProcessName);

                                if (process.ProcessName == "explorer")
                                {
                                    string classTItle = StringUtIl.GetClassTItle(hwnd);
                                    if (classTItle == "Shell_TrayWnd" && ConfIg.Instance.EtcConfIg.IsTaskbarborder)
                                        Taskbar = new(process.ProcessName, (int) ThreadId, hwnd, false, true, true, false, false);

                                    if (IsWIndowPopup(hwnd))
                                        return true;
                                }

                                if (Index == -1)
                                    Process_WIndow_LIst.Add(new(process.ProcessName, (int)ThreadId, hwnd, false, true));
                                else
                                    Process_WIndow_LIst.Add(new(process.ProcessName, (int)ThreadId, hwnd, false, true, ProcessColorChangeExceptLIst[Index].IsBorderChange, ProcessColorChangeExceptLIst[Index].IsCaptIonChange, ProcessColorChangeExceptLIst[Index].IsCaptIonTextChange));
                            }
                        }
                        _ = ApplyBorderCaptIonColor(Bordercolor, CaptIonTextColor, CaptIonTextColor);
                        ApplyTaskbarOptions(Bordercolor, ConfIg.Instance.EtcConfIg.TaskbarBorderCornermode);
                        SkIp = false;
                    }

                    else
                    {
                        if (hwnd == DesktopHwnd)
                            return true;

                        using (var process = Process.GetProcessById(ProcessID))
                        {
                            foreach (var nonprocess in ProcessColorChangeExceptLIst)
                            {
                                if (nonprocess.ProcessStrIng == process.ProcessName && nonprocess.IsBorderChange == false && nonprocess.IsCaptIonChange == false && nonprocess.IsCaptIonTextChange == false)
                                    SkIp = true;
                            }

                            if (SkIp == false)
                            {
                                if (process.ProcessName == "explorer" && IsWIndowPopup(hwnd))
                                    return true;

                                else
                                {
                                    //ProcessStrIngComparer ec = new();
                                    if (Process_WIndow_LIst.FindIndex(x => x.Hwnd == hwnd) == -1)
                                    {
                                        var Index = ProcessColorChangeExceptLIst.FindIndex(x => x.ProcessStrIng == process.ProcessName);

                                        if (Index == -1)
                                        {
                                            ProcessHwnd tep = new(process.ProcessName, (int)ThreadId, hwnd, false, true);
                                            ApplyBorderCaptIonColor(tep, Bordercolor, CaptIonColor, CaptIonTextColor);
                                            Process_WIndow_LIst.Add(tep);
                                        }
                                        else
                                        {
                                            ProcessHwnd tep = new(process.ProcessName, (int)ThreadId, hwnd, false, true, ProcessColorChangeExceptLIst[Index].IsBorderChange, ProcessColorChangeExceptLIst[Index].IsCaptIonChange, ProcessColorChangeExceptLIst[Index].IsCaptIonTextChange);
                                            ApplyBorderCaptIonColor(tep, Bordercolor, CaptIonColor, CaptIonTextColor);
                                            Process_WIndow_LIst.Add(tep);
                                        }
                                    }
                                }
                            }
                        }
                        SkIp = false;
                    }
                    return true;
                }, IntPtr.Zero);

            if (RefreshStack == 7)
            {
                foreach (var hwnd in Process_WIndow_LIst)
                {
                    if (IsWindowEnabled(hwnd.Hwnd) == false)
                        hwnd.used = false;
                }

                int tep = 0;
                while (tep < Process_WIndow_LIst.Count)
                {
                    if (Process_WIndow_LIst[tep].used == false)
                        _ = Process_WIndow_LIst.Remove(Process_WIndow_LIst[tep]);
                    else
                        tep++;
                }
                RefreshStack = 0;
            }

            RefreshStack++;

            //Debug.WriteLine("Taskbar hwnd - " + Taskbar.Hwnd.ToString("X"));
            //WrIteDebug("GetHwnd ", Process_WIndow_LIst);
            IsFIrstLoad = false;
            await Task.Delay(1140);
        }

        /// <summary>
        /// - 기능
        /// <br/>ㅤ<see cref="Process_WIndow_LIst"/> 리스트에서 각 요쇼의 창의 모서리, 캡션, 캠션 텍스트 색을 지정합니다. 이전에 속성을 적용한 창은 적용하지 않으며, 모든 색상은 ColorREF 형식을 사용합니다.
        /// </summary>
        /// <param name="bcolor">지정할 창 모서리 색상</param>
        /// <param name="ccolor">지정할 캡션 색상</param>
        /// <param name="ctcolor">지정할 캡션 텍스트 색상</param>
#pragma warning disable CS1998 // 이 비동기 메서드에는 'await' 연산자가 없으며 메서드가 동시에 실행됩니다.
        static async Task ApplyBorderCaptIonColor(int bcolor, int ccolor, int ctcolor)
#pragma warning restore CS1998 // 이 비동기 메서드에는 'await' 연산자가 없으며 메서드가 동시에 실행됩니다.
        {
            foreach (ProcessHwnd hwnd in Process_WIndow_LIst)
            {
                if (hwnd.HwndApplyed == false)
                {
                    if (hwnd.IsBorderChange)
                        _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_BORDER_COLOR, bcolor);

                    if (hwnd.IsCaptIonChange)
                        _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_CAPTION_COLOR, ccolor);
                    else
                    {
                        if (AppthFunction.GetAppTh() == Appth.Dark)
                            _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);
                        else
                            _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, false);
                    }

                    if (hwnd.IsCaptIonTextChange)
                        _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_TEXT_COLOR, ctcolor);

                    _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_WINDOW_CORNER_PREFERENCE, Corner_ConfIg);

                    hwnd.HwndApplyed = true;
                }
            }
        }

        /// <summary>
        /// - 기능
        /// <br/>ㅤ창의 모서리, 캡션, 캠션 텍스트 색을 지정합니다. 이전에 속성을 적용한 창은 적용하지 않으며, 모든 색상은 ColorREF 형식을 사용합니다.
        /// </summary>
        /// <param name="hwnd">지정할 창 핸들러를 담은 클래스</param>
        /// <param name="bcolor">지정할 창 모서리 색상</param>
        /// <param name="ccolor">지정할 캡션 색상</param>
        /// <param name="ctcolor">지정할 캡션 텍스트 색상</param>
        static void ApplyBorderCaptIonColor(ProcessHwnd hwnd, int bcolor, int ccolor, int ctcolor)
        {
            if (hwnd.HwndApplyed == false)
            {
                if (hwnd.IsBorderChange)
                    _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_BORDER_COLOR, bcolor);

                if (hwnd.IsCaptIonChange)
                    _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_CAPTION_COLOR, ccolor);
                else
                {
                    if (AppthFunction.GetAppTh() == Appth.Dark)
                        _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);
                    else
                        _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, false);
                }

                if (hwnd.IsCaptIonTextChange)
                    _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_TEXT_COLOR, ctcolor);

                _ = Dwm.DwmSetWindowAttribute_(hwnd.Hwnd, DwmWIndowAttrIbute.DWMWA_WINDOW_CORNER_PREFERENCE, Corner_ConfIg);

                hwnd.HwndApplyed = true;
            }
        }

        static void ApplyTaskbarOptions(int bcolor, Taskbar_Corner TaskbarCorner)
        {
            if (Taskbar.HwndApplyed == false)
            {
                _ = Dwm.DwmSetWindowAttribute_(Taskbar.Hwnd, DwmWIndowAttrIbute.DWMWA_WINDOW_CORNER_PREFERENCE, (DWM_WINDOW_CORNER_PREFERENCE) System.Enum.ToObject(typeof(Taskbar_Corner), TaskbarCorner));
                _ = Dwm.DwmSetWindowAttribute_(Taskbar.Hwnd, DwmWIndowAttrIbute.DWMWA_BORDER_COLOR, bcolor);
                Taskbar.HwndApplyed = true;
            }
        }

        public static async Task ConsumeTask(CancellationToken cancel)
        {
            foreach (var task in ProduceTask(cancel))
            {
                //Debug.WriteLine("task - ");
                await task;
            }
        }

        static IEnumerable<Task> ProduceTask(CancellationToken cancel)
        {
            while (cancel.IsCancellationRequested == false)
            {
                yield return GetAndApplyHwndWIthEnumWindow();
            }
        }

        public static void WrIteDebug<T>(string TItle, List<T> LIst) where T : ProcessHwnd
        {
            Debug.WriteLine("===========================================================================");
            Debug.WriteLine(TItle);
            foreach (var t in LIst)
            {
                Debug.WriteLine(TItle + " : " + t.ProcessStrIng);
                Debug.WriteLine("ProcessThreadId" + " : " + t.ProcessThreadId);
                Debug.WriteLine("Hwnd" + " : " + t.Hwnd.ToString("X"));
                Debug.WriteLine("HwndApplyed" + " : " + t.HwndApplyed);
                Debug.WriteLine("IsBorderChange" + " : " + t.IsBorderChange);
                Debug.WriteLine("IsCaptIonChange" + " : " + t.IsCaptIonChange);
                Debug.WriteLine("IsCaptIonTextChange" + " : " + t.IsCaptIonTextChange);
                //Debug.WriteLine("WS_POPUP" + " : " + ((GetWindowLong(t.Hwnd, -16) & 0x80000000L) == 0 == false).ToString());
                Debug.WriteLine("");
            }

            foreach (var tep in Process_tep)
            {
                Debug.WriteLine("Hwnd : " + tep.ToString("X"));
            }

            Debug.WriteLine("Length - " + LIst.Count);
            Debug.WriteLine("Process_tep Length : " + Process_tep.Count);
            Debug.WriteLine("");
        }

    }

    /// <summary>
    /// - 클래스
    /// <br/>ㅤ프로세스의 창에 대한 멤버들을 가지는 클래스
    /// </summary>
    public class ProcessHwnd
    {
        /// <summary>
        /// - 멤버
        /// <br/>ㅤ프로세스 문자열을 로드하거나 저장합니다.
        /// </summary>
        public string ProcessStrIng { get; set; }

        /// <summary>
        /// - 멤버
        /// <br/>ㅤ스레드 아이디을 로드하거나 저장합니다.
        /// </summary>
        public int ProcessThreadId { get; set; }

        /// <summary>
        /// - 멤버
        /// <br/>ㅤ창 핸들러을 로드하거나 저장합니다.
        /// </summary>
        public IntPtr Hwnd { get; set; }

        /// <summary>
        /// - 멤버
        /// <br/>ㅤ창 속성이 적용되었는지를 로드하거나 저장합니다.
        /// </summary>
        public bool HwndApplyed { get; set; }

        /// <summary>
        /// - 멤버
        /// <br/>ㅤ이 프로세스를 이용할 수 있는지를 로드하거나 저장합니다.
        /// </summary>
        public bool used { get; set; }

        public bool IsBorderChange { get; set; } = true;

        public bool IsCaptIonChange { get; set; } = true;

        public bool IsCaptIonTextChange { get; set; } = true;

        /// <summary>
        /// - 기능
        /// <br/>ㅤ새 인스턴스를 만듭니다. 캡션 텍스트 색상 변경 방식이 자동이면 <see cref="IsCaptIonTextChange"/>가 false로 강제됩니다.
        /// </summary>
        public ProcessHwnd(string _ProcessStrIng, int _ProcessThreadId, IntPtr _Hwnd, bool _HwndApplyed, bool _used)
        {
            ProcessStrIng = _ProcessStrIng;
            ProcessThreadId = _ProcessThreadId;
            Hwnd = _Hwnd;
            HwndApplyed = _HwndApplyed;
            used = _used;
            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 0)
                IsCaptIonTextChange = false;
        }
        /// <summary>
        /// - 기능
        /// <br/>ㅤ새 인스턴스를 만듭니다. 캡션 텍스트 색상 변경 방식이 자동이면 _IsCaptIonTextChange는 무시되어 <see cref="IsCaptIonTextChange"/>가 false로 강제됩니다.
        /// </summary>
        public ProcessHwnd(string _ProcessStrIng, int _ProcessThreadId, IntPtr _Hwnd, bool _HwndApplyed, bool _used, bool _IsBorderChange, bool _IsCaptIonChange, bool _IsCaptIonTextChange)
        {
            ProcessStrIng = _ProcessStrIng;
            ProcessThreadId = _ProcessThreadId;
            Hwnd = _Hwnd;
            HwndApplyed = _HwndApplyed;
            used = _used;
            IsBorderChange = _IsBorderChange;
            IsCaptIonChange = _IsCaptIonChange;
            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 0)
                IsCaptIonTextChange = false;
            else
                IsCaptIonTextChange = _IsCaptIonTextChange;
        }
    }

    /// <summary>
    /// - 구조체
    /// <br/>ㅤ각 앱에 대한 색깔 설정 옵션을 저장하는 구조체입니다. <see cref="IsBorderChange"/>, <see cref="IsCaptIonChange"/>, <see cref="IsCaptIonTextChange"/>가 모두 false이면 제외 프로그램으로 분류됩니다.
    /// </summary>
    public struct ProcessColorChangeExcept
    {
        public string ProcessStrIng { get; set; }

        public bool IsBorderChange { get; set; } = true;

        public bool IsCaptIonChange { get; set; } = true;

        public bool IsCaptIonTextChange { get; set; } = true;

        /// <summary>
        /// - 기능
        /// <br/>ㅤ새 인스턴스를 만듭니다. 캡션 텍스트 색상 변경 방식이 자동이면 <see cref="IsCaptIonTextChange"/>가 false로 강제됩니다.
        /// </summary>
        public ProcessColorChangeExcept(string _ProcessStrIng, bool Default)
        {
            ProcessStrIng = _ProcessStrIng;
            IsBorderChange = Default;
            IsCaptIonChange = Default;
            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 0)
                IsCaptIonTextChange = false;
            else
                IsCaptIonTextChange = Default;
        }

        /// <summary>
        /// - 기능
        /// <br/>ㅤ새 인스턴스를 만듭니다. 캡션 텍스트 색상 변경 방식이 자동이면 _IsCaptIonTextChange는 무시되어 <see cref="IsCaptIonTextChange"/>가 false로 강제됩니다.
        /// </summary>
        public ProcessColorChangeExcept(string _ProcessStrIng, bool _IsBorderChange, bool _IsCaptIonChange, bool _IsCaptIonTextChange)
        {
            ProcessStrIng = _ProcessStrIng;
            IsBorderChange = _IsBorderChange;
            IsCaptIonChange = _IsCaptIonChange;
            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 0)
                IsCaptIonTextChange = false;
            else
                IsCaptIonTextChange = _IsCaptIonTextChange;
        }
    }
}
