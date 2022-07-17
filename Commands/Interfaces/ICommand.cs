using Framework.Rules.Interfaces;

namespace Framework.Commands.Interfaces
{
    public interface ICommand : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        void Execute();
    }
}