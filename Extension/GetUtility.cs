using Framework.Interface;
using Framework.Interface.Restriction;

namespace Framework.Extension
{
    public static class GetUtilityExtension
    {
        public static T GetUtility<T>(this IGetUtility self) where T : class, IUtility
        {
            return self.GetArchitecture().GetUtility<T>();
        }
    }
}