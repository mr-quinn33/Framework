using System;
using Framework.Interface;

namespace Framework.EventSystems
{
    public class ActionRegistration<T> : IRegistration
    {
        public Action<T> action;
    }
}