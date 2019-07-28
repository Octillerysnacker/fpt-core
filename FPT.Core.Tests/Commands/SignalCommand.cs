using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class SignalCommand : IExecutable
    {
        public bool HasBeenRun { get; private set; }
        public object Execute(params string[] args)
        {
            HasBeenRun = true;
            return null;
        }
    }
}
