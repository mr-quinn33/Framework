using System;
using System.Threading;
using System.Threading.Tasks;
using Framework.Commands.Interfaces;
using Framework.Interfaces;
using Framework.Models.Interfaces;
using Framework.Queries.Interfaces;
using Framework.Systems.Interfaces;
using Framework.Utilities.Interfaces;

namespace Framework.GameModes.Interfaces
{
    public interface IGameMode
    {
        T GetSystem<T>() where T : class, ISystem;

        T GetModel<T>() where T : class, IModel;

        T GetUtility<T>() where T : class, IUtility;

        void SendCommand<T>(T command) where T : ICommand;

        void SendCommand<T>() where T : ICommand, new();

        Task SendCommandAsync<T>(T command) where T : ICommandAsync;

        Task SendCommandAsync<T>() where T : ICommandAsync, new();

        Task<TResult> SendCommandAsync<T, TResult>(T command) where T : ICommandAsync<TResult>;

        Task<TResult> SendCommandAsync<T, TResult>() where T : ICommandAsync<TResult>, new();

        Task SendCommandAsync<T>(T command, CancellationTokenSource source) where T : ICommandCancellableAsync;

        Task SendCommandAsync<T>(CancellationTokenSource source) where T : ICommandCancellableAsync, new();

        Task<TResult> SendCommandAsync<T, TResult>(T command, CancellationTokenSource source) where T : ICommandCancellableAsync<TResult>;

        Task<TResult> SendCommandAsync<T, TResult>(CancellationTokenSource source) where T : ICommandCancellableAsync<TResult>, new();

        TResult SendQuery<TResult>(IQuery<TResult> query);

        TResult SendQuery<T, TResult>() where T : IQuery<TResult>, new();

        IUnregisterHandler RegisterEvent<T>(Action<T> action);

        IUnregisterHandler RegisterEvent<T, TResult>(Func<T, TResult> func);

        void UnregisterEvent<T>(Action<T> action);

        void UnregisterEvent<T, TResult>(Func<T, TResult> func);

        void InvokeEvent<T>(T t);

        void InvokeEvent<T>() where T : new();

        TResult InvokeEvent<T, TResult>(T t);

        TResult InvokeEvent<T, TResult>() where T : new();

        void RegisterDependency<TDependency>(object dependency);

        TDependency ResolveDependency<TDependency>();

        void InjectDependency<TDependency>(object obj);
    }
}