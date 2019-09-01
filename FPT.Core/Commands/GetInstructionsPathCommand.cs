using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FPT.Core.Exceptions;
using FPT.Core.Levels.Providers;

namespace FPT.Core.Commands
{
    public class GetInstructionsPathCommand : IExecutable
    {
        private ILevelsProvider provider;
        public GetInstructionsPathCommand(ILevelsProvider provider)
        {
            this.provider = provider;
        }
        public object Execute(params string[] args)
        {
            if(args == null || args.Length == 0)
            {
                throw new InvalidCommandArrayException("Not enough parameters were passed (0 out of 1).");
            }
            var level = provider.GetLevel(args[0]);
            return Path.Combine(level.FolderFilepath, level.InstructionsFilepath);
        }
    }
}
