using System;
using Framework.Interface;

namespace Framework.ReactiveProperty
{
    public class ReactiveProperty<T>
    {
        private T _value;

        public ReactiveProperty(T value = default)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (value == null && _value == null) return;
                if (value != null && value.Equals(_value)) return;
                _value = value;
                OnValueChanged?.Invoke(_value);
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
            return _value.ToString();
        }

        public static implicit operator T(ReactiveProperty<T> reactiveProperty)
        {
            return reactiveProperty._value;
        }
    }
}