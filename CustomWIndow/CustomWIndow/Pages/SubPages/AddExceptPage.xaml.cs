using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages.SubPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddExceptPage : Page
    {
        public string ProcessString { get; set; }

        public bool IsExceptBorderColorChange { get; set; }
        public bool IsExceptCaptionrColorChange { get; set; }
        public bool IsExceptCaptionTextColorChange { get; set; }

        public AddExceptPage()
        {
            this.InitializeComponent();
        }
    }
}
