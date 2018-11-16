using SearchTools.Core;
using System;

namespace SearchTools.Test.CoreCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            new SearchEngine()
                .SetOutputAction(str => Console.WriteLine(str))
                .Search("nike");
        }
    }
}
