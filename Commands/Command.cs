using Framework.GameModes;
using Framework.Rules;

namespace Framework.Commands
{
    public interface ICommand : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, ISendEvent
    {
        void Execute();
    }

    public abstract class Command : ICommand
    {
        private IGameMode gameMode;

        IGameMode IGetGameMode.GetGameMode()
        {
            return gameMode;
        }

        void ISetGameMode.SetGameMode(IGameMode gameMode)
        {
            this.gameMode = gameMode;
        }

        void ICommand.Execute()
        {
            Execute();
        }

        protected abstract void Execute();
    }
}