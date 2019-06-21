using System;

namespace FPT.Core.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string notFoundCommandId) :
            base($"Command with ID {notFoundCommandId} was not found.")
        {
        }
    }
}