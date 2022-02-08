using System;
using Framework.Interface.Restriction;

namespace Framework.Extension
{
    public static class UnregisterEventExtension
    {
        public static void UnregisterEvent<T>(this IUnregisterEvent self, Action<T> action)
        {
            self.GetArchitecture().UnregisterEvent(action);
        }

        public static void UnregisterEvent<T, TResult>(this IUnregisterEvent self, Func<T, TResult> func)
        {
            self.GetArchitecture().UnregisterEvent(func);
        }
    }
}