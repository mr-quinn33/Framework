using System;
using Framework.Interface;

namespace Framework.EventSystem
{
    public class ActionUnregisterHandler<T> : IUnregisterHandler
    {
        private Action<T> action;
        private IEventSystem eventSystem;

        public ActionUnregisterHandler(IEventSystem eventSystem, Action<T> action)
        {
            this.action = action;
            this.eventSystem = eventSystem;
        }

        public void Unregister()
        {
            eventSystem.Unregister(action);
            eventSystem = null;
            action = null;
        }
    }
}