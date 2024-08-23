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

        public string title = string.Empty;
        public string description = string.Empty;
        
        public string HoverPrefix = " > ";

        public ConsoleKey SelectionKey = ConsoleKey.Enter;
        public ConsoleKey exitKey = ConsoleKey.Escape;

        protected internal int hoverindex = 0;
        public readonly ItemslectionMenu exit = new ItemslectionMenu() { title = "Exit", ReturnID = "specialKeyExit" };
        public List<ItemslectionMenu> items = new List<ItemslectionMenu>();
        public bool active = true;
        public bool HasExit = true;



        public void AddChoise(string Title, string returnID = null)
        {
            if (returnID == null) { returnID = Title; }
            var item = new ItemslectionMenu() { title = Title, ReturnID = returnID };
            items.Add(item);

        }




        public void AddChoise(string Title)
        {

            var item = new ItemslectionMenu() { title = Title, ReturnID = Title, };
            items.Add(item);

        }
    }
    private static void SelectedPrint(config config, int i)
    {
        if (config.items[i].IsHover) { ColorText.ColorWriteLine($"{config.HoverPrefix}{config.items[i].title}", config.hoverColor); }
        else { ColorText.ColorWriteLine($" {config.items[i].title}", config.unselectedColor); }


    }


    internal static void shiftHover(ConsoleKey key, config config)
    {
        if (key == ConsoleKey.UpArrow && config.hoverindex != 0) { config.hoverindex -= 1; config.items[config.hoverindex].IsHover = true; config.items[config.hoverindex + 1].IsHover = false; }
        if (key == ConsoleKey.DownArrow) { if (!(config.hoverindex == config.items.Count - 1)) { config.hoverindex += 1; config.items[config.hoverindex].IsHover = true; config.items[config.hoverindex - 1].IsHover = false; } }


    }

    internal static void printMenu(config config, int startY, int endY = -1)
    {
        if (config.hoverindex == 0) { config.items[0].IsHover = true; }

        for (var i = 0; i < config.items.Count; i++)
        {


            SelectedPrint(config, i);
            Console.WriteLine();


        }



    }

    public static string runtimeMenu(config config)
    {
        if (config.HasExit)
        {
            config.items.Add(config.exit);
        }

        var startY = Console.GetCursorPosition().Top;
        var endY = 0;
        Console.CursorVisible = false;
        while (config.active)
        {
            utilFunctions.ClearBetweenConsoleLines(startY, endY);

            if (config.title != string.Empty)
            {
                ColorText.ColorWriteLineIn($"{config.title}");
            }
            if (config.description != string.Empty)
            {
                ColorText.ColorWriteLine($"{config.description}\n", ConsoleColor.DarkGray);
            }


            printMenu(config, startY, endY);
            ColorText.ColorWriteLineIn($"[/]DarkGray[/]\nUse[//][/]Green[/] <{config.SelectionKey.ToString()}>,<arrow keys>[//][/]DarkGray[/] to select a option\n[//]");
            var key = Console.ReadKey().Key;
            if (key == config.SelectionKey)
            {


                utilFunctions.ClearBetweenConsoleLines(startY, endY);
                Console.CursorVisible = true;
                return config.items[config.hoverindex].ReturnID;

            }
            if (key == config.SelectionKey && config.items[config.items.Count - 1].IsHover && config.HasExit) { utilFunctions.ClearBetweenConsoleLines(startY, endY); config.active = false; }
            if (key == config.exitKey && config.HasExit) { Console.CursorVisible = true; utilFunctions.ClearBetweenConsoleLines(startY, endY); ; return config.exit.ReturnID; }
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



