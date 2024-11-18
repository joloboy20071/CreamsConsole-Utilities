using CreamsConsole_utils;

using System.Drawing;
using System.Text;



namespace CreamsConsole_utils
{
    public class conboxFunc
    {
        



        public static UCOORD GetScreenCoord(Conbox box)
        {

            
            var parent = box.Getparent();
            //if (parent != null) {
            if (box.hasparent == true & parent != null)
                {

                    if (parent.hasparent == false)
                    {
                        var boxpos = box.GetPosition;
                        var cord = parent.GetPosition;
                        return cord + boxpos;
                    }
                    if (box.hasparent == true & parent.hasparent == true)
                    {
                        var cord = GetScreenCoord(parent);
                        return cord + box.GetPosition;

                    }

            }


            return box.GetPosition;




        }
        public static void ConsolewriteMultiline(Conbox box, UCOORD statingPos, string message) {
            string[] strings = message.Split('\n');
            if ((strings.Length + statingPos.y) <= box.height)
            {
                for (int i = 0; i < strings.Length; i++)
                {
                    consolewriteAtpos(box, new UCOORD((int)statingPos.x, (int)statingPos.y + i), strings[i]);

                }
            }
        
        
        }


        public static void consolewriteAtpos(Conbox box,UCOORD writingpos, string massage)
        {
            var i = Console.GetCursorPosition();

            BoxRectUCOORD boxRectUCOORD = box.GetBoxRectuCoord();
            if (writingpos < boxRectUCOORD.bottomright) {

                if ((massage.Length + writingpos.x) <= box.width) {
                    UCOORD writelocation = GetScreenCoord(box) + writingpos;
                    //if (writelocation.y == 0) { writelocation.y ++;}
                    //if (writelocation.x == 0) { writelocation.x++; }



                    Console.SetCursorPosition((int)writelocation.x,(int)writelocation.y);
                    ColorText.ColorWriteIn(massage);
                    Console.SetCursorPosition(i.Left,i.Top);
                
                
                }




            }








        }


        


    }



   
    public class Conbox 
    {
        private string name = "";

        public string getname {
            get { return name; }
        }

        public void SetNmae(string name) { this.name = name; }

        private Boxsize boxsize;

        private Conbox? parent = null;

        private int maxwidth = Console.WindowWidth;
        private int maxheight = Console.WindowHeight;

        private Boxsize maxsize = new Boxsize(Console.WindowWidth, Console.WindowHeight);

        private UCOORD Pos = new UCOORD(0, 0);

        private void Setparent(Conbox parentbox)
        {
            if (parentbox.width > 0 & parentbox.height > 0) { this.parent = parentbox; }
            else {this.parent = null; throw new InvalidParentBox("givin parent box is invalid"); }


        }



       // public Conbox(Boxsize size, COORD pos, Conbox? parent = null) {new Conbox(size, pos, "", parent); }


        public Conbox(Boxsize size, COORD pos, string boxname, Conbox? parent =null)
        {
            if (parent == null) {
                this.setsize((int)size.height, (int)size.width);
                this.setPos(pos.x, pos.y);
            }
            if (parent != null) {
                this.Setparent(parent);
                this.setsize((int)size.height, (int)size.width);
                this.setPos(pos.x, pos.y);
                


            }



        }


        public BoxRectUCOORD GetBoxRectuCoord()
        {
            return new BoxRectUCOORD(boxsize);
        }



        public UCOORD GetPosition
        {
            get { return Pos; }
        }

        public bool hasparent
        {
            get {
                if (parent == null) { return false; }
                return true; 
            }
        }

        public int height
        {
            get { return (int)boxsize.height; }
        }

        public int width
        {
            get {
                return (int)this.boxsize.width;
            }
        }
        
        public Boxsize getboxsize
        {
            get {
                return this.boxsize;
            }
        }


        public Conbox? Getparent()
        {
            return this.parent;

        }

        public void setsize(int height, int width)
        {
            if (parent != null)
            {
                if (height<= parent.height & width <= parent.width)
                {
                    this.boxsize = new Boxsize(width, height);
                    return;
                }
                if (height > parent.height | width > parent.width)
                {
                    throw new InvalidBoxSize($"height or width value exceeds parrent box size of {parent.boxsize.ToString()} with given child size of {(new Boxsize(width, height)).ToString()}\n");

                }


            }

            else {
                if (height<=maxheight & width<= maxwidth) { this.boxsize = new Boxsize(width,height); }
                if (height> maxheight | width> maxwidth)
                {
                    throw new InvalidBoxSize($"height or width value exceeds Console window size of {this.maxsize.ToString()} with given box size of {(new Boxsize(width, height)).ToString()}\n ");
                    

                }



            }






        }

        public void setPos(int x, int y)
        {
            if (parent != null)
            {
                if ((x + boxsize.width)<= parent.width & (y + boxsize.height)<= parent.height) { this.Pos = new UCOORD(x, y); }
                if ((x + boxsize.width) > parent.width | (y + boxsize.height) >parent.height)
                {
                    throw new InvalidBoxPos("The pos you are trying to set exceeds parrent size ");

                }

            }
            else {
                if ((x + boxsize.width) <= maxwidth & (y + boxsize.height) <= maxheight) { this.Pos = new UCOORD(x, y); }
                if ((x + boxsize.width) > maxwidth | (y + boxsize.height) > maxheight)
                {
                    throw new InvalidBoxPos("The pos  you are trying to set exceeds console window size ");

                }

            }



        }

        
        












    }
}
public class BoxOutline
{


    private static string[] GetBoxStringArray(Conbox box) {
        
        List<string> templist = new List<string>();
        if (box.height < 3 | box.width < 3) {
            throw new InvalidBoxSize($"box outline that was givin ({box.width},{box.height}) when both need a minimum of 3");
            
        }


        string linefromWidth = "";
        for (int i = 0; i < box.width-2; i++) { linefromWidth += UnicodeROM.DefaultBoxUnicodeROM.Line; }


        
        templist.Add($"{UnicodeROM.DefaultBoxUnicodeROM.leftup}{linefromWidth}{UnicodeROM.DefaultBoxUnicodeROM.rightup}");
        templist.Add($"{UnicodeROM.DefaultBoxUnicodeROM.leftdown}{linefromWidth}{UnicodeROM.DefaultBoxUnicodeROM.righdown}");
        templist.Add(UnicodeROM.DefaultBoxUnicodeROM.streight);
       
        return templist.ToArray();
    }

    private static UCOORD[] getUcoords(Conbox box) {
        List<UCOORD> coordlist = new List<UCOORD>();
        

        for (int i = 1; i < box.height - 1; i++) {
            coordlist.Add(new UCOORD(0, i));
            coordlist.Add(new UCOORD(box.width-1, i));
 
        
        }
        return coordlist.ToArray();           
    
    }



    public static Conbox createBoxOutline(Conbox box,Color? BoxColor =null) { 
        var strings = GetBoxStringArray(box);
        UCOORD[] uCOORDs = getUcoords(box);
        conboxFunc.consolewriteAtpos(box, new UCOORD(0, 0), strings[0]);
        conboxFunc.consolewriteAtpos(box, new UCOORD(0, box.height-1), strings[1]);
        for (int i = 0; i < uCOORDs.Length; i++) {
            conboxFunc.consolewriteAtpos(box, uCOORDs[i], strings[2]);
        
        }
        return new Conbox(new Boxsize(box.width - 2, box.height - 2), new COORD(1, 1), $"box in {box.getname}", box);

    }









}