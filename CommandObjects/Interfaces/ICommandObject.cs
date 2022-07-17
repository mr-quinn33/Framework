using Framework.Rules.Interfaces;

namespace Framework.CommandObjects.Interfaces
{
    public interface ICommandObject : ISetGameMode, ISendCommand, ISendCommandAsync
    {
        void Execute();
    }
}