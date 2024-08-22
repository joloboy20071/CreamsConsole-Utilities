
using System.Runtime.InteropServices;


namespace CreamsConsole_utils;
internal class consoleAlloc
{
    private const int MF_BYCOMMAND = 0x00000000;



    public const int SC_MINIMIZE = 0xF020;
    public const int SC_MAXIMIZE = 0xF030;
    public const int SC_SIZE = 0xF000;

    [DllImport("kernel32", SetLastError = true)]
    static extern bool AttachConsole(uint dwProcessId);
    [DllImport("kernel32", SetLastError = true)]
    static extern bool FreeConsole();
    [DllImport("kernel32", SetLastError = true)]
    static extern bool AllocConsole();
    [DllImport("user32.dll")]
    // Documentation: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-deletemenu
    public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

    [DllImport("user32.dll")]
    // Documentation: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmenu
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    // Documentation: https://docs.microsoft.com/en-us/windows/console/getconsolewindow
    private static extern IntPtr GetConsoleWindow();


}
