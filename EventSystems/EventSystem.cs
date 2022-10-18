using System;
using System.Collections.Generic;
using Framework.Interfaces;

namespace Framework.EventSystems
{
    internal interface IEventSystem
    {
        IUnregisterHandler Register<T>(Action<T> action);

        IUnregisterHandler Register<T, TResult>(Func<T, TResult> func);

        void Unregister<T>(Action<T> action);

        void Unregister<T, TResult>(Func<T, TResult> func);

        void Invoke<T>(T t);

        void Invoke<T>() where T : new();

        TResult Invoke<T, TResult>(T t);

        TResult Invoke<T, TResult>() where T : new();

        void Clear();
    }

    internal class EventSystem : IEventSystem
    {
        private readonly IDictionary<Type, IRegistration> registrations = new Dictionary<Type, IRegistration>();

        IUnregisterHandler IEventSystem.Register<T>(Action<T> action)
        {
            var type = typeof(T);
            if (!registrations.TryGetValue(type, out var value))
            {
                value = new ActionRegistration<T>();
                registrations.Add(type, value);
            }

            if (value is IActionRegistration<T> registration) registration.Action += action;
            return new ActionUnregisterHandler<T>(this, action);
        }

        IUnregisterHandler IEventSystem.Register<T, TResult>(Func<T, TResult> func)
        {
            var type = typeof(T);
            if (!registrations.TryGetValue(type, out var value))
            {
                value = new FuncRegistration<T, TResult>();
                registrations.Add(type, value);
            }

            if (value is IFuncRegistration<T, TResult> registration) registration.Func += func;
            return new FuncUnregisterHandler<T, TResult>(this, func);
        }

        void IEventSystem.Unregister<T>(Action<T> action)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IActionRegistration<T> registration) registration.Action -= action;
        }

        void IEventSystem.Unregister<T, TResult>(Func<T, TResult> func)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IFuncRegistration<T, TResult> registration) registration.Func -= func;
        }

        void IEventSystem.Invoke<T>(T t)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IActionRegistration<T> {Action: { }} registration) registration.Action.Invoke(t);
        }

        void IEventSystem.Invoke<T>()
        {
            ((IEventSystem) this).Invoke(new T());
        }

        TResult IEventSystem.Invoke<T, TResult>(T t)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IFuncRegistration<T, TResult> {Func: { }} registration) return registration.Func.Invoke(t);
            return default;
        }

        TResult IEventSystem.Invoke<T, TResult>()
        {
            return ((IEventSystem) this).Invoke<T, TResult>(new T());
        }

        void IEventSystem.Clear()
        {
            registrations.Clear();
        }

        private class ActionUnregisterHandler<T> : IUnregisterHandler
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

        private class FuncUnregisterHandler<T, TResult> : IUnregisterHandler
        {
            private IEventSystem eventSystem;
            private Func<T, TResult> func;

            public FuncUnregisterHandler(IEventSystem eventSystem, Func<T, TResult> func)
            {
                this.eventSystem = eventSystem;
                this.func = func;
            }

            void IUnregisterHandler.Unregister()
            {
                eventSystem.Unregister(func);
                eventSystem = null;
                func = null;
            }
        }
    }
}