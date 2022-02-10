using System;
using Framework.Interface;

namespace Framework.EventSystem
{
    public class FuncUnregisterHandler<T, TResult> : IUnregisterHandler
    {
        private IEventSystem eventSystem;
        private Func<T, TResult> func;

        public FuncUnregisterHandler(IEventSystem eventSystem, Func<T, TResult> func)
        {
            this.eventSystem = eventSystem;
            this.func = func;
        }

        public void Unregister()
        {
            eventSystem.Unregister(func);
            eventSystem = null;
            func = null;
        }
    }
}