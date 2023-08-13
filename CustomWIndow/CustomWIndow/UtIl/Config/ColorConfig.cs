using Windows.UI;

namespace CustomWIndow.UtIl.Config
{
    internal class ColorConfig
    {
        public bool IsBorderSystemAccent { get; set; } = false;
        public bool IsBorderColorTransparency { get; set; } = false;
        public bool IsCaptIonSystemAccent { get; set; } = false;
        public bool IsCaptionColorTransparency { get; set; } = false;
        public bool IsCaptIonTextSystemAccent { get; set; } = false;
        public bool IsCaptionTextColorTransparency { get; set; } = false;
        /// <summary>
        /// - 멤버 
        /// <br/>ㅤ창 모서리 색상을 로드하거나 설정합니다. ColorRef 형식을 사용합니다.
        /// </summary>
        public int BorderColor { get; set; } = 1;
        /// <summary>
        /// - 멤버 
        /// <br/>ㅤ창 캡션 색상을 로드하거나 설정합니다. ColorRef 형식을 사용합니다.
        /// </summary>
        public int CaptIonColor { get; set; } = 1;
        /// <summary>
        /// - 멤버 
        /// <br/>ㅤ창 캡션 텍스트 색상을 로드하거나 설정합니다. ColorRef 형식을 사용합니다.
        /// </summary>
        public int CaptIonTextColor { get; set; } = 1;
        public Color BorderColor_ { get; set; } = new();
        public Color CaptIonColor_ { get; set; } = new();
        public Color CaptIonTextColor_ { get; set; } = new();

        /// <summary>
        /// - 멤버 
        /// <br/>ㅤ창 캡션 텍스트 색상의 변경 방법을 로드하거나 설정합니다. 0은 자동, 1은 수동입니다.
        /// </summary>
        public int CaptIonTextColormode { get; set; } = 0;

        public bool IsOnMasterToggleOfBorderWindow { get; set; } = true;
        public bool IsOnMasterToggleOfCaptionWindow { get; set; } = true;
    }
}
