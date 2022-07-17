using System;
using Framework.EventSystems.Interfaces;

namespace Framework.EventSystems
{
    public class ActionRegistration<T> : IActionRegistration<T>
    {
        public Action<T> Action { get; set; }
    }
}