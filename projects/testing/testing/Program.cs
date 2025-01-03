
using System;
using System.Drawing;
using CreamsConsole_utils;
using CreamsConsole_utils.src;




consoleAlloc.setupCreamsConsole();
eventqueue eventq = new eventqueue();



Conbox main = BoxOutline.createBoxOutline(conboxFunc.MainBody, new WritingStyle(), "| main Box |");
TermialWriterequest write = new TermialWriterequest(main.GetBoxinfo, "ik ben een test request\nen ik ben een mutli line", new UCOORD(0, 13), new WritingStyle(Color.Red));




static valueBar initbar(Conbox inoutline, UCOORD pos, string barname)
{
    valueBar Bar = new valueBar();
    Bar.parentBox = inoutline;
    Bar.startpos = pos;
    Bar.BarName = barname;

    return Bar;
}

valueBar spotifyBar = initbar(main, new UCOORD(0, 0), "spotify");
Task bar1 = Task.Factory.StartNew(() =>
{
    
    

    for (int i = 0; i < 101; i++)
    {
        spotifyBar.DisplayValueBoxQueue(eventq, i);
        Thread.Sleep(100);


    }
});



Task bar2 = Task.Factory.StartNew(() =>
{
    valueBar dic = initbar(main, new UCOORD(0, 3), "bar 2");
    

    for (int i = 0; i < 101; i++)
    {
        dic.DisplayValueBoxQueue(eventq, i);

        Thread.Sleep(50);

    }

});

Thread.Sleep(2000);
eventqueue.checkenqueue(write);



Console.ReadKey();