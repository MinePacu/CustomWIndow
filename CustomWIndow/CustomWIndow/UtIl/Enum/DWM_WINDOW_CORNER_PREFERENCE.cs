using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWIndow.UtIl.Enum
{
    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        DWMWCP_DEFAULT = 0,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ창 모서리를 둥글게 처리하지 않습니다.
        /// </summary>
        DWMWCP_DONOTROUND = 1,
        DWMWCP_ROUND = 2,
        DWMWCP_ROUNDSMALL = 3
    }
}
