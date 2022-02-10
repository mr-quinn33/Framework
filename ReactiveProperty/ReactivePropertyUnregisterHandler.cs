using System;
using Framework.Interface;

namespace Framework.ReactiveProperty
{
    public class ReactivePropertyUnregisterHandler<T> : IUnregisterHandler
    {
        private Action<T> action;
        private ReactiveProperty<T> reactiveProperty;

        public ReactivePropertyUnregisterHandler(ReactiveProperty<T> reactiveProperty, Action<T> action)
        {
            this.reactiveProperty = reactiveProperty;
            this.action = action;
        }

        public void Unregister()
        {
            reactiveProperty.Unregister(action);
            reactiveProperty = null;
            action = null;
        }
    }
}