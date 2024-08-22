using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreamsConsole_utils;
public class ColorText
{
    public static Dictionary<string, ConsoleColor> allColors = new Dictionary<string, ConsoleColor>();
    


    public static void innitAllColors()
    {
        foreach (ConsoleColor val in Enum.GetValues(typeof(ConsoleColor)))
        {

            string colorBase = $"{val.ToString()}";
            allColors.Add(colorBase, val);

        }
    }

    internal static string stringhelper(string Stringin)
    {
        var Stringinn = $" {Stringin}";
        string[] lines = Stringinn.Split("[/]", 3);
        Console.Write($"{lines[0].Remove(0,1)}");
        if (lines.Length > 2)
        {
            var console = allColors[lines[1]];
            string[] newlines = lines[2].Split("[//]",2);
            
            ColorWrite(newlines[0], console);
            if (newlines.Length > 1) { return newlines[1]; }
        }
        return string.Empty;
    } 

    public static void ColorWriteIn(string Stringin)
    {
        innitAllColors();
        List<string> strings = new List<string>();
        strings.Add(Stringin);
        while (strings.Count!> 0)
        {
            var output = stringhelper(strings[0]);
           
            if (output != string.Empty) {strings.Clear(); strings.Add(output); }
            else { strings.Clear(); }


        }
    }
    public static void ColorWriteLineIn(string streingin)
    {
        ColorWriteIn(streingin);
        

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








}
