using FPT.Core.Commands;
using System.IO.Abstractions;
using FPT.Core.Levels.Providers;
using FPT.Core.Levels.Initialization;
using FPT.Core.IO;
using FPT.Core.Verification;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core
{
    public class MainAppFactory
    {
        public static IExecutable MakeApp(string rootPath = ".")
        {
            var main = new RouterCommand();
            var fs = new FileSystem();
            var provider = new FileSystemLevelsProvider(fs, rootPath);
            var initializer = new MasterFolderLevelInitializer(
                    new UserJsonLevelInitializationDeterminer(fs),
                    new CopyDir(fs),
                    provider,
                    fs.Path);

            main.Register("levels", new GetLevelsCommand(provider));
            main.Register("open", new OpenLevelCommand(
                initializer, 
                provider));
            main.Register("instructions", new GetInstructionsPathCommand(provider));
            main.Register("verify", new VerifyCommand(
                provider,
                new JarFileVerifier(new JarFileProcessFactory())
                ));
            main.Register("reset", new ResetLevelCommand(initializer));

            return main;
        }
    }
}
