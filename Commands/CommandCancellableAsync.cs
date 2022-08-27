using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.Interfaces;
using Framework.GameModes.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Commands
{
    public abstract class CommandCancellableAsync : ICommandCancellableAsync
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

        async Task ICommandCancellableAsync.ExecuteAsync(CancellationTokenSource source)
        {
            try
            {
                await ExecuteAsync(source.Token);
            }
            catch (OperationCanceledException exception)
            {
                await Task.FromCanceled(exception.CancellationToken);
            }
            finally
            {
                source.Dispose();
            }
        }

        protected abstract Task ExecuteAsync(CancellationToken token);
    }

    public abstract class CommandCancellableAsync<T> : ICommandCancellableAsync<T>
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

        async Task<T> ICommandCancellableAsync<T>.ExecuteAsync(CancellationTokenSource source)
        {
            var task = ExecuteAsync(source.Token);

            try
            {
                await task;
            }
            catch (OperationCanceledException exception)
            {
                await Task.FromCanceled<T>(exception.CancellationToken);
            }
            finally
            {
                source.Dispose();
            }

            return task.Result;
        }

        protected abstract Task<T> ExecuteAsync(CancellationToken token);
    }
}