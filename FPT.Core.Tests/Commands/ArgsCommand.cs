using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Commands;
using System.Linq;
namespace FPT.Core.Commands
{
    public class ArgsCommand : IExecutable
    {
        public string[] Args { get; private set; }
        public object Execute(string[] args)
        {
            Args = args;
            return null;
        }
    }
}
