using System;
using Framework.Interface;

namespace Framework.ReactiveProperty
{
    [Serializable]
    public class ReactiveProperty<T>
    {
        private T value;

        public ReactiveProperty(T value = default)
        {
            this.value = value;
        }

        public T Value
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

        private event Action<T> OnValueChanged;

        public IUnregisterHandler Register(Action<T> action)
        {
            OnValueChanged += action;
            return new ReactivePropertyUnregisterHandler<T>(this, action);
        }

        public void Unregister(Action<T> action)
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