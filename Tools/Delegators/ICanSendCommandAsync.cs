using System.Threading.Tasks;
using Framework.Commands;

namespace Framework.Tools.Delegators
{
    public interface ICanSendCommandAsync
    {
        Task SendCommandAsync<T>(T command) where T : ICommandAsync;

        Task SendCommandAsync<T>() where T : ICommandAsync, new();
    }
}