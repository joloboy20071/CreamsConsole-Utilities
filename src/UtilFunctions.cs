namespace CreamsConsole_utils
{

    public static class utilFunctions
    {
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
                }



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
}
