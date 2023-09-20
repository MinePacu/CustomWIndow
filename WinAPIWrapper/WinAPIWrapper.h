#pragma once
#include "../WinAPI/Windowmodule.h"

#include <dwmapi.h>

using namespace System;

namespace WinAPIWrapper {
	public ref class WindowmoduleWrapper
	{
		// TODO: 여기에 이 클래스에 대한 메서드를 추가합니다.
	protected:
		Windowmodule* m_Windowmodule;

	public:
		/// <summary>
		/// 창 설정을 변경하기 위한 모듈을 초기화합니다.
		/// </summary>
		/// <param name="r">모서리 창 색깔의 R 값</param>
		/// <param name="g">모서리 창 색깔의 G 값</param>
		/// <param name="b">모서리 창 색깔의 B 값</param>
		/// <param name="captionR">제목 표시줄 색깔의 R 값</param>
		/// <param name="captionG">제목 표시줄 색깔의 G 값</param>
		/// <param name="captionB">제목 표시줄 색깔의 B 값</param>
		/// <param name="cornerProperty">창 모서리 설정</param>
		/// <param name="useDWM">모듈 자체에서 창 모서리를 그리는 대신에 DWM을 이용할지 여부입니다. 이 값이 <c>true</c>이면 m_Windowmodule 인스턴스가 초기화되지 않으며 설정 변경은 전부 DWM이 담당합니다.</param>
		WindowmoduleWrapper(byte r, byte g, byte b, byte captionR, byte captionG, byte captionB, int cornerProperty, bool useDWM);
		virtual ~WindowmoduleWrapper();

		bool RefreshHwnds(System::Collections::Generic::ICollection<IntPtr>^ hwndList);

		bool AssginBorders(IntPtr hwnd);

		void AddHwnd(IntPtr hwnd);
		bool FindHwnd(IntPtr hwnd);

		int GetWindowTitleLength(IntPtr hwnd);

		void SetBorderColortoSpecificWindow(IntPtr hwnd, int r, int g, int b);

		void CleanupBorderWindows();

		bool RestoreDwmMica(int buildVer);

		void TrackingWindows();

		void SetCornerPre(UINT cornerPre);
		void SetCaptionColor(byte r, byte g, byte b);
		void SetBuildVer(int BuildVer);

		void SetWindowBorderColorWithDwm(IntPtr hwnd, bool IsTransparency);
		void SetWindowCaptionColorWithDwm(IntPtr hwnd, bool IsTransparency, int Colormode);
		void SetWindowCaptionTextColorWithDwm(IntPtr hwnd, bool IsTransparency);

		void SetWindowCaptionColormode(IntPtr hwnd, bool ismode);

		void SetWindowCornerPropertyWithDwm(IntPtr hwnd);

		/// <summary>
		/// DWM를 이용하여 윈도우 설정을 기본값으로 초기화합니다.
		/// </summary>
		void SetDefaultWindowOptionWithDWM();

		/// <summary>
		/// DWM를 이용하여 작업 표시줄 설정을 변경합니다.
		/// </summary>
		/// <param name="Cornermode">작업 표시줄에 설정할 모서리 설정</param>
		/// <param name="r">작업 표시줄에 설정할 색 중 빨간색</param>
		/// <param name="g">작업 표시줄에 설정할 색 중 초록색</param>
		/// <param name="b">작업 표시줄에 설정할 색 중 파란색</param>
		void SetTaskbarRoundedCornerandBorderColor(int Cornermode, byte r, byte g, byte b);

		/// <summary>
		/// DWM를 이용하여 작업 표시줄의 설정을 기본값으로 복원합니다.
		/// </summary>
		void SetTaskbarDefaultSetting();

		/// <summary>
		/// 설정을 적용한 창의 리스트, 설정 적용이 완료된 창에 다시 설정을 적용하지 못하도록 해줍니다.
		/// </summary>
		System::Collections::Generic::ICollection<IntPtr>^ HwndList = gcnew System::Collections::Generic::List<IntPtr>();

		DWM_WINDOW_CORNER_PREFERENCE CornerProperty;

		byte BorderColor_r;
		byte BorderColor_g;
		byte BorderColor_b;

		byte CaptionColor_R;
		byte CaptionColor_G;
		byte CaptionColor_B;

		int GetDisplayCount();

	};
}
