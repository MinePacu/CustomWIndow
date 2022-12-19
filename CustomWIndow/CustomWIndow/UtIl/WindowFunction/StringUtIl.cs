using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static partial class StringUtIl
    {
        static StringBuilder _string { get; } = new(260);

        [DllImport("User32.dll", EntryPoint = "GetClassName", CharSet = CharSet.Unicode)]
        static extern int GetClassTItle_(IntPtr hWnd, StringBuilder lpClassTItle, int nMaxCount);

        public static string GetClassTItle(IntPtr hwnd)
        {
            _ = GetClassTItle_(hwnd, _string, _string.Capacity);

            return _string.ToString();
        }
    }
}
