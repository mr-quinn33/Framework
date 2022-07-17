using Framework.Rules.Interfaces;
using Framework.Systems.Interfaces;

namespace Framework.Extensions
{
    public static class GetSystemExtension
    {
        public static T GetSystem<T>(this IGetSystem self) where T : class, ISystem
        {
            return self.GetGameMode().GetSystem<T>();
        }
    }
}