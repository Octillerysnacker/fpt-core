using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.IO;
using System.Linq;
using FPT.Core.Exceptions;

namespace FPT.Core.Commands
{
    public class OpenLevelCommand : IExecutable
    {
        private ILevelInitializer levelInitializer;
        private ILevelsProvider levelsProvider;
        private string errorMessageFormat = "Not enough parameters were passed ({0} out of 2).";
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
            return System.IO.Path.Combine(levelsProvider.GetLevels().Single(level => level.Id == levelId).FolderFilepath, user, "project");
        }
    }
}
