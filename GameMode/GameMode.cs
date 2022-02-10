using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Interface;
using Framework.IOC;

namespace Framework.GameMode
{
    public abstract class GameMode<T> : GameModeBase where T : GameMode<T>, new()
    {
        private readonly EventSystem.EventSystem eventSystem = new();
        private readonly IOCContainer iocContainer = new();
        private readonly List<IModel> models = new();
        private readonly List<ISystem> systems = new();

        private static bool Initialized => Instances.TryGetValue(typeof(T), out var value) && value != null;

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
            iocContainer.Set(system);
            if (Initialized) system.Initialize();
            else systems.Add(system);
        }

        protected void RegisterModel<TModel>(TModel model) where TModel : class, IModel
        {
            model.SetArchitecture(this);
            iocContainer.Set(model);
            if (Initialized) model.Initialize();
            else models.Add(model);
        }

        protected void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility
        {
            iocContainer.Set(utility);
        }

        public override TSystem GetSystem<TSystem>()
        {
            return iocContainer.Get<TSystem>();
        }

        public override TModel GetModel<TModel>()
        {
            return iocContainer.Get<TModel>();
        }

        public override TUtility GetUtility<TUtility>()
        {
            return iocContainer.Get<TUtility>();
        }

        public override void SendCommand<TCommand>(TCommand command)
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public override void SendCommand<TCommand>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public override async Task SendCommandAsync<TCommand>(TCommand command)
        {
            command.SetArchitecture(this);
            await command.ExecuteAsync();
            command.SetArchitecture(null);
        }

        public override async Task SendCommandAsync<TCommand>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            await command.ExecuteAsync();
            command.SetArchitecture(null);
        }

        public override async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command)
        {
            command.SetArchitecture(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public override async Task<TResult> SendCommandAsync<TCommand, TResult>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public override async Task SendCommandAsync<TCommand>(TCommand command, CancellationTokenSource source)
        {
            command.SetArchitecture(this);
            await command.ExecuteAsync(source);
            command.SetArchitecture(null);
        }

        public override async Task SendCommandAsync<TCommand>(CancellationTokenSource source)
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            await command.ExecuteAsync(source);
            command.SetArchitecture(null);
        }

        public override async Task<TResult> SendCommandAsync<TCommand, TResult>(TCommand command,
            CancellationTokenSource source)
        {
            command.SetArchitecture(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public override async Task<TResult> SendCommandAsync<TCommand, TResult>(CancellationTokenSource source)
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        public override TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        public override TResult SendQuery<TQuery, TResult>()
        {
            var query = new TQuery();
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        public override IUnregisterHandler RegisterEvent<TEvent>(Action<TEvent> action)
        {
            return eventSystem.Register(action);
        }

        public override IUnregisterHandler RegisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            return eventSystem.Register(func);
        }

        public override void UnregisterEvent<TEvent>(Action<TEvent> action)
        {
            eventSystem.Unregister(action);
        }

        public override void UnregisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            eventSystem.Unregister(func);
        }

        public override void InvokeEvent<TEvent>(TEvent t)
        {
            eventSystem.Invoke(t);
        }

        public override void InvokeEvent<TEvent>()
        {
            eventSystem.Invoke<TEvent>();
        }

        public override TResult InvokeEvent<TEvent, TResult>(TEvent t)
        {
            return eventSystem.Invoke<TEvent, TResult>(t);
        }

        public override TResult InvokeEvent<TEvent, TResult>()
        {
            return eventSystem.Invoke<TEvent, TResult>();
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