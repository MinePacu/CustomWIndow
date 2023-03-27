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
	/// ������ �����츦 BorderWindow ����Ʈ�� �߰��ϸ�, ������ �������� �𼭸� â�� ����ϴ�.
	/// </summary>
	/// <param name="hwnd"> - ����Ʈ�� �߰��� ������ �ּ�</param>
	void WindowmoduleWrapper::AddHwnd(IntPtr hwnd)
	{
		m_Windowmodule->AddHwnd(marshalasIntPtrToHWND(hwnd));
	}

	/// <summary>
	/// Hwnd ����Ʈ���� ������ �����찡 �ִ����� �ε��մϴ�.
	/// </summary>
	/// <param name="hwnd"> - Hwnd ����Ʈ���� Ȯ���� ������ �ּ�</param>
	/// <returns>Hwnd ����Ʈ���� ������ �����찡 ������ <c>true</c>, ������ <c>false</c>�� ��ȯ�մϴ�.</returns>
	bool WindowmoduleWrapper::FindHwnd(IntPtr hwnd)
	{
		return m_Windowmodule->FindHwnd(marshalasIntPtrToHWND(hwnd));
	}

	/// <summary>
	/// ������ �������� ĸ�� �ؽ�Ʈ ���� ���� �ε��մϴ�.
	/// </summary>
	/// <param name="hwnd"> - ĸ�� �ؽ�Ʈ ���� ���� �ε��� ������</param>
	/// <returns>������ �������� ĸ�� �ؽ�Ʈ ���� ��</returns>
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
	/// ��� �����쿡 ���ؼ� Mica ����� �ٽ� �����մϴ�.
	/// </summary>
	/// <param name="buildVer"> - �����ϴ� ������ �ü���� �����ȣ</param>
	/// <returns>����� �ü������ �����Ͽ� �Լ��� ����Ǿ����� <c>true</c>, ������� ������ <c>false</c>�� ��ȯ�մϴ�.</returns>
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
