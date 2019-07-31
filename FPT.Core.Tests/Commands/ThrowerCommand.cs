using FPT.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.Commands
{
    public class ThrowerCommand : IExecutable
    {
        Exception e;
        public ThrowerCommand(Exception e)
        {
            this.e = e;
        }
        public object Execute(params string[] args)
        {
            throw e;
        }
    }
}
