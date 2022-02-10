using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class GetSystemExtension
    {
        public static T GetSystem<T>(this IGetSystem self) where T : class, ISystem
        {
            return self.GetArchitecture().GetSystem<T>();
        }
    }
}