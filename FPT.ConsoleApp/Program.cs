using System;
using FPT.Core.Commands;
using FPT.Core.IO;
using System.IO.Abstractions;
namespace FPT.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainCommand = new RouterCommand();
            mainCommand.Register(new GetLevelsCommand("levels", new FileSystemLevelsProvider(new FileSystem(), ".")));

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(mainCommand.Execute("levels")));
        }
    }
}
