using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class SendCommandAsyncExtension
    {
        public static async Task SendCommandAsync<T>(this ISendCommandAsync self, T command) where T : ICommandAsync
        {
            await self.GetGameMode().SendCommandAsync(command);
        }

        public static async Task SendCommandAsync<T>(this ISendCommandAsync self) where T : ICommandAsync, new()
        {
            await self.GetGameMode().SendCommandAsync<T>();
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self, T command) where T : ICommandAsync<TResult>
        {
            var task = self.GetGameMode().SendCommandAsync<T, TResult>(command);
            await task;
            return task.Result;
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self) where T : ICommandAsync<TResult>, new()
        {
            var task = self.GetGameMode().SendCommandAsync<T, TResult>();
            await task;
            return task.Result;
        }

        public static async Task SendCommandAsync<T>(this ISendCommandAsync self, T command, CancellationTokenSource source) where T : ICommandCancellableAsync
        {
            await self.GetGameMode().SendCommandAsync(command, source);
        }

        public static async Task SendCommandAsync<T>(this ISendCommandAsync self, CancellationTokenSource source) where T : ICommandCancellableAsync, new()
        {
            await self.GetGameMode().SendCommandAsync<T>(source);
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self, T command, CancellationTokenSource source) where T : ICommandCancellableAsync<TResult>
        {
            var task = self.GetGameMode().SendCommandAsync<T, TResult>(command, source);
            await task;
            return task.Result;
        }

        public static async Task<TResult> SendCommandAsync<T, TResult>(this ISendCommandAsync self, CancellationTokenSource source) where T : ICommandCancellableAsync<TResult>, new()
        {
            var task = self.GetGameMode().SendCommandAsync<T, TResult>(source);
            await task;
            return task.Result;
        }
    }
}