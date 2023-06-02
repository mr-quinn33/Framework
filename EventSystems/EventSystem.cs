using System;
using System.Collections.Generic;
using Framework.Interfaces;

namespace Framework.EventSystems
{
    internal interface IEventSystem
    {
        IUnregisterHandler Register<T>(Action<T> action);

        void Unregister<T>(Action<T> action);

        void Invoke<T>(T t);

        void Invoke<T>() where T : new();

        void Clear();
    }

    internal sealed class EventSystem : IEventSystem
    {
        private readonly IDictionary<Type, IRegistration> registrations = new Dictionary<Type, IRegistration>();

        IUnregisterHandler IEventSystem.Register<T>(Action<T> action)
        {
            Type type = typeof(T);
            if (!registrations.TryGetValue(type, out var value))
            {
                value = new Registration<T>();
                registrations.Add(type, value);
            }

            if (value is IRegistration<T> registration) registration.Action += action;
            return new ActionUnregisterHandler<T>(this, action);
        }

        void IEventSystem.Unregister<T>(Action<T> action)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IRegistration<T> registration) registration.Action -= action;
        }

        void IEventSystem.Invoke<T>(T t)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IRegistration<T> {Action: not null} registration) registration.Action.Invoke(t);
        }

        void IEventSystem.Invoke<T>()
        {
            ((IEventSystem) this).Invoke(new T());
        }

        void IEventSystem.Clear()
        {
            registrations.Clear();
        }

        private sealed class ActionUnregisterHandler<T> : IUnregisterHandler
        {
            private Action<T> action;
            private IEventSystem eventSystem;

            public ActionUnregisterHandler(IEventSystem eventSystem, Action<T> action)
            {
                this.eventSystem = eventSystem;
                this.action = action;
            }

            void IUnregisterHandler.Unregister()
            {
                eventSystem.Unregister(action);
                eventSystem = null;
                action = null;
            }
        }
    }
}