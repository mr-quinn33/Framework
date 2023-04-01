using System;
using Framework.Interfaces;

namespace Framework.Tools.Data
{
    public interface IAbstractData<T> : IReadOnlyAbstractData<T> where T : struct, IEquatable<T>, IComparable, IComparable<T>
    {
        void IncreaseValue(T value);

        void DecreaseValue(T value);

        void SetValue(T value);

        void IncreaseMaxValue(T value);

        void DecreaseMaxValue(T value);

        void SetMaxValue(T value);

        IAbstractData<T> AddValue(T value);

        IAbstractData<T> SubtractValue(T value);

        IAbstractData<T> AddMaxValue(T value);

        IAbstractData<T> SubtractMaxValue(T value);

        IAbstractData<T> Add(IAbstractData<T> data);

        IAbstractData<T> Subtract(IAbstractData<T> data);

        IUnregisterHandler RegisterOnValueChanged(Action<IReadOnlyAbstractData<T>> action);

        void UnregisterOnValueChanged(Action<IReadOnlyAbstractData<T>> action);

        IUnregisterHandler RegisterOnMaxValueChanged(Action<IReadOnlyAbstractData<T>> action);

        void UnregisterOnMaxValueChanged(Action<IReadOnlyAbstractData<T>> action);

        IUnregisterHandler RegisterOnValueEqualsMaxValue(Action<IReadOnlyAbstractData<T>> action);

        void UnregisterOnValueEqualsMaxValue(Action<IReadOnlyAbstractData<T>> action);

        IUnregisterHandler RegisterOnValueEqualsDefaultValue(Action<IReadOnlyAbstractData<T>> action);

        void UnregisterOnValueEqualsDefaultValue(Action<IReadOnlyAbstractData<T>> action);
    }
}