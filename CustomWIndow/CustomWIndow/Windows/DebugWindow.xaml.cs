using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CustomWIndow.UtIl.Enum;
using CustomWIndow.UtIl.WindowFunction;
using CustomWIndow.UtIl;

using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

using WinRT.Interop;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CustomWIndow.Windows
{
    /// <summary>
    /// 윈도우 목록 디버그용 윈도우
    /// </summary>
    public sealed partial class DebugWindow : Window
    {
        public string HWNDAddress { get; set; }

        public string ClassString { get; set; }

        public ICollection<IntPtr> DebugCollection { get; set; }
        public MicaHelper mica_helper { get; }

        private List<HWNDStruct> DataGridCollections { get; } = new(20);
        private List<HWNDStruct> tmp2 { get; } = new(10);
        public CancellationTokenSource cts { get; set; }

        StringBuilder classString = new(255);

        AppWindow WIndow;

        public DebugWindow()
        {
            this.InitializeComponent();
        }

        public DebugWindow(ICollection<IntPtr> _DebugCollection)
        {
            this.InitializeComponent();

            DebugCollection = _DebugCollection;

            WIndow = WinUIFunction.GetAppWIndowForWIndow(this);
            if (AppColorFunction.GetAppColor() == AppColor.Dark)
                Dwm.DwmSetWindowAttribute_(WindowNative.GetWindowHandle(this), DwmWIndowAttrIbute.DWMWA_USE_IMMERSIVE_DARK_MODE, true);

            WIndow.Title = "디버그";
            WIndow.SetIcon(Path.Combine(Environment.CurrentDirectory, "asset/window-system.ico"));

            mica_helper = new(this);
            mica_helper.TrySetMica(true, false, true);

            cts = new();
            _ = StartBackgroundTask(cts.Token);
        }

        private async Task SortedList()
        {
            tmp2.Clear();

            foreach (IntPtr item in DebugCollection)
            {
                classString.Clear();
                if (item != IntPtr.Zero && DataGridCollections.Any(ptrstruct => ptrstruct.HWNDAddress.Equals(item)) == false)
                {
                    if (HwndControl.IsWindowEnabled(item))
                    {
                        _ = StringUtIl.GetClassName(item, classString, classString.MaxCapacity);
                        HWNDStruct tmp = new(item, classString.ToString());
                        DataGridCollections.Add(tmp);
                    }
                }

                foreach (var HWNDStruct in DataGridCollections)
                {
                    if (!DebugCollection.Contains(HWNDStruct.HWNDAddress))
                        tmp2.Add(HWNDStruct);
                }

                foreach (var tmpStruct in tmp2)
                {
                    DataGridCollections.Remove(tmpStruct);
                }
            }

            DebugGrid.ItemsSource = null;
            DebugGrid.ItemsSource = DataGridCollections;

            await Task.Delay(ConfIg.Instance.EtcConfIg.WindowDelay);
        }


        public async Task StartBackgroundTask(CancellationToken cancel)
        {
            //Debug.WriteLine("Debug BackgroundTaks Started");
            foreach (var task in ProduceTask(cancel))
            {
                await task;
            }
        }

        private IEnumerable<Task> ProduceTask(CancellationToken cancel)
        {
            while (cancel.IsCancellationRequested == false)
            {
                yield return SortedList();
            }

            if (cancel.IsCancellationRequested)
            {
                DebugCollection.Clear();
                DebugCollection = null;

                DataGridCollections.Clear();
            }
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            cts.Cancel();
            mica_helper.DisposeMicaController();
        }
    }

    public struct HWNDStruct
    {
        public IntPtr HWNDAddress { get; set; }

        public string HWNDAddressString { get; set; }

        public string ClassString { get; set; }

        public HWNDStruct(IntPtr _HWNDAddress, string _ClassString)
        {
            HWNDAddress = _HWNDAddress;
            HWNDAddressString = _HWNDAddress.ToString("X");
            ClassString = _ClassString;
        }
    }
}
