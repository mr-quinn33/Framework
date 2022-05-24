using System;
using System.Collections.Generic;
using System.Linq;
using Framework.IOC.Attribute;
using Framework.IOC.Interface;

namespace Framework.IOC
{
    public class IOCContainer : IIOCContainer
    {
        private readonly ICollection<Type> registeredTypes = new HashSet<Type>();
        private readonly IDictionary<Type, Type> registeredDependencies = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, object> registeredInstances = new Dictionary<Type, object>();

        void IIOCContainer.Register<T>()
        {
            registeredTypes.Add(typeof(T));
        }

        void IIOCContainer.Register<T>(object instance)
        {
            var type = typeof(T);
            if (registeredInstances.ContainsKey(type)) registeredInstances[type] = instance;
            else registeredInstances.Add(type, instance);
        }

        void IIOCContainer.Register<TParent, TChild>()
        {
            var parentType = typeof(TParent);
            var childType = typeof(TChild);
            if (registeredDependencies.ContainsKey(parentType)) registeredDependencies[parentType] = childType;
            else registeredDependencies.Add(parentType, childType);
        }

        T IIOCContainer.Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        void IIOCContainer.Inject<T>(object obj)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0))
            {
                var value = Resolve(propertyInfo.PropertyType);
                if (value != null) propertyInfo.SetValue(obj, value);
            }
        }

        void IIOCContainer.Clear()
        {
            registeredDependencies.Clear();
            registeredInstances.Clear();
            registeredTypes.Clear();
        }

        private object Resolve(Type type)
        {
            if (registeredInstances.ContainsKey(type)) return registeredInstances[type];
            if (registeredTypes.Contains(type)) return Activator.CreateInstance(type);
            return registeredDependencies.ContainsKey(type) ? Activator.CreateInstance(registeredDependencies[type]) : default;
        }
    }
}