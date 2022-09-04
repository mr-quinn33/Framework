using Framework.Rules;
using Framework.Utilities;

namespace Framework.Extensions
{
    public static class GetUtilityExtension
    {
        public static T GetUtility<T>(this IGetUtility self) where T : class, IUtility
        {
            return self.GetGameMode().GetUtility<T>();
        }
    }
}