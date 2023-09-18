using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomWIndow.UtIl.Enum
{
    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        /// <summary>
        /// 운영체제 기본값을 사용합니다.
        /// </summary>
        DWMWCP_DEFAULT = 0,
        /// <summary>
        /// 창 모서리를 둥글게 처리하지 않습니다.
        /// </summary>
        DWMWCP_DONOTROUND = 1,
        /// <summary>
        /// 창 모서리를 둥글게 처리합니다.
        /// </summary>
        DWMWCP_ROUND = 2,
        /// <summary>
        /// 창 모서리를 조금만 둥글게 처리합니다.
        /// </summary>
        DWMWCP_ROUNDSMALL = 3
    }
}
