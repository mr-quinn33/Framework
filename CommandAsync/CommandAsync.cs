using System.Threading.Tasks;
using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.CommandAsync
{
    public abstract class CommandAsync : ICommandAsync
    {
        private IArchitecture _architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return _architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
        }

        async Task ICommandAsync.ExecuteAsync()
        {
            await ExecuteAsync();
        }
        
        protected abstract Task ExecuteAsync();
    }
    
    public abstract class CommandAsync<T> : ICommandAsync<T>
    {
        private IArchitecture _architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return _architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            _architecture = architecture;
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