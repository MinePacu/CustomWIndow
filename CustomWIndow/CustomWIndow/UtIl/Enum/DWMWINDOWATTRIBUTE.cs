using System;

namespace CustomWIndow.UtIl.Enum
{
    public enum DwmWIndowAttrIbute
    {
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ비 클라이언트 렌더링이 사용되는지 여부를 로드합니다. 로드한 값은 <see cref="bool"/> 형식입니다. 클라이언트가 아닌 렌더링을 사용하면 true이며, 그렇지 않으면 false입니다.
        /// </summary>
        DWMWA_NCRENDERING_ENABLED,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, bool)"/> 와 함께 사용합니다. 클라이언트가 아닌 렌더링 정책을 설정합니다.
        /// </summary>
        DwmWA_NCRENDERING_POLICY,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, bool)"/>와 함께 사용합니다. Dwm 전환을 사용하거나 강제로 사용하지 않도록 설정합니다. pvAttribute 매개 변수는 <see cref="bool"/> 형식의 값을 가리킵니다. 전환을 사용하지 않도록 설정하려면 true이거나, 전환을 사용하도록 설정하려면 false입니다.
        /// </summary>
        DwmWA_TRANSITIONS_FORCEDISABLED,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, bool)"/>와 함께 사용합니다. 클라이언트가 아닌 영역에서 렌더링된 콘텐츠를 Dwm에서 그린 프레임에 표시할 수 있습니다. pvAttribute 매개 변수는 <see cref="bool"/> 형식의 값을 가리킵니다. true 이면 클라이언트가 아닌 영역에서 렌더링된 콘텐츠를 프레임에 표시할 수 있습니다. 그렇지 않으면 false입니다.
        /// </summary>
        DwmWA_ALLOW_NCPAINT,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ창 상대 공간에서 캡션 단추 영역의 범위를 로드합니다. 로드된 값은 RECT 형식입니다. 창이 최소화되었거나 사용자에게 표시되지 않으면 로드된 RECT 의 값이 정의되지 않습니다. 검색된 RECT 에 작업할 수 있는 경계가 포함되어 있는지 확인해야 하며, 그렇지 않으면 창이 최소화되었거나 표시되지 않는다는 결론을 내릴 수 있습니다.
        /// </summary>
        DwmWA_CAPTION_BUTTON_BOUNDS,
        DwmWA_NONCLIENT_RTL_LAYOUT,
        DwmWA_FORCE_ICONIC_REPRESENTATION,
        DwmWA_FLIP3D_POLICY,
        DwmWA_EXTENDED_FRAME_BOUNDS,
        DwmWA_HAS_ICONIC_BITMAP,
        DwmWA_DISALLOW_PEEK,
        DwmWA_EXCLUDED_FROM_PEEK,
        DwmWA_CLOAK,
        DwmWA_CLOAKED,
        DWMWA_FREEZE_REPRESENTATION,
        DWMWA_PASSIVE_UPDATE_MODE,
        DWMWA_USE_HOSTBACKDROPBRUSH,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, bool)"/>와 함께 사용합니다.
        /// <br/>ㅤ어두운 모드 시스템 설정을 사용할 때 이 창의 창 프레임을 어두운 모드 색으로 그릴 수 있습니다. 호환성을 위해 모든 창은 시스템 설정에 관계없이 기본적으로 라이트 모드로 설정됩니다. attr_ref의 타입은 <see cref="bool"/> 형식입니다. 창에 어두운 모드를 적용하려면 true, 항상 라이트 모드를 사용하는 경우 false입니다.
        /// <br/>ㅤ<em>이 열거형은 윈도우 11부터 지원합니다.</em>
        /// </summary>
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, DWM_WINDOW_CORNER_PREFERENCE)"/>와 함께 사용합니다. 창의 둥근 모서리 기본 설정을 지정합니다. attr_ref의 타입은 <see cref="DWM_WINDOW_CORNER_PREFERENCE"/> 형식입니다.
        /// <br/>ㅤ<em>이 열거형은 윈도우 11부터 지원합니다.</em>
        /// </summary>
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, int)"/>와 함께 사용합니다. 창 테두리의 색을 지정합니다.  attr_ref는 ColorRef 형식입니다. 앱은 창 활성화 변경과 같은 상태 변경에 따라 테두리 색을 변경합니다.
        /// <br/>ㅤ<em>이 열거형은 윈도우 11부터 지원합니다.</em>
        /// </summary>
        DWMWA_BORDER_COLOR,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, int)"/>와 함께 사용합니다. 캡션의 색을 지정합니다. attr_ref의 타입은 ColorRef 형식입니다.
        /// <br/>ㅤ<em>이 열거형은 윈도우 11부터 지원합니다.</em>
        /// </summary>
        DWMWA_CAPTION_COLOR,
        /// <summary>
        /// - 열거형
        /// <br/>ㅤ<see cref="WindowFunction.Dwm.DwmSetWindowAttribute_(IntPtr, DwmWIndowAttrIbute, int)"/>와 함께 사용합니다. 캡션 텍스트의 색을 지정합니다. attr_ref의 타입은 ColorRef 형식입니다.
        /// <br/>ㅤ<em>이 열거형은 윈도우 11부터 지원합니다.</em>
        /// </summary>
        DWMWA_TEXT_COLOR,
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
        DWMWA_SYSTEMBACKDROP_TYPE,
        DWMWA_LAST
    }
}
