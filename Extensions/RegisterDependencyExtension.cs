using Framework.Rules;

namespace Framework.Extensions
{
    public static class RegisterDependencyExtension
    {
        public static void RegisterDependency<T>(this IRegisterDependency self, object dependency)
        {
            self.GetGameMode().RegisterDependency<T>(dependency);
        }
    }
}