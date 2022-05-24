using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class InjectDependencyExtension
    {
        public static void InjectDependency<T>(this IInjectDependency self, object obj)
        {
            self.GetArchitecture().InjectDependency<T>(obj);
        }
    }
}