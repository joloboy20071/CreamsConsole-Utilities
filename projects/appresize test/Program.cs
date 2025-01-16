// See https://aka.ms/new-console-template for more information
using CreamsConsole_utils;






namespace dashboardv2.ui
{
    static class Program
    {




        public static void updateUi()
        {





            conboxFunc.ClearBox(conboxFunc.MainBody);
            Console.Clear();
            conboxFunc.updateMainbody();
            DashboardUI.CreateBoxes();
        }





        static void Main(string[] args)
        {

            int minx= 200, miny=50;

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


                        if (x < minx | y < miny) { Console.SetWindowSize(minx + 1, miny + 1); oldX = minx + 1; oldY = miny + 1; Thread.Sleep(4);
                                                  
                        
                        }


                        if(Console.WindowWidth>minx-1 && Console.WindowHeight > miny-1) { updateUi(); oldX = Console.WindowWidth;  oldY = Console.WindowHeight; Thread.Sleep(4); }

                        



                    }
                    Thread.Sleep(1);
              


                }







            }


        }
    }

}