using System;
using System.Collections.Generic;
using System.Linq;
using Framework.IOC.Attributes;

namespace Framework.IOC
{
    public interface IIOCContainer
    {
        /// <summary>
        ///     Register a new instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>();

        /// <summary>
        ///     Register an instance
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        void Register<T>(object instance);

        void Register<TParent, TChild>() where TChild : TParent;

        T Resolve<T>();

        void Inject<T>(object obj);

        void Clear();
    }

    public class IOCContainer : IIOCContainer
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
            return registeredDependencies.ContainsKey(type) ? Activator.CreateInstance(registeredDependencies[type]) : null;
        }
    }
}