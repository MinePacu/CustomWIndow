using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages.Controls
{
    /// <summary>
    /// - 컨트롤
    /// <br/>ㅤ색 선택기에 쓰이는 사용자 지정 컨트롤
    /// </summary>
    public sealed partial class ColorPickerControl : UserControl
    {
        public Color ColorData { get; set; }

        public ColorPickerControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// - Initializer
        /// <br/>ㅤ매개 변수에 지정된 색깔을 기본으로 하는 ColorPicker를 만듭니다.
        /// </summary>
        /// <param name="DefaultColorData"></param>
        public ColorPickerControl(Color DefaultColorData)
        {
            this.InitializeComponent();
            ColorData = DefaultColorData;
        }
    }
}
