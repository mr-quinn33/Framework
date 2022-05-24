using System;
using Framework.Interface;
using Framework.ReactiveProperty.Interface;

namespace Framework.ReactiveProperty
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T value;

        private event Action<T> OnValueChanged;

        public ReactiveProperty(T value = default)
        {
            this.value = value;
        }

        T IReactiveProperty<T>.Value
        {
            get => value;
            set
            {
                if (value == null && this.value == null) return;
                if (value != null && value.Equals(this.value)) return;
                this.value = value;
                OnValueChanged?.Invoke(this.value);
            }
        }

        IUnregisterHandler IReactiveProperty<T>.Register(Action<T> action)
        {
            OnValueChanged += action;
            return new ReactivePropertyUnregisterHandler<T>(this, action);
        }

        void IReactiveProperty<T>.Unregister(Action<T> action)
        {
            OnValueChanged -= action;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator T(ReactiveProperty<T> reactiveProperty)
        {
            return reactiveProperty.value;
        }
    }
}