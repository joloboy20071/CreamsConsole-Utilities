using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreamsConsole_utils;
public class InvalidStyleOption : Exception
{
    public InvalidStyleOption(string message) : base(message) { Console.WriteLine(message); }



}
