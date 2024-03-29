﻿using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CustomWIndow.UtIl.WindowFunction
{
    public static partial class StringUtIl
    {
        static StringBuilder _string { get; } = new(260);

        [DllImport("User32.dll", EntryPoint = "GetClassName", CharSet = CharSet.Unicode)]
        static extern int GetClassTItle_(IntPtr hWnd, StringBuilder lpClassTItle, int nMaxCount);


        [DllImport("User32.dll", EntryPoint = "GetWindowText", CharSet = CharSet.Unicode)]
        static extern int GetWindowTItle_(IntPtr hWnd, StringBuilder lpClassTItle, int nMaxCount);

        public static string GetClassTItle(IntPtr hwnd)
        {
            _ = GetClassTItle_(hwnd, _string, _string.Capacity);

            return _string.ToString();
        }

        public static string GetWindowTItle(IntPtr hwnd)
        {
            _ = GetWindowTItle_(hwnd, _string, _string.Capacity);

            return _string.ToString();
        }

        [DllImport("user32")]
        public static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClass, int nMaxCount);
    }
}
