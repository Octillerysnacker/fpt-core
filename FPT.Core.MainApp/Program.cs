using FPT.Core.Commands;
using FPT.Core.IO;
using System;
using System.IO.Abstractions;
using Newtonsoft.Json;
namespace FPT.Core.MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = BuildApp();
            try
            {
                var result = main.Execute(args);
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(JsonConvert.SerializeObject(new SummarizedException(e)));
            }
        }
        static IExecutable BuildApp()
        {
            var main = new RouterCommand();
            var fs = new FileSystem();
            var provider = new FileSystemLevelsProvider(fs, System.Configuration.ConfigurationManager.AppSettings.Get("LevelsDirectory"));
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
                if (!(e.InnerException is null))
                {
                    InnerException = new SummarizedException(e.InnerException);
                }
            }
        }
    }
}
