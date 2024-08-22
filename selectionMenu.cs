


namespace CreamsConsole_utils;
public class selectionMenu
{
    public class ItemslectionMenu
    {
        public required string title;
        public bool IsHover;
        public required string ReturnID;
    }


    public class config
    {


        public ConsoleColor unselectedColor = ConsoleColor.White;
        public ConsoleColor hoverColor = ConsoleColor.Blue;
        public List<ItemslectionMenu> items = new List<ItemslectionMenu>();
        public string title = string.Empty;
        public string description = string.Empty;
        protected internal int hoverindex = 0;
        public bool active = true;
        public bool HasExit = true;


        public readonly ItemslectionMenu exit = new ItemslectionMenu() { title = "Exit", ReturnID = "specialKeyExit" };


        public void AddChoise(string Title, string returnID = null)
        {
            if (returnID == null) { returnID = Title; }
            ItemslectionMenu item = new ItemslectionMenu() { title = Title, ReturnID = returnID };
            items.Add(item);

        }




        public void AddChoise(string Title)
        {

            ItemslectionMenu item = new ItemslectionMenu() { title = Title, ReturnID = Title, };
            items.Add(item);

        }
    }
        private static void SelectedPrint(config config, int i)
        {
            if (config.items[i].IsHover) { utilFunctions.ColorWriteLine($" > {config.items[i].title}", config.hoverColor); }
            else { utilFunctions.ColorWriteLine($" {config.items[i].title}", config.unselectedColor); }


        }
    

    internal static void shiftHover(ConsoleKey key, config config)
    {
        if (key == ConsoleKey.UpArrow && config.hoverindex != 0) { config.hoverindex -= 1; config.items[config.hoverindex].IsHover = true; config.items[config.hoverindex + 1].IsHover = false; }
        if (key == ConsoleKey.DownArrow) { if (!(config.hoverindex == config.items.Count - 1)) { config.hoverindex += 1; config.items[config.hoverindex].IsHover = true; config.items[config.hoverindex - 1].IsHover = false; } }

        
    }

    internal static void printMenu(config config, int startY, int endY = -1)
    {
        if (config.hoverindex == 0) { config.items[0].IsHover = true; }

        for (int i = 0; i < config.items.Count; i++)
        {

          
                SelectedPrint(config, i);
                Console.WriteLine();
            
            
        }



    }
    internal static void PrintTitle(config config)
    {

        utilFunctions.ColorWrite("\nuse ", ConsoleColor.DarkGray);
        utilFunctions.ColorWrite("<enter>, <arrow keys>", ConsoleColor.Green);
        utilFunctions.ColorWrite(" to select a option\n\n", ConsoleColor.DarkGray);

    }

    public static string runtimeMenu(config config)
    {
        if (config.HasExit)
        {
            config.items.Add(config.exit);
        }

        int startY = Console.CursorTop;
        int endY = 0;
        Console.CursorVisible = false;
        while (config.active)
        {
            utilFunctions.ClearBetweenConsoleLines(startY, endY);

            if (config.title != string.Empty)
            {
                Console.WriteLine($"{config.title}");
            }
            if (config.description != string.Empty)
            {
                utilFunctions.ColorWriteLine($"{config.description}\n", ConsoleColor.DarkGray);
            }


            printMenu(config, startY, endY);
            PrintTitle(config);
            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.Enter)
            {


                //config.active = false;
                Console.CursorVisible = true;
                return config.items[config.hoverindex].ReturnID;

            }
            if (key == ConsoleKey.Enter && config.items[config.items.Count - 1].IsHover&& config.HasExit) { config.active = false; }
            if (key == ConsoleKey.Escape && config.HasExit) { Console.CursorVisible = true; return config.exit.ReturnID; }
            shiftHover(key, config);

            //Console.WriteLine(config.hoverindex);
            endY -= endY;
            endY += Console.CursorTop;



        }
        Console.CursorVisible = true;
        //return config.exit.ReturnID;
        return $"{config.exit.ReturnID}";
    }
}

    

