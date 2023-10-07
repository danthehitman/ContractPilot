using System;
using System.Runtime.InteropServices;

namespace ContractPilot;

internal static class ConsoleAllocator
{
	private const int SwHide = 0;

	private const int SwShow = 5;

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern bool AllocConsole();

	[DllImport("kernel32.dll")]
	private static extern nint GetConsoleWindow();

	[DllImport("user32.dll")]
	private static extern bool ShowWindow(nint hWnd, int nCmdShow);

	public static void ShowConsoleWindow()
	{
		nint handle = GetConsoleWindow();
		if (handle == IntPtr.Zero)
		{
			AllocConsole();
		}
		else
		{
			ShowWindow(handle, 5);
		}
	}

	public static void HideConsoleWindow()
	{
		nint handle = GetConsoleWindow();
		ShowWindow(handle, 0);
	}
}
