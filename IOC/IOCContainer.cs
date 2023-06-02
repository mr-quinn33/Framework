using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Framework.IOC.Attributes;

namespace Framework.IOC
{
    public interface IIOCContainer
    {
        void Register<T>();

        void Register<T>(object instance);

        void Register<TParent, TChild>() where TChild : TParent;

        T Resolve<T>();

        void Inject<T>(object obj);

        void Clear();
    }

    public sealed class IOCContainer : IIOCContainer
    {
        private readonly IDictionary<Type, Type> registeredDependencies = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, object> registeredInstances = new Dictionary<Type, object>();
        private readonly ICollection<Type> registeredTypes = new HashSet<Type>();

        void IIOCContainer.Register<T>()
        {
            registeredTypes.Add(typeof(T));
        }

        void IIOCContainer.Register<T>(object instance)
        {
            registeredInstances[typeof(T)] = instance;
        }

        void IIOCContainer.Register<TParent, TChild>()
        {
            registeredDependencies[typeof(TParent)] = typeof(TChild);
        }

        T IIOCContainer.Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }

        void IIOCContainer.Inject<T>(object obj)
        {
            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(InjectPropertyAttribute), true).Length > 0))
            {
                object value = Resolve(propertyInfo.PropertyType);
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
            if (registeredInstances.TryGetValue(type, out object resolve)) return resolve;
            if (registeredTypes.Contains(type)) return Activator.CreateInstance(type);
            return registeredDependencies.TryGetValue(type, out Type dependency) ? Activator.CreateInstance(dependency) : null;
        }
    }
}