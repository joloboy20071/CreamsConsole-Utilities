//namespace CreamsConsole_utils
//{
//    public class valuebar
//    {
//        public class ValueBarstyle
//        {
//        }



//        public class BarConfig
//        {
//            private int offsetbracketValue = 4;


//            public ValueBarstyle style = new ValueBarstyle();

//            public bool showBarValue = true;
//            public bool showleftBracet = true;
//            public bool showrightBracet = true;

//            private readonly int minHeight = 3;
//            private readonly int minlength = 5;

//            //public Conbox parentBox = conboxFunc.MainBody;

//            public UCOORD StartTopLeftCoords = new UCOORD(0, 0);


//            private int height = 3;
//            private int Barlength = 5;



//            public int GetBarHeight
//            {
//                get
//                {
//                    return height;
//                }
//            }

//            public int GetBarlength
//            {
//                get
//                {
//                    return Barlength;
//                }
//            }


//            private void verifyOffset()
//            {
//                if (showleftBracet == false) { offsetbracketValue -=2; }
//                if (showrightBracet == false) { offsetbracketValue -=2; }
//                if (offsetbracketValue <= 0) { offsetbracketValue = 0; }

//            }

//            public bool setBarlenght(int length)
//            {
//                verifyOffset();
//                if (length > 0)
//                {
//                    int space = parentBox.width - (int)StartTopLeftCoords.x;
//                    if (space - (length + offsetbracketValue) > 0) {
//                        if (length >= minlength) {
//                            Barlength = length;
//                            return true;
//                        }
//                        return false;

//                    }
//                    return false;

//                }
//                else { return false; }


//            }


//            public bool setBarHeight(int newheight)
//            {
//                if (newheight >= minHeight) {
//                    if (showleftBracet | showrightBracet) {
//                        var space = parentBox.height - (int)StartTopLeftCoords.y;
//                        space -= 2;
//                        if ((newheight - space) > 0 && newheight >= minHeight) {
//                            this.height = newheight;
//                            return true;
//                        }




//                        return false;



//                    }
//                    else { var space = parentBox.height - (int)StartTopLeftCoords.y;
//                        if ((newheight - space) > 0 && newheight >= minHeight)
//                        {
//                            this.height = newheight;
//                            return true;
//                        }
//                        return false;
//                    }               
//                    return false ;


//                }
//                else { return false; }

//            }
            
//            public char barChar = '\u2588';


//        }






//    }


//}