using System.Threading;
using System.Threading.Tasks;
using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface ICommandAsyncCancellable : ISetArchitecture, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task ExecuteAsync(CancellationTokenSource source);
    }
    
    public interface ICommandAsyncCancellable<T> : ISetArchitecture, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task<T> ExecuteAsync(CancellationTokenSource source);
    }
}