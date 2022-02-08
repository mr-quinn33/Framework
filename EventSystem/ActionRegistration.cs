using System;
using Framework.Interface;

namespace Framework.EventSystem
{
    public class ActionRegistration<T> : IRegistration
    {
        public Action<T> action;
    }
}