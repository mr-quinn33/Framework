using System;
using Framework.Interfaces;
using Framework.ReactiveProperties.Interfaces;

namespace Framework.ReactiveProperties
{
    public class ReactivePropertyUnregisterHandler<T> : IUnregisterHandler
    {
        private Action<T> action;
        private IReactiveProperty<T> reactiveProperty;

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