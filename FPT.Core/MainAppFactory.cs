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
        public static IExecutable MakeApp()
        {
            var main = new RouterCommand();
            var fs = new FileSystem();
            var provider = new FileSystemLevelsProvider(fs, ".");

            main.Register("levels", new GetLevelsCommand(provider));
            main.Register("open", new OpenLevelCommand(
                new MasterFolderLevelInitializer(
                    new UserJsonLevelInitializationDeterminer(fs),
                    new CopyDir(fs),
                    provider,
                    fs.Path), 
                provider));
            main.Register("instructions", new GetInstructionsPathCommand(provider));
            main.Register("verify", new VerifyCommand(
                provider,
                new JarFileVerifier(new JarFileProcessFactory())
                ));

            return main;
        }
    }
}
