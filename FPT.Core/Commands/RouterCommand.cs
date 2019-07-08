using System;
using System.Collections.Generic;
using FPT.Core.Exceptions;
using System.Linq;
namespace FPT.Core.Commands{
    public class RouterCommand : Command{
        private readonly List<Command> commands;
        public RouterCommand(string commandId) : base(commandId)
        {
            commands = new List<Command>();
        }
        public RouterCommand() : this("")
        {
        }
        public override object Execute(params string[] args)
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
            return null;
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