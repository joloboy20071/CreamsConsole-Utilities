using CreamsConsole_utils;

using System.Drawing;
using System.Text;



namespace CreamsConsole_utils
{
    public partial class conboxFunc
    {
        internal static Dictionary<int, Conbox> GetBoxFromId = new Dictionary<int, Conbox>();

        public static int[] GetallIds() {
            return GetBoxFromId.Keys.ToArray();
        }

        public static Conbox[] GetallBox() {
            return GetBoxFromId.Values.ToArray();
        }


        public static Conbox? GetboxfromID(int id) {
            try { return GetBoxFromId[id]; }
            catch { return null; }
        
        
        }



        public static COORD ZeroZero = new COORD(0, 0);


        public static Conbox MainBody = new Conbox(new Boxsize(Console.WindowWidth, Console.WindowHeight), ZeroZero, "mainbody");


        //private Dictionary<> BocUcoordContainer = new Dictionary<>;



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

        /// <summary>
        /// set the console cursor pos based on a Ucoord 
        /// </summary>
        /// <param name="location"></param>
        private static void setConsoleCursor(UCOORD location) {
            Console.SetCursorPosition((int)location.x, (int)location.y);
        
        }



        public static void ConsolewriteMultiline(Conbox box, UCOORD statingPos, string message,WritingStyle? style = null) {
            string[] strings = message.Split('\n');
            if ((strings.Length + statingPos.y) <= box.height)
            {
                for (int i = 0; i < strings.Length; i++)
                {
                    consolewriteAtpos(box, new UCOORD((int)statingPos.x, (int)statingPos.y + i), strings[i], style);

                }
            }
        
        
        }

        public static void consolewriteAtpos(Conbox box, UCOORD writingpos, string massage, WritingStyle? style = null)
        {
            if (style == null) { style = ConsoleOut.Defaultstyle; }



            BoxRectUCOORD boxRectUCOORD = box.GetBoxRectuCoord();
            if (writingpos < boxRectUCOORD.bottomright)
            {

                if ((massage.Length + writingpos.x) <= box.width)
                {

                    UCOORD writelocation = GetScreenCoord(box) + writingpos;

                    Console.SetCursorPosition((int)writelocation.x, (int)writelocation.y);
                    ConsoleOut.ConsoleWriteStyle(massage, style);



                }




            }








        }










        public static int GetMiddle(int width, string center)
        {
            return (int)((width / 2) - (center.Length / 2));

        }


        public static void BoxInfoWriteMulti(BoxInfo Boxinfo, UCOORD writingPos, string message, WritingStyle? style = null)
        {
            string [] strings = message.Split("\n");
            if ((int)(strings.Length + writingPos.y) <= Boxinfo.boxsize.height) {
                for (int i = 0; i < strings.Length;i++) {
                    BoxInfoWrite(Boxinfo, writingPos + new UCOORD(0,i), strings[i], style);         
                }
            }
        }


        public static void BoxInfoWriteMulti(TermialWriterequest request)
        {
            BoxInfoWriteMulti(request.Box, request.startPos, request.message, request.Style);

        }




        /// <summary>
        /// the default console write function with compatabilty with the conbox system
        /// </summary>
        /// <param name="Boxinfo"></param>
        /// <param name="writingPos"></param>
        /// <param name="message"></param>
        /// <param name="style"></param>
        public static void BoxInfoWrite(BoxInfo Boxinfo, UCOORD writingPos, string message, WritingStyle? style = null)
        {
            message = message.Replace("\n", "");
            if (style == null) { style = ConsoleOut.Defaultstyle; }
            string[] strings = message.Split('\n');
            if ((strings.Length + writingPos.y) <= Boxinfo.boxsize.height)
            {
                for (int i = 0; i < strings.Length; i++)
                    if (writingPos < new UCOORD(Boxinfo.boxsize.width, Boxinfo.boxsize.height))
                    {
                        if ((message.Length + writingPos.x) <= Boxinfo.boxsize.width)
                        {
                            writingPos = writingPos + new UCOORD(0, i);
                            setConsoleCursor(Boxinfo.globalpos + Boxinfo.globalpos + writingPos);
                            ConsoleOut.ConsoleWriteStyle(strings[i], style);
                            
                        }


                    }

            }
        }

        /// <summary>
        /// in functionnality the same as its default variant but should only be used with thread based uis 
        /// </summary>
        /// <param name="request"></param>
        public static void BoxInfoWrite(TermialWriterequest request) {

            BoxInfoWrite(request.Box,request.startPos,request.message,request.Style);
        
        }


        
        public static void ClearBox(Conbox box)
        {
            var size = box.getboxsize;
            string emptyspace = utilFunctions.stringgenerator(" ",(int)size.width);
            string row = utilFunctions.stringgenerator($"{emptyspace}\n", (int)size.height);
            ConsolewriteMultiline(box, new UCOORD(0, 0), row);

        }

        


    }



   
    public class Conbox 
    {

        private int Id = 0;// this defines a certain box when trying to find them

        private static Random random = new Random();

        private string name = "";

        public string getname {
            get { return name; }
        }

        public void SetName(string name) { this.name = name; }





        private Boxsize boxsize;

        private BoxInfo BoxInfo ;

        public BoxInfo GetBoxinfo {
            get { return BoxInfo; }
        
        }

        private Conbox? parent = null;

        private int maxwidth = Console.WindowWidth;
        private int maxheight = Console.WindowHeight;

        private Boxsize maxsize = new Boxsize(Console.WindowWidth, Console.WindowHeight);

        private UCOORD Pos = new UCOORD(99999, 99999);
        

        private void Setparent(Conbox parentbox)
        {
            if (parentbox.width > 0 & parentbox.height > 0) { if (this.parent != null) { BoxInfo.Setvalue(this.Id, conboxFunc.GetScreenCoord(this), boxsize, parentbox.Id, name); } this.parent = parentbox;  }
            else {this.parent = null; throw new InvalidParentBox("givin parent box is invalid"); }


        }



        public Conbox(Boxsize size, COORD pos, string boxname, Conbox? parent =null)
        {
            if (parent == null) {
                this.setsize((int)size.height, (int)size.width);
                this.setPos(pos.x, pos.y);
                this.BoxInfo = new BoxInfo(random.Next(),conboxFunc.GetScreenCoord(this),getboxsize,-1,name);
            }
            if (parent != null) {
                this.Setparent(parent);
                this.setsize((int)size.height, (int)size.width);
                this.setPos(pos.x, pos.y);
                this.BoxInfo = new BoxInfo(random.Next(), conboxFunc.GetScreenCoord(this), getboxsize, parent.Id, name);
            }

            conboxFunc.GetBoxFromId[this.Id] = this;

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

        private int GetparentId {
            get { if (hasparent) { return parent.Id; }
                return -1;
            }
        
        
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


            if (!boxsize.Equals(null)) { BoxInfo.Setvalue(this.Id, this.BoxInfo.globalpos,this.boxsize, GetparentId, name); }



        }

        public void setPos(int x, int y)
        {
            if (this.Pos != new UCOORD(99999, 99999)) { this.BoxInfo.Setvalue(this.Id, conboxFunc.GetScreenCoord(this),this.boxsize,this.GetparentId,this.name);  }


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



    public static Conbox createBoxOutline(Conbox box,WritingStyle? style=null) { 
        var strings = GetBoxStringArray(box);
        UCOORD[] uCOORDs = getUcoords(box);
        conboxFunc.consolewriteAtpos(box, new UCOORD(0, 0), strings[0], style);
        conboxFunc.consolewriteAtpos(box, new UCOORD(0, box.height-1), strings[1],style);
        for (int i = 0; i < uCOORDs.Length; i++) {
            conboxFunc.consolewriteAtpos(box, uCOORDs[i], strings[2],style);
        
        }
        return new Conbox(new Boxsize(box.width - 4, box.height - 2), new COORD(2, 1), $"box in {box.getname}", box);

    }









}