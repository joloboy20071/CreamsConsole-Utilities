
using System.Drawing;
using Microsoft.VisualBasic;
using static CreamsConsole_utils.UnicodeROM;
namespace CreamsConsole_utils;

public class BoxType(Location location, int width, int height, string? Title)
{
    public readonly int StartCollum = location.x;
    public readonly int StartRow = location.y;

    public readonly int witdh = width;
    public readonly int height = height;

    public readonly string? title = Title;

    public readonly int WritableHeight = height-3;
    public readonly int WritableWidth = width-3;
    public Data? data = null;


    public readonly Location writableStart = new Location(location.x+1, location.y+1);

   
}

public class Data
{
    public string? Currntval;
}


public class Location(int x, int y)
{
    public readonly int x = x;
    public readonly int y = y;
}

public class Boxrendering
{
    public static List<BoxType> boxTypes = new List<BoxType>();




    public static void WriteSingleInBox(string messgae  ,BoxType box , int StartAt = 0, Color? color = null)
    {
      
        
       


        if (box.WritableWidth > messgae.Length && box.WritableHeight > StartAt) {
            writeAtPost(messgae, box.writableStart.x, box.writableStart.y + StartAt, color.Value);
        }
        if (box.WritableWidth < messgae.Length && box.WritableHeight > StartAt) {

            writeAtPost(messgae[..(Int32)box.WritableWidth], box.writableStart.x, box.writableStart.y + StartAt, color.Value);


        }


    }
    public static void WriteLineInBox(string messgae, BoxType box, int BoxSize = 0, int StartAt = 0,bool Center=false, Color? color = null)
    {
        if (color == null) { color = ColorText.ConsoleColorToRGB(ConsoleColor.White); }


        string[] lines = messgae.Split('\n');
        int longest = lines.OrderByDescending(s => s.Length).First().Length;
        
        if (Center)
        {
            BoxSize = (int)((box.WritableWidth / 2) - (longest / 2));
        }
        
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (box.WritableWidth > lines[i].Length && box.WritableHeight > StartAt+i)
                {
                    writeAtPost(lines[i], box.writableStart.x+ BoxSize, box.writableStart.y + StartAt+i, color.Value);
                }
                if (box.WritableWidth < lines[i].Length && box.WritableHeight > StartAt+i)
                {

                    writeAtPost(lines[i][..(Int32)box.WritableWidth], box.writableStart.x+BoxSize, box.writableStart.y + StartAt+i, color.Value);


                }


            }
        }
    }

    public static void WriteLineInLineBox(string messgae, BoxType box, int BoxSize = 0, int StartAt = 0, bool Center = false, Color? color = null,int? limit=null)
    {
        if (color == null) { color = ColorText.ConsoleColorToRGB(ConsoleColor.White); }


        string[] lines = messgae.Split('\n');
        int longest = lines.OrderByDescending(s => s.Length).First().Length;

        if (Center)
        {
            BoxSize = (int)((box.WritableWidth / 2) - (longest / 2));
        }






        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (box.WritableWidth > lines[i].Length && box.WritableHeight > StartAt + i)
                {
                    writeInLineAtPost(lines[i], box.writableStart.x + BoxSize, box.writableStart.y + StartAt + i,limit);
                }
                if (box.WritableWidth < lines[i].Length && box.WritableHeight > StartAt + i)
                {

                    writeInLineAtPost(lines[i], box.writableStart.x + BoxSize, box.writableStart.y + StartAt + i,limit);


                }


            }
        }
    }

    public static void ClearBox(BoxType box)
    {
        var org = Console.GetCursorPosition();
        var line = "";
        for (int i = 0; i < box.WritableWidth; i++) { line += " "; }
        for (int i = 0; i < box.WritableHeight; i++) { writeAtPost(line, box.writableStart.x, box.writableStart.y + i, ColorText.ConsoleColorToRGB(ConsoleColor.White)); };
        Console.SetCursorPosition(org.Left,org.Top);



    }

    public static void writeAtPost(string message, int x, int y, Color color)
    {
        
        if (color == null) { color = ColorText.ConsoleColorToRGB(ConsoleColor.White); };
        Console.SetCursorPosition(x,y);
        ColorText.ColorWrite(message,color );

        
    }
    public static void writeInLineAtPost(string message, int x, int y,int?limit=null)
    {


        Console.SetCursorPosition(x, y);
        ColorText.ColorWriteLineIn(message);

    }


    public static string returnLine(int Witdh, bool IsTop =false ,string? hasTitle = null)
    {

        string returnMe = "" ;
         {
            for (int i = 0; i < Witdh - 2; i++) {

                returnMe += DefaultBoxUnicodeROM.Line;

            }
            if (!IsTop) { return $"{DefaultBoxUnicodeROM.leftdown}{returnMe}{DefaultBoxUnicodeROM.righdown}"; }
            else { return $"{DefaultBoxUnicodeROM.leftup}{returnMe}{DefaultBoxUnicodeROM.rightup}"; }
        }
    }





    public static BoxType RenderBoxAtplace(int witdh, int height, Location location, string? title=null, Color? colorFrame = null, Color? colorTitle = null)
    {
        if (!colorTitle.HasValue) { colorTitle = ColorText.HexToRGB(ColorText.allColors["White"]); }
        writeAtPost(returnLine(witdh, true), location.x, location.y,colorFrame.Value);
        for (int i = 0; i < height - 2; i++) { writeAtPost("┃", location.x, location.y + 1 + i, colorFrame.Value); writeAtPost("┃", location.x + witdh - 1, location.y + 1 + i, colorFrame.Value); }
        writeAtPost(returnLine(witdh, false), location.x, Console.CursorTop, colorFrame.Value);
      

        if (title != null) {

            Console.SetCursorPosition(location.x, location.y);
    
          
            writeAtPost(title, location.x + 3, location.y,colorTitle.Value);
        
        }




        
        Console.SetCursorPosition(Console.BufferWidth-1, Console.BufferHeight-1);
        var box = new BoxType(location, witdh, height, title);
        boxTypes.Add(box);
        return box;
    }


}
