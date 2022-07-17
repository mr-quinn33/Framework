using System;
using Framework.EventSystems.Interfaces;

namespace Framework.EventSystems
{
    public class FuncRegistration<T, TResult> : IFuncRegistration<T, TResult>
    {
        public Func<T, TResult> Func { get; set; }
    }
}