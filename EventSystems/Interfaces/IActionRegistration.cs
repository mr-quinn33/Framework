using System;

namespace Framework.EventSystems.Interfaces
{
    public interface IActionRegistration<T> : IRegistration
    {
        Action<T> Action { get; set; }
    }
}