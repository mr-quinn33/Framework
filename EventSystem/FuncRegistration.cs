using System;
using Framework.Interface;

namespace Framework.EventSystem
{
    public class FuncRegistration<T, TResult> : IRegistration
    {
        public Func<T, TResult> func;
    }
}