using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Exceptions
{
    public class InvalidCommandArrayException : Exception
    {
        public InvalidCommandArrayException(string message) : base(message)
        {
        }

        public InvalidCommandArrayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
