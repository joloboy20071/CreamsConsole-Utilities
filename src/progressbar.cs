namespace CreamsConsole_utils
{



    public class ProgressBars
    {

        public class ProgresBarConfig
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

        public int startBar(ProgresBarConfig config)
        {
            Console.CursorVisible = false;
            var Newcharcount = PrintProgress(config.totalTasks, 0, config);
            return Newcharcount;
        }
        public int UpdateBar(int currentTask, ProgresBarConfig config, int oldcharcount)
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




        private int PrintProgress(int totalTasks, int currentTask, ProgresBarConfig config)
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
}

