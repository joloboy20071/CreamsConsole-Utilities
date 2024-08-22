

namespace CreamsConsole_utils
{

    public static class utilFunctions
    {
        public static double round(double num)
        {
            double rounded = Math.Round(num);
            if (rounded > num) { return rounded - 1; }
            else { return rounded; }

        }

        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void ColorWriteLine(string text, ConsoleColor color)
        {
            Console.ResetColor();
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }


        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ClearBetweenConsoleLines(int y1, int y2)
        {
            if (y2 != -1) { 
            for (int i = 0; i < (y2 - y1)+y1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.BufferWidth));
            }



            Console.SetCursorPosition(0, y1);
        } }

        


    }
}
