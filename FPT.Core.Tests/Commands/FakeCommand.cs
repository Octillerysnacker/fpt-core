﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class FakeCommand : IExecutable
    {
        public object Execute(params string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
