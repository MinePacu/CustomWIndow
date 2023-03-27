#pragma once
#include "../WinAPI/Windowmodule.h"

using namespace System;

namespace WinAPIWrapper {
	public ref class WindowmoduleWrapper
	{
		// TODO: 여기에 이 클래스에 대한 메서드를 추가합니다.
	protected:
		Windowmodule* m_Windowmodule;

	public:
		WindowmoduleWrapper();
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
		void SetBuildVer(int BuildVer);
	};
}
