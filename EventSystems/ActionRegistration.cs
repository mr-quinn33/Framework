using System;

namespace Framework.EventSystems
{
    internal interface IActionRegistration<T> : IRegistration
    {
        Action<T> Action { get; set; }
    }

    internal sealed class ActionRegistration<T> : IActionRegistration<T>
    {
        public Action<T> Action { get; set; }
    }
}