using Framework.Rules;

namespace Framework.Extensions
{
    public static class ResolveDependencyExtension
    {
        public static T ResolveDependency<T>(this IResolveDependency self)
        {
            return self.GetGameMode().ResolveDependency<T>();
        }
    }
}