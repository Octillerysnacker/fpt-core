using System;
using System.Collections.Generic;
using FPT.Core.Commands;
using FPT.Core.Exceptions;
using System.Linq;
namespace FPT.Core{
    public class CommandExecutor{
        private readonly List<Command> commands;
        public CommandExecutor()
        {
            commands = new List<Command>();
        }
        public void Execute(params string[] args)
        {
            if(args is null || args.Length == 0 || args[0] is null || args[0].Trim() == "")
            {
                throw new ArgumentException("Command ID must exist, not be null or empty.");
            }
            var commandId = args[0];
            var command = commands.Find(c => c.CommandId.Trim() == commandId);
            if(command is null)
            {
                throw new CommandNotFoundException(commandId);
            }
            else
            {
                command.Execute(args.Skip(1).ToArray());
            }
        }

        public void Register(Command sc)
        {
            if(sc.CommandId.Trim() == "")
            {
                throw new ArgumentException("Command IDs cannot be blank.");
            }
            commands.Add(sc);
        }
    }
}