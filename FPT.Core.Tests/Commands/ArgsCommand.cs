using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Commands;
using System.Linq;
namespace FPT.Core.Commands
{
    public class ArgsCommand : Command
    {
        public string[] Args { get; private set; }

        public ArgsCommand(string commandId) :
            base(commandId)
        {

        }
        public override object Execute(string[] args)
        {
            Args = args;
            return null;
        }
    }
}
