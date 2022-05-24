using System;
using System.Collections.Generic;
using Framework.Interface;

namespace Framework.EventSystem
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

            if (value is ActionRegistration<T> registration) registration.action += action;
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

            if (value is FuncRegistration<T, TResult> registration) registration.func += func;
            return new FuncUnregisterHandler<T, TResult>(this, func);
        }

        void IEventSystem.Unregister<T>(Action<T> action)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is ActionRegistration<T> registration) registration.action -= action;
        }

        void IEventSystem.Unregister<T, TResult>(Func<T, TResult> func)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is FuncRegistration<T, TResult> registration) registration.func -= func;
        }

        void IEventSystem.Invoke<T>(T t)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is ActionRegistration<T> registration) registration.action?.Invoke(t);
        }

        void IEventSystem.Invoke<T>()
        {
            (this as IEventSystem).Invoke(new T());
        }

        TResult IEventSystem.Invoke<T, TResult>(T t)
        {
            if (registrations.TryGetValue(typeof(T), out var value) && value is FuncRegistration<T, TResult> {func: { }} registration) return registration.func.Invoke(t);
            return default;
        }

        TResult IEventSystem.Invoke<T, TResult>()
        {
            return (this as IEventSystem).Invoke<T, TResult>(new T());
        }
    }
}