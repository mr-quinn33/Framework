using System;
using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.Extension
{
    public static class RegisterEventExtension
    {
        public static IUnregisterHandler RegisterEvent<T>(this IRegisterEvent self, Action<T> action)
        {
            return self.GetArchitecture().RegisterEvent(action);
        }

        public static IUnregisterHandler RegisterEvent<T, TResult>(this IRegisterEvent self, Func<T, TResult> func)
        {
            return self.GetArchitecture().RegisterEvent(func);
        }
    }
}