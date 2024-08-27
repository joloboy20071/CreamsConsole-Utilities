# CreamsConsole-Utilities


A Console Utilities library inspired by spectre console focused on customization and good looking console applications

NuGet Package: https://www.nuget.org/packages/CreamsConsole-Utilities

Github Source code: https://github.com/joloboy20071/CreamsConsole-Utilities

Docs (work in progress): https://docs.creams-productions.nl/projects/creams-console-utilities-package-documentation

## Projects

A simple Terminal Euro Truck 2 Dashboard project made with Creams Console Utilities
![alt text](https://i.imgur.com/EhKxPac.pngc)



# example code

## Create Simple TUI's With Creams Console Utilities

```c#
using CreamsConsole_utils;

Console.OutputEncoding = Encoding.UTF8;

int collum = (Console.BufferWidth / 5);
int row = (Console.BufferHeight / 3);

var box1 = Boxrendering.RenderBoxAtplace(collum * 3, (row * 2)+1, new Location((0 * collum), 0), "Box 1");
var box21 = Boxrendering.RenderBoxAtplace(collum * 1, (row * 1) + 1, new Location((3*collum),0), "BOX 2.1");
var box22=  Boxrendering.RenderBoxAtplace((collum * 1), (row * 1)+1 , new Location((3 * collum), (row * 1)), "BOX 2.2");
var box3 = Boxrendering.RenderBoxAtplace(collum * 5, (row * 1) + 1, new Location(0, (row*2)),"BOX 3");



foreach (var box in Boxrendering.boxTypes) {
    Boxrendering.WriteLineInBox(FiggleFonts.Standard.Render($"{box.WritableHeight} X {box.WritableWidth}"), box, 0,0,false, ColorText.HexToRGB(ColorText.allColors["Gray"]));
    string area = $"   Writable box area: {box.WritableHeight} X {box.WritableWidth}";
}

Console.ReadKey();
Console.SetCursorPosition(Console.BufferWidth - 1, Console.BufferHeight - 1);
```
### output
![alt text](https://i.imgur.com/SMPZg3s.png)


## quick Experemental Documentation of TUI  
```C#
public class Location(int x, int y)
// stores a location in the terminal

public class BoxType(Location location, int width, int height, string? Title) 
//Holdes box data

public static void WriteSingleInBox(string messgae  ,BoxType box , int StartAtY = 0, Color? color = null)
// writes string to box Without newline

public static void WriteLineInBox(string messgae, BoxType box, int startAtX=0, int StartAtY = 0,bool Center=false, Color? color = null)
// samge as WriteSinglelineinBox but with new line 

public static void WriteLineInLineBox(string messgae, BoxType box, int startAtX = 0, int StartAtY = 0, bool Center = false, Color? color = null,int? limit=null)
// sane as WriteLineInBox but with in line color 

public static void ClearBox(BoxType box) 
// Clears all lines in a box

public static void writeAtPost(string message, int x, int y, Color color)
// Write message at a posistion in the terminal

public static void writeInLineAtPost(string message, int x, int y)
// same as writeAtPost but with a new line

public static BoxType RenderBoxAtplace(int witdh, int height, Location location, string? title=null, Color? colorFrame = null, Color? colorTitle = null)
//Writes a box to terminal and return the box type

```



## Write Colored text with in line syntax

```c#
using CreamsConsole_utils;

ColorText.CreateCustomColor("MidnightBlue", "#191970");//Define your own custom colors. must have hex value when defining custom colors
ColorText.ColorWriteIn("[/]{Invert}_Red[/]foo[//] [/]{Bold,UnderLine}_MidnightBlue[/]bar[//] , [/]{Italic,Strike}_[/]foo[//][/]Green[/] bar[//]");//Have Inline styling asswell 
 //Supported style  modifiers are [Invert,Bold,Strike,Italic,UnderLine]

```
### output
![alt text](https://i.imgur.com/rojWCZ7.png)

## Simple Selection menu example
```c#
using CreamsConsole_utils;

selectionMenu.config config = new selectionMenu.config() { };

config.AddChoise("Option 1", "returnID 1"); // add option with title and a return id 
config.AddChoise("Option 2", "returnID 2");
config.AddChoise("Option 3"); // add option with title and a return id = title
config.title = "Im a title";
config.description = "im a discription";


string output = selectionMenu.runtimeMenu(config);
Console.WriteLine(output);
```
### output
![alt text](https://i.imgur.com/xX7wYdH.png)


## Simple Selection menu example without exit
```c#
using CreamsConsole_utils;

selectionMenu.config config = new selectionMenu.config() { };

config.AddChoise("Option 1", "returnID 1"); // add option with title and a return id 
config.AddChoise("Option 2", "returnID 2");
config.AddChoise("Option 3"); // add option with title and a return id = title
config.title = "Im a title"; // sets the title
config.description = "im a discription";

config.HasExit = false;
string output = selectionMenu.runtimeMenu(config);
Console.WriteLine(output);
```
### output
![alt text](https://i.imgur.com/UFl2ylq.png)


## Simple multi Selection Menu
```c#
using CreamsConsole_utils;


MultiSelectionMenu.config config = new MultiSelectionMenu.config() { };

config.AddChoise("Option 1", "returnID 1", true); //add a option with Title: option 1, a returnID and set the option to true
config.AddChoise("Option 2", "returnID 2");// add a option with Title: option 2, a returnID and set the option to false
config.AddChoise("Option 3", true); //add a option with Title: option 3, a returnID = option title and set the option to true
config.title = "Im a title"; //Set the Menu title  
config.description = "im a discription"; // Set the menu discription 

MultiSelectionMenu.ReturnedData Output = MultiSelectionMenu.runtimeMenu(config);

if (Output.isSaved) { for (int i = 0; i < Output.returnIDS.Count; i++) { Console.WriteLine($"{Output.returnIDS[i]} -> {Output.returnDict[Output.returnIDS[i]]}"); } }
```
### output
![alt text](https://i.imgur.com/9t3W7uI.png)


## Simple progress bar
```c#
using CreamsConsole_utils;

ProgressBars progressBar = new ProgressBars();

ProgressBars.ProgresBarConfig progresBarConfig = new ProgressBars.ProgresBarConfig();



progresBarConfig.totalTasks = 100; //set the amount of tasks to complete
progresBarConfig.TaskName = "task";// set the main progeess bar name

int bar = progressBar.startBar(progresBarConfig);

for (int i = 0; i < progresBarConfig.totalTasks; i++) {
    Thread.Sleep(100); // do some work
    progressBar.UpdateBar(i + 1, progresBarConfig, bar);// update the progress bar
}
```
### output
![alt text](https://i.imgur.com/4yoaMbo.png)

![alt text](https://i.imgur.com/EN118Rt.png)

