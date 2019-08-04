using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Exceptions
{
    public class LevelNotFoundException : Exception
    {
        public LevelNotFoundException(string message) : base(message)
        {
        }
    }
}
