using System;
using Framework.Interfaces;

namespace Framework.ReactiveProperties.Interfaces
{
    public interface IReactiveProperty<T>
    {
        T Value { get; set; }

        IUnregisterHandler Register(Action<T> action);

        void Unregister(Action<T> action);
    }
}