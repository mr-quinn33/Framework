using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.CommandAsyncCancellable
{
    public abstract class CommandAsyncCancellable : ICommandAsyncCancellable
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

        async Task ICommandAsyncCancellable.ExecuteAsync(CancellationTokenSource source)
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
    
    public abstract class CommandAsyncCancellable<T> : ICommandAsyncCancellable<T>
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

        async Task<T> ICommandAsyncCancellable<T>.ExecuteAsync(CancellationTokenSource source)
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