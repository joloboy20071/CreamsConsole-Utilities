// See https://aka.ms/new-console-template for more information
using CreamsConsole_utils;






namespace dashboardv2.ui
{
    static class Program
    {
        static void Main(string[] args)
        {



            consoleAlloc.setupCreamsConsole();


            Console.WriteLine("press any key to start");
            Console.ReadKey();





            {
                var oldX = -1;
                var oldY = -1;
                while (true)
                {
                    int y = Console.WindowHeight;
                    int x = Console.WindowWidth;

                    if (oldX != x | oldY != y)
                    {


                        conboxFunc.ClearBox(conboxFunc.MainBody);
                        Console.Clear();
                        conboxFunc.updateMainbody();
                        DashboardUI.CreateBoxes();
                        oldX = x;
                        oldY = y;
                        
                    }

              


                }







            }


        }
    }

}