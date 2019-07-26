using FPT.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class ReturnCommand : IExecutable
    {
        private readonly object objToReturn;
        public ReturnCommand(string commandId, object objToReturn) : base(commandId)
        {
            this.objToReturn = objToReturn;
        }
        public override object Execute(params string[] args)
        {
            return objToReturn;
        }
    }
}
