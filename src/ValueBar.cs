using System.Drawing;

namespace CreamsConsole_utils
{




    //┌━          spotify          -===
    //│	████████████████████████████ |
    //│	████████████████████████████ |
    //│	████████████████████████████ |
    //└━	        100%            --
    //              


 
        public class ValueBarstyle
        {
            public bool showBarValue = true;
            public bool showBarName = true;    


            public bool showleftBracet = true;
            public bool showrightBracet = true;


            public WritingStyle bracket = new WritingStyle(Color.Gray);
            public WritingStyle Bar = new WritingStyle(Color.Green);
            public WritingStyle Text = new WritingStyle(Color.Blue);

        }



        public class valueBar
        {
            public string BarName = "";

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


            public void DisplayValueBar(int value) {
                if (!isActive) { this.progressBox = new Conbox(new Boxsize(Barlength + 4,Barheight + 2 ), startpos, $"{BarName}'s box", parentBox); }

            conboxFunc.ClearBox(this.progressBox);
            if (style.showleftBracet | style.showrightBracet)
            {
                string straightline = utilFunctions.stringgenerator($" {UnicodeROM.DefaultBoxUnicodeROM.streight}\n", Barheight);

                if (style.showleftBracet)
                {
                    string topleft = $"{UnicodeROM.DefaultBoxUnicodeROM.leftup}{UnicodeROM.DefaultBoxUnicodeROM.Line}\n" + straightline.Replace(" ", "");
                    string bottomleft = $"{UnicodeROM.DefaultBoxUnicodeROM.leftdown}{UnicodeROM.DefaultBoxUnicodeROM.Line}";
                    string finalL = topleft + bottomleft;

                    conboxFunc.ConsolewriteMultiline(progressBox, new UCOORD(0, 0), finalL, style.bracket);
                }

                if (style.showrightBracet)
                {
                    string topright = $"{UnicodeROM.DefaultBoxUnicodeROM.Line}{UnicodeROM.DefaultBoxUnicodeROM.rightup}\n" + straightline;
                    string bottomright = $"{UnicodeROM.DefaultBoxUnicodeROM.Line}{UnicodeROM.DefaultBoxUnicodeROM.righdown}";
                    string finalR = topright + bottomright;
                    conboxFunc.ConsolewriteMultiline(progressBox, new UCOORD(this.GetBarlength + 2, 0), finalR, style.bracket);

                }


            }            
                int taskperTile = (int)(MaxbarValue/Barlength);
            int amountTile = (int)(value / taskperTile);


                string block = utilFunctions.stringgenerator($"█", amountTile);
                string Lines =utilFunctions.stringgenerator($"{block}\n",Barheight);



                conboxFunc.ConsolewriteMultiline(progressBox, new UCOORD(2, 1), Lines,style.Bar);

            if (style.showBarValue) { 
                
                string valuestring = $"{value}%";
                var cord =new UCOORD(conboxFunc.GetMiddle(progressBox.width, valuestring),progressBox.height-1);
                conboxFunc.consolewriteAtpos(progressBox, cord, valuestring, style.Text);
            
            }
            if (style.showBarName) {
                var cord = new UCOORD(conboxFunc.GetMiddle(progressBox.width, BarName), 0);
                conboxFunc.consolewriteAtpos(progressBox, cord, this.BarName, style.Text);

            }





            }








            
            
            








        }


        



    }

