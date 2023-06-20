using CustomWIndow.UtIl.Enum;

namespace CustomWIndow.UtIl.Config
{
    /// <summary>
    /// - 클래스
    /// <br/>ㅤ작업 표시줄에 대한 Config 클래스
    /// </summary>
    internal class TaskBarConfig
    {
        /// <summary>
        /// - 옵션
        /// <br/>ㅤ작업 표시줄에 모서리 변경 기능 사용 여부
        /// </summary>
        public bool IsTaskbarborder { get; set; } = true;
        /// <summary>
        /// - 옵션
        /// <br/>ㅤ작업 표시줄에 모서리 색 표시 여부
        /// </summary>
        public bool IsTaskbarBorderColor { get; set; } = false;

        /// <summary>
        /// - 옵션
        /// <br/>ㅤ작업 표시줄의 모서리 둥글게 옵션
        /// </summary>
        public Taskbar_Corner TaskbarBorderCornermode { get; set; } = 0;
    }
}
