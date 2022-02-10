using System.Threading;
using System.Threading.Tasks;
using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class SendCommandAsyncExtension
    {
        public static async Task SendCommandAsync<T>(this ISendCommandAsync self, T command) where T : ICommandAsync
        {
            await self.GetArchitecture().SendCommandAsync(command);
        }

        public static async Task SendCommandAsync<T>(this ISendCommandAsync self) where T : ICommandAsync, new()
        {
            await self.GetArchitecture().SendCommandAsync<T>();
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self, T command)
            where T : ICommandAsync<TResult>
        {
            var task = self.GetArchitecture().SendCommandAsync<T, TResult>(command);
            await task;
            return task.Result;
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self)
            where T : ICommandAsync<TResult>, new()
        {
            var task = self.GetArchitecture().SendCommandAsync<T, TResult>();
            await task;
            return task.Result;
        }

        public static async Task SendCommandAsync<T>(this ISendCommandAsync self, T command,
            CancellationTokenSource source) where T : ICommandAsyncCancellable
        {
            await self.GetArchitecture().SendCommandAsync(command, source);
        }

        public static async Task SendCommandAsync<T>(this ISendCommandAsync self, CancellationTokenSource source)
            where T : ICommandAsyncCancellable, new()
        {
            await self.GetArchitecture().SendCommandAsync<T>(source);
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self, T command,
            CancellationTokenSource source) where T : ICommandAsyncCancellable<TResult>
        {
            var task = self.GetArchitecture().SendCommandAsync<T, TResult>(command, source);
            await task;
            return task.Result;
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self,
            CancellationTokenSource source) where T : ICommandAsyncCancellable<TResult>, new()
        {
            var task = self.GetArchitecture().SendCommandAsync<T, TResult>(source);
            await task;
            return task.Result;
        }
    }
}