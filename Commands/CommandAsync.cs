using System.Threading.Tasks;
using Framework.GameModes;
using Framework.Rules;

namespace Framework.Commands
{
    public interface ICommandAsync : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, ISendEvent
    {
        Task ExecuteAsync();
    }

    public interface ICommandAsync<T> : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, ISendEvent
    {
        Task<T> ExecuteAsync();
    }

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