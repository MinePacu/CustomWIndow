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
		/// <param name="useDWM">모듈 자체에서 창 모서리를 그리는 대신에 DWM을 이용할지 여부입니다. 이 값이 true이면 m_Windowmodule 인스턴스가 초기화되지 않으며 설정 변경은 전부 DWM이 담당합니다.</param>
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
		void SetWindowCaptionColorWithDwm(IntPtr hwnd, bool IsTransparency);
		void SetWindowCaptionTextColorWithDwm(IntPtr hwnd, bool IsTransparency);
		//void SetWindowCaptionTextColorWithDwm(IntPtr hwnd);

		void SetWindowCornerPropertyWithDwm(IntPtr hwnd);

		/// <summary>
		/// DWM를 이용하여 윈도우 설정을 기본값으로 초기화합니다.
		/// </summary>
		void SetDefaultWindowOptionWithDWM();

		void SetTaskbarRoundedCornerandBorderColor(int Cornermode, byte r, byte g, byte b);

		void SetTaskbarDefaultSetting();

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
