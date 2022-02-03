using System;
using System.Collections.Generic;
using Framework.EventSystems;
using Framework.Interface;
using Framework.IOC;

namespace Framework.Architecture
{
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, IGameMode, new()
    {
        private readonly EventSystem _eventSystem = new();
        private readonly IOCContainer _iocContainer = new();
        private readonly List<IModel> _models = new();
        private readonly List<ISystem> _systems = new();
        private T _instance;

        public IArchitecture Instance
        {
            get
            {
                if (!Initialized) MakeSureArchitecture();

                return _instance;
            }
        }

        private bool Initialized => _instance != null;

        public void RegisterSystem<TSystem>(TSystem system) where TSystem : class, ISystem
        {
            system.SetArchitecture(this);
            _iocContainer.Set(system);

            if (Initialized) system.Initialization();
            else _systems.Add(system);
        }

        public void RegisterModel<TModel>(TModel model) where TModel : class, IModel
        {
            model.SetArchitecture(this);
            _iocContainer.Set(model);

            if (Initialized) model.Initialization();
            else _models.Add(model);
        }

        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : class, IUtility
        {
            _iocContainer.Set(utility);
        }

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem
        {
            return _iocContainer.Get<TSystem>();
        }

        public TModel GetModel<TModel>() where TModel : class, IModel
        {
            return _iocContainer.Get<TModel>();
        }

        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility
        {
            return _iocContainer.Get<TUtility>();
        }

        public void SendCommand<TCommand>() where TCommand : ICommand, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }

        public IUnregisterHandler RegisterEvent<TEvent>(Action<TEvent> action)
        {
            return _eventSystem.Register(action);
        }

        public IUnregisterHandler RegisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            return _eventSystem.Register(func);
        }

        public void UnregisterEvent<TEvent>(Action<TEvent> action)
        {
            _eventSystem.Unregister(action);
        }

        public void UnregisterEvent<TEvent, TResult>(Func<TEvent, TResult> func)
        {
            _eventSystem.Unregister(func);
        }

        public void InvokeEvent<TEvent>() where TEvent : new()
        {
            _eventSystem.Invoke<TEvent>();
        }

        public void InvokeEvent<TEvent, TResult>(out TResult result) where TEvent : new()
        {
            _eventSystem.Invoke<TEvent, TResult>(out result);
        }

        public TResult InvokeEvent<TEvent, TResult>() where TEvent : new()
        {
            return _eventSystem.Invoke<TEvent, TResult>();
        }

        public void InvokeEvent<TEvent>(TEvent t)
        {
            _eventSystem.Invoke(t);
        }

        public void InvokeEvent<TEvent, TResult>(TEvent t, out TResult result)
        {
            _eventSystem.Invoke(t, out result);
        }

        public TResult InvokeEvent<TEvent, TResult>(TEvent t)
        {
            return _eventSystem.Invoke<TEvent, TResult>(t);
        }

        public event Action<T> OnInitializationCompleted;

        private void MakeSureArchitecture()
        {
            if (Initialized) return;

            _instance = new T();
            _instance.Initialization();
            OnInitializationCompleted?.Invoke(_instance);

            foreach (var model in _instance._models) model.Initialization();

            foreach (var system in _instance._systems) system.Initialization();

            _instance._models.Clear();
            _instance._systems.Clear();
        }

        private protected abstract void Initialization();
    }
}