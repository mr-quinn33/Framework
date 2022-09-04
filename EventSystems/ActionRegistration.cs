using System;

namespace Framework.EventSystems
{
    public interface IActionRegistration<T> : IRegistration
    {
        Action<T> Action { get; set; }
    }

    public class ActionRegistration<T> : IActionRegistration<T>
    {
        public Action<T> Action { get; set; }
    }
}