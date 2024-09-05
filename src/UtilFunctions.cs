
using System.Runtime.InteropServices;


namespace ConsoleExtender
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConsoleFont
    {
        public uint Index;
        public short SizeX, SizeY;
    }

    public static class ConsoleHelper
    {
       

       

        [DllImport("kernel32")]
        private extern static bool SetConsoleFont(IntPtr hOutput, uint index);

        private enum StdHandle
        {
            OutputHandle = -11
        }

        [DllImport("kernel32")]
        private static extern IntPtr GetStdHandle(StdHandle index);

        public static bool SetConsoleFont(uint index)
        {
            return SetConsoleFont(GetStdHandle(StdHandle.OutputHandle), index);
        }

        [DllImport("kernel32")]
        private static extern bool GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)] bool bMaximize,
            uint count, [MarshalAs(UnmanagedType.LPArray), Out] ConsoleFont[] fonts);

        [DllImport("kernel32")]
        private static extern uint GetNumberOfConsoleFonts();

        public static uint ConsoleFontsCount
        {
            get
            {
                return GetNumberOfConsoleFonts();
            }
        }

        public static ConsoleFont[] ConsoleFonts
        {
            get
            {
                ConsoleFont[] fonts = new ConsoleFont[GetNumberOfConsoleFonts()];
                if (fonts.Length > 0)
                    GetConsoleFontInfo(GetStdHandle(StdHandle.OutputHandle), false, (uint)fonts.Length, fonts);
                return fonts;
            }
        }

    }
}

namespace CreamsConsole_utils
{
    static class StringExtensions
    {

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength)
        {
            
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

    }






    public static class utilFunctions
    {
        

        public static void maximizeTerminal()
        {
            [DllImport("kernel32.dll", ExactSpelling = true)]
            static extern IntPtr GetConsoleWindow();

            [DllImport("user32.dll")]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            const int SW_MAXIMIZE = 3;


            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_MAXIMIZE);
            




            //Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            [DllImport("user32.dll")]
            static extern IntPtr GetForegroundWindow();


            [DllImport("user32.dll")]
            static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
            [DllImport("user32.dll")]
            static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
            // Constants for the ShowWindow function

            // Get the handle of the console window
            IntPtr consoleWindowHandle = GetForegroundWindow();
            // Maximize the console window
            ShowWindow(consoleWindowHandle, SW_MAXIMIZE);
            // Get the screen size
            Rect screenRect;
            GetWindowRect(consoleWindowHandle, out screenRect);
            // Resize and reposition the console window to fill the screen
            int width = screenRect.Right - screenRect.Left;
            int height = screenRect.Bottom - screenRect.Top;
            MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);

         
        }



        public static double round(double num)
        {
            var rounded = Math.Round(num);
            if (rounded > num) { return rounded - 1; }
            else { return rounded; }

        }



        public static void ClearCurrentConsoleLine()
        {
            var currentLineCursor = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ClearBetweenConsoleLines(int y1, int y2)
        {
            if (y2 != -1)
            {
                for (var i = 0; i < y2 - y1; i++)
                {
                    Console.SetCursorPosition(0, i + y1);
                    Console.Write(new string(' ', Console.BufferWidth));
                };
                Console.SetCursorPosition(0, y1);
            }
        }

        public static class EnumUtil
        {
            public static IEnumerable<T> GetValues<T>()
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }

    }
};
