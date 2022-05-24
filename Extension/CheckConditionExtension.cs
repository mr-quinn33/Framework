using Framework.Interface;
using Framework.Interface.Access;

namespace Framework.Extension
{
    public static class CheckConditionExtension
    {
        public static bool CheckCondition(this ICheckCondition self, ICondition condition)
        {
            return self.GetArchitecture().CheckCondition(condition);
        }
    }
}