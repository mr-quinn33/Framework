using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class RegisterDependencyExtension
    {
        public static void RegisterDependency<T>(this IRegisterDependency self, object dependency)
        {
            self.GetArchitecture().RegisterDependency<T>(dependency);
        }
    }
}