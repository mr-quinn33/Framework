using System;
using Framework.Interface;

namespace Framework.EventSystems
{
    public class FuncRegistration<T, TResult> : IRegistration
    {
        public Func<T, TResult> func;
    }
}