using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class FakeCommand : IExecutable
    {
        public FakeCommand(string commandId) : base(commandId)
        {
        }

        public override object Execute(params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
