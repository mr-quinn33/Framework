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
        private readonly IIOCContainer iocContainer = new IOCContainer();
        private readonly ICollection<IModel> models = new List<IModel>();
        private readonly ICollection<ISystem> systems = new List<ISystem>();
        private readonly IEventSystem eventSystem = new EventSystem.EventSystem();

        private static bool Initialized => Instances.TryGetValue(typeof(T), out var value) && value != null;

        TSystem IArchitecture.GetSystem<TSystem>() where TSystem : class
        {
            return iocContainer.Resolve<TSystem>();
        }

        TModel IArchitecture.GetModel<TModel>() where TModel : class
        {
            return iocContainer.Resolve<TModel>();
        }

        TUtility IArchitecture.GetUtility<TUtility>() where TUtility : class
        {
            return iocContainer.Resolve<TUtility>();
        }

        void IArchitecture.SendCommand<TCommand>(TCommand command)
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        void IArchitecture.SendCommand<TCommand>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        async Task IArchitecture.SendCommandAsync<TCommand>(TCommand command)
        {
            command.SetArchitecture(this);
            await command.ExecuteAsync();
            command.SetArchitecture(null);
        }

        async Task IArchitecture.SendCommandAsync<TCommand>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            await command.ExecuteAsync();
            command.SetArchitecture(null);
        }

        async Task<TResult> IArchitecture.SendCommandAsync<TCommand, TResult>(TCommand command)
        {
            command.SetArchitecture(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        async Task<TResult> IArchitecture.SendCommandAsync<TCommand, TResult>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            var task = command.ExecuteAsync();
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        async Task IArchitecture.SendCommandAsync<TCommand>(TCommand command, CancellationTokenSource source)
        {
            command.SetArchitecture(this);
            await command.ExecuteAsync(source);
            command.SetArchitecture(null);
        }

        async Task IArchitecture.SendCommandAsync<TCommand>(CancellationTokenSource source)
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            await command.ExecuteAsync(source);
            command.SetArchitecture(null);
        }

        async Task<TResult> IArchitecture.SendCommandAsync<TCommand, TResult>(TCommand command, CancellationTokenSource source)
        {
            command.SetArchitecture(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        async Task<TResult> IArchitecture.SendCommandAsync<TCommand, TResult>(CancellationTokenSource source)
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            var task = command.ExecuteAsync(source);
            await task;
            command.SetArchitecture(null);
            return task.Result;
        }

        TResult IArchitecture.SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        TResult IArchitecture.SendQuery<TQuery, TResult>()
        {
            var query = new TQuery();
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        IUnregisterHandler IArchitecture.RegisterEvent<TEvent>(Action<TEvent> action)
        {
            return eventSystem.Register(action);
        }

        IUnregisterHandler IArchitecture.RegisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            return eventSystem.Register(func);
        }

        void IArchitecture.UnregisterEvent<TEvent>(Action<TEvent> action)
        {
            eventSystem.Unregister(action);
        }

        void IArchitecture.UnregisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            eventSystem.Unregister(func);
        }

        void IArchitecture.InvokeEvent<TEvent>(TEvent t)
        {
            eventSystem.Invoke(t);
        }

        void IArchitecture.InvokeEvent<TEvent>()
        {
            eventSystem.Invoke<TEvent>();
        }

        TResult IArchitecture.InvokeEvent<TEvent, TResult>(TEvent t)
        {
            return eventSystem.Invoke<TEvent, TResult>(t);
        }

        TResult IArchitecture.InvokeEvent<TEvent, TResult>()
        {
            return eventSystem.Invoke<TEvent, TResult>();
        }

        void IArchitecture.RegisterDependency<TDependency>(object dependency)
        {
            iocContainer.Register<TDependency>(dependency);
        }

        TDependency IArchitecture.ResolveDependency<TDependency>()
        {
            return iocContainer.Resolve<TDependency>();
        }

        void IArchitecture.InjectDependency<TDependency>(object obj)
        {
            iocContainer.Inject<TDependency>(obj);
        }

        void IArchitecture.SendCommandObject<TEventObject>(TEventObject eventObject)
        {
            eventObject.SetArchitecture(this);
            eventObject.Execute();
            eventObject.SetArchitecture(null);
        }

        bool IArchitecture.CheckCondition<TCondition>(TCondition condition)
        {
            condition.SetArchitecture(this);
            var isValid = condition.IsValid;
            condition.SetArchitecture(null);
            return isValid;
        }

        public static T Load()
        {
            if (!Initialized) MakeSureArchitecture();
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