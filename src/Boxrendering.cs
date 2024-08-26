using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreamsConsole_utils.Boxrendering;
using static CreamsConsole_utils.UnicodeROM;

namespace CreamsConsole_utils;
public class Location(int x, int y)
{
    public readonly int x = x;
    public readonly int y = y;
}

public class Boxrendering
{
    public static void writeAtPost(string message, int x, int y, Color? color = null)
    {
        if (color == null) { color = ColorText.ConsoleColorToRGB(ConsoleColor.White); };
        Console.SetCursorPosition(x,y);
        ColorText.ColorWrite(message,(Color)color );
    
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





    public static void RenderBoxAtplace(int witdh, int height, Location location, string? title=null)
    {

        writeAtPost(returnLine(witdh, true), location.x, location.y);
        for (int i = 0; i < height - 2; i++) { writeAtPost("┃", location.x, location.y + 1 + i); writeAtPost("┃", location.x + witdh - 1, location.y + 1 + i); }
        writeAtPost(returnLine(witdh, false), location.x, Console.CursorTop);
      

        if (title != null) {

            Console.SetCursorPosition(location.x, location.y);
            string backspace = string.Empty;
            for (int i = 0; i < title.Length; i++) { backspace += "\b"; }
            writeAtPost(backspace,location.x+3,location.y);
            writeAtPost(title, location.x + 3, location.y);
        
        }




        
        Console.SetCursorPosition(Console.BufferWidth-1, Console.BufferHeight-1);
    }


}
