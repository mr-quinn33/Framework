using System;
using Framework.Interface;

namespace Framework.EventSystem
{
    public class ActionUnregisterHandler<T> : IUnregisterHandler
    {
        private IEventSystem eventSystem;
        private Action<T> action;

        public ActionUnregisterHandler(IEventSystem eventSystem, Action<T> action)
        {
            this.eventSystem = eventSystem;
            this.action = action;
        }

        void IUnregisterHandler.Unregister()
        {
            eventSystem.Unregister(action);
            eventSystem = null;
            action = null;
        }
    }
}