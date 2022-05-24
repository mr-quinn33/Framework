using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class ResolveDependencyExtension
    {
        public static T ResolveDependency<T>(this IResolveDependency self)
        {
            return self.GetArchitecture().ResolveDependency<T>();
        }
    }
}