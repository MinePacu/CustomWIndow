// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;

using CustomWIndow.Interfaces.Views;
using CustomWIndow.UtIl;
using CustomWIndow.UtIl.Enum;
using CustomWIndow.Windows;

using Microsoft.UI.Windowing;
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

            WIndowCornermodeCombo.SelectedIndex = (int) ConfIg.Instance.WindowConfig.WindowCornerOption;

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

                    HwndCheckerWithWrapper.cts = new();

                    //UtIl.UtIl.task = UtIl.UtIl.ConsumeTask(UtIl.UtIl.cts.Token);
                    HwndCheckerWithWrapper.BackgroundTask = HwndCheckerWithWrapper.StartBackgroundTask(HwndCheckerWithWrapper.cts.Token, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.R, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.G, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.B);
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

                        ProcessChecker.Corner_ConfIg = (DWM_WINDOW_CORNER_PREFERENCE)Enum.ToObject(typeof(DWM_WINDOW_CORNER_PREFERENCE), ConfIg.Instance.WindowConfig.WindowCornerOption);

                        ProcessChecker.IsFIrstLoad = true;
                        ProcessChecker.IsTaskWork = true;

                        HwndCheckerWithWrapper.cts = new();
                        //ProcessChecker.task = UtIl.ProcessChecker.ConsumeTask(ProcessChecker.cts.Token);
                        HwndCheckerWithWrapper.BackgroundTask = HwndCheckerWithWrapper.StartBackgroundTask(HwndCheckerWithWrapper.cts.Token,
                        UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.R, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.G, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.B);
                    }
                }
            }

            else
            {
                toggle.IsEnabled = false;
                /*
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

                */

                ProcessChecker.IsTaskWork = false;
                HwndCheckerWithWrapper.cts.Cancel();
                await HwndCheckerWithWrapper.BackgroundTask;

                // TaskOn 시키는 토글 UI (IView) 활성화
                toggle.IsEnabled = true;

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

        void WIndowCornermodeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Combo = (ComboBox)sender;

            ConfIg.Instance.WindowConfig.WindowCornerOption = (DWM_WINDOW_CORNER_PREFERENCE) Enum.ToObject(typeof(DWM_WINDOW_CORNER_PREFERENCE), Combo.SelectedIndex);
        }

        private async void ExceptProgrammanage_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessChecker.IsTaskWork == false || UtIl.UtIl.IsTaskWork == false)
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
            }

            NonProgram_LIst.IsEnabled = false; 

            var ExceptProgramWindow = new ExceptProgramWindow();
            ExceptProgramWindow.Activate();

            ExceptProgramWindow.Closed += delegate
            {
                NonProgram_LIst.IsEnabled = true;

                foreach (var ExceptProgram in ExceptProgramWindow.tempExceptLIst)
                {
                    string Line = "\r" + ExceptProgram.ProcessStrIng + " " + (ExceptProgram.IsBorderChange == false ? "/b " : "") + (ExceptProgram.IsCaptIonChange == false ? "/c " : "") + (ExceptProgram.IsCaptIonTextChange == false ? "/t " : "");
                    ConfIg.Instance.NonappLIst.Add(Line.Replace("\r", ""));
                }

                NonProgram_LIst.Text = "";

                int tep = 0;
                foreach (var Nonapp in ConfIg.Instance.NonappLIst)
                {
                    if (tep == ConfIg.Instance.NonappLIst.Count - 1)
                        NonProgram_LIst.Text += Nonapp;
                    else
                        NonProgram_LIst.Text += Nonapp + "\r";
                    tep++;
                }

                ExceptProgramWindow.Content = null;
                ExceptProgramWindow = null;
            };
        }
    }
}
