using FPT.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class ReturnCommand : IExecutable
    {
        private readonly object objToReturn;
        public ReturnCommand(object objToReturn)
        {
            this.objToReturn = objToReturn;
        }
        public object Execute(params string[] args)
        {
            return objToReturn;
        }
    }
}
