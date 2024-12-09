
using System.Runtime.InteropServices;


namespace CreamsConsole_utils;
public class consoleAlloc
{
    private const int MF_BYCOMMAND = 0x00000000;
    const int STD_OUPUT_HANDLE = -11;

    private const int STD_INPUT_HANDLE = -10;

    private const int STD_OUTPUT_HANDLE = -11;

    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

    private const uint ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200;
    public const int SC_MINIMIZE = 0xF020;
    public const int SC_MAXIMIZE = 0xF030;
    public const int SC_SIZE = 0xF000;

    [DllImport("kernel32", SetLastError = true)]
    static extern bool AttachConsole(uint dwProcessId);
    [DllImport("kernel32", SetLastError = true)]
    public static extern bool FreeConsole();
    [DllImport("kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AllocConsole();
    [DllImport("user32.dll")]
    // Documentation: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-deletemenu
    public static extern int DeleteMenu(nint hMenu, int nPosition, int wFlags);

    [DllImport("user32.dll")]
    // Documentation: https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmenu
    private static extern nint GetSystemMenu(nint hWnd, bool bRevert);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    // Documentation: https://docs.microsoft.com/en-us/windows/console/getconsolewindow
    private static extern nint GetConsoleWindow();
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [DllImport("kernel32.dll")]
    public static extern uint GetLastError();

    private static bool consoleinit = false;

    public static bool Consoleinit {
        get { return consoleinit; }
    
    }



    public static void setupCreamsConsole()
    {
        if (!consoleinit)
        {
            AllocConsole();

            var iStdIn = GetStdHandle(STD_INPUT_HANDLE);
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);

            if (!GetConsoleMode(iStdIn, out uint inConsoleMode))
            {
                Console.WriteLine("failed to get input console mode");
                Console.ReadKey();
                return;
            }
            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                Console.WriteLine("failed to get output console mode");
                Console.ReadKey();
                return;
            }

            inConsoleMode |= ENABLE_VIRTUAL_TERMINAL_INPUT;
            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;

            if (!SetConsoleMode(iStdIn, inConsoleMode))
            {
                Console.WriteLine($"failed to set input console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                Console.ReadKey();
                return;
            }


            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;


            consoleinit = true;




        }
    }


    public static void ResizeCreamsConsole(Boxsize size ) {
        ResizeCreamsConsole((int)size.width, (int)size.height);
    }


    public static void ResizeCreamsConsole(int x, int y) {
        if (Consoleinit) {
            Console.SetWindowSize(x, y);
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            return;
        }
        throw new NoConsoleInit("console was not init make sure this fucntion is called after the setupCreamsConsole(); ");
    
    }










}
