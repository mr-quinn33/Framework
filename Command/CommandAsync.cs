using System.Threading.Tasks;
using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Command
{
    public abstract class CommandAsync : ICommandAsync
    {
        private IArchitecture architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            this.architecture = architecture;
        }

        async Task ICommandAsync.ExecuteAsync()
        {
            await ExecuteAsync();
        }

        protected abstract Task ExecuteAsync();
    }

    public abstract class CommandAsync<T> : ICommandAsync<T>
    {
        private IArchitecture architecture;

        IArchitecture IGetArchitecture.GetArchitecture()
        {
            return architecture;
        }

        void ISetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            this.architecture = architecture;
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