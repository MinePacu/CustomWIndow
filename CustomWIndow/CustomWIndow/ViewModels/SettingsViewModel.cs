using System;
using System.Diagnostics;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using CustomWIndow.UtIl;
using CustomWIndow.UtIl.Enum;

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

using Windows.UI;
using CustomWIndow.Interfaces.Views;
using CustomWIndow.Interfaces;

namespace CustomWIndow.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private int _ProcessCheckermode;

        public int ProcessCheckermode
        {
            get => _ProcessCheckermode;
            set
            {
                SetProperty(ref _ProcessCheckermode, value);
                ConfIg.Instance.ProcessCheckermode = value;

                if (value == 1)
                {
                    // 프로그램 리스트 TextBlock를 담은 Expander(IView) Expanded 를 false로 전환
                    IExpander.ExpanderList[0].IsExpanded = false;
                    // 프로그램 리스트 TextBlock를 담은 Expander(IView) Enabled 를 false로 전환
                    IExpander.ExpanderList[0].IsEnabled = false;

                    // 논 프로그램 리스트 TextBlock를 담은 Expander(IView) Expanded 를 true로 전환
                    IExpander.ExpanderList[1].IsExpanded = true;
                    // 논 프로그램 리스트 TextBlock를 담은 Expander(IView) Enabled를 true로 전환
                    IExpander.ExpanderList[1].IsEnabled = true;
                }

                // else는 value == 1의 반대
                else
                {
                    // 프로그램 리스트 TextBlock를 담은 Expander(IView) Expanded 를 false로 전환
                    IExpander.ExpanderList[0].IsExpanded = true;
                    // 프로그램 리스트 TextBlock를 담은 Expander(IView) Enabled 를 false로 전환
                    IExpander.ExpanderList[0].IsEnabled = true;

                    // 논 프로그램 리스트 TextBlock를 담은 Expander(IView) Expanded 를 true로 전환
                    IExpander.ExpanderList[1].IsExpanded = false;
                    // 논 프로그램 리스트 TextBlock를 담은 Expander(IView) Enabled를 true로 전환
                    IExpander.ExpanderList[1].IsEnabled = false;
                }
            }
        }

        // ---------------------------------------------------------------------

        private bool _IsBorderSystemColor;

        // nullable 속성을 지정할 수 있음
        public bool IsBorderSystemColor
        {
            get => _IsBorderSystemColor;
            set
            {
                SetProperty(ref _IsBorderSystemColor, value);
                ConfIg.Instance.ColorConfIg.IsBorderSystemAccent = value;

                if (value)
                {
                    // 모서리 색상을 시스템 색상으로 설정하도록 한 것이므로 모서리 색 설정 버튼(IView)의 Enabled를 false로 전환
                    ConfIg.Instance.ColorConfIg.BorderColor = ColorConverter.ConvertToColorREF((ColorConverter.GetAccentColor()));
                    ConfIg.Instance.ColorConfIg.BorderColor_ = ColorConverter.GetAccentColor();
                }

                else
                {
                    // 모서리 색상을 시스템 색상으로 설정하지 않도록 한 것이므로 모서리 색 설정 버튼(IView)의 Enabled를 true로 전환 
                }
            }
        }

        private bool _IsCaptionSystemColor;

        public bool IsCaptionSystemColor
        {
            get => _IsCaptionSystemColor;
            set
            {
                SetProperty(ref _IsCaptionSystemColor, value);
                ConfIg.Instance.ColorConfIg.IsCaptIonSystemAccent = value;
                if (value)
                {
                    // 모서리 색상을 시스템 색상으로 설정하도록 한 것이므로 모서리 색 설정 버튼(IView)의 Enabled를 false로 전환
                    ConfIg.Instance.ColorConfIg.CaptIonColor = ColorConverter.ConvertToColorREF((ColorConverter.GetAccentColor()));
                    ConfIg.Instance.ColorConfIg.CaptIonColor_ = ColorConverter.GetAccentColor();
                }

                else
                {
                    // 모서리 색상을 시스템 색상으로 설정하지 않도록 한 것이므로 모서리 색 설정 버튼(IView)의 Enabled를 true로 전환 
                }
            }
        }

        private bool _IsCaptionTextSystemColor;

        public bool IsCaptionTextSystemColor
        {
            get => _IsCaptionTextSystemColor;
            set
            {
                SetProperty(ref _IsCaptionTextSystemColor, value);
                ConfIg.Instance.ColorConfIg.IsCaptIonTextSystemAccent = value;
                if (value)
                {
                    // 모서리 색상을 시스템 색상으로 설정하도록 한 것이므로 모서리 색 설정 버튼(IView)의 Enabled를 false로 전환
                    ConfIg.Instance.ColorConfIg.CaptIonTextColor = ColorConverter.ConvertToColorREF((ColorConverter.GetAccentColor()));
                    ConfIg.Instance.ColorConfIg.CaptIonTextColor_ = ColorConverter.GetAccentColor();
                }

                else
                {
                    // 모서리 색상을 시스템 색상으로 설정하지 않도록 한 것이므로 모서리 색 설정 버튼(IView)의 Enabled를 true로 전환 
                }
            }
        }

        // ---------------------------------------------------------------------

        private int _WindowCornermode;

        public int WindowCornermode
        {
            get => _WindowCornermode;
            set
            {
                SetProperty(ref _WindowCornermode, value);
                ConfIg.Instance.WindowConfig.WindowCornerOption = (DWM_WINDOW_CORNER_PREFERENCE) value;
            }
        }

        // ---------------------------------------------------------------------

        private Color _borderColor;

        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                SetProperty(ref _borderColor, value);
                ConfIg.Instance.ColorConfIg.BorderColor = int.Parse(value.ToString());
                ConfIg.Instance.ColorConfIg.BorderColor_ = value;
            }
        }

        private Color _captionColor;

        public Color CaptionColor
        {
            get => _captionColor;
            set
            {
                SetProperty(ref _captionColor, value);
                ConfIg.Instance.ColorConfIg.CaptIonColor = int.Parse(value.ToString());
                ConfIg.Instance.ColorConfIg.CaptIonColor_ = value;
            }
        }

        private Color _captionTextColor;

        public Color CaptionTextColor
        {
            get => _captionTextColor;
            set
            {
                SetProperty(ref _captionTextColor, value);
                ConfIg.Instance.ColorConfIg.CaptIonTextColor = int.Parse(value.ToString());
                ConfIg.Instance.ColorConfIg.CaptIonTextColor_ = value;
            }
        }

        private int _CaptionTextColormode;

        public int CaptionTextColormode
        {
            get => _CaptionTextColormode;
            set
            {
                SetProperty(ref _CaptionTextColormode, value);
                ConfIg.Instance.ColorConfIg.CaptIonTextColormode = value;
            }
        }

        // ---------------------------------------------------------------------

        private bool _IsAutoTask;

        public bool IsAutotask
        {
            get => _IsAutoTask;
            set
            {
                SetProperty(ref _IsAutoTask, value);
                WindowLoadTask();
            }
        }

        // ---------------------------------------------------------------------

        private string _ProgramListText;

        public string ProgramListText
        {
            get => _ProgramListText;
            set
            {
                SetProperty(ref _ProgramListText, value);
            }
        }

        private string _NonProgramListText;

        public string NonProgramListText
        {
            get => _NonProgramListText;
            set
            {
                SetProperty(ref _NonProgramListText, value);
            }
        }

        // ---------------------------------------------------------------------

        public ICommand WindowLoadTaskCommand { get; }
        public ICommand OpenborderColorUICommand { get; }
        public ICommand OpencaptionColorUICommand { get; }
        public ICommand OpencaptionTextColorUICommand { get; }
        public ICommand OpenExceptProgramManageCommand { get; }


        public SettingsViewModel()
        {
            ProcessCheckermode = ConfIg.Instance.ProcessCheckermode;

            IsBorderSystemColor = ConfIg.Instance.ColorConfIg.IsBorderSystemAccent;
            IsCaptionSystemColor = ConfIg.Instance.ColorConfIg.IsCaptIonSystemAccent;
            IsCaptionTextSystemColor = ConfIg.Instance.ColorConfIg.IsCaptIonTextSystemAccent;

            WindowCornermode = (int) ConfIg.Instance.WindowConfig.WindowCornerOption;

            BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
            CaptionColor = ConfIg.Instance.ColorConfIg.CaptIonColor_;
            CaptionTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor_;

            IsAutotask = ProcessChecker.IsTaskWork;

            WindowLoadTaskCommand = new RelayCommand(WindowLoadTask);
            OpenborderColorUICommand = new RelayCommand(OpenborderColorUI);
            OpencaptionColorUICommand = new RelayCommand(OpencaptionColorUI);
            OpencaptionTextColorUICommand = new RelayCommand(OpencaptionTextColorUI);
            OpenExceptProgramManageCommand = new RelayCommand(OpenExceptProgramManage);

            if (ConfIg.Instance.AppLIst.Count == 0 == false)
            {
                int tep = 0;
                foreach (string pro in ConfIg.Instance.AppLIst)
                {
                    if (tep == ConfIg.Instance.AppLIst.Count - 1)
                        ProgramListText += pro;
                    else
                        ProgramListText += pro + "\r";
                    tep++;
                }
            }

            if (ConfIg.Instance.NonappLIst.Count == 0 == false)
            {
                int tep = 0;
                foreach (string pro in ConfIg.Instance.NonappLIst)
                {
                    if (tep == ConfIg.Instance.NonappLIst.Count - 1)
                        NonProgramListText += pro;
                    else
                        NonProgramListText += pro + "\r";
                    tep++;
                }
            }
        }

        /// <summary>
        /// - 기능
        /// <br/>ㅤ윈도우를 체크하기 위한 백그라운드 작업을 활성화합니다.
        /// </summary>
        async void WindowLoadTask()
        {
            if (IsAutotask)
            {
                if (ProcessCheckermode == 0)
                {
                    // IView를 이용한 프로그램 리스트 TextBlock를 읽기 전용으로 전환
                    ITextBox.TextblockList[0].IsReadOnly = true;
                    IComboBox.ComboBoxList[0].IsEnabled = false;

                    string[] lines = ProgramListText.Split("\r");

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

                    if (IsBorderSystemColor == false)
                        UtIl.UtIl.Bordercolor = ConfIg.Instance.ColorConfIg.BorderColor;
                    else
                        UtIl.UtIl.Bordercolor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    if (IsCaptionSystemColor == false)
                        UtIl.UtIl.CaptIonColor = ConfIg.Instance.ColorConfIg.CaptIonColor;
                    else
                        UtIl.UtIl.CaptIonColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    if (IsCaptionTextSystemColor == false)
                        UtIl.UtIl.CaptIonTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor;
                    else
                        UtIl.UtIl.CaptIonTextColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    //UtIl.UtIl.task = UtIl.UtIl.ConsumeTask(UtIl.UtIl.cts.Token);
                    HwndCheckerWithWrapper.BackgroundTask = HwndCheckerWithWrapper.StartBackgroundTask(HwndCheckerWithWrapper.cts.Token,
                        UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.R, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.G, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.B);
                }

                else
                {
                    // IView를 이용한 프로그램 리스트 TextBlock를 읽기 전용으로 전환
                    ITextBox.TextblockList[1].IsReadOnly = true;
                    // 프로세스 체크 방식 변경 비활성화
                    IComboBox.ComboBoxList[0].IsEnabled = false;

                    string[] lines = NonProgramListText.Split("\r");

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

                    if (IsBorderSystemColor == false)
                        ProcessChecker.Bordercolor = ConfIg.Instance.ColorConfIg.BorderColor;
                    else
                        ProcessChecker.Bordercolor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    if (IsCaptionSystemColor == false)
                        ProcessChecker.CaptIonColor = ConfIg.Instance.ColorConfIg.CaptIonColor;
                    else
                        ProcessChecker.CaptIonColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    if (IsCaptionTextSystemColor == false)
                        ProcessChecker.CaptIonTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor;
                    else
                        ProcessChecker.CaptIonTextColor = ColorConverter.ConvertToColorREF(ColorConverter.GetAccentColor());

                    ProcessChecker.Corner_ConfIg = (DWM_WINDOW_CORNER_PREFERENCE)Enum.ToObject(typeof(DWM_WINDOW_CORNER_PREFERENCE), ConfIg.Instance.WindowConfig.WindowCornerOption);

                    ProcessChecker.IsFIrstLoad = true;
                    ProcessChecker.IsTaskWork = true;
                    //ProcessChecker.task = UtIl.ProcessChecker.ConsumeTask(ProcessChecker.cts.Token);
                    HwndCheckerWithWrapper.BackgroundTask = HwndCheckerWithWrapper.StartBackgroundTask(HwndCheckerWithWrapper.cts.Token,
                        UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.R, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.G, UtIl.ConfIg.Instance.ColorConfIg.BorderColor_.B);
                }
            }
            
            else
            {
                // TaskOn 시키는 토글 UI (IView) 비활성화
                IToggleSwitch.ToggleSwitchList[0].IsEnabled = false;

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

                HwndCheckerWithWrapper.cts.Cancel();
                await HwndCheckerWithWrapper.BackgroundTask;

                // TaskOn 시키는 토글 UI (IView) 활성화
                IToggleSwitch.ToggleSwitchList[0].IsEnabled = true;


                if (ProcessCheckermode == 0)
                    ITextBox.TextblockList[0].IsReadOnly = false; // IView를 이용한 프로그램 리스트 TextBlock를 읽기 전용에서 해제
                else
                    ITextBox.TextblockList[1].IsReadOnly = false; // IView를 이용한 논 프로그램 리스트 TextBlock를 읽기 전용에서 해제

                // 윈도우 체커 방식을 정하는 콤보박스 활성화
                IComboBox.ComboBoxList[0].IsEnabled = true;
            }
        }

        async void OpenborderColorUI()
        {
            ContentDialog ColorwIndow = new()
            {
                // Xamlroot를 인터페이스를 이용하여 로드하기
                XamlRoot = IXamlRoot.XamlRootList[0],
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
                ConfIg.Instance.ColorConfIg.BorderColor = ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.BorderColor_ = ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color;
                BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color));
            }
        }

        async void OpencaptionColorUI()
        {
            ContentDialog ColorwIndow = new()
            {
                XamlRoot = IXamlRoot.XamlRootList[0],
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
                ConfIg.Instance.ColorConfIg.BorderColor = ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.BorderColor_ = ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color;
                BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color));
            }
        }
        
        async void OpencaptionTextColorUI()
        {
            ContentDialog ColorwIndow = new()
            {
                XamlRoot = IXamlRoot.XamlRootList[0],
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
                ConfIg.Instance.ColorConfIg.CaptIonTextColor = ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.CaptIonTextColor_ = ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color;
                CaptionTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color));
            }
        }

        async void OpenExceptProgramManage()
        {
            if (ProcessChecker.IsTaskWork == false || UtIl.UtIl.IsTaskWork == false)
            {
                string[] lines = NonProgramListText.Split("\r");

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

                //NonProgram_LIst.IsEnabled = false; 
                ITextBox.TextblockList[1].IsEnabled = false;

                var ExceptProgramWindow = new Windows.ExceptProgramWindow();
                ExceptProgramWindow.Activate();

                ExceptProgramWindow.Closed += delegate
                {
                    // NonProgram_LIst.IsEnabled = true;
                    ITextBox.TextblockList[1].IsEnabled = true;

                    foreach (var ExceptProgram in ExceptProgramWindow.tempExceptLIst)
                    {
                        string Line = "\r" + ExceptProgram.ProcessStrIng + " " + (ExceptProgram.IsBorderChange == false ? "/b " : "") + (ExceptProgram.IsCaptIonChange == false ? "/c " : "") + (ExceptProgram.IsCaptIonTextChange == false ? "/t " : "");
                        ConfIg.Instance.NonappLIst.Add(Line.Replace("\r", ""));
                    }

                    NonProgramListText = "";

                    int tep = 0;
                    foreach (var Nonapp in ConfIg.Instance.NonappLIst)
                    {
                        if (tep == ConfIg.Instance.NonappLIst.Count)
                            NonProgramListText += Nonapp;
                        else
                            NonProgramListText += Nonapp + "\r";
                        tep++;
                    }

                    ExceptProgramWindow.Content = null;
                    ExceptProgramWindow = null;
                };
            }

        }
    }
}
