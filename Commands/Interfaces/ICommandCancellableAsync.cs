using System.Threading;
using System.Threading.Tasks;
using Framework.Rules.Interfaces;

namespace Framework.Commands.Interfaces
{
    public interface ICommandCancellableAsync : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task ExecuteAsync(CancellationTokenSource source);
    }

    public interface ICommandCancellableAsync<T> : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task<T> ExecuteAsync(CancellationTokenSource source);
    }
}