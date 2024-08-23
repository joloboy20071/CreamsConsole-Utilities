# CreamsConsole-Utilities


A Console Utilities library inspired by spectre console focused on customization and good looking console applications

NuGet Package: https://www.nuget.org/packages/CreamsConsole-Utilities

Github Source code: https://github.com/joloboy20071/CreamsConsole-Utilities

Docs (work in progress): https://docs.creams-productions.nl/projects/creams-console-utilities-package-documentation


# example code 

## Write Colored text with in line syntax EXPEREMENTAL!

```c#
using CreamsConsole_utils;

ColorText.CreateCustomColor("MidnightBlue", "#191970");//Define your own custom colors. must have hex value when defining custom colors
ColorText.ColorWriteIn("[/]{Invert}_Red[/]foo[//] [/]{Bold,UnderLine}_MidnightBlue[/]bar[//] , [/]{Italic,Strike}_[/]foo[//][/]Green[/] bar[//]");//Have Inline styling asswell 
 //Supported style  modifiers are [Invert,Bold,Strike,Italic,UnderLine]

```
### output
![alt text](https://github.com/joloboy20071/CreamsConsole-Utilities/blob/master/pics/inlineConsoleColor.png)

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
![alt text](https://github.com/joloboy20071/CreamsConsole-Utilities/blob/master/pics/selectionMenu.png)


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
![alt text](https://github.com/joloboy20071/CreamsConsole-Utilities/blob/master/pics/selectionMenu1.png)


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
![alt text](https://github.com/joloboy20071/CreamsConsole-Utilities/blob/master/pics/multiselectionmenu.png)


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
![alt text](https://github.com/joloboy20071/CreamsConsole-Utilities/blob/master/pics/funcbar.png)

![alt text](https://github.com/joloboy20071/CreamsConsole-Utilities/blob/master/pics/funcbar2.png)

