using System;
using Framework.EventSystems.Interfaces;
using Framework.Interfaces;

namespace Framework.EventSystems
{
    public class ActionUnregisterHandler<T> : IUnregisterHandler
    {
        private Action<T> action;
        private IEventSystem eventSystem;

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