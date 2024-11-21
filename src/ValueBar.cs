using System.Drawing;

namespace CreamsConsole_utils
{




    //┌━          spotify          -===
    //│	████████████████████████████ |
    //│	████████████████████████████ |
    //│	████████████████████████████ |
    //└━	        100             --
    //              


    public class valuebar
    {
        public class ValueBarstyle
        {
            public bool showBarValue = true;
            public bool showleftBracet = true;
            public bool showrightBracet = true;


            public WritingStyle bracket = new WritingStyle(Color.Gray);
            public WritingStyle Bar = new WritingStyle(Color.Green);
            public WritingStyle Text = new WritingStyle(Color.Blue);

        }



        public class BarConfig
        {
            public string boxName = "";

            private int offsetbracketValue = 4;

            public Conbox ParrentBox = conboxFunc.MainBody;

            private bool isActive = false;
            public UCOORD startpos = new UCOORD(0,0);


            public ValueBarstyle style = new ValueBarstyle();

            private readonly int minHeight = 3;
            private readonly int minlength = 7;

            public Conbox parentBox = conboxFunc.MainBody;
            public Conbox progressBox;

            

            private int Barheight = 3;
            private int Barlength = 50;

            private int MaxbarValue = 100;
            private bool valueisProcentage=true;


            public void SetMaxValue(bool isProcentage,int maxValue) {
                if (isProcentage) { valueisProcentage = true; MaxbarValue = 100; return; }
                else { valueisProcentage = false; MaxbarValue = maxValue; return; }

            }


            public int GetMaxValue {
                get { return MaxbarValue; }
            }


            public int GetBarHeight
            {
                get
                {
                    return Barheight;
                }
            }

            public int GetBarlength
            {
                get
                {
                    return Barlength;
                }
            }


            private void verifyOffset()
            {
                if (style.showleftBracet == false) { offsetbracketValue -=2; }
                if (style.showrightBracet == false) { offsetbracketValue -=2; }
                if (offsetbracketValue <= 0) { offsetbracketValue = 0; }

            }

            public bool setBarlenght(int length)
            {
                verifyOffset();
                if (length > 0)
                {
                    int space = parentBox.width - (int)startpos.x;
                    if (space - (length + offsetbracketValue) > 0) {
                        if (length >= minlength) {
                            Barlength = length;
                            return true;
                        }
                        return false;

                    }
                    return false;

                }
                else { return false; }


            }


            public bool setBarHeight(int newheight)
            {
                if (newheight >= minHeight) {
                    if (style.showleftBracet | style.showrightBracet) {
                        var space = parentBox.height - (int)startpos.y;
                        space -= 2;
                        if ((newheight - space) > 0 && newheight >= minHeight) {
                            this.Barheight = newheight;
                            return true;
                        }




                        return false;



                    }
                    else { var space = parentBox.height - (int)startpos.y;
                        if ((newheight - space) > 0 && newheight >= minHeight)
                        {
                            this.Barheight = newheight;
                            return true;
                        }
                        return false;
                    }               
                    return false ;


                }
                else { return false; }

            }

            public char barChar = '\u2588';


            public void DisplayCalueBar(int value) {
                if (!isActive) { this.progressBox = new Conbox(new Boxsize(Barheight + 2, Barlength + 4), startpos, $"{boxName}'s box", parentBox); }


                string straightline = utilFunctions.stringgenerator(UnicodeROM.DefaultBoxUnicodeROM.streight, Barheight + 2);


            
            
            }








        }


        



    }


}