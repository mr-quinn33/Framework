using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Interface;

namespace Framework.GameMode
{
    public abstract class GameModeBase : IArchitecture
    {
        private protected static readonly Dictionary<Type, IArchitecture> Instances = new();

        public abstract T GetSystem<T>() where T : class, ISystem;

        public abstract T GetModel<T>() where T : class, IModel;

        public abstract T GetUtility<T>() where T : class, IUtility;

        public abstract void SendCommand<T>(T command) where T : ICommand;

        public abstract void SendCommand<T>() where T : ICommand, new();

        public abstract Task SendCommandAsync<T>(T command) where T : ICommandAsync;

        public abstract Task SendCommandAsync<T>() where T : ICommandAsync, new();

        public abstract Task<TResult> SendCommandAsync<T, TResult>(T command) where T : ICommandAsync<TResult>;

        public abstract Task<TResult> SendCommandAsync<T, TResult>() where T : ICommandAsync<TResult>, new();

        public abstract Task SendCommandAsync<T>(T command, CancellationTokenSource source)
            where T : ICommandAsyncCancellable;

        public abstract Task SendCommandAsync<T>(CancellationTokenSource source)
            where T : ICommandAsyncCancellable, new();

        public abstract Task<TResult> SendCommandAsync<T, TResult>(T command, CancellationTokenSource source)
            where T : ICommandAsyncCancellable<TResult>;

        public abstract Task<TResult> SendCommandAsync<T, TResult>(CancellationTokenSource source)
            where T : ICommandAsyncCancellable<TResult>, new();

        public abstract TResult SendQuery<TResult>(IQuery<TResult> query);

        public abstract TResult SendQuery<T, TResult>() where T : IQuery<TResult>, new();

        public abstract IUnregisterHandler RegisterEvent<T>(Action<T> action);

        public abstract IUnregisterHandler RegisterEvent<T, TResult>(Func<T, TResult> func);

        public abstract void UnregisterEvent<T>(Action<T> action);

        public abstract void UnregisterEvent<T, TResult>(Func<T, TResult> func);

        public abstract void InvokeEvent<T>(T t);

        public abstract void InvokeEvent<T>() where T : new();

        public abstract TResult InvokeEvent<T, TResult>(T t);

        public abstract TResult InvokeEvent<T, TResult>() where T : new();
    }
}