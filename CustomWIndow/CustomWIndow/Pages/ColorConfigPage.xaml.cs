using System.Diagnostics;
using System;

using CustomWIndow.UtIl;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ColorConfigPage : Page
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
        public static readonly DependencyProperty CaptionColorProperty = DependencyProperty.Register("CaptionColor", typeof(Color), typeof(SettingsPage), null);
        public static readonly DependencyProperty CaptionTextColorProperty = DependencyProperty.Register("CaptionTextColor", typeof(Color), typeof(SettingsPage), null);


        public ColorConfigPage()
        {
            this.InitializeComponent();

            IsBorderSystemColor.IsChecked = ConfIg.Instance.ColorConfIg.IsBorderSystemAccent;
            IsCaptIonSystemColor.IsChecked = ConfIg.Instance.ColorConfIg.IsCaptIonSystemAccent;

            CaptIonTextColormodebox.SelectedIndex = ConfIg.Instance.ColorConfIg.CaptIonTextColormode;

            IsCaptIonTextSystemColor.IsChecked = ConfIg.Instance.ColorConfIg.IsCaptIonTextSystemAccent;

            BorderColorMasterToggle.IsOn = ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfBorderWindow;
            CaptionColorMasterToggle.IsOn = ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfCaptionWindow;

            IsBorderColorTransparency.IsChecked = ConfIg.Instance.ColorConfIg.IsBorderColorTransparency;
            IsCaptionColorTransparency.IsChecked = ConfIg.Instance.ColorConfIg.IsCaptionColorTransparency;
            IsCaptionTextColorTransparency.IsChecked = ConfIg.Instance.ColorConfIg.IsCaptionTextColorTransparency;

            CaptionColormodeCombo.SelectedIndex = ConfIg.Instance.ColorConfIg.CaptionColormode;

            BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
            CaptionColor = ConfIg.Instance.ColorConfIg.CaptIonColor_;
            CaptionTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor_;
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
            var ColorPickerControler = new Controls.ColorPickerControl(ConfIg.Instance.ColorConfIg.BorderColor_);

            ContentDialog ColorwIndow = new()
            {
                XamlRoot = this.Content.XamlRoot,
                Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
                Title = "테두리 색",
                PrimaryButtonText = "설정",
                CloseButtonText = "취소",

                DefaultButton = ContentDialogButton.Close,

                Content = ColorPickerControler
            };

            var ColorR = await ColorwIndow.ShowAsync();

            if (ColorR == ContentDialogResult.Primary)
            {
                ConfIg.Instance.ColorConfIg.BorderColor = ColorConverter.ConvertToColorREF(ColorPickerControler.ColorData);
                ConfIg.Instance.ColorConfIg.BorderColor_ = ColorPickerControler.ColorData;
                BorderColor = ConfIg.Instance.ColorConfIg.BorderColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(ColorPickerControler.ColorData));
            }

            //GC.Collect();
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
                ConfIg.Instance.ColorConfIg.CaptIonColor = ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.CaptIonColor_ = ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color;
                CaptionColor = ConfIg.Instance.ColorConfIg.CaptIonColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color));
                Debug.WriteLine("Debug Color - " + ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
            }

            GC.Collect();
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
                ConfIg.Instance.ColorConfIg.CaptIonTextColor = ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
                ConfIg.Instance.ColorConfIg.CaptIonTextColor_ = ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color;
                CaptionTextColor = ConfIg.Instance.ColorConfIg.CaptIonTextColor_;
                Debug.WriteLine("Debug Color - " + ColorConverter.ConvertToColorREF(((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color));
                Debug.WriteLine("Debug Color - " + ((ColorPicker)((ScrollViewer)ColorwIndow.Content).Content).Color);
            }

            GC.Collect();
        }

        void CaptIonTextColormodebox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combobox = (ComboBox)sender;

            ConfIg.Instance.ColorConfIg.CaptIonTextColormode = combobox.SelectedIndex;

            if (combobox.SelectedIndex == 0)
            {
                CaptIonTextColormode.Text = "자동 - 윈도우 기본 설정입니다.";
                IsCaptIonTextSystemColor.IsEnabled = false;
                IsCaptionTextColorTransparency.IsEnabled = false;
                captIonTextColorbutton.IsEnabled = false;
            }
            else
            {
                CaptIonTextColormode.Text = "수동 - 사용자가 지정한 색상으로 캡션 텍스트 색상을 적용합니다.";
                IsCaptIonTextSystemColor.IsEnabled = true;

                if (IsCaptIonTextSystemColor.IsChecked == true)
                {
                    captIonTextColorbutton.IsEnabled = false;
                    IsCaptionTextColorTransparency.IsEnabled = false;
                }
                else
                {
                    captIonTextColorbutton.IsEnabled = true;
                    IsCaptionTextColorTransparency.IsEnabled = true;
                }
            }
        }

        void CaptionColormodeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ComboBox = (ComboBox)sender;
            ConfIg.Instance.ColorConfIg.CaptionColormode = ComboBox.SelectedIndex;
        }

        private void BorderColorMasterToggle_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfBorderWindow = ((ToggleSwitch)sender).IsOn;
        }
        private void CaptionColorMasterToggle_Toggled(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.ColorConfIg.IsOnMasterToggleOfCaptionWindow = ((ToggleSwitch)sender).IsOn;
            CaptionColormodeCombo.IsEnabled = !((ToggleSwitch)sender).IsOn;
        }

        private void IsBorderColorTransparency_Checked(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.ColorConfIg.IsBorderColorTransparency = (bool) ((CheckBox)sender).IsChecked;
        }
        private void IsCaptionColorTransparency_Checked(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.ColorConfIg.IsCaptionColorTransparency = (bool)((CheckBox)sender).IsChecked;
        }
        private void IsCaptionTextColorTransparency_Checked(object sender, RoutedEventArgs e)
        {
            ConfIg.Instance.ColorConfIg.IsCaptionTextColorTransparency = (bool)((CheckBox)sender).IsChecked;
        }
    }
}
