using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages.Controls
{
    /// <summary>
    /// - ��Ʈ��
    /// <br/>�Ի� ���ñ⿡ ���̴� ����� ���� ��Ʈ��
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
        /// <br/>�ԸŰ� ������ ������ ������ �⺻���� �ϴ� ColorPicker�� ����ϴ�.
        /// </summary>
        /// <param name="DefaultColorData"></param>
        public ColorPickerControl(Color DefaultColorData)
        {
            this.InitializeComponent();
            ColorData = DefaultColorData;
        }
    }
}
