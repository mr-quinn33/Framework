using System;
using Framework.Interfaces;

namespace Framework.ReactiveProperties
{
    public interface IReactiveProperty<T>
    {
        T Value { get; set; }

        IUnregisterHandler Register(Action<T> action);

        void Unregister(Action<T> action);
    }

    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T value;

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

        private event Action<T> OnValueChanged;

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator T(ReactiveProperty<T> reactiveProperty)
        {
            return reactiveProperty.value;
        }

        private class ReactivePropertyUnregisterHandler<TValue> : IUnregisterHandler
        {
            private Action<TValue> action;
            private IReactiveProperty<TValue> reactiveProperty;

            public ReactivePropertyUnregisterHandler(IReactiveProperty<TValue> reactiveProperty, Action<TValue> action)
            {
                this.reactiveProperty = reactiveProperty;
                this.action = action;
            }

            void IUnregisterHandler.Unregister()
            {
                reactiveProperty.Unregister(action);
                reactiveProperty = null;
                action = null;
            }
        }
    }
}