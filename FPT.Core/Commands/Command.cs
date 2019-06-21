namespace FPT.Core.Commands{
    public abstract class Command{
        public string CommandId { get; }
        public Command(string commandId)
        {
            CommandId = commandId;
        }

        public abstract object Execute(params string[] args);
    }
}