using System;
using Framework.Interface;

namespace Framework.EventSystems
{
    public class ActionUnregisterHandler<T> : IUnregisterHandler
    {
        private Action<T> _action;
        private IEventSystem _eventSystem;

        public ActionUnregisterHandler(IEventSystem eventSystem, Action<T> action)
        {
            _action = action;
            _eventSystem = eventSystem;
        }

        public void Unregister()
        {
            _eventSystem.Unregister(_action);
            _eventSystem = null;
            _action = null;
        }
    }
}