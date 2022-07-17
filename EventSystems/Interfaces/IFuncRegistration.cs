using System;

namespace Framework.EventSystems.Interfaces
{
    public interface IFuncRegistration<T, TResult> : IRegistration
    {
        Func<T, TResult> Func { get; set; }
    }
}