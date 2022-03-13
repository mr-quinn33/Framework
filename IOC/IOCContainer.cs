using System;
using System.Collections.Generic;
using System.Linq;
using Framework.IOC.Attribute;
using Framework.IOC.Interface;

namespace Framework.IOC
{
    public class IOCContainer : IIOCContainer
    {
        private readonly Dictionary<Type, Type> registeredDependencies = new();
        private readonly Dictionary<Type, object> registeredInstances = new();
        private readonly HashSet<Type> registeredTypes = new();

        public void Register<T>()
        {
            registeredTypes.Add(typeof(T));
        }

        public void Register<T>(object instance)
        {
            var type = typeof(T);
            if (registeredInstances.ContainsKey(type))
                registeredInstances[type] = instance;
            else
                registeredInstances.Add(type, instance);
        }

        public void Register<TParent, TChild>() where TChild : TParent
        {
            var parentType = typeof(TParent);
            var childType = typeof(TChild);
            if (registeredDependencies.ContainsKey(parentType))
                registeredDependencies[parentType] = childType;
            else
                registeredDependencies.Add(parentType, childType);
        }

        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        public void Inject<T>(object obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties()
                         .Where(p => p.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0))
            {
                var value = Resolve(propertyInfo.PropertyType);
                if (value != null) propertyInfo.SetValue(obj, value);
            }
        }

        public void Clear()
        {
            registeredDependencies.Clear();
            registeredInstances.Clear();
            registeredTypes.Clear();
        }

        private object Resolve(Type type)
        {
            if (registeredInstances.ContainsKey(type)) return registeredInstances[type];
            if (registeredTypes.Contains(type)) return Activator.CreateInstance(type);
            return registeredDependencies.ContainsKey(type)
                ? Activator.CreateInstance(registeredDependencies[type])
                : default;
        }
    }
}