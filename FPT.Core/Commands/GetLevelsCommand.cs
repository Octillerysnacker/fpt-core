using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class GetLevelsCommand : IExecutable
    {
        private ILevelsProvider levelsProvider;
        public GetLevelsCommand(ILevelsProvider levelsProvider)
        {
            this.levelsProvider = levelsProvider;
        }

        public object Execute(params string[] args)
        {
            return levelsProvider.GetLevels();
        }
    }
}
