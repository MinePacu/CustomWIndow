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


namespace WinAPIWrapper
{
	WindowmoduleWrapper::WindowmoduleWrapper() :
		m_Windowmodule(new Windowmodule())
	{

	}

	WindowmoduleWrapper::~WindowmoduleWrapper()
	{
		if (m_Windowmodule)
		{
			delete m_Windowmodule;
			m_Windowmodule = nullptr;
		}
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

	void WindowmoduleWrapper::SetBuildVer(int BuildVer)
	{
		m_Windowmodule->BuildVer = BuildVer;
	}
}
