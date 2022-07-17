using System;
using System.Collections.Generic;
using Framework.EventSystems.Interfaces;
using Framework.Interfaces;

namespace Framework.EventSystems
{
    public class EventSystem : IEventSystem
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
            if (registrations.TryGetValue(typeof(T), out var value) && value is IActionRegistration<T> registration) registration.Action?.Invoke(t);
        }

        void IEventSystem.Invoke<T>()
        {
            (this as IEventSystem).Invoke(new T());
        }

        TResult IEventSystem.Invoke<T, TResult>(T t)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is IFuncRegistration<T, TResult> {Func: { }} registration) return registration.Func.Invoke(t);
            return default;
        }

        TResult IEventSystem.Invoke<T, TResult>()
        {
            return (this as IEventSystem).Invoke<T, TResult>(new T());
        }

        void IEventSystem.Clear()
        {
            registrations.Clear();
        }
    }
}