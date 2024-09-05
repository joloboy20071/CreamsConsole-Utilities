using System;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Text;





namespace CreamsConsole_utils;
[Serializable]


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
        { "Gray","#a5a5a5"},
        { "Green","#008000"},
        { "Magenta","#FF00FF"},
        { "Red","#FF0000"},
        { "White","#FFFFFF"},
        { "Yellow","#FFFF00" }
    };

    public class style
    {
        public  bool Bold = false;
        private string bold = "1;";

        public  bool Italic;
        private string italic = "3;";

        public  bool UnderLine;
        private string underline = "4;";

        public  bool Strike;
        public string strike = "9;";

        public  bool Invert;
        private string invert = "7;";
        




        public string ReturnStyleString()
        {
            List<string> list = new List<string>() { bold, italic, underline, strike, invert };
            List<bool> options = new List<bool>() { Bold, Italic, UnderLine, Strike, Invert };

            string styleString = ";";
            for (int i = 0; i < options.Count; i++) {
                if (options[i])
                {
                    styleString += list[i];
                }
            }
            return styleString.Remove(styleString.Length - 1);


        }




    }
    public static void setOptions(string option, style style)
    {
        
        try
        {
            switch (option)
            {

                case "Bold":
                    style.Bold = true;
                    break;
                case "Italic":
                    style.Italic = true;
                    break;
                case "UnderLine":
                    style.UnderLine = true;
                    break;
                case "Strike":
                    style.Strike = true;
                    break;
                case "Invert":
                    style.Invert = true;
                    break;



            }



        }
        catch { throw new InvalidStyleOption($"optiom {option} was invalid"); }
  

    }

    public style DefaultStyle = new style();

    public static List<string> CustomColorNames = new List<string>();

    public static string testfunc(string inn)
    {
        return stringhelper(inn);
    }

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

    protected internal static string StyleHelper(string Stringin)
    {
        string options = Stringin.Replace("{", "").Replace("}_", "").Replace(" ", "");
        string[] optionArrry = options.Split(',');
        style style = new style();
        for (int i = 0; i < optionArrry.Length; i++) {
            setOptions(optionArrry[i], style);
        }

        return style.ReturnStyleString();

    }



    protected internal static string stringhelper(string Stringin)
    {
        var Stringinn = $" {Stringin}";
        var lines = Stringinn.Split("[/]", 3);
        Console.Write($"{lines[0].Remove(0, 1)}");
        if (lines.Length > 2)
        {
            
            if (lines[1].Contains("}_")) {
                string cOnsole;
                var styleANDColor = lines[1].Split("}_");
                if (styleANDColor[1] != "")
                {
                     cOnsole = allColors[styleANDColor[1]];
                }
                else {  cOnsole = allColors["Gray"]; }


                string StyleString = StyleHelper(styleANDColor[0]);
                string[] newline = lines[2].Split("[//]", 2);

                writeRGB(newline[0], HexToRGB(cOnsole), StyleString);

                if (newline[1] != "") { return newline[1]; } else { return string.Empty; }
            }
            else {
                var style = new style();
                var cOnsole = allColors[lines[1]];


                string[] newlines = lines[2].Split("[//]", 2);



                writeRGB(newlines[0], HexToRGB(cOnsole), style.ReturnStyleString());


                string line = newlines[1];
                if (newlines[1] != " ")
                {
                    if (newlines[1] != "") { return line; }
                }
                else
                {
                    return string.Empty;
                }
            } 
        }
        
        return string.Empty;
    } 
    /// <summary>
    /// A function to write Inline colored console text 
    /// </summary>
    /// <param name="Stringin"></param>
    public static void ColorWriteIn(string Stringin, int? Limit=null)
    {
        if(Limit == null) {
        var strings = new List<string>();
        strings.Add(Stringin);
        while (strings.Count! > 0)
        {
            var output = stringhelper(strings[0]);

            if (output != string.Empty) { strings.Clear(); strings.Add(output); }
            else { strings.Clear(); }


        }}
    }

    public static void writeRGB(string text, Color color, string modifireString = null)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write($"\x1b[38;2;{color.R};{color.G};{color.B}{modifireString}m{text}\x1b[0m");
    }
    public static void writeRGB(string text, Color color)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Write($"\x1b[38;2;{color.R};{color.G};{color.B}m{text}\x1b[0m");
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

    public static bool PerformanceTesting(string message)
    {
        ColorWriteLineIn(message);
        return true;

    }






}
