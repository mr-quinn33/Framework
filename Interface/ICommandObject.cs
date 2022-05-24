using Framework.Interface.Access;

namespace Framework.Interface
{
    public interface ICommandObject : ISetArchitecture, ISendCommand, ISendCommandAsync
    {
        void Execute();
    }
}