using System;
using System.Collections.Generic;

namespace Framework.IOC
{
    public class IOCContainer
    {
        private readonly Dictionary<Type, object> instances = new();

        public void Set<T>(T instance) where T : class
        {
            var type = typeof(T);
            if (instances.ContainsKey(type)) instances[type] = instance;
            else instances.Add(type, instance);
        }

        public T Get<T>() where T : class
        {
            if (instances.TryGetValue(typeof(T), out var value)) return value as T;
            return null;
        }
    }
}