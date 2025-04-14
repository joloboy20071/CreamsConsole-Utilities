using System.Drawing;
using System.Dynamic;

namespace CreamsConsole_utils
{



    public class lagacyProgressBars
    {

        public class lagacyProgresBarConfig
        {
            public string barchar = "\u2501";
            public string TaskName = "placeholder";
            public byte TotalLengthBar = 50;

            // standard Colors
            public string completedColorHex = ColorText.allColors["Green"];
            public string incompleteColorHex = ColorText.allColors["Gray"];
            public string StepColorHex = ColorText.allColors["Yellow"];
            public string TaskStatusColorHex = ColorText.allColors["Blue"];
            
            public int totalTasks;

        }

        private double GetDoneTiles(int totalTasks, int currentTask, int TotalLengthBar)
        {
            double procentDone = currentTask * 100f / totalTasks;

            var ratio = 100.0 / TotalLengthBar;
            var beforeRound = procentDone / ratio;
            var TilesDone = utilFunctions.round(beforeRound);
            return TilesDone;
        }

        public int startBar(lagacyProgresBarConfig config)
        {
            Console.CursorVisible = false;
            var Newcharcount = PrintProgress(config.totalTasks, 0, config);
            return Newcharcount;
        }
        public int UpdateBar(int currentTask, lagacyProgresBarConfig config, int oldcharcount)
        {
            var remove = "\b";
            for (var i = 0; i < oldcharcount; i++)
            {
                remove += "\b";
            }
            Console.Write(remove);

            var Newcharcount = PrintProgress(config.totalTasks, currentTask, config);
            return Newcharcount;
        }




        private int PrintProgress(int totalTasks, int currentTask, lagacyProgresBarConfig config)
        {
            var charcount = 0;


            var tilesDone = GetDoneTiles(totalTasks, currentTask, config.TotalLengthBar);
            var Tilesremander = config.TotalLengthBar - tilesDone;
            var taskName = $"{config.TaskName} ";
            ColorText.ColorWriteIn(taskName);

            var Done = "";
            var rem = "";
            for (var i = 0; i < tilesDone; i++)
            {
                Done += config.barchar;
            }
            for (var i = 0; i < Tilesremander; i++)
            {
                rem += config.barchar;
            }
            if (Tilesremander != 0.0)
            {
                ColorText.ColorWrite(Done, ColorText.HexToRGB(config.StepColorHex));
                ColorText.ColorWrite(rem, ColorText.HexToRGB(config.incompleteColorHex));
            }
            else { ColorText.ColorWrite(Done, ColorText.HexToRGB(config.completedColorHex)); }
            var taskstatus = $" [{currentTask} / {totalTasks}]";

            charcount = taskstatus.Length + Done.Length + rem.Length + taskName.Length;


            ColorText.ColorWrite(taskstatus, ColorText.HexToRGB(config.TaskStatusColorHex));
            if (Tilesremander == 0.0)
            {
                Console.Write("\n\n");
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.CursorVisible = true;
            }

            return charcount;
        }





    }

    public class progressBar
    {
        private bool Iscompleted = false;
        private bool isActive = false;

        public string barchar = "\u2501";

        public bool showTaskName = true;
        public string TaskName = "placeholder";

        public WritingStyle CompletedColor = new WritingStyle(Color.Green);
        public WritingStyle incompleteColor = new WritingStyle(Color.Gray);
        public WritingStyle StepColor = new WritingStyle(Color.AliceBlue);
        public WritingStyle taskStatusColor = new WritingStyle(Color.DarkCyan);

        public Conbox parentBox = conboxFunc.MainBody;
        private Conbox ProgressBox;


        public UCOORD startplace = new UCOORD(0, 0);

        private int TotalTasks = 0;

        private int barlength = 50;


        public bool Iscomplete{
            get
            {
                return Iscompleted;
            }
        }

        public progressBar(int totalTasks)
        {
            this.TotalTasks = totalTasks;
        }

        public int taskAmount
        {

            get
            {
                return TotalTasks;
            }
        }

        public Conbox GetprogressBox
        {
            get
            {
                return ProgressBox;
            }

        }


        public void setBarLenght(int length){
            if (GetBarWidth($"[{TotalTasks}/{TotalTasks}]",length) < (parentBox.width - startplace.x)) { barlength = length;return; }
            return;
        }

        public int getBarLenght
        {
            get
            {
                return barlength;
            }
        }


        private (int,int,string) GetProgressData(int TaskDone)
        {
            int TaskperTile = (int)(TotalTasks / barlength);
            int CompletedTiles = (int)(TaskDone / TaskperTile);
            int incompleteTiles = this.barlength - CompletedTiles;

            return (CompletedTiles,incompleteTiles,$"[{TaskDone}/{TotalTasks}]");



        }
        private int GetBarWidth(string tasks,int newleng=0)
        {
            int Barwidth = 0;

            if (showTaskName) { Barwidth += TaskName.Length + 1; }
            if (newleng == 0)
            {
                Barwidth += tasks.Length + barlength + 1;
            }
            if (newleng != 0) { barlength += tasks.Length + newleng + 1; }
            return Barwidth;

        }



        public void DisplayProgress(int taskdone =0)
        {
            int cursorpos = 0;


            var data = GetProgressData(taskdone);

            if (!isActive) { ProgressBox = new Conbox(new Boxsize(GetBarWidth($"[{TotalTasks}/{TotalTasks}]"), 1), startplace, $"{TaskName}'s progressbar", parentBox); }

            string completeString = utilFunctions.stringgenerator(this.barchar, data.Item1);
            string incompleteString = utilFunctions.stringgenerator(this.barchar, data.Item2);

            if (showTaskName) { conboxFunc.consolewriteAtpos(ProgressBox, new UCOORD(0, 0), TaskName); cursorpos += TaskName.Length + 1; }


            if (completeString.Length < barlength)
            {
                conboxFunc.consolewriteAtpos(ProgressBox, new UCOORD(cursorpos, 0), completeString, this.StepColor); cursorpos += completeString.Length;
                conboxFunc.consolewriteAtpos(ProgressBox, new UCOORD(cursorpos, 0), incompleteString, this.incompleteColor); cursorpos += incompleteString.Length + 1;
                conboxFunc.consolewriteAtpos(ProgressBox, new UCOORD(cursorpos, 0), data.Item3, this.taskStatusColor);
                return;
            
            }
            if (completeString.Length == barlength) {
                conboxFunc.consolewriteAtpos(ProgressBox, new UCOORD(cursorpos, 0), completeString, this.CompletedColor); cursorpos += completeString.Length+1;
                conboxFunc.consolewriteAtpos(ProgressBox, new UCOORD(cursorpos, 0), data.Item3, this.taskStatusColor);
                Iscompleted = true;
                return;

            }








        }






    }
















}




