﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetWIndowStyle.UtIl
{
    public class ProcessWIndow
    {
        public string Process_StrIng { get; set; }

        public List<IntPtr> ThreadHwnd { get; } = new();

        public int ThreadLength { get; set; } = 0;

        public ProcessWIndow(string _Process_StrIng, List<IntPtr> _ThreadHwnd)
        {
            Process_StrIng = _Process_StrIng;
            ThreadHwnd = _ThreadHwnd;
        }
    }

    public class HwndClass
    {
        public string HwndCaptIon { get; set; }
        public IntPtr hwnd { get; set; }

        public HwndClass(string _hwndCation, IntPtr _hwnd)
        {
            HwndCaptIon = _hwndCation;
            hwnd = _hwnd;
        }
    }
}
