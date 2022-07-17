using Framework.Commands.Interfaces;
using Framework.GameModes.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Commands
{
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