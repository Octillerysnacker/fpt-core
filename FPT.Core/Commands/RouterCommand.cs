using System;
using System.Collections.Generic;
using FPT.Core.Exceptions;
using System.Linq;
namespace FPT.Core.Commands{
    public class RouterCommand : IExecutable{
        private readonly Dictionary<string, IExecutable> commands;
        public RouterCommand()
        {
            commands = new Dictionary<string, IExecutable>();
        }
        public object Execute(params string[] args)
        {
            if(args is null || args.Length == 0 || args[0] is null || args[0].Trim() == "")
            {
                throw new ArgumentException("Command ID must exist, not be null or empty.");
            }
            var commandId = args[0];
            commands.TryGetValue(commandId, out var command);
            if(command is null)
            {
                throw new CommandNotFoundException(commandId);
            }
            else
            {
                return command.Execute(args.Skip(1).ToArray());
            }
        }

        public void Register(string id, IExecutable sc)
        {
            if(id.Trim() == "")
            {
                throw new ArgumentException("Command IDs cannot be blank.");
            }
            commands.Add(id, sc);
        }
    }
}