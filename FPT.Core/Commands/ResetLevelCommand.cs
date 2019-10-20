using FPT.Core.Exceptions;
using FPT.Core.Levels.Initialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class ResetLevelCommand : IExecutable
    {
        private readonly ILevelInitializer initializer;
        public ResetLevelCommand(ILevelInitializer initializer)
        {
            this.initializer = initializer;
        }
        public object Execute(params string[] args)
        {
            if(args is null || args.Length < 2)
            {
                throw new InvalidCommandArrayException("Not enough parameters were passed. 2 parameters are required.");
            }
            var levelId = args[0];
            var user = args[1];
            if(string.IsNullOrWhiteSpace(levelId) || string.IsNullOrWhiteSpace(user))
            {
                throw new InvalidCommandArrayException("No null or whitespace arguments are allowed.");
            }
            initializer.Clean(levelId, user);
            initializer.InitializeIfNecessary(levelId, user);
            return null;
        }
    }
}
