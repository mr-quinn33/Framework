using Framework.Conditions.Interfaces;
using Framework.Rules.Interfaces;

namespace Framework.Extensions
{
    public static class CheckConditionExtension
    {
        public static bool CheckCondition(this ICheckCondition self, ICondition condition)
        {
            return self.GetGameMode().CheckCondition(condition);
        }
    }
}