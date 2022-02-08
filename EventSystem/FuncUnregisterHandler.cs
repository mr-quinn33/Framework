using System;
using Framework.Interface;

namespace Framework.EventSystem
{
    public class FuncUnregisterHandler<T, TResult> : IUnregisterHandler
    {
        private IEventSystem _eventSystem;
        private Func<T, TResult> _func;

        public FuncUnregisterHandler(IEventSystem eventSystem, Func<T, TResult> func)
        {
            _eventSystem = eventSystem;
            _func = func;
        }

        public void Unregister()
        {
            _eventSystem.Unregister(_func);
            _eventSystem = null;
            _func = null;
        }
    }
}