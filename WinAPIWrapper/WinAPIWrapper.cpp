#include "pch.h"

#pragma comment(lib, "user32.lib")
#pragma comment(lib, "dwmapi.lib")
#pragma comment(lib, "d2d1.lib")
#pragma comment(lib, "windowsapp")
#pragma comment(lib, "shcore.lib")

#include "WinAPIWrapper.h"

std::vector<HWND> marshalasIntPtrTovectorHwnd(System::Collections::Generic::ICollection<IntPtr>^ list)
{
	if (list == nullptr)
		throw gcnew ArgumentNullException(L"List is null");

	std::vector<HWND> result;
	result.reserve(list->Count);
	for each (IntPtr & ele in list)
	{
		HWND nativeHWND = (HWND)ele.ToPointer();
		result.push_back(nativeHWND);
	}

	return result;
}

HWND marshalasIntPtrToHWND(IntPtr^ hwnd)
{
	return (HWND)hwnd->ToPointer();
}

BOOL CALLBACK DisplayEnumProc(HMONITOR hDisplay, HDC hdcDisplay, LPRECT lprcDisplay, LPARAM dwData)
{
	int* Count = (int*)dwData;
	(*Count)++;
	return TRUE;
}

namespace WinAPIWrapper
{
	WindowmoduleWrapper::WindowmoduleWrapper(byte r, byte g, byte b, byte captionR, byte captionG, byte captionB, int cornerProperty, bool useDWM)
	{
		BorderColor_r = r;
		BorderColor_g = g;
		BorderColor_b = b;

		CaptionColor_R = captionR;
		CaptionColor_G = captionG;
		CaptionColor_B = captionB;

		CornerProperty = static_cast<DWM_WINDOW_CORNER_PREFERENCE>(cornerProperty);

		if (!useDWM)
			m_Windowmodule = new Windowmodule(r, g, b, RGB(captionR, captionG, captionB));
	}

	WindowmoduleWrapper::~WindowmoduleWrapper()
	{
		if (m_Windowmodule)
		{
			delete m_Windowmodule;
			m_Windowmodule = nullptr;
		}

		delete HwndList;
		HwndList = nullptr;
	}

	bool WindowmoduleWrapper::RefreshHwnds(System::Collections::Generic::ICollection<IntPtr>^ hwndlist)
	{
		return m_Windowmodule->RefreshHwnds(marshalasIntPtrTovectorHwnd(hwndlist));
	}

	bool WindowmoduleWrapper::AssginBorders(IntPtr hwnd)
	{
		return m_Windowmodule->AssignBorder((HWND)hwnd.ToPointer());
	}

	/// <summary>
	/// 지정한 윈도우를 BorderWindow 리스트에 추가하며, 지정한 윈도우의 모서리 창을 만듭니다.
	/// </summary>
	/// <param name="hwnd"> - 리스트에 추가할 윈도우 주소</param>
	void WindowmoduleWrapper::AddHwnd(IntPtr hwnd)
	{
		m_Windowmodule->AddHwnd(marshalasIntPtrToHWND(hwnd));
	}

	/// <summary>
	/// Hwnd 리스트에서 지정한 윈도우가 있는지를 로드합니다.
	/// </summary>
	/// <param name="hwnd"> - Hwnd 리스트에서 확인할 윈도우 주소</param>
	/// <returns>Hwnd 리스트에서 지정한 윈도우가 있으면 <c>true</c>, 없으면 <c>false</c>를 반환합니다.</returns>
	bool WindowmoduleWrapper::FindHwnd(IntPtr hwnd)
	{
		return m_Windowmodule->FindHwnd(marshalasIntPtrToHWND(hwnd));
	}

	/// <summary>
	/// 지정한 윈도우의 캡션 텍스트 글자 수를 로드합니다.
	/// </summary>
	/// <param name="hwnd"> - 캡션 텍스트 글자 수를 로드할 윈도우</param>
	/// <returns>지정한 윈도우의 캡션 텍스트 글자 수</returns>
	int WindowmoduleWrapper::GetWindowTitleLength(IntPtr hwnd)
	{
		return GetWindowTextLength(marshalasIntPtrToHWND(hwnd));
	}

	void WindowmoduleWrapper::SetBorderColortoSpecificWindow(IntPtr hwnd, int r, int g, int b)
	{

	}

	void WindowmoduleWrapper::CleanupBorderWindows()
	{
		m_Windowmodule->ClearBorderWindows();
	}

	/// <summary>
	/// 모든 윈도우에 대해서 Mica 기능을 다시 설정합니다.
	/// </summary>
	/// <param name="buildVer"> - 설정하는 윈도우 운영체재의 빌드번호</param>
	/// <returns>기능을 운영체제에서 지원하여 함수가 수행되었으면 <c>true</c>, 수행되지 않으면 <c>false</c>를 반환합니다.</returns>
	bool WindowmoduleWrapper::RestoreDwmMica(int buildVer)
	{
		if (buildVer >= 22000)
		{
			m_Windowmodule->RestoreDwmMica(buildVer);
			return true;
		}
		else
			return false;
	}

	void WindowmoduleWrapper::TrackingWindows()
	{
		m_Windowmodule->TrackingWindows();
	}

	void WindowmoduleWrapper::SetCornerPre(UINT cornerpre)
	{
		m_Windowmodule->cornerPreference = cornerpre;
	}

	void WindowmoduleWrapper::SetCaptionColor(byte r, byte g, byte b)
	{
		m_Windowmodule->CaptionColor = RGB(r, g, b);
	}

	void WindowmoduleWrapper::SetBuildVer(int BuildVer)
	{
		m_Windowmodule->BuildVer = BuildVer;
	}

	void WindowmoduleWrapper::SetWindowBorderColorWithDwm(IntPtr hwnd, bool IsTransparency)
	{
		auto Hwnd = marshalasIntPtrToHWND(hwnd);
		auto BorderColor = RGB(BorderColor_r, BorderColor_g, BorderColor_b);
		if (IsTransparency)
			BorderColor = DWMWA_COLOR_NONE;

		DwmSetWindowAttribute(Hwnd, DWMWA_BORDER_COLOR, &BorderColor, sizeof(BorderColor));
	}

	void WindowmoduleWrapper::SetWindowCaptionColorWithDwm(IntPtr hwnd, bool IsTransparency)
	{
		auto Hwnd = marshalasIntPtrToHWND(hwnd);
		auto CaptionColor = RGB(CaptionColor_R, CaptionColor_G, CaptionColor_B);
		if (IsTransparency)
			CaptionColor = DWMWA_COLOR_NONE;

		DwmSetWindowAttribute(Hwnd, DWMWA_CAPTION_COLOR, &CaptionColor, sizeof(CaptionColor));
	}

	void WindowmoduleWrapper::SetWindowCaptionTextColorWithDwm(IntPtr hwnd, bool IsTransparency)
	{
		auto Hwnd = marshalasIntPtrToHWND(hwnd);
		auto CaptionColor = RGB(CaptionColor_R, CaptionColor_G, CaptionColor_B);
		if (IsTransparency)
			CaptionColor = DWMWA_COLOR_NONE;

		DwmSetWindowAttribute(Hwnd, DWMWA_TEXT_COLOR, &CaptionColor, sizeof(CaptionColor));
	}

	void WindowmoduleWrapper::SetWindowCornerPropertyWithDwm(IntPtr hwnd)
	{
		auto Hwnd = marshalasIntPtrToHWND(hwnd);
		const auto _CornerProperty = CornerProperty;

		DwmSetWindowAttribute(Hwnd, DWMWA_WINDOW_CORNER_PREFERENCE, &_CornerProperty, sizeof(_CornerProperty));
	}

	void WindowmoduleWrapper::SetDefaultWindowOptionWithDWM()
	{
		const COLORREF DefaultColor = DWMWA_COLOR_DEFAULT;
		const auto _CornerProperty = DWMWCP_ROUND;	

		for each (IntPtr hwnd in HwndList)
		{
			auto Hwnd = marshalasIntPtrToHWND(hwnd);
			DwmSetWindowAttribute(Hwnd, DWMWA_BORDER_COLOR, &DefaultColor, sizeof(DefaultColor));
			DwmSetWindowAttribute(Hwnd, DWMWA_CAPTION_COLOR, &DefaultColor, sizeof(DefaultColor));
			DwmSetWindowAttribute(Hwnd, DWMWA_TEXT_COLOR, &DefaultColor, sizeof(DefaultColor));
			DwmSetWindowAttribute(Hwnd, DWMWA_WINDOW_CORNER_PREFERENCE, &_CornerProperty, sizeof(_CornerProperty));
		}
	}

	void WindowmoduleWrapper::SetTaskbarRoundedCornerandBorderColor(int Cornermode, byte r, byte g, byte b)
	{
		const auto cornermode = static_cast<DWM_WINDOW_CORNER_PREFERENCE>(Cornermode);
		const COLORREF color = RGB(r, g, b);

		const int DisplayCount = GetDisplayCount();
		if (DisplayCount < 2)
		{
			if (cornermode != DWMWCP_DONOTROUND)
			{
				const auto TaskbarHWND = FindWindow(L"Shell_TrayWnd", NULL);
				DwmSetWindowAttribute(TaskbarHWND, DWMWA_WINDOW_CORNER_PREFERENCE, &cornermode, sizeof(cornermode));
				DwmSetWindowAttribute(TaskbarHWND, DWMWA_BORDER_COLOR, &color, sizeof(color));
			}
			return;
		}

		if (cornermode != DWMWCP_DONOTROUND)
		{
			for (int i = 0; i < DisplayCount; i++)
			{
				LPWSTR string;
				switch (i)
				{
				case 0:
					string = L"Shell_TrayWnd";
					break;
				case 1:
					string = L"Shell_SecondaryTrayWnd";
				}
				DwmSetWindowAttribute(FindWindow(string, NULL), DWMWA_WINDOW_CORNER_PREFERENCE, &cornermode, sizeof(cornermode));
				DwmSetWindowAttribute(FindWindow(string, NULL), DWMWA_BORDER_COLOR, &color, sizeof(color));
			}
		}
	}

	void WindowmoduleWrapper::SetTaskbarDefaultSetting()
	{
		const COLORREF color = DWMWA_COLOR_DEFAULT;
		const auto cornermode = DWMWCP_DONOTROUND;
		const int DisplayCount = GetDisplayCount();

		//auto TaskbarHWND = FindWindow(L"Shell_TrayWnd", NULL);
		if (DisplayCount < 2)
		{
			const auto TaskbarHWND = FindWindow(L"Shell_TrayWnd", NULL);
			DwmSetWindowAttribute(TaskbarHWND, DWMWA_WINDOW_CORNER_PREFERENCE, &cornermode, sizeof(cornermode));
			DwmSetWindowAttribute(TaskbarHWND, DWMWA_BORDER_COLOR, &color, sizeof(color));

			return;
		}

		for (int i = 0; i < DisplayCount; i++)
		{
			LPWSTR string;
			switch (i)
			{
			case 0:
				string = L"Shell_TrayWnd";
				break;
			case 1:
				string = L"Shell_SecondaryTrayWnd";
			}
			DwmSetWindowAttribute(FindWindow(string, NULL), DWMWA_WINDOW_CORNER_PREFERENCE, &cornermode, sizeof(cornermode));
			DwmSetWindowAttribute(FindWindow(string, NULL), DWMWA_BORDER_COLOR, &color, sizeof(color));
		}
	}

	int WindowmoduleWrapper::GetDisplayCount()
	{
		int Count = 0;
		if (EnumDisplayMonitors(NULL, NULL, DisplayEnumProc, (LPARAM)&Count))
			return Count;
		return -1;
	}
}