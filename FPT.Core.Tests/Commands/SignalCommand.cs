using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class SignalCommand : IExecutable
    {
        public SignalCommand(string commandId) :
            base(commandId)
        {
        }

        public bool HasBeenRun { get; private set; }
        public override object Execute(params string[] args)
        {
            HasBeenRun = true;
            return null;
        }
    }
}
