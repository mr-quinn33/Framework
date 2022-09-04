using System;
using Framework.Rules;

namespace Framework.Extensions
{
    public static class UnregisterEventExtension
    {
        public static void UnregisterEvent<T>(this IUnregisterEvent self, Action<T> action)
        {
            self.GetGameMode().UnregisterEvent(action);
        }

        public static void UnregisterEvent<T, TResult>(this IUnregisterEvent self, Func<T, TResult> func)
        {
            self.GetGameMode().UnregisterEvent(func);
        }
    }
}