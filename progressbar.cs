

namespace CreamsConsole_utils
{



    public class ProgressBars
    {

        public class ProgresBarConfig
        {
            public string barchar = "\u2014";
            public string TaskName = "placeholder";
            public byte TotalLengthBar = 50;
            public ConsoleColor completedColor = ConsoleColor.Green;
            public ConsoleColor incompleteColor = ConsoleColor.Gray;
            public ConsoleColor StepColor = ConsoleColor.Yellow;
            public int totalTasks;




        }




        


        private double GetDoneTiles(int totalTasks, int currentTask, int TotalLengthBar)
        {
            double procentDone = (currentTask * 100f / totalTasks);

            var ratio = 100.0 / TotalLengthBar;
            double beforeRound = procentDone / ratio;
            double TilesDone = utilFunctions.round(beforeRound);
            return TilesDone;
        }

        public int startBar(ProgresBarConfig config)
        {
            Console.CursorVisible = false;
            int Newcharcount = this.PrintProgress(config.totalTasks, 0, config);
            return Newcharcount;
        }
        public int UpdateBar(int currentTask, ProgresBarConfig config, int oldcharcount)
        {
            string remove = "\b";
            for (int i = 0; i < oldcharcount; i++)
            {
                remove += "\b";
            }
            Console.Write(remove);

            int Newcharcount = this.PrintProgress(config.totalTasks, currentTask, config);
            return Newcharcount;
        }




        private int PrintProgress(int totalTasks, int currentTask, ProgresBarConfig config)
        {
            int charcount = 0;


            double tilesDone = GetDoneTiles(totalTasks, currentTask, config.TotalLengthBar);
            double Tilesremander = config.TotalLengthBar - tilesDone;
            string taskName = $"{config.TaskName} ";
            Console.Write(taskName);

            string Done = "";
            string rem = "";
            for (int i = 0; i < tilesDone; i++)
            {
                Done += config.barchar;
            }
            for (int i = 0; i < Tilesremander; i++)
            {
                rem += config.barchar;
            }
            if (Tilesremander != 0.0)
            {
                ColorText.ColorWrite(Done, config.StepColor);
                ColorText.ColorWrite(rem, config.incompleteColor);
            }
            else { ColorText.ColorWrite(Done, config.completedColor); }
            string taskstatus = $" [{currentTask} / {totalTasks}]";

            charcount = taskstatus.Length + Done.Length + rem.Length + taskName.Length;


            ColorText.ColorWrite(taskstatus, ConsoleColor.Blue);
            if (Tilesremander == 0.0)
            {
                Console.Write("\n"); Console.Write("\n");
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.CursorVisible = true;
            }

            return charcount;
        }





    }
}

