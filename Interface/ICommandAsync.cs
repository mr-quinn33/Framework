using System.Threading.Tasks;
using Framework.Interface.Restriction;

namespace Framework.Interface
{
    public interface ICommandAsync : ISetArchitecture, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task ExecuteAsync();
    }
    
    public interface ICommandAsync<T> : ISetArchitecture, IGetSystem, IGetModel, IGetUtility, ISendCommand, ISendCommandAsync, ISendQuery, IInvokeEvent
    {
        Task<T> ExecuteAsync();
    }
}