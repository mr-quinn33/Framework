using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Interface;
using Framework.IOC;
using Framework.IOC.Interface;

namespace Framework.GameMode
{
    public abstract class GameMode<T> : GameModeBase, IArchitecture where T : GameMode<T>, new()
    {
        private readonly IEventSystem eventSystem = new EventSystem.EventSystem();
        private readonly IIOCContainer iocContainer = new IOCContainer();
        private readonly List<IModel> models = new();
        private readonly List<ISystem> systems = new();

        private static bool Initialized => Instances.TryGetValue(typeof(T), out var value) && value != null;

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem
        {
            return iocContainer.Resolve<TSystem>();
        }

        public TModel GetModel<TModel>() where TModel : class, IModel
        {
            return iocContainer.Resolve<TModel>();
        }

        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility
        {
            return iocContainer.Resolve<TUtility>();
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendCommand<TCommand>() where TCommand : ICommand, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public async Task SendCommandAsync<TCommand>(TCommand command) where TCommand : ICommandAsync
        {
            command.SetArchitecture(this);
            await command.ExecuteAsync();
            command.SetArchitecture(null);
        }

        public async Task SendCommandAsync<TCommand>() where TCommand : ICommandAsync, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            await command.ExecuteAsync();
            command.SetArchitecture(null);
        }

        public async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command)
            where TCommand : ICommandAsync<TResult>
        {
            command.SetArchitecture(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public async Task<TResult> SendCommandAsync<TCommand, TResult>() where TCommand : ICommandAsync<TResult>, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public async Task SendCommandAsync<TCommand>(TCommand command, CancellationTokenSource source)
            where TCommand : ICommandAsyncCancellable
        {
            command.SetArchitecture(this);
            await command.ExecuteAsync(source);
            command.SetArchitecture(null);
        }

        public async Task SendCommandAsync<TCommand>(CancellationTokenSource source)
            where TCommand : ICommandAsyncCancellable, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            await command.ExecuteAsync(source);
            command.SetArchitecture(null);
        }

        public async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command, CancellationTokenSource source)
            where TCommand : ICommandAsyncCancellable<TResult>
        {
            command.SetArchitecture(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public async Task<TResult> SendCommandAsync<TCommand, TResult>(CancellationTokenSource source)
            where TCommand : ICommandAsyncCancellable<TResult>, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        public TResult SendQuery<TQuery, TResult>() where TQuery : IQuery<TResult>, new()
        {
            var query = new TQuery();
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        public IUnregisterHandler RegisterEvent<TEvent>(Action<TEvent> action)
        {
            return eventSystem.Register(action);
        }

        public IUnregisterHandler RegisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            return eventSystem.Register(func);
        }

        public void UnregisterEvent<TEvent>(Action<TEvent> action)
        {
            eventSystem.Unregister(action);
        }

        public void UnregisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            eventSystem.Unregister(func);
        }

        public void InvokeEvent<TEvent>(TEvent t)
        {
            eventSystem.Invoke(t);
        }

        public void InvokeEvent<TEvent>() where TEvent : new()
        {
            eventSystem.Invoke<TEvent>();
        }

        public TResult InvokeEvent<TEvent, TResult>(TEvent t)
        {
            return eventSystem.Invoke<TEvent, TResult>(t);
        }

        public TResult InvokeEvent<TEvent, TResult>() where TEvent : new()
        {
            return eventSystem.Invoke<TEvent, TResult>();
        }

        public static T Load()
        {
            if (!Initialized) MakeSureArchitecture();
            return Instances[typeof(T)] as T;
        }

        public static void Unload()
        {
            if (!Initialized) return;
            if (!Instances.Remove(typeof(T))) Instances.Clear();
        }

        protected void RegisterSystem<TSystem>(TSystem system) where TSystem : class, ISystem
        {
            system.SetArchitecture(this);
            iocContainer.Register<TSystem>(system);
            if (Initialized) system.Initialize();
            else systems.Add(system);
        }

        protected void RegisterModel<TModel>(TModel model) where TModel : class, IModel
        {
            model.SetArchitecture(this);
            iocContainer.Register<TModel>(model);
            if (Initialized) model.Initialize();
            else models.Add(model);
        }

        protected void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility
        {
            iocContainer.Register<TUtility>(utility);
        }

        private static void MakeSureArchitecture()
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