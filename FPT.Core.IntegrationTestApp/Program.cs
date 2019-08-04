using FPT.Core.Commands;
using FPT.Core.IO;
using System;
using System.IO.Abstractions;

namespace FPT.Core.IntegrationTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = BuildApp();

            var input = Console.ReadLine();
            while(input != "quit")
            {
                object result = "No output returned.";
                try
                {
                    result = main.Execute(input.Split(' '));
                }catch(Exception e)
                {
                    result = new SummarizedException(e);
                }
                finally
                {
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                }
                input = Console.ReadLine();
            }
        }
        static IExecutable BuildApp()
        {
            var main = new RouterCommand();
            var fs = new FileSystem();
            var provider = new FileSystemLevelsProvider(fs, ".");
            main.Register("levels", new GetLevelsCommand(provider));
            main.Register("open", new OpenLevelCommand(new MasterFolderLevelInitializer(new UserJsonLevelInitializationDeterminer(fs), new CopyDir(fs), provider, fs.Path), provider));
            return main;
        }
        private class SummarizedException
        {
            public Type Type { get; }
            public string Message { get; }
            public SummarizedException InnerException { get; }
            public SummarizedException(Exception e)
            {
                Type = e.GetType();
                Message = e.Message;
                if(!(e.InnerException is null))
                {
                    InnerException = new SummarizedException(e.InnerException);
                }
            }
        }
    }
}
