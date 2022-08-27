using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.EventSystems;
using Framework.EventSystems.Interfaces;
using Framework.GameModes.Interfaces;
using Framework.Interfaces;
using Framework.IOC;
using Framework.IOC.Interfaces;
using Framework.Models.Interfaces;
using Framework.Queries.Interfaces;
using Framework.Systems.Interfaces;
using Framework.Utilities.Interfaces;

namespace Framework.GameModes
{
    public abstract class GameMode<T> : GameModeBase, IGameMode where T : GameMode<T>, new()
    {
        private readonly IEventSystem eventSystem = new EventSystem();
        private readonly IIOCContainer iocContainer = new IOCContainer();
        private readonly ICollection<IModel> models = new List<IModel>();
        private readonly ICollection<ISystem> systems = new List<ISystem>();

        private static bool Initialized => Instances.TryGetValue(typeof(T), out var value) && value != null;

        TSystem IGameMode.GetSystem<TSystem>() where TSystem : class
        {
            return iocContainer.Resolve<TSystem>();
        }

        TModel IGameMode.GetModel<TModel>() where TModel : class
        {
            return iocContainer.Resolve<TModel>();
        }

        TUtility IGameMode.GetUtility<TUtility>() where TUtility : class
        {
            return iocContainer.Resolve<TUtility>();
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

        void IGameMode.InvokeEvent<TEvent>(TEvent t)
        {
            eventSystem.Invoke(t);
        }

        void IGameMode.InvokeEvent<TEvent>()
        {
            eventSystem.Invoke<TEvent>();
        }

        TResult IGameMode.InvokeEvent<TEvent, TResult>(TEvent t)
        {
            return eventSystem.Invoke<TEvent, TResult>(t);
        }

        TResult IGameMode.InvokeEvent<TEvent, TResult>()
        {
            return eventSystem.Invoke<TEvent, TResult>();
        }

        void IGameMode.RegisterDependency<TDependency>(object dependency)
        {
            iocContainer.Register<TDependency>(dependency);
        }

        TDependency IGameMode.ResolveDependency<TDependency>()
        {
            return iocContainer.Resolve<TDependency>();
        }

        void IGameMode.InjectDependency<TDependency>(object obj)
        {
            iocContainer.Inject<TDependency>(obj);
        }

        public static T Load()
        {
            if (!Initialized) InitializeGameMode();
            return Instances[typeof(T)] as T;
        }

        public static void Unload()
        {
            if (!Initialized) return;
            if (!Instances.Remove(typeof(T), out var value)) return;
            if (value is not GameMode<T> gameMode) return;
            gameMode.models.Clear();
            gameMode.systems.Clear();
            gameMode.eventSystem.Clear();
            gameMode.iocContainer.Clear();
        }

        protected void RegisterSystem<TSystem>(TSystem system) where TSystem : class, ISystem
        {
            system.SetGameMode(this);
            iocContainer.Register<TSystem>(system);
            if (Initialized) system.Initialize();
            else systems.Add(system);
        }

        protected void RegisterModel<TModel>(TModel model) where TModel : class, IModel
        {
            model.SetGameMode(this);
            iocContainer.Register<TModel>(model);
            if (Initialized) model.Initialize();
            else models.Add(model);
        }

        protected void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility
        {
            iocContainer.Register<TUtility>(utility);
        }

        private static void InitializeGameMode()
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