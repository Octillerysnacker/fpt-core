namespace FPT.Core.Commands{
    public interface IExecutable{
        object Execute(params string[] args);
    }
}