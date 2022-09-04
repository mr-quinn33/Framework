using System;

namespace Framework.EventSystems
{
    public interface IFuncRegistration<T, TResult> : IRegistration
    {
        Func<T, TResult> Func { get; set; }
    }

    public class FuncRegistration<T, TResult> : IFuncRegistration<T, TResult>
    {
        public Func<T, TResult> Func { get; set; }
    }
}