using System;
using Framework.Interfaces;

namespace Framework.Tools.Data
{
    [Serializable]
    public abstract class AbstractData<T> : IAbstractData<T> where T : struct, IEquatable<T>, IComparable, IComparable<T>
    {
#if UNITY_EDITOR
        [UnityEngine.SerializeField]
#endif
        private T value;
#if UNITY_EDITOR
        [UnityEngine.SerializeField]
#endif
        private T maxValue;

        private event Action<IReadOnlyAbstractData<T>> OnValueChanged;
        private event Action<IReadOnlyAbstractData<T>> OnMaxValueChanged;
        private event Action<IReadOnlyAbstractData<T>> OnValueEqualsMaxValue;
        private event Action<IReadOnlyAbstractData<T>> OnValueEqualsDefaultValue;

        protected AbstractData(T value, T maxValue)
        {
            this.value = value;
            this.maxValue = maxValue;
        }

        protected AbstractData(IReadOnlyAbstractData<T> other) : this(other.Value, other.MaxValue)
        {
        }

        public T Value
        {
            get => value;
            protected set
            {
                T newValue = value;
                if (newValue.CompareTo(default) < 0) newValue = default;
                if (newValue.CompareTo(maxValue) > 0) newValue = maxValue;
                if (newValue.Equals(this.value)) return;
                this.value = newValue;
                OnValueChanged?.Invoke(this);
                if (this.value.Equals(maxValue)) OnValueEqualsMaxValue?.Invoke(this);
                if (this.value.Equals(default)) OnValueEqualsDefaultValue?.Invoke(this);
            }
        }

        public T MaxValue
        {
            get => maxValue;
            protected set
            {
                T newMaxValue = value;
                if (newMaxValue.CompareTo(default) < 0) newMaxValue = default;
                if (newMaxValue.Equals(maxValue)) return;
                maxValue = newMaxValue;
                OnMaxValueChanged?.Invoke(this);
                if (maxValue.CompareTo(this.value) < 0) Value = maxValue;
            }
        }

        public abstract void IncreaseValue(T value);

        public abstract void DecreaseValue(T value);

        public abstract void IncreaseMaxValue(T value);

        public abstract void DecreaseMaxValue(T value);

        public void SetValue(T value)
        {
            Value = value;
        }

        public void SetMaxValue(T value)
        {
            MaxValue = value;
        }

        public IAbstractData<T> AddValue(T value)
        {
            IncreaseValue(value);
            return this;
        }

        public IAbstractData<T> SubtractValue(T value)
        {
            DecreaseValue(value);
            return this;
        }

        public IAbstractData<T> AddMaxValue(T value)
        {
            IncreaseMaxValue(value);
            return this;
        }

        public IAbstractData<T> SubtractMaxValue(T value)
        {
            DecreaseMaxValue(value);
            return this;
        }

        public IAbstractData<T> Add(IAbstractData<T> data)
        {
            return AddMaxValue(data.MaxValue).AddValue(data.Value);
        }

        public IAbstractData<T> Subtract(IAbstractData<T> data)
        {
            return SubtractValue(data.Value).SubtractMaxValue(data.MaxValue);
        }

        public IUnregisterHandler RegisterOnValueChanged(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueChanged += action;
            return new AbstractDataOnValueChangedUnregisterHandler(this, action);
        }

        public void RegisterOnValueChangedNonAlloc(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueChanged += action;
        }

        public void UnregisterOnValueChanged(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueChanged -= action;
        }

        public IUnregisterHandler RegisterOnMaxValueChanged(Action<IReadOnlyAbstractData<T>> action)
        {
            OnMaxValueChanged += action;
            return new AbstractDataOnMaxValueChangedUnregisterHandler(this, action);
        }

        public void RegisterOnMaxValueChangedNonAlloc(Action<IReadOnlyAbstractData<T>> action)
        {
            OnMaxValueChanged += action;
        }

        public void UnregisterOnMaxValueChanged(Action<IReadOnlyAbstractData<T>> action)
        {
            OnMaxValueChanged -= action;
        }

        public IUnregisterHandler RegisterOnValueEqualsMaxValue(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueEqualsMaxValue += action;
            return new AbstractDataOnValueEqualsMaxValueUnregisterHandler(this, action);
        }
        
        public void RegisterOnValueEqualsMaxValueNonAlloc(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueEqualsMaxValue += action;
        }

        public void UnregisterOnValueEqualsMaxValue(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueEqualsMaxValue -= action;
        }

        public IUnregisterHandler RegisterOnValueEqualsDefaultValue(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueEqualsDefaultValue += action;
            return new AbstractDataOnValueEqualsDefaultValueUnregisterHandler(this, action);
        }
        
        public void RegisterOnValueEqualsDefaultValueNonAlloc(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueEqualsDefaultValue += action;
        }

        public void UnregisterOnValueEqualsDefaultValue(Action<IReadOnlyAbstractData<T>> action)
        {
            OnValueEqualsDefaultValue -= action;
        }

        public override string ToString()
        {
            return $"{typeof(T).Name} {value.ToString()} / {maxValue.ToString()}";
        }

        private sealed class AbstractDataOnValueChangedUnregisterHandler : IUnregisterHandler
        {
            private AbstractData<T> data;
            private Action<IReadOnlyAbstractData<T>> action;

            public AbstractDataOnValueChangedUnregisterHandler(AbstractData<T> data, Action<IReadOnlyAbstractData<T>> action)
            {
                this.data = data;
                this.action = action;
            }

            public void Unregister()
            {
                data.UnregisterOnValueChanged(action);
                data = null;
                action = null;
            }
        }

        private sealed class AbstractDataOnMaxValueChangedUnregisterHandler : IUnregisterHandler
        {
            private AbstractData<T> data;
            private Action<IReadOnlyAbstractData<T>> action;

            public AbstractDataOnMaxValueChangedUnregisterHandler(AbstractData<T> data, Action<IReadOnlyAbstractData<T>> action)
            {
                this.data = data;
                this.action = action;
            }

            public void Unregister()
            {
                data.UnregisterOnMaxValueChanged(action);
                data = null;
                action = null;
            }
        }

        private sealed class AbstractDataOnValueEqualsMaxValueUnregisterHandler : IUnregisterHandler
        {
            private AbstractData<T> data;
            private Action<IReadOnlyAbstractData<T>> action;

            public AbstractDataOnValueEqualsMaxValueUnregisterHandler(AbstractData<T> data, Action<IReadOnlyAbstractData<T>> action)
            {
                this.data = data;
                this.action = action;
            }

            public void Unregister()
            {
                data.UnregisterOnValueEqualsMaxValue(action);
                data = null;
                action = null;
            }
        }

        private sealed class AbstractDataOnValueEqualsDefaultValueUnregisterHandler : IUnregisterHandler
        {
            private AbstractData<T> data;
            private Action<IReadOnlyAbstractData<T>> action;

            public AbstractDataOnValueEqualsDefaultValueUnregisterHandler(AbstractData<T> data, Action<IReadOnlyAbstractData<T>> action)
            {
                this.data = data;
                this.action = action;
            }

            public void Unregister()
            {
                data.UnregisterOnValueEqualsDefaultValue(action);
                data = null;
                action = null;
            }
        }
    }
}