
using System.Drawing;


namespace CreamsConsole_utils;


struct Rect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}



public struct UCOORD
{
    public uint x;
    public uint y;

    public UCOORD(int x, int y)
    {
        try
        {

            this.x = (uint)x;
            this.y = (uint)y;
        }
        catch
        {
            if (x < 0 | y < 0) throw new InvaldidCOORD("invalid x or y vlaue must be positive");


        }
    }
    public UCOORD(uint x, uint y)
    {
        this.y = y;
        this.x = x;

    }
    public override string ToString() => $"({x},{y})";
    public static UCOORD operator +(UCOORD b, UCOORD c)
    {
        return new UCOORD((b.x + c.x), (b.y + c.y));

    }
    public static bool operator >=(UCOORD b, UCOORD c)
    {
        if (b.x >= c.x | b.y >= c.y) { return true; }
        return false;

    }
    public static bool operator >(UCOORD b, UCOORD c)
    {
        if (b.x >= c.x | b.y >= c.y) { return true; }
        return false;

    }
    public static bool operator <(UCOORD b, UCOORD c)
    {
        if (b.x < c.x && b.y <c.y) { return true; }
        return false;

    }


    public static bool operator <=(UCOORD b, UCOORD c)
    {
        if (b.x <= c.x && b.y <= c.y) { return true; }
        return false;


    }





}


public struct COORD
{
    public int x;
    public int y;

    public COORD(int x, int y)
    {
        this.x = x;
        this.y = y;
        
    }
    public COORD(uint x, uint y)
    {
        this.x = (int)x;
        this.y = (int)y;

    }



    public override string ToString() => $"({x},{y})";
}

public  struct BoxRectUCOORD
{
    public UCOORD bottomright;
    public UCOORD bottomleft;
    public UCOORD topleft;
    public UCOORD topright;


    public BoxRectUCOORD(Boxsize box)
    {
        topright = new UCOORD(box.width, 0);
        topleft = new UCOORD(0, 0);
        bottomright = new UCOORD(box.width, box.height);
        bottomleft = new UCOORD(0, box.height);
    } 





}



public struct Boxsize
{
    public uint width;
    public uint height;

    public Boxsize(int width, int height)
    {
        if (width>0 | height > 0) { 
        this.height = (uint)height;
        this.width = (uint)width;
            }

    }
    public Boxsize(uint width, uint height)
    {
        if (width > 0 | height > 0)
        {
            this.height = height;
            this.width = width;
        }


    }

    public override string ToString() => $"({width}, {height})";
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

