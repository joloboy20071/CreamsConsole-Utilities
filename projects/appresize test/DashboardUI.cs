using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CreamsConsole_utils;

namespace dashboardv2.ui
{
    public static class DashboardUI
    {

        public readonly static string struck = "    ___________\n   /           |\n  /---.        |\n  |    |       |\n  |    |       |\n  |---'        |\n  [    ________|_____________________\n  [__.'.---.                 .---.   |\n  [   / ___ \\               / ___ \\  |\n  [__/ /   \\ \\_____________/ /   \\ \\_|\n      | ( ) |               | ( ) |\n       \\___/                 \\___/";
        public readonly static string trailer = " ________________________________________________\n |                                              |\n |                                              |\n |                          Creams Productions  |\n |                                              |\n |                                              |\n |_______________________      ____      ____   |\n        ||               |    / __ \\    / __ \\  |\\\n        \\/               |___| /  \\ |__| /  \\ |_|/\n                              | () |    | () |\n                               \\__/      \\__/";



        private static int AmountOfRows = 3;
        private static int AmountOfColumns = 5;

        public static int amountofcollumns{ 
            get{
                return AmountOfColumns; } 
        }


        public static int amountofrows
        {
            get
            {
                return AmountOfRows; }
        }



        public static Dictionary<string, int> nameToboxId = new Dictionary<string, int>();
        public static List<string> boxNames = new List<string>();


        public static Dictionary<int,string> idToboxdata = new Dictionary<int,string>();    

        public static void AddBox(Conbox box) {
            string name = box.getname;
            int id = box.GetBoxinfo.boxid;
        

            nameToboxId.Add(name, id);
            idToboxdata.Add(id, string.Empty);
            boxNames.Add(name);

        
        }






        public static Boxsize CreateBoxSize(int row, int column)
        {
            return CreateBoxSize((float)row, (float)column);    

        }

        public static Boxsize CreateBoxSize(float row, float column)
        {
           
            int rowheight = (int)(Console.WindowHeight / amountofrows);
            int columnWidth = (int)(Console.WindowWidth / amountofcollumns);
      
            Boxsize boxsize = new Boxsize( (int)(columnWidth * column),(int)(rowheight * row));

            return boxsize;




        }


        public static void CreateBoxes()
        {
            WritingStyle red = new WritingStyle(Color.Red);
            WritingStyle blue = new WritingStyle(Color.SteelBlue);
            WritingStyle Default = new WritingStyle(); 




            Boxsize x1_1 = CreateBoxSize(1,1);
            Boxsize x1_2 = CreateBoxSize(1,2);
            Boxsize x1_2_5 = CreateBoxSize(1.0f, 2.5f);




            //creates boxes for outline
            Conbox Gearout = new Conbox(x1_1,new UCOORD(0,0), "Gearout",conboxFunc.MainBody);
            Conbox FuelRangeout = new Conbox(x1_2, new UCOORD(x1_1.width, 0), "FuelRangeout", conboxFunc.MainBody);
            Conbox distanceout = new Conbox(x1_2, new UCOORD(x1_1.width+x1_2.width, 0), "distanceout", conboxFunc.MainBody);

            Conbox speedout = new Conbox(x1_2_5, new UCOORD(0, x1_2_5.height), "Speedout", conboxFunc.MainBody);
            Conbox toerenout = new Conbox(x1_2_5, new UCOORD(x1_2_5.width, x1_2_5.height), "ToertellerOut", conboxFunc.MainBody);
            Conbox Truckdamageout= new Conbox(x1_2_5, new UCOORD(0, x1_2_5.height*2), "Truckdamageout", conboxFunc.MainBody);
            Conbox Trailerdamageout = new Conbox(x1_2_5, new UCOORD(x1_2_5.width, x1_2_5.height * 2), "Trailerdamageout", conboxFunc.MainBody); 

             

            //
            



           //filler box
            Conbox Truckdamage = BoxOutline.createBoxOutline(Truckdamageout, red, "| Truck damage |", Default);
            Conbox Trailerdamage = BoxOutline.createBoxOutline(Trailerdamageout, red, "| Trailer damage |", Default);



            //writable boxes
            Conbox gear = BoxOutline.createBoxOutline(Gearout, blue, "| Gear |", Default);
            Conbox FuelRange = BoxOutline.createBoxOutline(FuelRangeout, blue, "| FuelRange |", Default);
            Conbox distance = BoxOutline.createBoxOutline(distanceout, blue, "| Distance to desination |", Default);
            Conbox speed = BoxOutline.createBoxOutline(speedout, blue, "| Speed |", Default);
            Conbox toeren = BoxOutline.createBoxOutline(toerenout, blue, "| Toeren teller |", Default);



            Conbox Truckicon = new Conbox(new Boxsize(40, 13), new UCOORD(0, Truckdamage.getboxsize.height - 14), "TruckIcon", Truckdamage);
            Conbox truckTextarea = new Conbox(new Boxsize(Truckdamage.width - 40, Truckdamage.height), new UCOORD(40, 0), "TruckDamageText", Truckdamage);
            Conbox trailerIcon = new Conbox(new Boxsize(50, 11), new UCOORD(0, Trailerdamage.getboxsize.height - 13), "trailerIcon", Trailerdamage);
            Conbox trailerTextarea = new Conbox(new Boxsize(Trailerdamageout.width - 54, Trailerdamage.height), new UCOORD(50, 0), "trailer Text place", Trailerdamage);

            //write truck and trailer icon to there respective box
            conboxFunc.ConsolewriteMultiline(Truckicon, conboxFunc.ZeroZero, struck);
            conboxFunc.ConsolewriteMultiline(trailerIcon, conboxFunc.ZeroZero, trailer);


            //    // add to list of active boxes
            //    AddBox(gear);
            //    AddBox(FuelRange);
            //    AddBox(distance);
            //    AddBox(speed);
            //    AddBox(toeren);
            //    AddBox(Truckicon);
            //    AddBox(trailerIcon);
            //    AddBox(trailerTextarea);
            //    AddBox(truckTextarea);


        }







    }
}
