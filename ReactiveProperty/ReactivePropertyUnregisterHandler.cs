using System;
using Framework.Interface;

namespace Framework.ReactiveProperty
{
    public class ReactivePropertyUnregisterHandler<T> : IUnregisterHandler
    {
        private Action<T> _action;
        private ReactiveProperty<T> _reactiveProperty;

        public ReactivePropertyUnregisterHandler(ReactiveProperty<T> reactiveProperty, Action<T> action)
        {
            _reactiveProperty = reactiveProperty;
            _action = action;
        }

        public void Unregister()
        {
            _reactiveProperty.Unregister(_action);
            _reactiveProperty = null;
            _action = null;
        }
    }
}