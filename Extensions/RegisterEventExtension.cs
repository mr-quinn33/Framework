using System;
using Framework.Interfaces;
using Framework.Rules;

namespace Framework.Extensions
{
    public static class RegisterEventExtension
    {
        public static IUnregisterHandler RegisterEvent<T>(this IRegisterEvent self, Action<T> action)
        {
            return self.GetGameMode().RegisterEvent(action);
        }
    }
}