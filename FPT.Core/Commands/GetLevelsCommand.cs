using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class GetLevelsCommand : Command
    {
        private IFPTContext context;
        public GetLevelsCommand(string commandId, IFPTContext context) : base(commandId)
        {
            this.context = context;
        }

        public override object Execute(params string[] args)
        {
            return context.Levels;
        }
    }
}
