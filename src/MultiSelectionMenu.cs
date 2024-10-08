﻿namespace CreamsConsole_utils;
public class MultiSelectionMenu
{
    public class config
    {


        public ConsoleColor unselectedColor = ConsoleColor.White;
        public ConsoleColor hoverColor = ConsoleColor.Blue;

        public List<MultiSelectionMenuItem> items = new List<MultiSelectionMenuItem>();

        public string title = string.Empty;
        public string description = string.Empty;

        public string selectedString = "[X]";
        public string UnselectedString = "[ ]";
        public string HoverPrefix = " > ";
        public bool useSpacesBetweenOptions = true;


        public ConsoleKey SelectionKey = ConsoleKey.Enter;
        public ConsoleKey exitKey = ConsoleKey.Escape;

        //Internal stuff
        public readonly MultiSelectionMenuItem save = new MultiSelectionMenuItem() { title = "Save", ReturnID = "specialKey" };
        public readonly MultiSelectionMenuItem exit = new MultiSelectionMenuItem() { title = "Exit", ReturnID = "specialKeyExit" };
        protected internal int hoverindex = 0;
        public bool active = true;


        public void AddChoise( string Title, bool IsSelected = false, string returnID = null)
        {
            if (returnID == null) { returnID = Title; }
            var item = new MultiSelectionMenuItem() { title = Title, ReturnID = returnID, isSelected = IsSelected };
            items.Add(item);

        }
        public void AddChoise(string Title, string returnID = null, bool IsSelected = false)
        {
            if (returnID == null) { returnID = Title; }
            var item = new MultiSelectionMenuItem() { title = Title, ReturnID = returnID, isSelected = IsSelected };
            items.Add(item);

        }
        public void AddChoise(string Title, string returnID = null)
        {
            if (returnID == null) { returnID = Title; }
            var item = new MultiSelectionMenuItem() { title = Title, ReturnID = returnID, isSelected = false };
            items.Add(item);

        }
        public void AddChoise(string Title, bool IsSelected = false)
        {

            var item = new MultiSelectionMenuItem() { title = Title, ReturnID = Title, isSelected = IsSelected };
            items.Add(item);

        }


    }


    public class ReturnedData
    {
        public Dictionary<string, bool> returnDict = new Dictionary<string, bool>();

        public List<string> returnIDS = new List<string>();
        public bool isSaved;

    }


    public class MultiSelectionMenuItem
    {
        public required string title;
        public bool isSelected;
        public bool IsHover;
        public required string ReturnID;



    }

    internal static string ReturnItemString(MultiSelectionMenuItem item, string selectedString, string UnselectedString)
    {

        if (item.ReturnID != "specialKey" && item.ReturnID != "specialKeyExit")
        {
            if (item.isSelected) { return $"{selectedString} {item.title}"; }
            else { return $"{UnselectedString} {item.title}"; }
        }
        else
        {
            return $"{item.title}";
        }

    }
    public static void SelectedPrint(config config, int i)
    {
        if (config.useSpacesBetweenOptions)
        {
            if (config.items[i].IsHover) { ColorText.ColorWriteLine($"{config.HoverPrefix}{ReturnItemString(config.items[i], config.selectedString, config.UnselectedString)}", ColorText.ConsoleColorToRGB(config.hoverColor)); }
            else { ColorText.ColorWriteLine(ReturnItemString(config.items[i], config.selectedString, config.UnselectedString), ColorText.ConsoleColorToRGB(config.unselectedColor)); }
        }
        else
        {
            if (config.items[i].IsHover)
            {
                if (config.items[i].ReturnID != "specialKey")
                {
                    ColorText.ColorWrite($"{config.HoverPrefix}{ReturnItemString(config.items[i], config.selectedString, config.UnselectedString)}", ColorText.ConsoleColorToRGB(config.hoverColor));
                }
                else
                {
                    ColorText.ColorWriteLine($"\n{config.HoverPrefix}{ReturnItemString(config.items[i], config.selectedString, config.UnselectedString)}", ColorText.ConsoleColorToRGB(config.hoverColor));
                }
            }

            else
            {
                if (config.items[i].ReturnID != "specialKey")
                {
                    ColorText.ColorWrite(ReturnItemString(config.items[i], config.selectedString, config.UnselectedString), ColorText.ConsoleColorToRGB(config.unselectedColor));
                }
                else { ColorText.ColorWriteLine($"\n{ReturnItemString(config.items[i], config.selectedString, config.UnselectedString)}", ColorText.ConsoleColorToRGB(config.unselectedColor)); }
            }

        }


    }

    internal static void printMenu(config config, int startY, int endY = -1)
    {
        if (config.hoverindex == 0) { config.items[0].IsHover = true; }

        for (var i = 0; i < config.items.Count; i++)
        {

            if (i < config.items.Count - 2)
            {
                SelectedPrint(config, i);
                Console.WriteLine();
            }
            else
            {
                SelectedPrint(config, i);
            }
        }



    }

  


    internal static void shiftHover(ConsoleKey key, config config)
    {
        if (key == ConsoleKey.UpArrow && config.hoverindex != 0) { config.hoverindex -= 1; config.items[config.hoverindex].IsHover = true; config.items[config.hoverindex + 1].IsHover = false; }
        if (key == ConsoleKey.DownArrow) { if (!(config.hoverindex == config.items.Count - 1)) { config.hoverindex += 1; config.items[config.hoverindex].IsHover = true; config.items[config.hoverindex - 1].IsHover = false; } }
        if (key == config.SelectionKey) { config.items[config.hoverindex].isSelected = !config.items[config.hoverindex].isSelected; }
        if (key == config.exitKey) { Console.CursorVisible = true; config.active = false; }
    }

    public static ReturnedData runtimeMenu(config config)
    {
        config.items.Add(config.save);
        config.items.Add(config.exit);

        var startY = Console.CursorTop;
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
                ColorText.ColorWriteLine($"{config.description}\n", ColorText.HexToRGB(ColorText.allColors["DarkGray"]));
            }


            printMenu(config, startY, endY);
            ColorText.ColorWriteLineIn($"[/]DarkGray[/]\nUse[//][/]Green[/] <{config.SelectionKey.ToString()}>,<arrow keys>[//][/]DarkGray[/] to select a option\n[//]");
            var key = Console.ReadKey().Key;
            if (key == config.SelectionKey && config.items[config.items.Count - 2].IsHover)
            {
                var type = new ReturnedData();
                for (var i = 0; config.items.Count - 2 > i; i++)
                {
                    type.returnDict.Add(config.items[i].ReturnID, config.items[i].isSelected);
                    type.returnIDS.Add(config.items[i].ReturnID);
                }
                utilFunctions.ClearBetweenConsoleLines(startY, endY);
                type.isSaved = true;
                //config.active = false;
                Console.CursorVisible = true;
                return type;

            }
            if (key == config.SelectionKey && config.items[config.items.Count - 1].IsHover) { config.active = false; }

            shiftHover(key, config);

            //Console.WriteLine(config.hoverindex);
            endY -= endY;
            endY += Console.CursorTop;



        }
        Console.CursorVisible = true;
        utilFunctions.ClearBetweenConsoleLines(startY, endY);
        return new ReturnedData();
    }























}
