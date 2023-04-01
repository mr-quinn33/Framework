using System;

namespace Framework.EventSystems
{
    internal interface IFuncRegistration<T, TResult> : IRegistration
    {
        Func<T, TResult> Func { get; set; }
    }

    internal sealed class FuncRegistration<T, TResult> : IFuncRegistration<T, TResult>
    {
        public Func<T, TResult> Func { get; set; }
    }
}