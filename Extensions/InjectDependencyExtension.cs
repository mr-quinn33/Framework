using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class InjectDependencyExtension
    {
        public static void InjectDependency<T>(this IInjectDependency self, object obj)
        {
            self.GetGameMode().InjectDependency<T>(obj);
        }
    }
}