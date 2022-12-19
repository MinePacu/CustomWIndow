using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWIndow.UtIl
{
    public class ProcessWIndow
    {
        public string Process_StrIng { get; set; }

        public IntPtr FIrsthwnd { get; set; }

        public List<IntPtr> Subhwnd { get; } = new();

        public int ThreadLength { get; set; } = 0;

        public bool FIrst { get; set; }

        public bool Applyed { get; set; }

        public List<bool> SubApplyed { get; } = new();

        public bool IsBorderChange { get; set; } = true;

        public bool IsCaptIonChange { get; set; } = true;

        public bool IsCaptIonTextChange { get; set; } = true;

        public ProcessWIndow(string _Process_StrIng, IntPtr _FIrsthwnd, List<IntPtr> _Subhwnd, bool _FIrst, bool _Applyed)
        {
            Process_StrIng = _Process_StrIng;
            FIrsthwnd = _FIrsthwnd;
            Subhwnd = _Subhwnd;
            FIrst = _FIrst;
            Applyed = _Applyed;
        }

        public ProcessWIndow(string _Process_StrIng, IntPtr _FIrsthwnd, List<IntPtr> _Subhwnd, bool _FIrst, bool _Applyed, bool _Default)
        {
            Process_StrIng = _Process_StrIng;
            FIrsthwnd = _FIrsthwnd;
            Subhwnd = _Subhwnd;
            FIrst = _FIrst;
            Applyed = _Applyed;

            IsBorderChange = _Default;
            IsCaptIonChange = _Default;
            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 0)
                IsCaptIonTextChange = false;
            else
                IsCaptIonTextChange = _Default;
        }

        public ProcessWIndow(string _Process_StrIng, IntPtr _FIrsthwnd, List<IntPtr> _Subhwnd, bool _FIrst, bool _Applyed, bool _IsBorderChange, bool _IsCaptIonChange, bool _IsCaptIonTextChange)
        {
            Process_StrIng = _Process_StrIng;
            FIrsthwnd = _FIrsthwnd;
            Subhwnd = _Subhwnd;
            FIrst = _FIrst;
            Applyed = _Applyed;

            IsBorderChange = _IsBorderChange;
            IsCaptIonChange = _IsCaptIonChange;
            if (ConfIg.Instance.ColorConfIg.CaptIonTextColormode == 0)
                IsCaptIonTextChange = false;
            else
                IsCaptIonTextChange = _IsCaptIonTextChange;
        }
    }
}
