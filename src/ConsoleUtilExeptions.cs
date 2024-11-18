using System;
namespace CreamsConsole_utils;
public class InvalidStyleOption : Exception
{
    public InvalidStyleOption(string message) : base(message) { ColorText.writeRGB(message,System.Drawing.Color.Red); Console.ReadKey();
    } 

}

public class InvalidParentBox : Exception
{
    public InvalidParentBox(string message) : base(message)
    {
        ColorText.writeRGB(message, System.Drawing.Color.Red);
        Console.ReadKey();
    }

}





public class InvalidBoxSize : Exception
{
    public InvalidBoxSize(string message) : base(message)
    {
        ColorText.writeRGB(message, System.Drawing.Color.Red);
        Console.ReadKey();
    }

}
public class InvalidBoxPos : Exception
{
    public InvalidBoxPos(string message) : base(message)
    {
        ColorText.writeRGB(message, System.Drawing.Color.Red);
        Console.ReadKey();
    }

}
public class InvaldidCOORD : Exception
{
    public InvaldidCOORD(string message) : base(message)
    {
        ColorText.writeRGB(message, System.Drawing.Color.Red);
        Console.ReadKey();
    }

}