using Framework.Rules.Interfaces;
using Framework.Utilities.Interfaces;

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