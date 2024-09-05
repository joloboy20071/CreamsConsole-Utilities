using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreamsConsole_utils;


struct Rect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
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


public class BoxType(Location location, int width, int height, string? Title)
{
    public readonly int StartCollum = location.x;
    public readonly int StartRow = location.y;

    public readonly int witdh = width;
    public readonly int height = height;

    public readonly string? title = Title;

    public readonly int WritableHeight = height - 3;
    public readonly int WritableWidth = width - 3;
    public Data? data = null;


    public readonly Location writableStart = new Location(location.x + 1, location.y + 1);


}


public class Boxconfig()
{
    public static int width = 0;
    public static int height = 0;
    public static Location Location = new Location(0,0);
    public static string? Title = null; 
    public static Color? Colorframe = ColorText.HexToRGB(ColorText.allColors["White"]);
    public static Color? ColorTitle = ColorText.HexToRGB(ColorText.allColors["White"]);





}

