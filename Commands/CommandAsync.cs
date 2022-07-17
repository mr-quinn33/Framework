using System.Threading.Tasks;
using Framework.Commands.Interfaces;
using Framework.GameModes.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Commands
{
    public abstract class CommandAsync : ICommandAsync
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

        async Task ICommandAsync.ExecuteAsync()
        {
            await ExecuteAsync();
        }

        protected abstract Task ExecuteAsync();
    }

    public abstract class CommandAsync<T> : ICommandAsync<T>
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

        async Task<T> ICommandAsync<T>.ExecuteAsync()
        {
            var task = ExecuteAsync();
            await task;
            return task.Result;
        }

        protected abstract Task<T> ExecuteAsync();
    }
}