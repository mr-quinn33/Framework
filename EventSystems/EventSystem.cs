using System;
using System.Collections.Generic;
using Framework.Interface;

namespace Framework.EventSystems
{
    public class EventSystem : IEventSystem
    {
        private readonly Dictionary<Type, IRegistration> _registrations = new();

        public IUnregisterHandler Register<T>(Action<T> action)
        {
            var type = typeof(T);

            if (!_registrations.TryGetValue(type, out var registration))
            {
                registration = new ActionRegistration<T>();
                _registrations.Add(type, registration);
            }

            if (registration is ActionRegistration<T> reg) reg.action += action;

            return new ActionUnregisterHandler<T>(this, action);
        }

        public IUnregisterHandler Register<T, TResult>(Func<T, TResult> func)
        {
            var type = typeof(T);

            if (!_registrations.TryGetValue(type, out var registration))
            {
                registration = new FuncRegistration<T, TResult>();
                _registrations.Add(type, registration);
            }

            if (registration is FuncRegistration<T, TResult> reg) reg.func += func;

            return new FuncUnregisterHandler<T, TResult>(this, func);
        }

        public void Unregister<T>(Action<T> action)
        {
            if (_registrations.TryGetValue(typeof(T), out var registration) && registration is ActionRegistration<T> reg) reg.action -= action;
        }

        public void Unregister<T, TResult>(Func<T, TResult> func)
        {
            if (_registrations.TryGetValue(typeof(T), out var registration) && registration is FuncRegistration<T, TResult> reg) reg.func -= func;
        }

        public void Invoke<T>() where T : new()
        {
            Invoke(new T());
        }

        public void Invoke<T, TResult>(out TResult result) where T : new()
        {
            Invoke(new T(), out result);
        }

        public TResult Invoke<T, TResult>() where T : new()
        {
            return Invoke<T, TResult>(new T());
        }

        public void Invoke<T>(T t)
        {
            if (_registrations.TryGetValue(typeof(T), out var registration) && registration is ActionRegistration<T> reg) reg.action?.Invoke(t);
        }

        public void Invoke<T, TResult>(T t, out TResult result)
        {
            if (_registrations.TryGetValue(typeof(T), out var registration) && registration is FuncRegistration<T, TResult> {func: { }} reg) result = reg.func.Invoke(t);

            result = default;
        }

        public TResult Invoke<T, TResult>(T t)
        {
            if (_registrations.TryGetValue(typeof(T), out var registration) && registration is FuncRegistration<T, TResult> {func: { }} reg) return reg.func.Invoke(t);

            return default;
        }
    }
}