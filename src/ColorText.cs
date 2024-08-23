using System.Drawing;
using System.Text;


namespace CreamsConsole_utils;
public class ColorText
{
    
    /// <summary>
    /// A dict with a color name as a key and returns the hex string as value
    /// </summary>
    public static Dictionary<string, string> allColors = new Dictionary<string, string>() {
        
        { "Black","#000000"},
        { "Blue","#416fe7"},
        { "Cyan","#00FFFF"},
        { "DarkBlue","#00008B"},
        { "DarkCyan","#008B8B"},
        { "DarkGray","#A9A9A9"},
        { "DarkGreen","#29b920"},
        { "DarkMagenta","#8B008B"},
        { "DarkRed","#8B0000"},
        { "DarkYellow","#d7c32a"},
        { "Gray","#808080"},
        { "Green","#008000"},
        { "Magenta","#FF00FF"},
        { "Red","#FF0000"},
        { "White","#FFFFFF"},
        { "Yellow","#FFFF00" }
    };

    public static List<string> CustomColorNames = new List<string>();



    public static Color HexToRGB(string HexString)
    {
        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(HexString);
        return col;

    }

    public static Color ConsoleColorToRGB(ConsoleColor color)
    {
        var RGB = HexToRGB(allColors[color.ToString()]);
        return RGB;
    }


    protected internal static string stringhelper(string Stringin)
    {
        var Stringinn = $" {Stringin}";
        var lines = Stringinn.Split("[/]", 3);
        Console.Write($"{lines[0].Remove(0, 1)}");
        if (lines.Length > 2)
        {
            var cOnsole = allColors[lines[1]];
            var newlines = lines[2].Split("[//]", 2);



            ColorWrite(newlines[0], HexToRGB(cOnsole));
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

        var strings = new List<string>();
        strings.Add(Stringin);
        while (strings.Count! > 0)
        {
            var output = stringhelper(strings[0]);

            if (output != string.Empty) { strings.Clear(); strings.Add(output); }
            else { strings.Clear(); }


        }
    }

    public static void writeRGB(string text, Color color, string modifireString = null)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write($"\x1b[38;2;{color.R};{color.G};{color.B}{modifireString}m{text}\x1b[0m");
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
    /// Write a string with a color
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public static void ColorWrite(string text, Color color)
    {
       writeRGB(text, color);  
    }


    /// <summary>
    /// Write a string with a color to a new line
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    public static void ColorWriteLine(string text, Color color)
    {
        writeRGB(text, color);
        Console.WriteLine();
    }

    public static void CreateCustomColor(string name, string Hexcode)
    {
        allColors[name] = Hexcode;
        CustomColorNames.Add(name);


    }






}
