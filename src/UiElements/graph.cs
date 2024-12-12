using System;

namespace CreamsConsole_utils;
public class graph
{
    public class Graph
    {
        public string name;

        // size of each step of the graph
        public int stepSizeX;
        public int stepSizeY;


        // amount of steps points
        public int amountstepsX = 8;
        public int amountstepsY = 5;


        // default -1 means size not set
        internal int graphpixelSizeX = 10;
        internal int graphpixelSizeY = 20;

        internal int minCellsize = 1;
        internal int cellsize = 4;
        internal int Cellsizehigh = 2;

        // check if fits in current console window
        public bool consolesizecheck = true;


        public int Graphconsolewidth
        {
            get
            {
                return graphpixelSizeX;
            }
        }
        public int Graphconsoleheight
        {
            get
            {
                return graphpixelSizeY;
            }
        }

        // sets the graphpixel size ints
        public void setGraphConsoleSize(int SizeX, int SizeY)
        {
            if (consolesizecheck)
            {
                if (Console.WindowWidth < SizeX) { ColorText.ColorWrite($"graph size ERROR: given size for graph width was to big for console window\n  given {SizeX}, ConsoleWindowsize:{Console.WindowWidth}\n if you want to use console buffer instead disable check with the graphinstance.consolesizecheck = false", ColorText.HexToRGB(ColorText.allColors["Red"])); return; }
                if (Console.WindowHeight < SizeY) { ColorText.ColorWrite($"graph size ERROR: given size for graph Height was to big for console window\n  given {SizeY}, ConsoleWindowsize:{Console.WindowHeight}\nif you want to use console buffer instead disable check with the graphinstance.consolesizecheck = false", ColorText.HexToRGB(ColorText.allColors["Red"])); return; }
            }

            graphpixelSizeX = SizeX;
            graphpixelSizeY = SizeY;
            if (amountstepsX != -1 & amountstepsY != -1) { var newcellsize = (int)SizeX / amountstepsX; cellsize = newcellsize; }




        }






        internal string[] graphstringbuilder()
        {

            string celllineX = utilFunctions.stringgenerator(UnicodeROM.DefaultBoxUnicodeROM.Line, this.cellsize);
            string celllineempty = utilFunctions.stringgenerator(UnicodeROM.DefaultBoxUnicodeROM.spacebutno, this.cellsize);


            List<string> tempstrings = new List<string>();


            if (this.Graphconsoleheight == -1 | this.Graphconsoleheight == -1) { ColorText.ColorWrite("stringbuiler error Graphsize not set", System.Drawing.Color.Red); return [string.Empty]; }


            string lineintersect = string.Empty;
            string lineNormal = string.Empty;
            string linebotom = string.Empty;


            for (int i = 0; i < amountstepsX; i++)
            {



                if (i == 0)
                {
                    linebotom += $"{UnicodeROM.DefaultBoxUnicodeROM.leftdown}";
                    lineintersect += UnicodeROM.DefaultBoxUnicodeROM.TLEFT;
                    lineNormal += $"{UnicodeROM.DefaultBoxUnicodeROM.streight}";


                }

                if (i == amountstepsX - 1)
                {
                    linebotom += $"{UnicodeROM.DefaultBoxUnicodeROM.TUP}";
                    lineNormal += $"{UnicodeROM.DefaultBoxUnicodeROM.streight}";
                    break;

                }

                lineNormal += celllineempty;
                linebotom += celllineX;
                if (i + 1 != amountstepsX - 1)
                {
                    linebotom += UnicodeROM.DefaultBoxUnicodeROM.TUP;
                    lineNormal += UnicodeROM.DefaultBoxUnicodeROM.streight;
                }
                lineintersect += celllineX;



                if (i != amountstepsX) { lineintersect += UnicodeROM.DefaultBoxUnicodeROM.Cross; }






            }
            tempstrings.Add(lineNormal);
            tempstrings.Add(linebotom);
            tempstrings.Add(lineintersect);

            return tempstrings.ToArray();
        }

        public void Displaygraph()
        {
            ColorText.ColorWrite("BE AWARE THIS IS NOT FULLY IMPLEMENTED DO NOT USE IN PROJECT", System.Drawing.Color.Red);



            string[] strings = graphstringbuilder();
            for (int i = 0; i < amountstepsY; i++)
            {
                
                Console.WriteLine(strings[2]);
                for (int j = 0; j < Cellsizehigh; j++) { Console.WriteLine(strings[0]); }
                if (i == amountstepsY - 1) { Console.WriteLine(strings[1]); }







            }

        }
















    }
}
