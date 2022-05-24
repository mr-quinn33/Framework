using System;
using Framework.Interface;
using Framework.ReactiveProperty.Interface;

namespace Framework.ReactiveProperty
{
    public class ReactivePropertyUnregisterHandler<T> : IUnregisterHandler
    {
        private IReactiveProperty<T> reactiveProperty;
        private Action<T> action;

        public ReactivePropertyUnregisterHandler(IReactiveProperty<T> reactiveProperty, Action<T> action)
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