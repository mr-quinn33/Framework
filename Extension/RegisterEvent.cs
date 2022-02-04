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

        public static IUnregisterHandler[] RegisterEvent<T>(this IRegisterEvent self, params Action<T>[] actions)
        {
            return self.GetArchitecture().RegisterEvent(actions);
        }

        public static IUnregisterHandler RegisterEvent<T, TResult>(this IRegisterEvent self, Func<T, TResult> func)
        {
            return self.GetArchitecture().RegisterEvent(func);
        }

        public static IUnregisterHandler[] RegisterEvent<T, TResult>(this IRegisterEvent self, params Func<T, TResult>[] functions)
        {
            return self.GetArchitecture().RegisterEvent(functions);
        }
    }
}