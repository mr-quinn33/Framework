using System.Threading;
using System.Threading.Tasks;
using Framework.Rules.Interfaces;

namespace Framework.Commands.Interfaces
{
    public interface ICommandAsyncCancellable : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task ExecuteAsync(CancellationTokenSource source);
    }

    public interface ICommandAsyncCancellable<T> : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task<T> ExecuteAsync(CancellationTokenSource source);
    }
}