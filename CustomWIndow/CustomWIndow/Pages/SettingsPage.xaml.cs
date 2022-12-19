// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;

using CustomWIndow.UtIl;
using CustomWIndow.UtIl.Enum;
using CustomWIndow.Windows;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public Color CaptionColor
        {
            get { return (Color)GetValue(CaptionColorProperty); }
            set { SetValue(CaptionColorProperty, value); }
        }

        public Color CaptionTextColor
        {
            get { return (Color)GetValue(CaptionTextColorProperty); }
            set { SetValue(CaptionTextColorProperty, value); }
        }

        public static readonly DependencyProperty BorderColorProperty = DependencyProperty.Register("BorderColor", typeof(Color), typeof(SettingsPage), null);
        public static readonly DependencyProperty CaptionColorProperty = DependencyProperty.Register("CatprionColor", typeof(Color), typeof(SettingsPage), null);
        public static readonly DependencyProperty CaptionTextColorProperty = DependencyProperty.Register("CaptionTextColor", typeof(Color), typeof(SettingsPage), null);


        public SettingsPage()
        {
            this.InitializeComponent();
			
            if (ConfIg.Instance.AppLIst.Count == 0 == false)
            {
                int tep = 0;
                foreach (string pro in ConfIg.Instance.AppLIst)
                {
                    if (tep == ConfIg.Instance.AppLIst.Count - 1)
                        Program_LIst.Text += pro;
                    else
                        Program_LIst.Text += pro + "\r";
                    tep++;
                }
            }

            if (ConfIg.Instance.NonappLIst.Count == 0 == false)
            {
                int tep = 0;
                foreach (string pro in ConfIg.Instance.NonappLIst)
                {
                    if (tep == ConfIg.Instance.NonappLIst.Count - 1)
                        NonProgram_LIst.Text += pro;
                    else
                        NonProgram_LIst.Text += pro + "\r";
                    tep++;
                }
            }

            SelectOptIon.SelectedIndex = ConfIg.Instance.ProcessCheckermode;
            IsBorderSystemColor.IsChecked = ConfIg.Instance.ColorConfIg.IsBorderSystemAccent;
            IsCaptIonSystemColor.IsChecked = ConfIg.Instance.ColorConfIg.IsCaptIonSystemAccent;

            CaptIonTextColormodebox.SelectedIndex = ConfIg.Instance.ColorConfIg.CaptIonTextColormode;

            IsCaptIonTextSystemColor.IsChecked = ConfIg.Instance.ColorConfIg.IsCaptIonTextSystemAccent;

            WIndowCornermodeCombo.SelectedIndex = (int) ConfIg.Instance.WIndowCornermode;

            TaskToggle.IsOn = ProcessChecker.IsTaskWork;

            BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
            CaptionColor = ConfIg.Instance.ColorConfIg.CaptIonColor_;
            CaptionTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor_;
        }

        async void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var toggle = (ToggleSwitch)sender;
            if (toggle.IsOn)
            {
                //UtIl.UtIl.Color = 0x00009514;

                if (SelectOptIon.SelectedIndex == 0)
                {
                    Program_LIst.IsReadOnly = true;

                    string[] lines = Program_LIst.Text.Split("\r");

                    if (lines.Length == 0)
                        return;

                    ConfIg.Instance.AppLIst.Clear();

                    foreach (string line in lines)
                    {
                        Debug.WriteLine("app - " + line);

                        if (line.Contains('/') == false)
                        {
                            UtIl.UtIl.ProcessLIst.Add(line);
                            ConfIg.Instance.AppLIst.Add(line);
                            UtIl.UtIl.Process_WIndow.Add(new(line, IntPtr.Zero, new(), true, false));
                        }

                        else
                        {
                            ConfIg.Instance.AppLIst.Add(line);
                            UtIl.UtIl.Process_WIndow.Add(new(line, IntPtr.Zero, new(), true, false, line.Contains("/b") == true, line.Contains("/c") == true, line.Contains("/t") == true));
                        }
                    }

                    if (IsBorderSystemColor.IsChecked == false)
                        UtIl.UtIl.Bordercolor = ConfIg.Instance.ColorConfIg.BorderColor;
                    else
                        UtIl.UtIl.Bordercolor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    if (IsCaptIonSystemColor.IsChecked == false)
                        UtIl.UtIl.CaptIonColor = ConfIg.Instance.ColorConfIg.CaptIonColor;
                    else
                        UtIl.UtIl.CaptIonColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    if (IsCaptIonTextSystemColor.IsChecked == false)
                        UtIl.UtIl.CaptIonTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor;
                    else
                        UtIl.UtIl.CaptIonTextColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    UtIl.UtIl.task = UtIl.UtIl.ConsumeTask(UtIl.UtIl.cts.Token);
                }
                else
                {
                    NonProgram_LIst.IsReadOnly = true;
                    SelectOptIon.IsEnabled = false;
                    if (ProcessChecker.IsTaskWork == false)
                    {
                        string[] lines = NonProgram_LIst.Text.Split("\r");

                        if (lines.Length == 0)
                            return;

                        ConfIg.Instance.NonappLIst.Clear();
                        ProcessChecker.ProcessColorChangeExceptLIst.Clear();

                        foreach (string line in lines)
                        {
                            Debug.WriteLine("Nonapp - " + line);

                            if (line.Contains('/') == false)
                            {
                                //UtIl.UtIl.ProcessLIst.Add(line);
                                ConfIg.Instance.NonappLIst.Add(line);
                                UtIl.ProcessChecker.ProcessColorChangeExceptLIst.Add(new(line, false));
                                //UtIl.UtIl.Process_WIndow.Add(new(line, IntPtr.Zero, new(), 1, 0));
                            }

                            else
                            {
                                string[] optIons = line.Split(" ");

                                ConfIg.Instance.NonappLIst.Add(line);
                                ProcessChecker.ProcessColorChangeExceptLIst.Add(new(optIons[0], line.Contains("/b") == false, line.Contains("/c") == false, line.Contains("/t") == false));
                            }
                        }

                        if (IsBorderSystemColor.IsChecked == false)
                            ProcessChecker.Bordercolor = ConfIg.Instance.ColorConfIg.BorderColor;
                        else
                            ProcessChecker.Bordercolor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                        if (IsCaptIonSystemColor.IsChecked == false)
                            ProcessChecker.CaptIonColor = ConfIg.Instance.ColorConfIg.CaptIonColor;
                        else
                            ProcessChecker.CaptIonColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                        if (IsCaptIonTextSystemColor.IsChecked == false)
                            ProcessChecker.CaptIonTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor;
                        else
                            ProcessChecker.CaptIonTextColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                        ProcessChecker.Corner_ConfIg = (DWM_WINDOW_CORNER_PREFERENCE)Enum.ToObject(typeof(DWM_WINDOW_CORNER_PREFERENCE), ConfIg.Instance.WIndowCornermode);

                        ProcessChecker.IsFIrstLoad = true;
                        ProcessChecker.IsTaskWork = true;
                        ProcessChecker.task = UtIl.ProcessChecker.ConsumeTask(ProcessChecker.cts.Token);
                    }
                }
            }

            else
            {
                toggle.IsEnabled = false;
                if (ProcessChecker.IsTaskWork)
                {
                    ProcessChecker.cts.Cancel();
                    await ProcessChecker.task;

                    ProcessChecker.cts.Dispose();
                    ProcessChecker.cts = new();

                    ProcessChecker.IsTaskWork = false;
                }

                else if (UtIl.UtIl.IsTaskWork)
                {
                    UtIl.UtIl.cts.Cancel();
                    await UtIl.UtIl.task;

                    UtIl.UtIl.cts.Dispose();
                    UtIl.UtIl.cts = new();

                    UtIl.UtIl.IsTaskWork = false;
                }

                toggle.IsEnabled = true;
                if (SelectOptIon.SelectedIndex == 0)
                    Program_LIst.IsReadOnly = false;
                else
                    NonProgram_LIst.IsReadOnly = false;

                SelectOptIon.IsEnabled = true;
            }
        }
		
        void SelectOptIon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectoptIon = (ComboBox)sender;
            if (selectoptIon.SelectedIndex == 1)
            {
                Program_Expander.IsExpanded = false;
                Program_Expander.IsEnabled = false;

                NonProgram_Expander.IsExpanded = true;
                NonProgram_Expander.IsEnabled = true;

                ConfIg.Instance.ProcessCheckermode = 1;
            }

            else
            {
                Program_Expander.IsExpanded = true;
                Program_Expander.IsEnabled = true;

                NonProgram_Expander.IsExpanded = false;
                NonProgram_Expander.IsEnabled = false;

                ConfIg.Instance.ProcessCheckermode = 0;
            }
        }

        void IsBorderSystemColor_Checked(object sender, RoutedEventArgs e)
        {
            var Checkbox = (CheckBox)sender;

            if (Checkbox.IsChecked == true)
            {
                borderColorbutton.IsEnabled = false;
                ConfIg.Instance.ColorConfIg.BorderColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());
                ConfIg.Instance.ColorConfIg.BorderColor_ = ColorConverter.GetAccentColor();
            }
            else
                borderColorbutton.IsEnabled = true;

            ConfIg.Instance.ColorConfIg.IsBorderSystemAccent = (bool)Checkbox.IsChecked;

            Debug.WriteLine("Debug Accent Color - " + ColorConverter.GetAccentColor().ToString());
            Debug.WriteLine("Debug Accent ColorRef - " + ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor()).ToString());
        }

        void IsCaptIonSystemColor_Checked(object sender, RoutedEventArgs e)
        {
            var Checkbox = (CheckBox)sender;

            if (Checkbox.IsChecked == true)
            {
                captIonColorbutton.IsEnabled = false;
                ConfIg.Instance.ColorConfIg.CaptIonColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());
                ConfIg.Instance.ColorConfIg.CaptIonColor_ = ColorConverter.GetAccentColor();
            }
            else
                captIonColorbutton.IsEnabled = true;

            ConfIg.Instance.ColorConfIg.IsCaptIonSystemAccent = (bool)Checkbox.IsChecked;

            Debug.WriteLine("Debug Accent Color - " + ColorConverter.GetAccentColor().ToString());
            Debug.WriteLine("Debug Accent ColorRef - " + ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor()).ToString());
        }

        void IsCaptIonTextSystemColor_Checked(object sender, RoutedEventArgs e)
        {
            var Checkbox = (CheckBox)sender;

            if (Checkbox.IsChecked == true)
            {
                captIonTextColorbutton.IsEnabled = false;
                ConfIg.Instance.ColorConfIg.CaptIonTextColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());
                ConfIg.Instance.ColorConfIg.CaptIonTextColor_ = ColorConverter.GetAccentColor();
            }
            else
                captIonTextColorbutton.IsEnabled = true;

            ConfIg.Instance.ColorConfIg.IsCaptIonTextSystemAccent = (bool)Checkbox.IsChecked;

            Debug.WriteLine("Debug Accent Color - " + ColorConverter.GetAccentColor().ToString());
            Debug.WriteLine("Debug Accent ColorRef - " + ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor()).ToString());
        }

        async void borderColorbutton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog ColorwIndow = new()
            {
                XamlRoot = this.Content.XamlRoot,
                Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
                Title = "테두리 색",
                PrimaryButtonText = "설정",
                CloseButtonText = "취소",

                DefaultButton = ContentDialogButton.Close,

                Content = new ScrollViewer()
                {
                    Width = 450,
                    Content = new ColorPicker()
                    {
                        ColorSpectrumShape = ColorSpectrumShape.Box,
                        IsMoreButtonVisible = true,
                        IsColorSliderVisible = true,
                        IsColorChannelTextInputVisible = true,
                        IsHexInputVisible = true,
                        IsAlphaEnabled = false,
                        IsAlphaSliderVisible = false,
                        IsAlphaTextInputVisible = false,
                        Color = ConfIg.Instance.ColorConfIg.BorderColor_
                    },
                }
            };

            var ColorR = await ColorwIndow.ShowAsync();

            if (ColorR == ContentDialogResult.Primary)
            {
                ConfIg.Instance.ColorConfIg.BorderColor = ColorConverter.ConvertToColorREF(((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.BorderColor_ = ((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color;
                BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color));
            }
        }

        async void captIonColorbutton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog ColorwIndow = new()
            {
                XamlRoot = this.Content.XamlRoot,
                Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
                Title = "캡션 색",
                PrimaryButtonText = "설정",
                CloseButtonText = "취소",

                DefaultButton = ContentDialogButton.Close,

                Content = new ScrollViewer()
                {
                    Width = 450,
                    Content = new ColorPicker()
                    {
                        ColorSpectrumShape = ColorSpectrumShape.Box,
                        IsMoreButtonVisible = true,
                        IsColorSliderVisible = true,
                        IsColorChannelTextInputVisible = true,
                        IsHexInputVisible = true,
                        IsAlphaEnabled = false,
                        IsAlphaSliderVisible = false,
                        IsAlphaTextInputVisible = false,
                        Color = ConfIg.Instance.ColorConfIg.CaptIonColor_
                    },
                }
            };

            var ColorR = await ColorwIndow.ShowAsync();

            if (ColorR == ContentDialogResult.Primary)
            {
                ConfIg.Instance.ColorConfIg.CaptIonColor = ColorConverter.ConvertToColorREF(((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.CaptIonColor_ = ((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color;
                CaptionColor = ConfIg.Instance.ColorConfIg.CaptIonColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color));
            }
        }

        async void captIonTextColorbutton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog ColorwIndow = new()
            {
                XamlRoot = this.Content.XamlRoot,
                Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
                Title = "캡션 텍스트 색",
                PrimaryButtonText = "설정",
                CloseButtonText = "취소",

                DefaultButton = ContentDialogButton.Close,

                Content = new ScrollViewer()
                {
                    Width = 450,
                    Content = new ColorPicker()
                    {
                        ColorSpectrumShape = ColorSpectrumShape.Box,
                        IsMoreButtonVisible = true,
                        IsColorSliderVisible = true,
                        IsColorChannelTextInputVisible = true,
                        IsHexInputVisible = true,
                        IsAlphaEnabled = false,
                        IsAlphaSliderVisible = false,
                        IsAlphaTextInputVisible = false,
                        Color = ConfIg.Instance.ColorConfIg.CaptIonTextColor_
                    },
                }
            };

            var ColorR = await ColorwIndow.ShowAsync();

            if (ColorR == ContentDialogResult.Primary)
            {
                ConfIg.Instance.ColorConfIg.CaptIonTextColor = ColorConverter.ConvertToColorREF(((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.CaptIonTextColor_ = ((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color;
                CaptionTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker) ((ScrollViewer)ColorwIndow.Content).Content).Color));
            }
        }

        void CaptIonTextColormodebox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            ConfIg.Instance.ColorConfIg.CaptIonTextColormode = combobox.SelectedIndex;

            if (combobox.SelectedIndex == 0)
            {
                CaptIonTextColormode.Text = "자동 - 윈도우 기본 설정입니다.";
                IsCaptIonTextSystemColor.IsEnabled = false;
                captIonTextColorbutton.IsEnabled = false;
            }
            else
            {
                CaptIonTextColormode.Text = "수동 - 사용자가 지정한 색상으로 캡션 텍스트 색상을 적용합니다.";
                IsCaptIonTextSystemColor.IsEnabled = true;

                if (IsCaptIonTextSystemColor.IsChecked == true)
                    captIonTextColorbutton.IsEnabled = false;
                else
                    captIonTextColorbutton.IsEnabled = true;
            }
        }

        void WIndowCornermodeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Combo = (ComboBox)sender;

            ConfIg.Instance.WIndowCornermode = (DWM_WINDOW_CORNER_PREFERENCE) Enum.ToObject(typeof(DWM_WINDOW_CORNER_PREFERENCE), Combo.SelectedIndex);
        }

        private void ExceptProgrammanage_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = NonProgram_LIst.Text.Split("\r");

            if (lines.Length == 0)
                return;

            ConfIg.Instance.NonappLIst.Clear();
            ProcessChecker.ProcessColorChangeExceptLIst.Clear();

            foreach (string line in lines)
            {
                if (line.Contains('/') == false)
                {
                    ConfIg.Instance.NonappLIst.Add(line);
                    UtIl.ProcessChecker.ProcessColorChangeExceptLIst.Add(new(line, false));
                }

                else
                {
                    string[] optIons = line.Split(" ");

                    ConfIg.Instance.NonappLIst.Add(line);
                    ProcessChecker.ProcessColorChangeExceptLIst.Add(new(optIons[0], line.Contains("/b") == false, line.Contains("/c") == false, line.Contains("/t") == false));
                }
            }

            var ExceptProgramWindow = new ExceptProgramWindow();
            ExceptProgramWindow.Activate();
        }
    }
}
