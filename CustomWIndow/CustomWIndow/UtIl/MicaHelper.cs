using System.Runtime.InteropServices;

using WinRT;

using Windows.System;

using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using Microsoft.UI.Composition;

namespace CustomWIndow.UtIl
{
    /// <summary>
    /// 창에 미카 효과를 적용하기 위한 도구
    /// </summary>
    public class MicaHelper
    {
        WindowsSystemDispatcherQueueHelper wsdqHelper;
        MicaController mica_Controller;
        SystemBackdropConfiguration configurationSource;

        private bool IsReplacedWindowActivatedEvent { get; set; }
        private bool IsReplacedWindowClosedEvent { get; set; }

        readonly Window window;

        /// <summary>
        /// 새 미카 컨트롤러 인스턴스를 만듭니다.
        /// </summary>
        /// <param name="_window">미카 효과를 적용할 창</param>
        public MicaHelper(Window _window)
        {
            window = _window;
        }

        /// <summary>
        /// 인스턴스를 만들 때 지정한 창에 미카 효과를 적용합니다.
        /// </summary>
        /// <param name="ReplaceWindowActivatedEvent">창이 켜졌을 때, 이벤트 메소드를 이 클래스에 미리 정의된 메소드로 대체할지 여부입니다. 
        /// <br/>따로 정의한 이벤트 메소드가 있으면 <c>false</c>로 지정하세요. 이 매개 변수를 <c>false</c>로 지정하면 따로 정의한 이벤트 메소드에 <see cref="SetIsInputActive(WindowActivatedEventArgs)"/> 메소드를 호출하세요.</param>
        /// <param name="ReplaceWindowClosedEvent">창을 닫을 때, 이벤트 메소드를 이 클래스에 미리 정의된 메소드로 대체할지 여부입니다.
        /// <br/>따로 정의한 이벤트 메소드가 있으면 <c>false</c>로 지정하세요. 이 매개 변수를 <c>false</c>로 지정하면, <see cref="MicaController"/> 인스턴스가 자동으로 Dispose되지 않습니다. 따로 지정한 이벤트 메소드에서 <see cref="DisposeMicaController"/> 메소드를 호출하세요.</param>
        /// <returns>미카 효과가 적용되면 <c>true</c>, 적용되지 못하면 <c>false</c>를 반환합니다.</returns>
        public bool TrySetMica(bool ReplaceWindowActivatedEvent, bool ReplaceWindowClosedEvent)
        {
            if (MicaController.IsSupported())
            {
                wsdqHelper = new();
                wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                configurationSource = new();

                if (ReplaceWindowActivatedEvent)
                {
                    IsReplacedWindowActivatedEvent = ReplaceWindowActivatedEvent;
                    window.Activated += Window_Activated;
                }

                if (ReplaceWindowClosedEvent)
                {
                    IsReplacedWindowClosedEvent = ReplaceWindowClosedEvent;
                    window.Closed += Window_Closed;
                }

                ((FrameworkElement)window.Content).ActualThemeChanged += Window_ThemeChanged;

                configurationSource.IsInputActive = true;
                SetConfigurationSourceTheme();

                mica_Controller = new();

                mica_Controller.AddSystemBackdropTarget(window.As<ICompositionSupportsSystemBackdrop>());
                mica_Controller.SetSystemBackdropConfiguration(configurationSource);

                return true;
            }

            return false;
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args) => configurationSource.IsInputActive = args.WindowActivationState == WindowActivationState.Deactivated == false;

        private void Window_Closed(object sender, WindowEventArgs args) => DisposeMicaController();

        /// <summary>
        /// 시스템에서 현재 창을 입력 포커스가 있는 것으로 판단시킵니다.
        /// </summary>
        /// <param name="args">설정을 변경할 창의 상태를 포함하는 클래스</param>
        public void SetIsInputActive(WindowActivatedEventArgs args) => configurationSource.IsInputActive = args.WindowActivationState == WindowActivationState.Deactivated == false;

        /// <summary>
        /// 미카 컨트롤러가 사용 중인 메모리를 해제합니다.
        /// </summary>
        public void DisposeMicaController()
        {
            if (mica_Controller == null == false)
            {
                mica_Controller.Dispose();
                mica_Controller = null;
            }

            if (IsReplacedWindowActivatedEvent)
                window.Activated -= Window_Activated;

            configurationSource = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (configurationSource == null == false)
                SetConfigurationSourceTheme();
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)window.Content).ActualTheme)
            {
                case ElementTheme.Dark:
                    configurationSource.Theme = SystemBackdropTheme.Dark; break;

                case ElementTheme.Light:
                    configurationSource.Theme = SystemBackdropTheme.Light; break;

                case ElementTheme.Default:
                    configurationSource.Theme = SystemBackdropTheme.Default; break;
            }
        }
    }

    class WindowsSystemDispatcherQueueHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DispatcherQueueOptions
        {
            internal int dwSize;
            internal int threadType;
            internal int apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        object m_dispatcherQueueController = null;

        public void EnsureWindowsSystemDispatcherQueueController()
        {
            if (DispatcherQueue.GetForCurrentThread() == null == false)
                return;

            if (m_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options = new()
                {
                    dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions)),
                    threadType = 2,
                    apartmentType = 2
                };

                _ = CreateDispatcherQueueController(options, ref m_dispatcherQueueController);
            }
        }
    }
}
