using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

using CustomWIndow.UtIl.Enum;
using CustomWIndow.UtIl;
using CustomWIndow.UtIl.WindowFunction;

using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;

using WinRT.Interop;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExceptProgramWindow : Window
    {
        AppWindow WIndow;
        public List<ProcessColorChangeExcept> tempExceptLIst { get; set; } = new();

        public ExceptProgramWindow()
        {
            this.InitializeComponent();

            WIndow = WinUIFunction.GetAppWIndowForWIndow(this);

            WIndow.Title = "제외 프로그램 관리";
            WIndow.Resize(new(750, 400));
            WIndow.SetIcon(Path.Combine(Environment.CurrentDirectory, "asset/window-system.ico"));

            if (AppColorFunction.GetAppColor() == AppColor.Dark)
                Dwm.DwmSetWindowAttribute_(WindowNative.GetWindowHandle(this), DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);

            var apppresenter = (OverlappedPresenter) WIndow.Presenter;

            apppresenter.IsMaximizable = false;
            //apppresenter.IsResizable = false;
        }

        private async void AddExceptProgram_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog AddExceptProgramWindow = new()
            {
                XamlRoot = this.Content.XamlRoot,
                Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
                Title = "제외 프로그램 추가",
                PrimaryButtonText = "추가",
                CloseButtonText = "취소",

                DefaultButton = ContentDialogButton.Close,

                Content = new Pages.SubPages.AddExceptPage(),
            };

            var AddedExceptProgram = await AddExceptProgramWindow.ShowAsync();

            if (AddedExceptProgram == ContentDialogResult.Primary)
            {
                var AddExceptProgramPage = (Pages.SubPages.AddExceptPage)AddExceptProgramWindow.Content;

                //Debug.WriteLine("Debug Process String - " + AddExceptProgramPage.ProcessString);
                //Debug.WriteLine("Debug IsExceptBorderColorChange - " + AddExceptProgramPage.IsExceptBorderColorChange);
                //Debug.WriteLine("Debug IsExceptCaptionrColorChange - " + AddExceptProgramPage.IsExceptCaptionrColorChange);
                //Debug.WriteLine("Debug IsExceptCaptionTextColorChange - " + AddExceptProgramPage.IsExceptCaptionTextColorChange);

                ProcessColorChangeExcept tempExceptProgram = new(AddExceptProgramPage.ProcessString, AddExceptProgramPage.IsExceptBorderColorChange == false, AddExceptProgramPage.IsExceptCaptionrColorChange == false, AddExceptProgramPage.IsExceptCaptionTextColorChange == false);

                ProcessChecker.ProcessColorChangeExceptLIst.Add(tempExceptProgram);
                tempExceptLIst.Add(tempExceptProgram);

                ProcessExceptGrid.ItemsSource = null;
                ProcessExceptGrid.ItemsSource = ProcessChecker.ProcessColorChangeExceptLIst;
            }
        }

        private void RemoveExceptProgram_Click(object sender, RoutedEventArgs e)
        {
            var Index = ProcessExceptGrid.SelectedIndex;

            ProcessChecker.ProcessColorChangeExceptLIst.RemoveAt(Index);
            ProcessExceptGrid.ItemsSource = null;
            ProcessExceptGrid.ItemsSource = ProcessChecker.ProcessColorChangeExceptLIst;

            ConfIg.Instance.NonappLIst.RemoveAt(Index);
        }

        private void ProcessExceptGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count >= 1)
            {
                RemoveExceptProgram.IsEnabled = true;
            }
        }
    }
}
