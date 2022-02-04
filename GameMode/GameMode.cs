using System;
using System.Collections.Generic;
using Framework.EventSystems;
using Framework.Interface;
using Framework.IOC;

namespace Framework.GameMode
{
    public abstract class GameMode<T> : GameModeBase where T : GameMode<T>, new()
    {
        private readonly EventSystem _eventSystem = new();
        private readonly IOCContainer _iocContainer = new();
        private readonly List<IModel> _models = new();
        private readonly List<ISystem> _systems = new();

        private static bool Initialized => Instances.TryGetValue(typeof(T), out var value) && value != null;

        public static IArchitecture Load()
        {
            if (!Initialized) MakeSureArchitecture();

            return Instances[typeof(T)];
        }

        public static void Unload()
        {
            if (!Initialized) return;

            if (!Instances.Remove(typeof(T))) Instances.Clear();
        }

        public override void RegisterSystem<TSystem>(TSystem system)
        {
            system.SetArchitecture(this);
            _iocContainer.Set(system);

            if (Initialized) system.Initialize();
            else _systems.Add(system);
        }

        public override void RegisterModel<TModel>(TModel model)
        {
            model.SetArchitecture(this);
            _iocContainer.Set(model);

            if (Initialized) model.Initialize();
            else _models.Add(model);
        }

        public override void RegisterUtility<TUtility>(TUtility utility)
        {
            _iocContainer.Set(utility);
        }

        public override TSystem GetSystem<TSystem>()
        {
            return _iocContainer.Get<TSystem>();
        }

        public override TModel GetModel<TModel>()
        {
            return _iocContainer.Get<TModel>();
        }

        public override TUtility GetUtility<TUtility>()
        {
            return _iocContainer.Get<TUtility>();
        }

        public override void SendCommand<TCommand>()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public override void SendCommand<TCommand>(TCommand command)
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public override TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            var result = query.Execute();
            query.SetArchitecture(null);
            return result;
        }

        public override IUnregisterHandler RegisterEvent<TEvent>(Action<TEvent> action)
        {
            return _eventSystem.Register(action);
        }

        public override IUnregisterHandler[] RegisterEvent<TEvent>(params Action<TEvent>[] actions)
        {
            return _eventSystem.Register(actions);
        }

        public override IUnregisterHandler RegisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            return _eventSystem.Register(func);
        }

        public override IUnregisterHandler[] RegisterEvent<T1, TResult>(params Func<T1, TResult>[] functions)
        {
            return _eventSystem.Register(functions);
        }

        public override void UnregisterEvent<TEvent>(Action<TEvent> action)
        {
            _eventSystem.Unregister(action);
        }

        public override void UnregisterEvent<T1>(params Action<T1>[] actions)
        {
            _eventSystem.Unregister(actions);
        }

        public override void UnregisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            _eventSystem.Unregister(func);
        }

        public override void UnregisterEvent<T1, TResult>(params Func<T1, TResult>[] functions)
        {
            _eventSystem.Unregister(functions);
        }

        public override void InvokeEvent<TEvent>()
        {
            _eventSystem.Invoke<TEvent>();
        }

        public override void InvokeEvent<TEvent, TResult>(out TResult result)
        {
            _eventSystem.Invoke<TEvent, TResult>(out result);
        }

        public override TResult InvokeEvent<TEvent, TResult>()
        {
            return _eventSystem.Invoke<TEvent, TResult>();
        }

        public override void InvokeEvent<TEvent>(TEvent t)
        {
            _eventSystem.Invoke(t);
        }

        public override void InvokeEvent<TEvent, TResult>(TEvent t, out TResult result)
        {
            _eventSystem.Invoke(t, out result);
        }

        public override TResult InvokeEvent<TEvent, TResult>(TEvent t)
        {
            return _eventSystem.Invoke<TEvent, TResult>(t);
        }
        
        private static void MakeSureArchitecture()
        {
            if (Initialized) return;
            
            var instance = new T();
            instance.Initialize();
            
            foreach (var model in instance._models) model.Initialize();

            foreach (var system in instance._systems) system.Initialize();

            instance._models.Clear();
            instance._systems.Clear();
            Instances.Add(typeof(T), instance);
        }

        private protected abstract void Initialize();
    }
}