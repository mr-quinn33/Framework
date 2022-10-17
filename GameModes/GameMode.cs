using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Commands;
using Framework.EventSystems;
using Framework.Interfaces;
using Framework.IOC;
using Framework.Models;
using Framework.Queries;
using Framework.Systems;
using Framework.Utilities;

namespace Framework.GameModes
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

        void SendEvent<T>(T t);

        void SendEvent<T>() where T : new();

        TResult SendEvent<T, TResult>(T t);

        TResult SendEvent<T, TResult>() where T : new();

        void RegisterDependency<TDependency>(object dependency);

        TDependency ResolveDependency<TDependency>();

        void InjectDependency<TDependency>(object obj);
    }

    public abstract class GameMode<T> : GameModeBase, IGameMode where T : GameMode<T>, new()
    {
        private readonly IIOCContainer coreContainer = new IOCContainer();
        private readonly IIOCContainer dependencyContainer = new IOCContainer();
        private readonly IEventSystem eventSystem = new EventSystem();
        private readonly ICollection<IModel> models = new List<IModel>();
        private readonly ICollection<ISystem> systems = new List<ISystem>();

        private static bool Initialized => Instances.TryGetValue(typeof(T), out var value) && value != null;

        TSystem IGameMode.GetSystem<TSystem>() where TSystem : class
        {
            return coreContainer.Resolve<TSystem>();
        }

        TModel IGameMode.GetModel<TModel>() where TModel : class
        {
            return coreContainer.Resolve<TModel>();
        }

        TUtility IGameMode.GetUtility<TUtility>() where TUtility : class
        {
            return coreContainer.Resolve<TUtility>();
        }

        void IGameMode.SendCommand<TCommand>(TCommand command)
        {
            command.SetGameMode(this);
            command.Execute();
            command.SetGameMode(null);
        }

        void IGameMode.SendCommand<TCommand>()
        {
            var command = new TCommand();
            command.SetGameMode(this);
            command.Execute();
            command.SetGameMode(null);
        }

        async Task IGameMode.SendCommandAsync<TCommand>(TCommand command)
        {
            command.SetGameMode(this);
            await command.ExecuteAsync();
            command.SetGameMode(null);
        }

        async Task IGameMode.SendCommandAsync<TCommand>()
        {
            var command = new TCommand();
            command.SetGameMode(this);
            await command.ExecuteAsync();
            command.SetGameMode(null);
        }

        async Task<TResult> IGameMode.SendCommandAsync<TCommand, TResult>(TCommand command)
        {
            command.SetGameMode(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetGameMode(null);
            return task.Result;
        }

        async Task<TResult> IGameMode.SendCommandAsync<TCommand, TResult>()
        {
            var command = new TCommand();
            command.SetGameMode(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetGameMode(null);
            return task.Result;
        }

        async Task IGameMode.SendCommandAsync<TCommand>(TCommand command, CancellationTokenSource source)
        {
            command.SetGameMode(this);
            await command.ExecuteAsync(source);
            command.SetGameMode(null);
        }

        async Task IGameMode.SendCommandAsync<TCommand>(CancellationTokenSource source)
        {
            var command = new TCommand();
            command.SetGameMode(this);
            await command.ExecuteAsync(source);
            command.SetGameMode(null);
        }

        async Task<TResult> IGameMode.SendCommandAsync<TCommand, TResult>(TCommand command, CancellationTokenSource source)
        {
            command.SetGameMode(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetGameMode(null);
            return task.Result;
        }

        async Task<TResult> IGameMode.SendCommandAsync<TCommand, TResult>(CancellationTokenSource source)
        {
            var command = new TCommand();
            command.SetGameMode(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetGameMode(null);
            return task.Result;
        }

        TResult IGameMode.SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetGameMode(this);
            var result = query.Execute();
            query.SetGameMode(null);
            return result;
        }

        TResult IGameMode.SendQuery<TQuery, TResult>()
        {
            var query = new TQuery();
            query.SetGameMode(this);
            var result = query.Execute();
            query.SetGameMode(null);
            return result;
        }

        IUnregisterHandler IGameMode.RegisterEvent<TEvent>(Action<TEvent> action)
        {
            return eventSystem.Register(action);
        }

        IUnregisterHandler IGameMode.RegisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            return eventSystem.Register(func);
        }

        void IGameMode.UnregisterEvent<TEvent>(Action<TEvent> action)
        {
            eventSystem.Unregister(action);
        }

        void IGameMode.UnregisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            eventSystem.Unregister(func);
        }

        void IGameMode.SendEvent<TEvent>(TEvent t)
        {
            eventSystem.Invoke(t);
        }

        void IGameMode.SendEvent<TEvent>()
        {
            eventSystem.Invoke<TEvent>();
        }

        TResult IGameMode.SendEvent<TEvent, TResult>(TEvent t)
        {
            return eventSystem.Invoke<TEvent, TResult>(t);
        }

        TResult IGameMode.SendEvent<TEvent, TResult>()
        {
            return eventSystem.Invoke<TEvent, TResult>();
        }

        void IGameMode.RegisterDependency<TDependency>(object dependency)
        {
            dependencyContainer.Register<TDependency>(dependency);
        }

        TDependency IGameMode.ResolveDependency<TDependency>()
        {
            return dependencyContainer.Resolve<TDependency>();
        }

        void IGameMode.InjectDependency<TDependency>(object obj)
        {
            dependencyContainer.Inject<TDependency>(obj);
        }

        public static T Load()
        {
            if (!Initialized) CreateInstance();
            return (T) Instances[typeof(T)];
        }

        public static void Unload()
        {
            if (!Initialized) return;
            if (!Instances.Remove(typeof(T), out var value)) return;
            if (value is not GameMode<T> gameMode) return;
            gameMode.eventSystem.Clear();
            gameMode.coreContainer.Clear();
            gameMode.dependencyContainer.Clear();
        }

        protected void RegisterSystem<TSystem>(TSystem system) where TSystem : class, ISystem
        {
            system.SetGameMode(this);
            coreContainer.Register<TSystem>(system);
            if (Initialized) system.Initialize();
            else systems.Add(system);
        }

        protected void RegisterModel<TModel>(TModel model) where TModel : class, IModel
        {
            model.SetGameMode(this);
            coreContainer.Register<TModel>(model);
            if (Initialized) model.Initialize();
            else models.Add(model);
        }

        protected void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility
        {
            coreContainer.Register<TUtility>(utility);
        }

        private static void CreateInstance()
        {
            if (Initialized) return;
            var instance = new T();
            instance.Initialize();
            foreach (var model in instance.models) model.Initialize();
            foreach (var system in instance.systems) system.Initialize();
            instance.models.Clear();
            instance.systems.Clear();
            Instances.Add(typeof(T), instance);
        }

        protected abstract void Initialize();
    }
}