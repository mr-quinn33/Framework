using System.Threading.Tasks;
using Framework.Rules.Interfaces;

namespace Framework.Commands.Interfaces
{
    public interface ICommandAsync : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task ExecuteAsync();
    }

    public interface ICommandAsync<T> : ISetGameMode, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task<T> ExecuteAsync();
    }
}