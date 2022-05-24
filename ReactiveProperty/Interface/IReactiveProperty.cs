using System;
using Framework.Interface;

namespace Framework.ReactiveProperty.Interface
{
    public interface IReactiveProperty<T>
    {
        T Value { get; set; }

        IUnregisterHandler Register(Action<T> action);

        void Unregister(Action<T> action);
    }
}