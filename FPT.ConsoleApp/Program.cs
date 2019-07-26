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
            object output = "Nothing was serialized.";
            try
            {
                var mainCommand = new RouterCommand();
                var fileSystem = new FileSystem();
                var levelsProvider = new FileSystemLevelsProvider(fileSystem, ".");
                mainCommand.Register(new GetLevelsCommand("levels", levelsProvider));
                mainCommand.Register(new OpenLevelCommand("open", new MasterFolderLevelInitializer(new UserJsonLevelInitializationDeterminer(fileSystem), new CopyDir(fileSystem), levelsProvider,fileSystem.Path),levelsProvider));
                output = mainCommand.Execute(args);
            }
            catch (Exception e)
            {
                output = e;
            }
            finally
            {
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(output));
            }
        }
    }
}
