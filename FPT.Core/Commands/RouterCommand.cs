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
            if(args is null || args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
            {
                throw new InvalidCommandArrayException("The given command array was invalid and could not be executed.");
            }
            var commandId = args[0];
            commands.TryGetValue(commandId, out var command);
            if(command is null)
            {
                throw new CommandNotFoundException(commandId);
            }
            else
            {
                try
                {
                    return command.Execute(args.Skip(1).ToArray());
                }catch(InvalidCommandArrayException e)
                {
                    throw new InvalidCommandArrayException("A registered command was given an invalid command array.", e);
                }
            }
        }

        public void Register(string id, IExecutable command)
        {
            id = id?.Trim();
            if(id is null || !id.All(c => char.IsLetterOrDigit(c)))
            {
                throw new ArgumentException("Command ID must be alphanumeric.");
            }
            commands.Add(id, command);
        }
    }
}