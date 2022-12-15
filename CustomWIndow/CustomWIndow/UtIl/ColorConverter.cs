using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI;
using Windows.UI.ViewManagement;

namespace CustomWIndow.UtIl
{
    public static class ColorConverter
    {
        public static int ConvertToColorREF(int r, int g, int b)
        {
            string _r = string.Format("X", r);
            string _g = string.Format("X", g);
            string _b = string.Format("X", b);

            string _colorref = "0x00" + _b + _g + _r;

            return Convert.ToInt32(_colorref, 16);
        }

        public static int ConvertToColorREF(byte r, byte g, byte b)
        {
            string _r = string.Format("X", r);
            string _g = string.Format("X", g);
            string _b = string.Format("X", b);

            string _colorref = "0x00" + _b + _g + _r;

            return Convert.ToInt32(_colorref, 16);
        }

        public static int ConvertToColorREF(Color _color)
        {
            int r = _color.R;
            int g = _color.G;
            int b = _color.B;

            string _r = r.ToString("X");
            string _g = g.ToString("X");
            string _b = b.ToString("X");

            string _colorref = "0x00" + _b + _g + _r;

            return Convert.ToInt32(_colorref, 16);
        }

        public static Color GetAccentColor()
        {
            var uisettings = new UISettings();
            return uisettings.GetColorValue(UIColorType.Accent);
        }
    }
}
