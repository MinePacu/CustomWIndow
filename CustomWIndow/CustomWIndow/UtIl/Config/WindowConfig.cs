using CustomWIndow.UtIl.Enum;

namespace CustomWIndow.UtIl.Config
{
    /// <summary>
    /// - 클래스
    /// <br/>ㅤ프로그램 설정 창과 이 프로그램에 의해 관리되는 윈도우에 대한 Config 클래스
    /// </summary>
    internal class WindowConfig
    {
        /// <summary>
        /// 메인 윈도우의 가로
        /// </summary>
        public int MainWindowGaro { get; set; } = 1540;

        /// <summary>
        /// 메인 윈도우의 세로
        /// </summary>
        public int MainWindowSero { get; set; } = 770;

        /// <summary>
        /// 팝업 창에도 창 색을 적용할지 여부
        /// </summary>
        public bool IsUseContextPopupBorderColor { get; set; } = false;

        /// <summary>
        /// 창의 모서리 처리 옵션
        /// </summary>
        public DWM_WINDOW_CORNER_PREFERENCE WindowCornerOption { get; set; } = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DEFAULT;
    }
}
