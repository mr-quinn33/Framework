using System;
using System.Collections.Generic;

namespace Framework.IOC
{
    public class IOCContainer
    {
        private readonly Dictionary<Type, object> _instances = new();

        public void Set<T>(T instance) where T : class
        {
            var type = typeof(T);
            if (_instances.ContainsKey(type)) _instances[type] = instance;
            else _instances.Add(type, instance);
        }

        public T Get<T>() where T : class
        {
            if (_instances.TryGetValue(typeof(T), out var value)) return value as T;
            return null;
        }
    }
}