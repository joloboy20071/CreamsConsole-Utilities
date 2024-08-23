namespace CreamsConsole_utils;
public class ColorText
{
    /// <summary>
    /// A dict with a color name as a key and returns the consoleColor as value
    /// </summary>
    public static Dictionary<string, ConsoleColor> allColors = new Dictionary<string, ConsoleColor>();

    /// <summary>
    /// Contains all availabe color names
    /// </summary>
    public static List<string> allColorNames = new List<string>();


    public static void innitAllColors()
    {

        if (allColors.Count != 16)
        {
            foreach (ConsoleColor val in Enum.GetValues(typeof(ConsoleColor)))
            {

                var colorBase = $"{val.ToString()}";
                allColors.Add(colorBase, val);
                allColorNames.Add(colorBase);

            }
        }
        
    }

    protected internal static string stringhelper(string Stringin)
    {
        var Stringinn = $" {Stringin}";
        var lines = Stringinn.Split("[/]", 3);
        Console.Write($"{lines[0].Remove(0, 1)}");
        if (lines.Length > 2)
        {
            var console = allColors[lines[1]];
            var newlines = lines[2].Split("[//]", 2);

            ColorWrite(newlines[0], console);
            if (newlines.Length > 1) { return newlines[1]; }
        }
        return string.Empty;
    }
    /// <summary>
    /// A function to write Inline colored console text 
    /// </summary>
    /// <param name="Stringin"></param>
    public static void ColorWriteIn(string Stringin)
    {
        innitAllColors();
        var strings = new List<string>();
        strings.Add(Stringin);
        while (strings.Count! > 0)
        {
            var output = stringhelper(strings[0]);

            if (output != string.Empty) { strings.Clear(); strings.Add(output); }
            else { strings.Clear(); }


        }
    }



    /// <summary>
    /// A function to write Inline colored console text to a new line
    /// </summary>
    /// <param name="streingin"></param>
    public static void ColorWriteLineIn(string streingin)
    {
        ColorWriteIn(streingin);
        Console.WriteLine();

    }

    
    /// <summary>
    /// Write a string with a Consolecolor
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public static void ColorWrite(string text, ConsoleColor color)
    {
        Console.ResetColor();
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }


    /// <summary>
    /// Write a string with a Consolecolor to a new line
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public static void ColorWriteLine(string text, ConsoleColor color)
    {
        Console.ResetColor();
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }








}
