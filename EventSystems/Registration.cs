using System;

namespace Framework.EventSystems
{
    internal interface IRegistration
    {
    }

    internal interface IRegistration<T> : IRegistration
    {
        Action<T> Action { get; set; }
    }

    internal sealed class Registration<T> : IRegistration<T>
    {
        public Action<T> Action { get; set; }
    }
}