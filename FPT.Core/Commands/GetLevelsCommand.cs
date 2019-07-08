using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class GetLevelsCommand : Command
    {
        private ILevelsProvider levelsProvider;
        public GetLevelsCommand(string commandId, ILevelsProvider levelsProvider) : base(commandId)
        {
            this.levelsProvider = levelsProvider;
        }

        public override object Execute(params string[] args)
        {
            return levelsProvider.Levels;
        }
    }
}
