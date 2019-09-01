using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FPT.Core.Exceptions;
using FPT.Core.Levels.Initialization;
using FPT.Core.Levels.Providers;

namespace FPT.Core.Commands
{
    public class OpenLevelCommand : IExecutable
    {
        private readonly ILevelInitializer levelInitializer;
        private readonly ILevelsProvider levelsProvider;
        private readonly string errorMessageFormat = "Not enough parameters were passed ({0} out of 2).";
        public OpenLevelCommand(ILevelInitializer levelInitializer, ILevelsProvider levelsProvider)
        {
            this.levelInitializer = levelInitializer;
            this.levelsProvider = levelsProvider;
        }

        public object Execute(params string[] args)
        {
            if(args is null)
            {
                throw new InvalidCommandArrayException(string.Format(errorMessageFormat, 0));
            }else if(args.Length < 2)
            {
                throw new InvalidCommandArrayException(string.Format(errorMessageFormat, args.Length));
            }
            string levelId = args[0];
            string user = args[1];

            levelInitializer.InitializeIfNecessary(levelId, user);

            //TO-DO: Remove logical dependency on how folders are structured
            return System.IO.Path.Combine(levelsProvider.GetLevel(levelId).FolderFilepath, user, "project");
        }
    }
}
